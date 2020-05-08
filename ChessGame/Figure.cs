using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Figure
    {
        public FigureName Name { get; private set; }
        public Color Color;

        public Figure(FigureName name, Color color)
        {
            Name = name;
            Color = color;
        }

        public void ChangeSide()
        {
            Color = Color == Color.White ? Color.Black : Color.White;
        }
    }
}
