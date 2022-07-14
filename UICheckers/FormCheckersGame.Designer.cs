namespace UICheckers
{
    partial class FormCheckersGame
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
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelPlayer1Result = new System.Windows.Forms.Label();
            this.labelPlayer2Result = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(53, 18);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(305, 18);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer2.TabIndex = 1;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // labelPlayer1Result
            // 
            this.labelPlayer1Result.AutoSize = true;
            this.labelPlayer1Result.Location = new System.Drawing.Point(123, 18);
            this.labelPlayer1Result.Name = "labelPlayer1Result";
            this.labelPlayer1Result.Size = new System.Drawing.Size(16, 17);
            this.labelPlayer1Result.TabIndex = 2;
            this.labelPlayer1Result.Text = "0";
            // 
            // labelPlayer2Result
            // 
            this.labelPlayer2Result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2Result.AutoSize = true;
            this.labelPlayer2Result.Location = new System.Drawing.Point(375, 18);
            this.labelPlayer2Result.Name = "labelPlayer2Result";
            this.labelPlayer2Result.Size = new System.Drawing.Size(16, 17);
            this.labelPlayer2Result.TabIndex = 3;
            this.labelPlayer2Result.Text = "0";
            // 
            // FormCheckersGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 384);
            this.Controls.Add(this.labelPlayer2Result);
            this.Controls.Add(this.labelPlayer1Result);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormCheckersGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelPlayer1Result;
        private System.Windows.Forms.Label labelPlayer2Result;
    }
}