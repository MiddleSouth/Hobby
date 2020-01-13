using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RLSimulation.Logic;
using RLSimulation.Data;
using System.Timers;

namespace RLSimulation.Model
{
    public class IndexModel : ComponentBase
    {
        /// <summary>
        /// 設定メニューの開閉状態
        /// </summary>
        public bool CollapseSettingMenu = true;

        /// <summary>
        /// 設定メニュー開閉のCSS
        /// </summary>
        public string SettingCssClass => CollapseSettingMenu ? "d-none" : null;

        /// <summary>
        /// 設定メニュー開閉ボタンのテキスト
        /// </summary>
        public string SettingToggleButtonText => CollapseSettingMenu ? "設定を開く" : "設定を閉じる";

        /// <summary>
        /// 迷路作成ボタンの有効/無効制御
        /// </summary>
        public bool IsMazeCreateButtonDisabled => Agent != null;

        /// <summary>
        /// 行動スキップボタンの有効/無効制御
        /// </summary>
        public bool IsSkipButtonDisabled => ActTimer == null || !ActTimer.Enabled;

        /// <summary>
        /// エージェントのパラメータ入力の有効/無効制御
        /// </summary>
        public bool IsAgentParameterDisabled => Agent != null;

        /// <summary>
        /// 学習対象の環境：迷路
        /// </summary>
        public Maze EnvMaze { get; set; }

        /// <summary>
        /// 迷路の表示データ
        /// </summary>
        public MazeCellViewModel[,] MazeCellViews { get; set; }

        /// <summary>
        /// 入力フォームにバインドするデータ
        /// </summary>
        public IndexData IndexData { get; set; } = new IndexData();

        /// <summary>
        /// 強化学習エージェント
        /// </summary>
        public QLearningAgent Agent { get; set; }

        /// <summary>
        /// エージェント非同期行動用のタイマ
        /// </summary>
        private Timer ActTimer { get; set; }

        /// <summary>
        /// エージェントの行動に対する画面表示（矢印）のセット
        /// </summary>
        private Dictionary<Maze.Direction, string> DirectionSet { get; }
            = new Dictionary<Maze.Direction, string>()
            {
                {Maze.Direction.Up,   "↑" },
                {Maze.Direction.Down, "↓" },
                {Maze.Direction.Left, "←" },
                {Maze.Direction.Right,"→" }
            };

        /// <summary>
        /// 通路のCSSクラス（セルの背景色）
        /// </summary>
        private readonly string CSS_CLASS_WALL = "bg-dark";

        /// <summary>
        /// 壁のCSSクラス（セルの背景色）
        /// </summary>
        private readonly string CSS_CLASS_PATH = "bg-light";

        /// <summary>
        /// 迷路作成のためのDI
        /// </summary>
        [Inject]
        protected MazeService mazeService { get; set; }

        /// <summary>
        /// 初期表示時
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            EnvMaze = await mazeService.GetMazeAsync();
            CreateMaze();
        }

        /// <summary>
        /// 設定の開閉
        /// </summary>
        public void ToggleSettingMenu()
        {
            CollapseSettingMenu = !CollapseSettingMenu;
        }

        /// <summary>
        /// 迷路生成
        /// </summary>
        public void CreateMaze()
        {
            if(EnvMaze.CreateMaze(Maze.CreateMethod.ExtendWall, IndexData.MazeWidth, IndexData.MazeHeight))
            {
                InitializeMazeView();
                ClearAgent();
            }
        }

        /// <summary>
        /// 学習開始
        /// </summary>
        public void StartLearning()
        {
            if (Agent == null)
            {
                SetAgent();
            }

            if (ActTimer == null)
            {
                ActTimer = new Timer(IndexData.AnimationInterval);
                ActTimer.Elapsed += (sender, e) =>
                {
                    ActAgentAsync().Wait();
                };
            }

            ActTimer.Start();
        }

        /// <summary>
        /// アニメーションスピード変更
        /// </summary>
        /// <param name="e">ドロップダウンの変更イベント</param>
        public void SetInterval(ChangeEventArgs e)
        {
            IndexData.AnimationInterval = int.Parse(e.Value.ToString());
            if(ActTimer != null)
            {
                ActTimer.Interval = IndexData.AnimationInterval;
            }
        }

        /// <summary>
        /// 学習一時停止
        /// </summary>
        public void StopLearning()
        {
            if (ActTimer != null)
            {
                ActTimer.Stop();
            }
        }

        /// <summary>
        /// 指定回数行動を進める
        /// </summary>
        public void SkipActions()
        {
            if (ActTimer == null)
            {
                return;
            }

            ActTimer.Stop();

            for (int i = 0; i < IndexData.SkipActCount; i++)
            {
                ActAgent();

                if (EnvMaze.Cells[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].IsGoal)
                {
                    if (IndexData.IsStopWhenGoal)
                    {
                        return;
                    }

                    break;
                }
            }

            ActTimer.Start();
        }

        /// <summary>
        /// 指定回数ゴールするまで行動する
        /// </summary>
        public void SkipGoals()
        {
            if (ActTimer == null)
            {
                return;
            }

            ActTimer.Stop();

            for (int i = 0; i < IndexData.SkipGoalCount; i++)
            {
                while (true)
                {
                    ActAgent();

                    if (EnvMaze.Cells[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].IsGoal)
                    {
                        // 自動学習再開後にゴール処理が実行されるので、最後のゴール時のみゴール処理を行わない
                        if (i + 1 < IndexData.SkipGoalCount)
                        {
                            Agent.EndOneLearning();
                        }

                        break;
                    }
                }
            }

            if (!IndexData.IsStopWhenGoal)
            {
                ActTimer.Start();
            }
        }

        /// <summary>
        /// エージェントを削除する
        /// </summary>
        public void ClearAgent()
        {
            if (Agent != null)
            {
                Agent = null;
            }

            if (ActTimer != null)
            {
                ActTimer.Stop();
                ActTimer.Dispose();
                ActTimer = null;
            }

            IndexData.AgentStateMessage = string.Empty;
            InitializeMazeView();
        }

        /// <summary>
        /// エージェントの行動
        /// </summary>
        /// <returns></returns>
        private async Task ActAgentAsync()
        {
            await InvokeAsync(() =>
            {
                if (EnvMaze.Cells[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].IsGoal)
                {
                    MazeCellViews[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].CellCssClass = CSS_CLASS_PATH;
                    Agent.EndOneLearning();
                }
                else
                {
                    ActAgent();

                    if (IndexData.IsStopWhenGoal && EnvMaze.Cells[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].IsGoal)
                    {
                        ActTimer.Stop();
                    }

                    if(EnvMaze.Cells[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].IsGoal && Agent.ActCount == EnvMaze.ShortestStep)
                    {
                        IndexData.AgentStateMessage = "最短経路に到達しました。";
                        if(IndexData.IsStopWhenBestRoot)
                        {
                            ActTimer.Stop();
                        }
                    }

                    this.StateHasChanged();
                }
            });
        }

        /// <summary>
        /// エージェントを1回行動させる
        /// </summary>
        private void ActAgent()
        {
            var locate = new Maze.CellLocate(Agent.CurrentLocate);
            MazeCellViews[locate.X, locate.Y].CellCssClass = CSS_CLASS_PATH;

            double qValue = Agent.Act();
            MazeCellViews[Agent.CurrentLocate.X, Agent.CurrentLocate.Y].CellCssClass = "bg-success";

            if (qValue > 0)
            {
                Maze.Direction direction = Agent.GetMaxQDirection(locate);
                EnvMaze.Cells[locate.X, locate.Y].Text = DirectionSet[direction];
                if (!EnvMaze.Cells[locate.X, locate.Y].IsStart)
                {
                    MazeCellViews[locate.X, locate.Y].Text = DirectionSet[direction];
                }
            }
        }

        /// <summary>
        /// エージェントを設定する
        /// </summary>
        private void SetAgent()
        {
            EnvironmentMaze env = new EnvironmentMaze(EnvMaze, IndexData.GoalReword);
            Agent = new QLearningAgent(env, IndexData.Alpha, IndexData.Gamma, IndexData.Epsilon);
        }

        /// <summary>
        /// 迷路の表示を初期化する
        /// </summary>
        private void InitializeMazeView()
        {
            MazeCellViews = new MazeCellViewModel[IndexData.MazeWidth, IndexData.MazeHeight];

            for (int y = 0; y < IndexData.MazeHeight; y++)
            {
                for (int x = 0; x < IndexData.MazeWidth; x++)
                {
                    MazeCellViews[x, y] = new MazeCellViewModel();

                    if (EnvMaze.Cells[x, y].State == MazeCell.MazeCellState.Wall)
                    {
                        MazeCellViews[x, y].CellCssClass = CSS_CLASS_WALL;
                    }
                    else
                    {
                        MazeCellViews[x, y].CellCssClass = CSS_CLASS_PATH;
                    }

                    if (EnvMaze.Cells[x, y].IsStart)
                    {
                        MazeCellViews[x, y].Text = "S";
                    }
                    else if (EnvMaze.Cells[x, y].IsGoal)
                    {
                        MazeCellViews[x, y].Text = "G";
                    }
                }
            }
        }
    }
}
