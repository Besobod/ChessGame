using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessGame
{
    class Pawn : IFigure
    {
        public Bitmap FigureSprite { get; private set; }
        public Color Side { get; private set; }

        public void ChangeSide()
        {
            Side = Side == Color.White ? Color.Black : Color.White;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackPawn : Properties.Resources.whitePawn;
        }

        public bool CorrectMove(Tuple<int, int> start, Tuple<int, int> finish)
        {
            if (Game.GameLink.HasFigureAt(finish) && Game.GameLink.Board[finish.Item1, finish.Item2].Side == Side)
                return false;
            int dy = finish.Item1 - start.Item1;
            int dx = finish.Item2 - start.Item2;
            return (Side == Color.White ? dy == -1 : dy == 1)
                && ((Math.Abs(dx) == 0 && !Game.GameLink.HasFigureAt(finish))
                || (Math.Abs(dx) == 1 && Game.GameLink.HasFigureAt(finish)));
        }

        public Pawn(Color side)
        {
            Side = side;
            FigureSprite = Side == Color.Black ? Properties.Resources.blackPawn : Properties.Resources.whitePawn;
        }
    }
}
