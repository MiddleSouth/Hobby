using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RLSimulation.CustomValidation;
using RLSimulation.Logic;

namespace RLSimulation.Data
{
    public class IndexData
    {
        /// <summary>
        /// 迷路作成時の幅の指定値
        /// </summary>
        [Range(5, 21, ErrorMessage = "5～21の範囲で入力してください")]
        [OddNumberValidation]
        public int MazeWidth { get; set; } = 15;

        /// <summary>
        /// 迷路の幅の選択リスト
        /// </summary>
        public List<int> MazeWidthList { get; } = new List<int>() { 5, 7, 9, 11, 13, 15, 17, 19, 21 };

        /// <summary>
        /// 迷路作成時の高さの指定値
        /// </summary>
        [Range(5, 21, ErrorMessage = "5～21の範囲で入力してください")]
        [OddNumberValidation]
        public int MazeHeight { get; set; } = 15;

        /// <summary>
        /// 迷路の高さの選択リスト
        /// </summary>
        public List<int> MazeHeightList { get; } = new List<int>() { 5, 7, 9, 11, 13, 15, 17, 19, 21 };

        /// <summary>
        /// ゴール時の報酬
        /// </summary>
        [Range(1, 10000, ErrorMessage = "1～10000の範囲で入力してください")]
        public double GoalReword { get; set; } = 100;

        /// <summary>
        /// Q学習の学習率
        /// </summary>
        [RangeNotEqualValidation(0, 1, ErrorMessage = "0 < α < 1 の範囲で入力してください")]
        public double Alpha { get; set; } = 0.1;

        /// <summary>
        /// Q学習の割引率
        /// </summary>
        [RangeNotEqualValidation(0, 1, ErrorMessage = "0 < γ < 1 の範囲で入力してください")]
        public double Gamma { get; set; } = 0.9;

        /// <summary>
        /// ε-greedy法のε値
        /// </summary>
        [RangeNotEqualValidation(0, 1, ErrorMessage = "0 < ε < 1 の範囲で入力してください")]
        public double Epsilon { get; set; } = 0.03;

        /// <summary>
        /// ゴール時にエージェントを一時停止する
        /// </summary>
        public bool IsStopWhenGoal { get; set; } = false;

        /// <summary>
        /// 最短経路到達時にエージェントを一時停止する
        /// </summary>
        public bool IsStopWhenBestRoot { get; set; } = true;

        /// <summary>
        /// エージェントの行動をスキップする回数
        /// </summary>
        public int SkipActCount { get; set; } = 100;

        /// <summary>
        /// エージェントをゴールさせる回数
        /// </summary>
        public int SkipGoalCount { get; set; } = 10;

        /// <summary>
        /// アニメーションスピード
        /// </summary>
        public int AnimationInterval { get; set; } = 50;

        /// <summary>
        /// アニメーションスピード選択リスト
        /// </summary>
        public Dictionary<int, string> AnimationSpeedList { get; }
            = new Dictionary<int, string>()
            {
                {50,  "速い" },
                {200, "普通" },
                {500, "遅い" }
            };

        /// <summary>
        /// エージェントの状態に関するメッセージ
        /// </summary>
        public string AgentStateMessage { get; set; } = string.Empty;

    }
}
