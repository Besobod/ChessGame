using System;
using System.Collections.Generic;
using System.Linq;
namespace ChessGame
{
     public enum FigureName
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King

    }

    public enum Color
    {
        White,
        Black,
    }

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

    public class Game
    {
        public Figure[,] Board;
        public Tuple<int, int> SelectedFigureCoordinates;
        Dictionary<Color, List<Figure>> DeadFigures;

        public Game()
        {
            Board = new Figure[8, 8];
            DeadFigures = new Dictionary<Color, List<Figure>>();
            DeadFigures.Add(Color.Black, new List<Figure>());
            DeadFigures.Add(Color.White, new List<Figure>());
            PlayerTurn = Color.White;
        }

        public bool GameFinished { get; private set; }

        public Color PlayerTurn { get; private set; }

        public void ChangeTurn()
        {
            PlayerTurn = PlayerTurn == Color.White ? Color.Black : Color.White;
        }

        public void SelectFigure(Tuple<int,int> coordinates)
        {
            if(Board[coordinates.Item1, coordinates.Item2] != null
                && Board[coordinates.Item1, coordinates.Item2].Color == PlayerTurn)
            {
                SelectedFigureCoordinates = coordinates;
            }
        }

        public bool IsCorrectMove(Tuple<int,int> destination)
        {
            if (SelectedFigureCoordinates == null
                || destination.Equals(SelectedFigureCoordinates)
                || (Board[destination.Item1, destination.Item2] != null
                && Board[destination.Item1, destination.Item2].Color ==
                Board[SelectedFigureCoordinates.Item1, SelectedFigureCoordinates.Item2].Color)) 
                return false;
            var figure = Board[SelectedFigureCoordinates.Item1, SelectedFigureCoordinates.Item2];
            var dy = destination.Item1 - SelectedFigureCoordinates.Item1;
            var dx = destination.Item2 - SelectedFigureCoordinates.Item2;
            switch (figure.Name)
            {
                case FigureName.Pawn:
                    {
                        return (figure.Color == Color.White ? dy == -1 : dy == 1)
                            && ((dx == 0 && Board[destination.Item1, destination.Item2] == null)
                            || (Board[destination.Item1, destination.Item2] != null && (dx == 1 || dx == -1))
                            );
                    }
                case FigureName.Knight:
                    {
                        
                        return (Math.Abs(dx) == 1 && Math.Abs(dy) == 2) || (Math.Abs(dx) == 2 && Math.Abs(dy) == 1);
                    }
                case FigureName.Bishop:
                    {
                        return Math.Abs(dx) == Math.Abs(dy) && NoFiguresInWay(destination);
                    }
                case FigureName.Rook:
                    {
                        return (dy == 0 || dx == 0) && NoFiguresInWay(destination);
                    }
                case FigureName.Queen:
                    {
                        return (Math.Abs(dx) == Math.Abs(dy) || dy == 0 || dx == 0) && NoFiguresInWay(destination);
                    }
                case FigureName.King:
                    {
                        return Math.Abs(dx)<=1 && Math.Abs(dy) <= 1;
                    }
            }
            return true;
        }
        bool NoFiguresInWay(Tuple<int,int> destination)
        {
            var dy = destination.Item1 - SelectedFigureCoordinates.Item1;
            var dx = destination.Item2 - SelectedFigureCoordinates.Item2;
            while(dx!=0 || dy!=0)
            {
                dx -= Math.Sign(dx);
                dy -= Math.Sign(dy);
                if (Board[SelectedFigureCoordinates.Item1 + dy, SelectedFigureCoordinates.Item2 + dx] != null
                    && Math.Abs(dx) + Math.Abs(dy) > 0)
                    return false;
            }
            return true;
        }

        public bool MoveFigure(Tuple<int,int> coordinates)
        {
            if (!IsCorrectMove(coordinates))
                return false;
            if (Board[coordinates.Item1, coordinates.Item2] != null)
                KillFigure(coordinates);
            Board[coordinates.Item1, coordinates.Item2] =
                Board[SelectedFigureCoordinates.Item1, SelectedFigureCoordinates.Item2];
            Board[SelectedFigureCoordinates.Item1, SelectedFigureCoordinates.Item2] = null;
            SelectedFigureCoordinates = null;
            ChangeTurn();
            return true;
        }

        void KillFigure(Tuple<int,int> figurePosition)
        {
            var figure = Board[figurePosition.Item1, figurePosition.Item2];
            if (figure.Name == FigureName.King)
            {
                GameFinished = true;
                return;
            }
            DeadFigures[figure.Color].Add(figure);
            Board[figurePosition.Item1, figurePosition.Item2] = null;
        }
        public void SpawnFigure(Tuple<int,int> position,string name)
        {
            if (Board[position.Item1, position.Item2] != null)
                return;
            var enemyColor = PlayerTurn == Color.White ? Color.Black : Color.White;
            if (DeadFigures[enemyColor].Count > 0)
            {
                var deadFigures = DeadFigures[enemyColor].Where(figure => figure.Name.ToString() == name);
                if (deadFigures != null)
                {
                    var deadFigure = deadFigures.First();
                    deadFigure.ChangeSide();
                    Board[position.Item1, position.Item2] = deadFigure;
                    DeadFigures[enemyColor].Remove(deadFigure);
                    ChangeTurn();
                }
            }
        }

        public void InitializeGame()
        {
            for (int i = 0; i < Board.GetLength(1); i++)
                Board[6, i] = new Figure(FigureName.Pawn, Color.White);
            Board[7,0]= new Figure(FigureName.Rook, Color.White);
            Board[7,1]= new Figure(FigureName.Knight, Color.White);
            Board[7,2]= new Figure(FigureName.Bishop, Color.White);
            Board[7,3]= new Figure(FigureName.Queen, Color.White);
            Board[7,4]= new Figure(FigureName.King, Color.White);
            Board[7,5]= new Figure(FigureName.Bishop, Color.White);
            Board[7,6]= new Figure(FigureName.Knight, Color.White);
            Board[7,7]= new Figure(FigureName.Rook, Color.White);

            for (int i = 0; i < Board.GetLength(1); i++)
                Board[1, i] = new Figure(FigureName.Pawn, Color.Black);
            Board[0, 0] = new Figure(FigureName.Rook, Color.Black);
            Board[0, 1] = new Figure(FigureName.Knight, Color.Black);
            Board[0, 2] = new Figure(FigureName.Bishop, Color.Black);
            Board[0, 3] = new Figure(FigureName.Queen, Color.Black);
            Board[0, 4] = new Figure(FigureName.King, Color.Black);
            Board[0, 5] = new Figure(FigureName.Bishop, Color.Black);
            Board[0, 6] = new Figure(FigureName.Knight, Color.Black);
            Board[0, 7] = new Figure(FigureName.Rook, Color.Black);
        }
    }
}
