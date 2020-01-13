using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLSimulation.Logic
{
    public class Maze
    {
        /// <summary>
        /// 迷路を進む方向
        /// </summary>
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        // TODO:迷路の生成方法を選択可能にする
        public enum CreateMethod
        {
            StickDown,
            ExtendWall,
            DigHoles
        }

        /// <summary>
        /// 迷路の横幅（マス数）
        /// </summary>
        public int Width { get; private set; } = 15;

        /// <summary>
        /// 迷路の縦幅（マス数）
        /// </summary>
        public int Height { get; private set; } = 15;

        /// <summary>
        /// 迷路
        /// </summary>
        public MazeCell[,] Cells { get; private set; }

        /// <summary>
        /// ゴールまでの最短ステップ数
        /// </summary>
        public int ShortestStep { get; private set; }

        /// <summary>
        /// 乱数
        /// </summary>
        private Random Rand { get; set; } = new Random();

        /// <summary>
        /// セル座標
        /// </summary>
        public struct CellLocate
        {
            /// <summary>
            /// X座標
            /// </summary>
            public int X { get; }

            /// <summary>
            /// Y座標
            /// </summary>
            public int Y { get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="x">X座標</param>
            /// <param name="y">Y座標</param>
            public CellLocate(int x, int y)
            {
                X = x;
                Y = y;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="locate">セル座標</param>
            public CellLocate(CellLocate locate)
            {
                X = locate.X;
                Y = locate.Y;
            }
        }

        /// <summary>
        /// スタートセルの座標
        /// </summary>
        private CellLocate StartCell { get; set; }

        /// <summary>
        /// ゴールセルの座標
        /// </summary>
        private CellLocate GoalCell { get; set; }

        /// <summary>
        /// 現在位置と移動元を保持する構造体
        /// </summary>
        private struct CellRoute
        {
            public CellLocate Locate { get; set; }
            public CellLocate BeforeLocate { get; set; }

            public CellRoute(CellLocate locate, CellLocate beforeLocate)
            {
                Locate = locate;
                BeforeLocate = beforeLocate;
            }
        }

        /// <summary>
        /// 迷路を生成する
        /// </summary>
        /// <param name="method">迷路の生成方法</param>
        /// <param name="width">迷路の横幅</param>
        /// <param name="height">迷路の縦幅</param>
        /// <returns>false：迷路作成不可</returns>
        public bool CreateMaze(CreateMethod method, int width, int height)
        {
            if (Width < 5 || Width % 2 == 0
                || Height < 5 || Height % 2 == 0)
            {
                return false;
            }

            Width = width;
            Height = height;

            CreateMazeWithExtendMethod();

            SetShortestPath();

            return true;
        }

        /// <summary>
        /// 迷路を生成する（壁伸ばし法）
        /// </summary>
        private void CreateMazeWithExtendMethod()
        {
            // 迷路を初期化する
            List<CellLocate> startLocates = InitializeMaze();

            // すべての壁生成候補地点が無くなるまで壁伸ばし処理を実行する
            while(startLocates.Count > 0)
            {
                var index = Rand.Next(startLocates.Count);
                var locate = startLocates[index];
                startLocates.RemoveAt(index);

                if(Cells[locate.X, locate.Y].State == MazeCell.MazeCellState.Path)
                {
                    ExtendWall(locate);
                }
            }

            // スタートとゴールを初期化する
            SetStartCell(new CellLocate(1, 1));
            SetGoalCell(new CellLocate(Width - 2, Height - 2));
        }

        /// <summary>
        /// セルを移動する
        /// </summary>
        /// <param name="currentLocate">現在地</param>
        /// <param name="direction">移動方向</param>
        /// <returns>移動先のセル位置</returns>
        public CellLocate Move(CellLocate currentLocate, Direction direction)
        {
            CellLocate nextLocate;

            switch (direction)
            {
                case Direction.Up:
                    nextLocate = new CellLocate(currentLocate.X, currentLocate.Y - 1);
                    break;
                case Direction.Down:
                    nextLocate = new CellLocate(currentLocate.X, currentLocate.Y + 1);
                    break;
                case Direction.Left:
                    nextLocate = new CellLocate(currentLocate.X - 1, currentLocate.Y);
                    break;
                case Direction.Right:
                    nextLocate = new CellLocate(currentLocate.X + 1, currentLocate.Y);
                    break;
                default:
                    // 通常、ここには到達しない
                    nextLocate = new CellLocate(0,0);
                    break;
            }

            // 移動先が迷路の領域外の場合は移動しない
            if(nextLocate.X < 0 || nextLocate.X > Cells.GetLength(0) ||
                nextLocate.Y < 0 || nextLocate.Y > Cells.GetLength(1))
            {
                nextLocate = currentLocate;
            }

            // 移動先が壁なら移動しない
            if (Cells[nextLocate.X, nextLocate.Y].State == MazeCell.MazeCellState.Wall)
            {
                nextLocate = currentLocate;
            }

            return nextLocate;
        }

        /// <summary>
        /// スタート地点を設定する
        /// </summary>
        /// <param name="locate">スタート地点に設定したいセル</param>
        /// <returns>true:設定成功 false:設定変更できないセルが指定された</returns>
        public bool SetStartCell(CellLocate locate)
        {
            // 壁はスタートに設定不可
            if(Cells[locate.X, locate.Y].State == MazeCell.MazeCellState.Wall)
            {
                return false;
            }

            foreach (MazeCell cell in Cells)
            {
                cell.IsStart = false;
            }

            Cells[locate.X, locate.Y].IsStart = true;
            StartCell = new CellLocate(locate);

            return true;
        }

        /// <summary>
        /// ゴール地点を設定する
        /// </summary>
        /// <param name="locate">ゴール地点に設定したいセル</param>
        /// <returns>true:設定成功 false:設定変更できないセルが指定された</returns>
        public bool SetGoalCell(CellLocate locate)
        {
            // 壁はゴールに設定不可
            if (Cells[locate.X, locate.Y].State == MazeCell.MazeCellState.Wall)
            {
                return false;
            }

            foreach (MazeCell cell in Cells)
            {
                cell.IsGoal = false;
            }

            Cells[locate.X, locate.Y].IsGoal = true;
            GoalCell = new CellLocate(locate);

            return true;
        }

        /// <summary>
        /// 迷路を初期化する
        /// </summary>
        /// <returns>壁生成の候補座標リストを返す。</returns>
        private List<CellLocate> InitializeMaze()
        {
            Cells = new MazeCell[Width, Height];
            var startLocates = new List<CellLocate>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[x, y] = new MazeCell();
                    // 外周はすべて壁とする
                    if (x == 0 || x == Width - 1
                        || y == 0 || y == Height - 1)
                    {
                        Cells[x, y].State = MazeCell.MazeCellState.Wall;
                    }
                    else
                    {
                        Cells[x, y].State = MazeCell.MazeCellState.Path;

                        // x,y座標いずれも偶数のマスは、壁生成開始の候補地点として記録する
                        if (x % 2 == 0 && y % 2 == 0)
                        {
                            startLocates.Add(new CellLocate(x, y));
                        }
                    }
                }
            }

            return startLocates;
        }

        /// <summary>
        /// 壁を拡張する
        /// </summary>
        /// <param name="curenntLocate">現在の座標</param>
        /// <param name="activeWalls">現在拡張中の壁の座標リスト</param>
        private void ExtendWall(CellLocate currentLocate, Stack<CellLocate> activeWalls = null)
        {
            if(activeWalls == null)
            {
                activeWalls = new Stack<CellLocate>();
            }

            List<Direction> directions = GetExtendableDirection(currentLocate, activeWalls);

            if(directions.Count == 0)
            {
                // 進行できる方向が無い場合、現在地を壁にし、バックして進行できる方向を探す
                Cells[currentLocate.X, currentLocate.Y].State = MazeCell.MazeCellState.Wall;
                var beforeLocate = activeWalls.Pop();
                ExtendWall(beforeLocate);
            }
            else
            {
                var index = Rand.Next(directions.Count);
                CellLocate nextLocate = ExtendWallOneStep(currentLocate, directions[index], activeWalls);
                if(Cells[nextLocate.X, nextLocate.Y].State == MazeCell.MazeCellState.Path)
                {
                    ExtendWall(nextLocate, activeWalls);
                }
            }
        }

        /// <summary>
        /// 壁を拡張可能な方向のリストを取得する
        /// </summary>
        /// <param name="currentLocate">現在の座標</param>
        /// <param name="activeWalls">現在拡張中の壁の座標リスト</param>
        /// <returns>拡張可能な方向のリスト</returns>
        private List<Direction> GetExtendableDirection(CellLocate currentLocate, IReadOnlyCollection<CellLocate> activeWalls)
        {
            var directions = new List<Direction>();
            var x = currentLocate.X;
            var y = currentLocate.Y;

            // 1マス先が通路、かつ2マス先が拡張中の壁でない場合は拡張可能
            if(Cells[x, y - 1].State == MazeCell.MazeCellState.Path && !activeWalls.Contains(new CellLocate(x, y-2)))
            {
                directions.Add(Direction.Up);
            }

            if (Cells[x, y + 1].State == MazeCell.MazeCellState.Path && !activeWalls.Contains(new CellLocate(x, y + 2)))
            {
                directions.Add(Direction.Down);
            }

            if (Cells[x - 1, y].State == MazeCell.MazeCellState.Path && !activeWalls.Contains(new CellLocate(x - 2, y)))
            {
                directions.Add(Direction.Left);
            }

            if (Cells[x + 1, y].State == MazeCell.MazeCellState.Path && !activeWalls.Contains(new CellLocate(x + 2, y)))
            {
                directions.Add(Direction.Right);
            }

            return directions;
        }

        /// <summary>
        /// 壁拡張を1ステップ分（2マス）だけ進める
        /// </summary>
        /// <param name="locate">現在の座標</param>
        /// <param name="direction">拡張する方向</param>
        /// <param name="activeWalls">現在拡張中の壁の座標リスト</param>
        /// <returns>進行後の座標</returns>
        private CellLocate ExtendWallOneStep(CellLocate locate, Direction direction, Stack<CellLocate> activeWalls)
        {
            Cells[locate.X, locate.Y].State = MazeCell.MazeCellState.Wall;
            activeWalls.Push(new CellLocate(locate.X, locate.Y));

            CellLocate nextLocate;
            switch(direction)
            {
                case Direction.Up:
                    Cells[locate.X, locate.Y - 1].State = MazeCell.MazeCellState.Wall;
                    nextLocate = new CellLocate(locate.X, locate.Y - 2);
                    break;
                case Direction.Down:
                    Cells[locate.X, locate.Y + 1].State = MazeCell.MazeCellState.Wall;
                    nextLocate = new CellLocate(locate.X, locate.Y + 2);
                    break;
                case Direction.Left:
                    Cells[locate.X - 1, locate.Y].State = MazeCell.MazeCellState.Wall;
                    nextLocate = new CellLocate(locate.X - 2, locate.Y);
                    break;
                case Direction.Right:
                    Cells[locate.X + 1, locate.Y].State = MazeCell.MazeCellState.Wall;
                    nextLocate = new CellLocate(locate.X + 2, locate.Y);
                    break;
                default:
                    // 通常、ここには到達しない
                    return new CellLocate(-1, -1);
            }

            return nextLocate;
        }

        /// <summary>
        /// 最短経路を設定する
        /// </summary>
        private void SetShortestPath()
        {
            bool isGoal = false;
            var routeStack = new Stack<CellRoute>();
            var searchQueue = new Queue<CellLocate>();
            var visitedLocate = new List<CellLocate>();
            var startLocate = new CellLocate(StartCell);

            routeStack.Push(new CellRoute(startLocate,startLocate));
            searchQueue.Enqueue(startLocate);
            visitedLocate.Add(startLocate);

            // ゴールを探索する
            while(isGoal == false && searchQueue.Count > 0)
            {
                CellLocate currentLocate = searchQueue.Dequeue();

                foreach(Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    CellLocate nextLocate = Move(currentLocate, direction);

                    // 移動に失敗した場合
                    if (Equals(currentLocate, nextLocate))
                    {
                        continue;
                    }

                    // 移動に成功したが探索済みの場合
                    if(visitedLocate.Contains(nextLocate))
                    {
                        continue;
                    }

                    // 未探索の場合
                    searchQueue.Enqueue(nextLocate);
                    visitedLocate.Add(nextLocate);
                    routeStack.Push(new CellRoute(nextLocate, currentLocate));

                    if(Cells[nextLocate.X, nextLocate.Y].IsGoal)
                    {
                        isGoal = true;
                        break;
                    }
                }
            }

            // ゴールから最短ルートを逆算する
            CellRoute search = routeStack.Pop();
            CellLocate beforeLocate = search.BeforeLocate;
            Cells[search.Locate.X, search.Locate.Y].IsCriticalPath = true;
            int shortestStep = 0;

            while(routeStack.Count > 0)
            {
                search = routeStack.Pop();

                if(Equals(search.Locate, beforeLocate))
                {
                    Cells[search.Locate.X, search.Locate.Y].IsCriticalPath = true;
                    shortestStep++;
                    beforeLocate = search.BeforeLocate;
                }
            }

            ShortestStep = shortestStep;
        }

    }
}
