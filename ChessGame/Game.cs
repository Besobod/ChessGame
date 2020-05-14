using System;
using System.Collections.Generic;
using System.Linq;
namespace ChessGame
{
    public class Game
    {
        public static Game GameLink { get; private set; }
        public IFigure[,] Board { get; private set; }
        public Tuple<int, int> SelectedFigureCoordinates;
        Dictionary<Color, List<IFigure>> DeadFigures;
        public bool GameFinished { get; private set; }
        public Color PlayerTurn { get; private set; }

        public Game()
        {
            GameLink = this;
            Board = new IFigure[8, 8];
            DeadFigures = new Dictionary<Color, List<IFigure>>();
            DeadFigures.Add(Color.Black, new List<IFigure>());
            DeadFigures.Add(Color.White, new List<IFigure>());
            PlayerTurn = Color.White;
        }

        public bool HasFigureAt(Tuple<int,int> tile)
        {
            return Board[tile.Item1, tile.Item2] != null;
        }

        public void ChangeTurn()
        {
            PlayerTurn = PlayerTurn == Color.White ? Color.Black : Color.White;
        }

        public void SelectFigure(Tuple<int,int> coordinates)
        {
            if(Board[coordinates.Item1, coordinates.Item2] != null
                && Board[coordinates.Item1, coordinates.Item2].Side == PlayerTurn)
            {
                SelectedFigureCoordinates = coordinates;
            }
        }

        public bool MoveFigure(Tuple<int,int> coordinates)
        {
            if (SelectedFigureCoordinates == null
                || !Board[SelectedFigureCoordinates.Item1, SelectedFigureCoordinates.Item2]
                .CorrectMove(SelectedFigureCoordinates, coordinates))
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
            if (figure is King)
            {
                GameFinished = true;
                return;
            }
            DeadFigures[figure.Side].Add(figure);
            Board[figurePosition.Item1, figurePosition.Item2] = null;
        }

        public void SpawnFigure(Tuple<int,int> position,string name)
        {
            Type figureType = Type.GetType("ChessGame."+name);
            if (Board[position.Item1, position.Item2] != null)
                return;
            var enemyColor = PlayerTurn == Color.White ? Color.Black : Color.White;
            if (DeadFigures[enemyColor].Count > 0)
            {
                var deadFigures = DeadFigures[enemyColor].Where(figure => Equals(figure.GetType(), figureType));
                if (deadFigures.Count()>0)
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
                Board[6, i] = new Pawn(Color.White);
            Board[7,0]= new Rook(Color.White);
            Board[7,1]= new Knight(Color.White);
            Board[7,2]= new Bishop(Color.White);
            Board[7,3]= new Queen(Color.White);
            Board[7,4]= new King(Color.White);
            Board[7,5]= new Bishop(Color.White);
            Board[7,6]= new Knight(Color.White);
            Board[7,7]= new Rook(Color.White);

            for (int i = 0; i < Board.GetLength(1); i++)
                Board[1, i] = new Pawn(Color.Black);
            Board[0, 0] = new Rook(Color.Black);
            Board[0, 1] = new Knight(Color.Black);
            Board[0, 2] = new Bishop(Color.Black);
            Board[0, 3] = new Queen(Color.Black);
            Board[0, 4] = new King(Color.Black);
            Board[0, 5] = new Bishop(Color.Black);
            Board[0, 6] = new Knight(Color.Black);
            Board[0, 7] = new Rook(Color.Black);
        }
    }
}
