using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLSimulation.Logic
{
    public class QLearningAgent
    {
        /// <summary>
        /// 学習率
        /// </summary>
        public double Alpha { get; private set; }

        /// <summary>
        /// 割引率
        /// </summary>
        public double Gamma { get; private set; }

        /// <summary>
        /// [状態][行動]ごとのQ値
        /// </summary>
        public double[][] QValues { get; set;}

        /// <summary>
        /// 現在の状態
        /// </summary>
        public int CurrentState { get; set; }

        /// <summary>
        /// 現在の状態（座標）
        /// </summary>
        public Maze.CellLocate CurrentLocate { get { return Env.DeserializeState(CurrentState); } }

        /// <summary>
        /// ε-Greedyの値
        /// </summary>
        public double Epsilon { get; set; }

        /// <summary>
        /// 行動回数
        /// </summary>
        public int ActCount { get; private set; }

        /// <summary>
        /// 1学習における行動回数の最大値
        /// </summary>
        public int MaxActCount { get; private set; }

        /// <summary>
        /// 1学習における行動回数の最小値
        /// </summary>
        public int MinActCount { get; private set; }

        /// <summary>
        /// 学習回数
        /// </summary>
        public int LearningCount { get; private set; }

        /// <summary>
        /// 乱数
        /// </summary>
        private Random Rand { get; set; } = new Random();

        /// <summary>
        /// エージェントが行動する環境
        /// </summary>
        private EnvironmentMaze Env { get; set; }

        /// <summary>
        /// 引数なしコンストラクタを禁止する
        /// </summary>
        private QLearningAgent(){ }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="env">学習対象の環境</param>
        /// <param name="alpha">学習率</param>
        /// <param name="gamma">割引率</param>
        /// <param name="epsilon">ε-Greedyの値</param>
        public QLearningAgent(EnvironmentMaze env, double alpha, double gamma, double epsilon)
        {
            Alpha = alpha;
            Gamma = gamma;
            Epsilon = epsilon;
            Env = env;
            InitialaizeState();
        }

        /// <summary>
        /// 1回行動する
        /// </summary>
        public double Act()
        {
            int action;
            double reword;
            int nextState;
            double qValue;

            // 行動選択
            action = SelectAction();

            // 行動結果取得（報酬と移動後位置）
            Env.GetMoveResult(CurrentState, action, out nextState, out reword);

            // 獲得報酬からQ値計算
            QValues[CurrentState][action] += Alpha * (reword + (Gamma * QValues[nextState].Max()) - QValues[CurrentState][action]);
            qValue = QValues[CurrentState][action];

            // 移動
            CurrentState = nextState;
            ActCount++;

            return qValue;
        }

        /// <summary>
        /// 指定座標にてQ値が最大の方向を返す
        /// </summary>
        /// <returns>Q値が最大の方向</returns>
        public Maze.Direction GetMaxQDirection(Maze.CellLocate locate)
        {
            int state = Env.SerializeState(locate);

            // Q値が最大の行動からランダムに選択する
            var actList = GetMaxQDirectionList(Env.SerializeState(locate));

            return (Maze.Direction)actList[Rand.Next(actList.Count)];
        }

        /// <summary>
        /// 1回の学習が終了した時の処理
        /// </summary>
        public void EndOneLearning()
        {
            if(LearningCount == 0)
            {
                MaxActCount = ActCount;
                MinActCount = ActCount;
            }

            if (ActCount > MaxActCount)
            {
                MaxActCount = ActCount;
            }

            if(ActCount < MinActCount)
            {
                MinActCount = ActCount;
            }

            LearningCount++;
            ActCount = 0;
            SetStart();
        }

        /// <summary>
        /// エージェントを初期化する
        /// </summary>
        private void InitialaizeState()
        {
            InitializeQValue();
            SetStart();
        }

        /// <summary>
        /// 状態をスタート位置に設定する
        /// </summary>
        private void SetStart()
        {
            CurrentState = Env.GetStartState();
        }

        /// <summary>
        /// Q値を初期化する
        /// </summary>
        private void InitializeQValue()
        {
            int stateCount;
            int actionCount;
            Env.GetSerializeEnvironment(out stateCount, out actionCount);

            QValues = new double[stateCount][];
            for(int i = 0; i < QValues.Length; i++)
            {
                QValues[i] = new double[actionCount];
            }

        }

        /// <summary>
        /// 行動選択
        /// </summary>
        /// <returns>選択した行動</returns>
        private int SelectAction()
        {
            if(Rand.Next(100) < Epsilon * 100)
            {
                return Rand.Next(QValues[CurrentState].Length);
            }

            List<int> actList = GetMaxQDirectionList(CurrentState);

            return actList[Rand.Next(actList.Count)];
        }

        /// <summary>
        /// 指定した状態の中で、Q値が最大の行動のリストを取得する
        /// </summary>
        /// <param name="state">状態</param>
        /// <returns>Q値が最大の行動リスト</returns>
        private List<int> GetMaxQDirectionList(int state)
        {
            var actList = new List<int>();
            var maxQValue = QValues[state].Max();
            for (int i = 0; i < QValues[state].Length; i++)
            {
                if (QValues[state][i] == maxQValue)
                {
                    actList.Add(i);
                }
            }

            return actList;
        }

    }
}
