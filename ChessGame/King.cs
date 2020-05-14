using System;
using System.Drawing;

namespace ChessGame
{
    class King : IFigure
    {
        public Bitmap FigureSprite { get; private set; }
        public Color Side { get; private set; }
        public void ChangeSide()
        {
            throw new Exception("King cannot be converted.");
        }

        public bool CorrectMove(Tuple<int, int> start, Tuple<int, int> finish)
        {
            if (Game.GameLink.HasFigureAt(finish) && Game.GameLink.Board[finish.Item1, finish.Item2].Side == Side)
                return false;
            int dy = finish.Item1 - start.Item1;
            int dx = finish.Item2 - start.Item2;
            if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
                return true;
            return false;

        }

        public King(Color side)
        {
            Side = side;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackKing : Properties.Resources.whiteKing;
        }
    }
}
