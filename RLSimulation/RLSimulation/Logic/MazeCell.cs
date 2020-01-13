using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLSimulation.Logic
{
    public class MazeCell
    {
        /// <summary>
        /// マスの状態（壁 or 通路）
        /// </summary>
        public enum MazeCellState
        {
            Wall, Path
        }

        /// <summary>
        /// マスの状態（壁 or 通路）
        /// </summary>
        public MazeCellState State { get; set; }

        /// <summary>
        /// スタートマスか
        /// </summary>
        public bool IsStart { get; set; } = false;

        /// <summary>
        /// ゴールマスか
        /// </summary>
        public bool IsGoal { get; set; } = false;

        /// <summary>
        /// 最短経路マスか
        /// </summary>
        public bool IsCriticalPath { get; set; } = false;

        /// <summary>
        /// セルに設定するCSSクラス
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// セルに表示する文字
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// セルに表示する文字のCSSクラス
        /// </summary>
        public string TextCssClass { get; set; } = string.Empty;
    }
}
