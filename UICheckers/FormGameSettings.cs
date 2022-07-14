using System;
using System.Windows.Forms;
using CheckersLogic;

namespace UICheckers
{
    public partial class FormGameSettings : Form
    {
        private const int k_MaxTextBoxLength = 20;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        public string Player1Name
        {
            get { return textBoxPlayer1Name.Text; }
        }

        public bool IsComputerOpponent
        {
            get { return !checkBoxPlayer2.Checked; }
        }

        public int GetBoardSize()
        {
            eBoardSize boardSize;

            if (radioButton6X6.Checked)
            {
                boardSize = eBoardSize.Small;
            }
            else if (radioButton8X8.Checked)
            {
                boardSize = eBoardSize.Medium;
            }
            else
            {
                boardSize = eBoardSize.Large;
            }

            return (int)boardSize;
        }

        public string GetPlayer2Name()
        {
            string name;

            if (IsComputerOpponent)
            {
                name = "Computer";
            }
            else
            {
                name = textBoxPlayer2Name.Text;
            }

            return name;
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(textBoxPlayer2Name.Enabled)
            {
                textBoxPlayer2Name.Enabled = false;
                textBoxPlayer2Name.Text = "[Computer]";
            }
            else
            {
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.Text = string.Empty;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPlayer1Name.Text) || string.IsNullOrEmpty(textBoxPlayer2Name.Text))
            {
                MessageBox.Show("Please enter your player name.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void textBoxPlayer1Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleInvalidText(sender, e);
        }

        private void textBoxPlayer2Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleInvalidText(sender, e);
        }

        private void handleInvalidText(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter only letters!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if(textBox.Text.Length > k_MaxTextBoxLength)
            {
                e.Handled = true;
                MessageBox.Show("You can enter only 20 letters!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
