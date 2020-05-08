using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessGameForm : Form
    {
        Game Game;

        public ChessGameForm()
        {
            InitializeComponent();
        }
        void RedrawBoard()
        {
            for(int i=0; i<Game.Board.GetLength(0);i++)
                for(int j=0;j<Game.Board.GetLength(1);j++)
                {
                    var pictureBox = ChessBoard.GetControlFromPosition(j,i) as PictureBox;
                    if (Game.Board[i, j] != null)
                        pictureBox.Image = AssociateSpriteWithFigure(Game.Board[i, j]);
                    else
                        pictureBox.Image = null;
                }
        }

        Bitmap AssociateSpriteWithFigure(Figure figure)
        {
            switch(figure.Name)
            {
                case FigureName.Pawn:
                        return figure.Color == Color.White ? Properties.Resources.whitePawn : Properties.Resources.blackPawn;
                case FigureName.Knight:
                    return figure.Color == Color.White ? Properties.Resources.whiteKnight : Properties.Resources.blackKnight;
                case FigureName.Bishop:
                    return figure.Color == Color.White ? Properties.Resources.whiteBishop : Properties.Resources.blackBishop;
                case FigureName.Rook:
                    return figure.Color == Color.White ? Properties.Resources.whiteRook : Properties.Resources.blackRook;
                case FigureName.Queen:
                    return figure.Color == Color.White ? Properties.Resources.whiteQueen : Properties.Resources.blackQueen;
                case FigureName.King:
                    return figure.Color == Color.White ? Properties.Resources.whiteKing : Properties.Resources.blackKing;
            }
            return null;
        }

        private void ChessTile_MouseClick(object sender, MouseEventArgs e)
        {
            var selectedTileCoordinates =Tuple.Create( 
                ChessBoard.GetCellPosition(sender as Control).Row,
                ChessBoard.GetCellPosition(sender as Control).Column);
            if (e.Button == MouseButtons.Left)
            {
                Game.SelectFigure(selectedTileCoordinates);
            }
            if (e.Button == MouseButtons.Right)
            {
                if (figureList.SelectedIndex==-1 || figureList.SelectedIndex == 0)
                    Game.MoveFigure(selectedTileCoordinates);
                else
                    Game.SpawnFigure(selectedTileCoordinates, figureList.SelectedItem.ToString());
                figureList.ClearSelected();
                RedrawBoard();
                if (Game.GameFinished)
                {
                    MessageBox.Show(Game.PlayerTurn.ToString() + " player lost the game!");
                    ChessBoard.Enabled = false;
                }
                
            }
        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChessBoard.Enabled = true;
            Game = new Game();
            Game.InitializeGame();
            RedrawBoard();
        }

        private void ChessGameForm_Load(object sender, EventArgs e)
        {
            ChessBoard.Enabled = false;
        }
    }
}
