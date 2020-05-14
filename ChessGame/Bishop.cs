using System;
using System.Drawing;

namespace ChessGame
{
    class Bishop : IFigure
    {
        public Bitmap FigureSprite { get; private set; }
        public Color Side { get; private set; }

        public void ChangeSide()
        {
            Side = Side == Color.White ? Color.Black : Color.White;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackBishop : Properties.Resources.whiteBishop;
        }

        public bool CorrectMove(Tuple<int, int> start, Tuple<int, int> finish)
        {
            if (Game.GameLink.HasFigureAt(finish) && Game.GameLink.Board[finish.Item1, finish.Item2].Side == Side)
                return false;
            int dy = finish.Item1 - start.Item1;
            int dx = finish.Item2 - start.Item2;
            if (Math.Abs(dx) == Math.Abs(dy) && NoFiguresInWay(start, finish))
                return true;
            return false;

        }

        bool NoFiguresInWay(Tuple<int,int> start, Tuple<int, int> finish)
        {
            var dy = finish.Item1 - start.Item1;
            var dx = finish.Item2 - start.Item2;
            while (dx != 0 || dy != 0)
            {
                dx -= Math.Sign(dx);
                dy -= Math.Sign(dy);
                if (Game.GameLink.HasFigureAt(Tuple.Create(start.Item1 + dy, start.Item2 + dx))
                    && Math.Abs(dx) + Math.Abs(dy) > 0)
                    return false;
            }
            return true;
        }

        public Bishop(Color side)
        {
            Side = side;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackBishop : Properties.Resources.whiteBishop;
        }
    }
}
