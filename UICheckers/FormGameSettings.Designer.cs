namespace UICheckers
{
    partial class FormGameSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(22, 22);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(81, 17);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.Location = new System.Drawing.Point(31, 51);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(59, 21);
            this.radioButton6X6.TabIndex = 2;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Text = "6 x 6";
            this.radioButton6X6.UseVisualStyleBackColor = true;
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.Location = new System.Drawing.Point(96, 51);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(59, 21);
            this.radioButton8X8.TabIndex = 3;
            this.radioButton8X8.TabStop = true;
            this.radioButton8X8.Text = "8 x 8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.Location = new System.Drawing.Point(160, 51);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(75, 21);
            this.radioButton10X10.TabIndex = 4;
            this.radioButton10X10.TabStop = true;
            this.radioButton10X10.Text = "10 x 10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(22, 85);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(59, 17);
            this.labelPlayers.TabIndex = 5;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(35, 114);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer1.TabIndex = 6;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(38, 145);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(86, 21);
            this.checkBoxPlayer2.TabIndex = 8;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(130, 114);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(105, 22);
            this.textBoxPlayer1Name.TabIndex = 9;
            this.textBoxPlayer1Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPlayer1Name_KeyPress);
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(130, 144);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(105, 22);
            this.textBoxPlayer2Name.TabIndex = 10;
            this.textBoxPlayer2Name.Text = "[Computer]";
            this.textBoxPlayer2Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPlayer2Name_KeyPress);
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(160, 179);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 11;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(265, 220);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButton10X10);
            this.Controls.Add(this.radioButton8X8);
            this.Controls.Add(this.radioButton6X6);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Button buttonDone;
    }
}