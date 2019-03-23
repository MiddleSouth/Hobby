using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LangtonsAnt
{
    class AntSimulate
    {
        #region 定数
        #endregion

        #region プライベート変数
        private Random _random = new Random();
        private int _antWorldWidth = 350;
        private int _antWorldHeight = 250;

        private WriteableBitmap _antWorldWb = null;

        private ArrayList _ants;

        private DispatcherTimer _antWalkTimer;
        private int _moveSkip = 1;

        private int _antAddSpan = 0;
        private int _antMoveCount = 0;
        private int _maxAntCount = 0;
        private int _antlife = 0;

        private int _antTotalCount = 0;
        #endregion

        #region プロパティ
        public WriteableBitmap AntWorldWb
        {
            get { return this._antWorldWb; }
        }

        /// <summary>
        /// アリの数
        /// </summary>
        public int AntCount
        {
            get
            {
                if (this._ants == null)
                {
                    return 0;
                }
                else
                {
                    return this._ants.Count;
                }
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AntSimulate()
        {
            this._antWalkTimer = new DispatcherTimer(DispatcherPriority.Background);
            this._antWalkTimer.Interval = new TimeSpan(10000);
            this._antWalkTimer.Tick += new EventHandler(MoveAntTimer);
        }

        /// <summary>
        /// コンストラクタ：スクリーンセーバ用初期設定付き
        /// </summary>
        /// <param name="antLifespan">アリの寿命</param>
        /// <param name="antAddSpan">アリを自動追加する間隔</param>
        /// <param name="maxAntCount">アリの最大数</param>
        public AntSimulate(int antLifespan, int antAddSpan, int maxAntCount)
        {
            this._antWalkTimer = new DispatcherTimer(DispatcherPriority.Background);
            this._antWalkTimer.Interval = new TimeSpan(10000);
            this._antWalkTimer.Tick += new EventHandler(AutoMoveAntTimer);

            this._antlife = antLifespan;
            this._antAddSpan = antAddSpan;
            this._maxAntCount = maxAntCount;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// アリの世界を作る
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void MakeAntWorld(int width, int height)
        {
            this._antWorldWidth = width;
            this._antWorldHeight = height;
            this._antWorldWb = null;
            this._antWorldWb = new WriteableBitmap(this._antWorldWidth, this._antWorldHeight, 96, 96, PixelFormats.Bgr32, null);
            PaintTool.PaintFull(ref this._antWorldWb, this._antWorldWidth, this._antWorldHeight, PaintTool.MakeColor(0, 0, 0, 0));
            this._ants = null;
            this._ants = new ArrayList();
        }

        /// <summary>
        /// アリの世界の設定変更
        /// </summary>
        /// <param name="speed">シミュレーションスピード</param>
        /// <param name="antMaxCount">アリの最大数</param>
        /// <param name="antAddSpan">アリを自動で追加する間隔</param>
        /// <param name="antLife">アリの寿命</param>
        public void SetConfigAntWorld(int speed, int antMaxCount, int antAddSpan, int antLife)
        {
            this._maxAntCount = antMaxCount;
            this._antAddSpan = antAddSpan;
            this._antlife = antLife;
            this.SetAntWalkInterval(speed);
        }

        /// <summary>
        /// アリの行動開始
        /// </summary>
        /// <returns></returns>
        public bool StartAntWalk()
        {
            if (this._ants == null)
            {
                return false;
            }

            this._antMoveCount = 0;
            this._antWalkTimer.Start();

            return true;
        }

        /// <summary>
        /// アリの行動停止
        /// </summary>
        public void StopAntWalk()
        {
            this._antWalkTimer.Stop();
        }

        /// <summary>
        /// アリを追加
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <param name="color"></param>
        /// <param name="lifespan"></param>
        /// <returns></returns>
        public bool AddAnt(int x, int y, int direction, uint color, int lifespan = 0)
        {
            if (this._ants == null)
            {
                return false;
            }

            this._ants.Add(new Ant(x, y, direction, color, lifespan));

            return true;
        }

        /// <summary>
        /// アリを削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteAnt(int antIndex)
        {
            if (this._ants == null || this._ants.Count == 0)
            {
                return false;
            }

            this._ants.RemoveAt(antIndex);

            return true;
        }

        /// <summary>
        /// アリの速度設定
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="moveSkip"></param>
        public void SetAntWalkInterval(int speed)
        {
            switch (speed)
            {
                case 0:
                    this._moveSkip = 1;
                    break;
                case 1:
                    this._moveSkip = 5;
                    break;
                case 2:
                    this._moveSkip = 20;
                    break;
                case 3:
                    this._moveSkip = 40;
                    break;
                case 4:
                    this._moveSkip = 80;
                    break;
                case 5:
                    this._moveSkip = 500;
                    break;
                default:
                    this._moveSkip = 20;
                    break;
            }
        }

        /// <summary>
        /// アリの移動
        /// </summary>
        private void MoveAnt()
        {
            for (int i = 0; i < this._moveSkip; i++)
            {
                foreach (Ant ant in _ants)
                {
                    int x;
                    int y;
                    uint currentColor;

                    x = ant.X;
                    y = ant.Y;
                    currentColor = PaintTool.GetCurrentColor(this._antWorldWb, x, y);

                    if (currentColor == ConstValue.BLACK)
                    {
                        ant.TurnRight();
                        PaintTool.PaintDot(ref this._antWorldWb, x, y, ant.Color);
                    }
                    else
                    {
                        ant.TurnLeft();
                        PaintTool.PaintDot(ref this._antWorldWb, x, y, ConstValue.BLACK);
                    }

                    ant.GoStraight(this._antWorldWidth - 1, this._antWorldHeight - 1);
                }
                this._antMoveCount++;
            }

        }

        /// <summary>
        /// アリの行動
        /// </summary>
        private void ControlAnt()
        {
            if (this._ants.Count == 0)
            {
                AddAntAuto();
                return;
            }

            Ant ant = (Ant)this._ants[0];

            if (ant.IsAlive == false)
            {
                DeleteAnt(0);
            }

            if (this._antMoveCount > this._antAddSpan && this._ants.Count < this._maxAntCount)
            {
                //アリ追加
                AddAntAuto();

                this._antMoveCount = 0;
            }
        }

        /// <summary>
        /// アリの自動追加
        /// </summary>
        private void AddAntAuto()
        {
            uint antColor = ConstValue.WHITE;

            int x = this._random.Next(_antWorldWidth);
            int y = this._random.Next(_antWorldHeight);
            switch (this._antTotalCount)
            {
                case 0:
                    antColor = ConstValue.WHITE;
                    break;
                case 1:
                    antColor = ConstValue.RED;
                    break;
                case 2:
                    antColor = ConstValue.GREEN;
                    break;
                case 3:
                    antColor = ConstValue.BLUE;
                    break;
                case 4:
                    antColor = ConstValue.MAGENTA;
                    break;
                case 5:
                    antColor = ConstValue.YELLOW;
                    break;
                case 6:
                    antColor = ConstValue.CIAN;
                    break;
                default:
                    antColor = PaintTool.MakeColor((uint)(this._random.Next(256)), (uint)(this._random.Next(256)), (uint)(this._random.Next(256)), 0);
                    break;
            }

            this._antTotalCount++;

            AddAnt(x, y, this._random.Next(4), antColor, this._antlife);
        }

        /// <summary>
        /// アリの動作のタイマイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveAntTimer(object sender, EventArgs e)
        {
            MoveAnt();
        }

        /// <summary>
        /// アリの動作のタイマイベント(自動版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoMoveAntTimer(object sender, EventArgs e)
        {
            MoveAnt();
            ControlAnt();
        }
        #endregion
    }
}
