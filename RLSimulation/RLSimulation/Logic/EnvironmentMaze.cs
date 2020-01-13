using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLSimulation.Logic
{
    public class EnvironmentMaze
    {
        /// <summary>
        /// ゴール時の報酬
        /// </summary>
        public double GoalReword { get; private set; }

        /// <summary>
        /// 環境（迷路）
        /// </summary>
        private Maze EnvMaze { get; set; }

        /// <summary>
        /// 引数なしコンストラクタを禁止する
        /// </summary>
        private EnvironmentMaze() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="maze">生成済みの迷路</param>
        /// <param name="goalReword">ゴール時の報酬</param>
        public EnvironmentMaze(Maze maze, double goalReword)
        {
            EnvMaze = maze;
            GoalReword = goalReword;
        }

        /// <summary>
        /// シリアル化した環境の状態数と行動数を取得する
        /// </summary>
        /// <param name="stateCount">状態数</param>
        /// <param name="actionCount">行動数</param>
        public void GetSerializeEnvironment(out int stateCount, out int actionCount)
        {
            stateCount = EnvMaze.Cells.Length;
            actionCount = Enum.GetValues(typeof(Maze.Direction)).Length;
        }

        // TODO:スタート位置を取得する
        public int GetStartState()
        {
            for(int x = 0; x < EnvMaze.Width; x++)
            {
                for(int y = 0; y < EnvMaze.Height; y++)
                {
                    if(EnvMaze.Cells[x,y].IsStart)
                    {
                        return SerializeState(new Maze.CellLocate(x, y));
                    }
                }
            }

            //スタート未設定の場合
            var startCell = new Maze.CellLocate(1, 1);
            EnvMaze.SetStartCell(startCell);

            return SerializeState(startCell);
        }

        /// <summary>
        /// 行動結果を取得する
        /// </summary>
        /// <param name="currentState">現在の状態</param>
        /// <param name="action">選択した行動</param>
        /// <param name="nextState">行動後の状態</param>
        /// <param name="reword">行動結果の報酬</param>
        public void GetMoveResult(int currentState, int action, out int nextState, out double reword)
        {
            Maze.CellLocate nextLocate = EnvMaze.Move(DeserializeState(currentState), (Maze.Direction)action);
            nextState = SerializeState(nextLocate);
            reword = EnvMaze.Cells[nextLocate.X, nextLocate.Y].IsGoal ? GoalReword : 0;
        }

        /// <summary>
        /// 状態番号を逆シリアル化（座標に変換）する
        /// </summary>
        /// <param name="stateNo">状態番号</param>
        /// <returns>デシリアライズ後の座標</returns>
        public Maze.CellLocate DeserializeState(int stateNo)
        {
            int X = stateNo == 0 ? 0 : stateNo % EnvMaze.Width;
            int Y = stateNo == 0 ? 0 : stateNo / EnvMaze.Width;
            return new Maze.CellLocate(X, Y);
        }

        /// <summary>
        /// 状態（座標）をシリアル化する
        /// </summary>
        /// <param name="locate">シリアル化したい座標</param>
        /// <returns>シリアル化後の状態番号</returns>
        public int SerializeState(Maze.CellLocate locate)
        {
            return (locate.Y * EnvMaze.Width) + locate.X;
        }
    }
}
