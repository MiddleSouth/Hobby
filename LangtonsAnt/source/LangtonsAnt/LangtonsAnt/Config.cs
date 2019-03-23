using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LangtonsAnt
{
    public class Config
    {
        private int _speed;
        private int _maxAntnum;
        private int _antAddSpan;
        private int _antLife;

        /// <summary>
        /// シミュレーションのスピード
        /// </summary>
        public int Speed
        {
            set { this._speed = value; }
            get { return this._speed; }
        }

        /// <summary>
        /// アリの最大数
        /// </summary>
        public int MaxAntNum
        {
            set { this._maxAntnum = value; }
            get { return this._maxAntnum; }
        }

        /// <summary>
        /// アリを自動追加する間隔
        /// </summary>
        public int AntAddSpan
        {
            set { this._antAddSpan = value; }
            get { return this._antAddSpan; }
        }

        /// <summary>
        /// アリの寿命
        /// </summary>
        public int AntLife
        {
            set { this._antLife = value; }
            get { return this._antLife; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Config()
        {
            this._speed = 1;
            this._maxAntnum = 10;
            this._antAddSpan = 4000;
            this._antLife = 60000;
        }
    }
}
