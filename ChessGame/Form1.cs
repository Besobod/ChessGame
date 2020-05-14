using System;
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
                        pictureBox.Image = Game.Board[i, j].FigureSprite;
                    else
                        pictureBox.Image = null;
                }
            PlayerTurnLabel.Text = "Player Turn:" + Game.PlayerTurn.ToString();
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
                    PlayerTurnLabel.Text = @"Press ""Start game"" button";
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
