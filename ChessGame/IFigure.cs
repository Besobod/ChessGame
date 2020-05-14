using System;
using System.Drawing;

namespace ChessGame
{
    public interface IFigure
    {
        bool CorrectMove(Tuple<int, int> start, Tuple<int, int> finish);
        Color Side { get; }
        Bitmap FigureSprite { get; }
        void ChangeSide();
    }
}
