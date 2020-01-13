using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLSimulation.Model
{
    public class MazeCellViewModel
    {
        /// <summary>
        /// 迷路のマスに表示するテキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 迷路のマスのスタイル
        /// </summary>
        public string CellCssClass { get; set; }

        /// <summary>
        /// 迷路マスのテキストのスタイル
        /// </summary>
        public string TestCssClass { get; set; }

    }
}
