using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChessGame
{
    [TestFixture]
    class GameTests
    {
        [Test]
        public void PawnMovesCorrectly()
        {
            var game = new Game();
            var blackPawn = new Figure(FigureName.Pawn,Color.Black);
            var whitePawn = new Figure(FigureName.Pawn, Color.White);
            game.Board[3, 3] = blackPawn;
            game.Board[4, 4] = whitePawn;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            for (int i = 0; i < game.Board.GetLength(0); i++)
                for (int j = 0; j < game.Board.GetLength(1); j++)
                    if ((i == 4 && j == 3)
                        || (i == 4 && j == 4))
                        Assert.IsTrue(game.IsCorrectMove(Tuple.Create(i, j)));
                    else
                        Assert.IsFalse(game.IsCorrectMove(Tuple.Create(i, j)));
        }

        [Test]
        public void KnightMovesCorrectly()
        {
            var game = new Game();
            var blackKnight = new Figure(FigureName.Knight, Color.Black);
            game.Board[3, 3] = blackKnight;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            for (int i = 0; i < game.Board.GetLength(0); i++)
                for (int j = 0; j < game.Board.GetLength(1); j++)
                    if ((i == 1 && j == 2)
                        || (i == 1 && j == 4)
                        || (i == 2 && j == 1)
                        || (i == 2 && j == 5)
                        || (i == 4 && j == 1)
                        || (i == 4 && j == 5)
                        || (i == 5 && j == 2)
                        || (i == 5 && j == 4))
                        Assert.IsTrue(game.IsCorrectMove(Tuple.Create(i, j)));
                    else
                        Assert.IsFalse(game.IsCorrectMove(Tuple.Create(i, j)));
        }

        [Test]
        public void BishopMovesCorrectly()
        {
            var game = new Game();
            var blackBishop = new Figure(FigureName.Bishop, Color.Black);
            game.Board[3, 3] = blackBishop;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            for (int i = 0; i < game.Board.GetLength(0); i++)
                for (int j = 0; j < game.Board.GetLength(1); j++)
                    if (i != 3 && ((i == j)
                        || (i + j == 6)))
                        Assert.IsTrue(game.IsCorrectMove(Tuple.Create(i, j)));
                    else
                        Assert.IsFalse(game.IsCorrectMove(Tuple.Create(i, j)));
        }

        [Test]
        public void RookMovesCorrectly()
        {
            var game = new Game();
            var blackRook = new Figure(FigureName.Rook, Color.Black);
            game.Board[3, 3] = blackRook;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            for (int i = 0; i < game.Board.GetLength(0); i++)
                for (int j = 0; j < game.Board.GetLength(1); j++)
                    if ((i == 3 || j == 3) && !(i == 3 && j == 3))
                        Assert.IsTrue(game.IsCorrectMove(Tuple.Create(i, j)));
                    else
                        Assert.IsFalse(game.IsCorrectMove(Tuple.Create(i, j)));
        }

        [Test]
        public void QueenMovesCorrectly()
        {
            var game = new Game();
            var blackQueen = new Figure(FigureName.Queen, Color.Black);
            game.Board[3, 3] = blackQueen;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            for (int i = 0; i < game.Board.GetLength(0); i++)
                for (int j = 0; j < game.Board.GetLength(1); j++)
                    if (((i == 3 || j == 3) && !(i == 3 && j == 3))
                        || (i != 3 && ((i == j)
                        || (i + j == 6))))
                        Assert.IsTrue(game.IsCorrectMove(Tuple.Create(i, j)));
                    else
                        Assert.IsFalse(game.IsCorrectMove(Tuple.Create(i, j)));
        }

        [Test]
        public void KingMovesCorrectly()
        {
            var game = new Game();
            var blackKing = new Figure(FigureName.King, Color.Black);
            game.Board[3, 3] = blackKing;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            for (int i = 0; i < game.Board.GetLength(0); i++)
                for (int j = 0; j < game.Board.GetLength(1); j++)
                    if (!(i == 3 && j == 3) 
                        && (1 < i && i < 5) 
                        && (1 < j && j < 5))
                        Assert.IsTrue(game.IsCorrectMove(Tuple.Create(i, j)));
                    else
                        Assert.IsFalse(game.IsCorrectMove(Tuple.Create(i, j)));
        }
        
        [Test]
        public void FiguresDontReplaceAllies()
        {
            var game = new Game();
            var blackKing = new Figure(FigureName.King, Color.Black);
            var blackQueen = new Figure(FigureName.Queen, Color.Black);
            game.Board[3, 3] = blackKing;
            game.Board[4, 4] = blackQueen;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            Assert.IsFalse(game.IsCorrectMove(Tuple.Create(4, 4)));
        }

        [Test]
        public void FiguresDontMoveThrough()
        {
            var game = new Game();
            var blackKing = new Figure(FigureName.King, Color.Black);
            var blackQueen = new Figure(FigureName.Queen, Color.Black);
            game.Board[3, 3] = blackQueen;
            game.Board[4, 3] = blackKing;
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            Assert.IsFalse(game.IsCorrectMove(Tuple.Create(6, 3)));
            game.Board[3, 3] = null;
            var blackBishop = new Figure(FigureName.Bishop, Color.Black);
            game.Board[3, 2] = blackBishop;
            game.SelectedFigureCoordinates = Tuple.Create(3, 2);
        }

        [Test]
        public void FiguresCantSpawnOnOthers()
        {
            var game = new Game();
            var whiteQueen = new Figure(FigureName.Queen, Color.White);
            var blackPawn = new Figure(FigureName.Pawn, Color.Black);
            var blackBishop = new Figure(FigureName.Bishop, Color.Black);
            game.SelectedFigureCoordinates = Tuple.Create(3, 3);
            game.Board[3, 3] = whiteQueen;
            game.Board[4, 3] = blackPawn;
            game.Board[0, 0] = blackBishop;
            game.MoveFigure(Tuple.Create(4, 3));
            game.SelectedFigureCoordinates = Tuple.Create(0, 0);
            game.MoveFigure(Tuple.Create(1, 1));
            game.SpawnFigure(Tuple.Create(1, 1), "Pawn");
            game.SpawnFigure(Tuple.Create(4, 3), "Pawn");
            Assert.AreEqual(game.Board[1, 1].Name, FigureName.Bishop);
            Assert.AreEqual(game.Board[4, 3].Name, FigureName.Queen);
        }
    }
}
