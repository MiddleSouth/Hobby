using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LangtonsAnt
{
    class Ant
    {
        #region 定数
        #endregion

        #region プライベート変数
        private int _x;
        private int _y;
        private uint _antColor;
        private int _direction; //0:上 1:右 2:下 3:左
        private int _moveCount;
        private int _lifespan;
        #endregion

        #region プロパティ
        /// <summary>
        /// アリのX座標
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// アリのY座標
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// アリの色
        /// </summary>
        public uint Color
        {
            get { return _antColor; }
        }

        /// <summary>
        /// アリは生きているか？
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if (this._lifespan != 0 && this._moveCount >= this._lifespan)
                {
                    return false;
                }

                return true;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">初期のx座標</param>
        /// <param name="y">初期のy座標</param>
        /// <param name="direction">向き</param>
        /// <param name="color">アリが塗りつぶす色</param>
        /// <param name="lifespan">寿命</param>
        public Ant(int x, int y, int direction, uint color, int lifespan)
        {
            this._x = x;
            this._y = y;
            this._direction = direction;
            this._antColor = color;
            this._moveCount = 0;
            this._lifespan = lifespan;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 90°右に方向転換
        /// </summary>
        public void TurnRight()
        {
            this._direction++;
            if (this._direction > 3)
            {
                this._direction = 0;
            }
        }

        /// <summary>
        /// 90°左に方向転換
        /// </summary>
        public void TurnLeft()
        {
            this._direction--;
            if (this._direction < 0)
            {
                this._direction = 3;
            }
        }

        /// <summary>
        /// 一歩前進
        /// </summary>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        public void GoStraight(int maxX, int maxY)
        {
            switch (this._direction)
            {
                case ConstValue.ANT_UP:
                    _y--;
                    if (_y < 0)
                    {
                        _y = maxY;
                    }
                    break;
                case ConstValue.ANT_RIGHT:
                    _x++;
                    if (_x > maxX)
                    {
                        _x = 0;
                    }
                    break;
                case ConstValue.ANT_DOWN:
                    _y++;
                    if (_y > maxY)
                    {
                        _y = 0;
                    }
                    break;
                case ConstValue.ANT_LEFT:
                    _x--;
                    if (_x < 0)
                    {
                        _x = maxX;
                    }
                    break;
            }

            this._moveCount++;
        }
        #endregion
    }
}
