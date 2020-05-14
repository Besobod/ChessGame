using System;
using System.Drawing;

namespace ChessGame
{
    class Knight : IFigure
    {
        public Bitmap FigureSprite { get; private set; }
        public Color Side { get; private set; }

        public void ChangeSide()
        {
            Side = Side == Color.White ? Color.Black : Color.White;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackKnight : Properties.Resources.whiteKnight;
        }

        public bool CorrectMove(Tuple<int, int> start, Tuple<int, int> finish)
        {
            if (Game.GameLink.HasFigureAt(finish) && Game.GameLink.Board[finish.Item1, finish.Item2].Side == Side)
                return false;
            int dy = finish.Item1 - start.Item1;
            int dx = finish.Item2 - start.Item2;
            return (Math.Abs(dx) == 1 && Math.Abs(dy) == 2)
                || (Math.Abs(dx) == 2 && Math.Abs(dy) == 1);
        }
        
        public Knight(Color side)
        {
            Side = side;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackKnight : Properties.Resources.whiteKnight;
        }
    }
}
