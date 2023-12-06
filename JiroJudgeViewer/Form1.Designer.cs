namespace JiroJudgeViewer {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label4 = new System.Windows.Forms.Label();
            this.LbGrP1 = new System.Windows.Forms.Label();
            this.LbGdP1 = new System.Windows.Forms.Label();
            this.LbBdP1 = new System.Windows.Forms.Label();
            this.LbSbNotesP1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Rat = new System.Windows.Forms.Label();
            this.LbRating = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LbScoreP1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LbScoreP2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LbBdP2 = new System.Windows.Forms.Label();
            this.LbGdP2 = new System.Windows.Forms.Label();
            this.LbGrP2 = new System.Windows.Forms.Label();
            this.LbSbNotesP2 = new System.Windows.Forms.Label();
            this.LbTotalScore = new System.Windows.Forms.Label();
            this.LbISRESULT = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtResultOutput = new System.Windows.Forms.Button();
            this.LbExScore = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LbMAXMinus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LbGaugeStatusP1 = new System.Windows.Forms.Label();
            this.LbGaugeStatusP2 = new System.Windows.Forms.Label();
            this.BtResultCopy = new System.Windows.Forms.Button();
            this.cbHardGauge = new System.Windows.Forms.ComboBox();
            this.CbIfDowned = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(12, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(288, 48);
            this.label4.TabIndex = 3;
            this.label4.Text = "残りノーツ数(P1)：";
            // 
            // LbGrP1
            // 
            this.LbGrP1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbGrP1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LbGrP1.Location = new System.Drawing.Point(74, 9);
            this.LbGrP1.Name = "LbGrP1";
            this.LbGrP1.Size = new System.Drawing.Size(118, 48);
            this.LbGrP1.TabIndex = 6;
            this.LbGrP1.Text = "0";
            this.LbGrP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbGdP1
            // 
            this.LbGdP1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbGdP1.ForeColor = System.Drawing.Color.Green;
            this.LbGdP1.Location = new System.Drawing.Point(198, 9);
            this.LbGdP1.Name = "LbGdP1";
            this.LbGdP1.Size = new System.Drawing.Size(118, 48);
            this.LbGdP1.TabIndex = 7;
            this.LbGdP1.Text = "0";
            this.LbGdP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbBdP1
            // 
            this.LbBdP1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbBdP1.ForeColor = System.Drawing.Color.Gray;
            this.LbBdP1.Location = new System.Drawing.Point(324, 9);
            this.LbBdP1.Name = "LbBdP1";
            this.LbBdP1.Size = new System.Drawing.Size(118, 48);
            this.LbBdP1.TabIndex = 8;
            this.LbBdP1.Text = "0";
            this.LbBdP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbSbNotesP1
            // 
            this.LbSbNotesP1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbSbNotesP1.Location = new System.Drawing.Point(290, 192);
            this.LbSbNotesP1.Name = "LbSbNotesP1";
            this.LbSbNotesP1.Size = new System.Drawing.Size(124, 48);
            this.LbSbNotesP1.TabIndex = 9;
            this.LbSbNotesP1.Text = "0";
            this.LbSbNotesP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Rat
            // 
            this.Rat.AutoSize = true;
            this.Rat.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Rat.Location = new System.Drawing.Point(12, 346);
            this.Rat.Name = "Rat";
            this.Rat.Size = new System.Drawing.Size(162, 48);
            this.Rat.TabIndex = 12;
            this.Rat.Text = "Rating：";
            // 
            // LbRating
            // 
            this.LbRating.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbRating.Location = new System.Drawing.Point(180, 346);
            this.LbRating.Name = "LbRating";
            this.LbRating.Size = new System.Drawing.Size(168, 48);
            this.LbRating.TabIndex = 13;
            this.LbRating.Text = "0.00";
            this.LbRating.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 48);
            this.label1.TabIndex = 16;
            this.label1.Text = "P1";
            // 
            // LbScoreP1
            // 
            this.LbScoreP1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbScoreP1.Location = new System.Drawing.Point(448, 9);
            this.LbScoreP1.Name = "LbScoreP1";
            this.LbScoreP1.Size = new System.Drawing.Size(168, 48);
            this.LbScoreP1.TabIndex = 17;
            this.LbScoreP1.Text = "0.00%";
            this.LbScoreP1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 48);
            this.label3.TabIndex = 18;
            this.label3.Text = "残りノーツ数(P2)：";
            // 
            // LbScoreP2
            // 
            this.LbScoreP2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbScoreP2.Location = new System.Drawing.Point(448, 57);
            this.LbScoreP2.Name = "LbScoreP2";
            this.LbScoreP2.Size = new System.Drawing.Size(168, 48);
            this.LbScoreP2.TabIndex = 23;
            this.LbScoreP2.Text = "0.00%";
            this.LbScoreP2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(12, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 48);
            this.label8.TabIndex = 22;
            this.label8.Text = "P2";
            // 
            // LbBdP2
            // 
            this.LbBdP2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbBdP2.ForeColor = System.Drawing.Color.Gray;
            this.LbBdP2.Location = new System.Drawing.Point(324, 57);
            this.LbBdP2.Name = "LbBdP2";
            this.LbBdP2.Size = new System.Drawing.Size(118, 48);
            this.LbBdP2.TabIndex = 21;
            this.LbBdP2.Text = "0";
            this.LbBdP2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbGdP2
            // 
            this.LbGdP2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbGdP2.ForeColor = System.Drawing.Color.Green;
            this.LbGdP2.Location = new System.Drawing.Point(198, 57);
            this.LbGdP2.Name = "LbGdP2";
            this.LbGdP2.Size = new System.Drawing.Size(118, 48);
            this.LbGdP2.TabIndex = 20;
            this.LbGdP2.Text = "0";
            this.LbGdP2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbGrP2
            // 
            this.LbGrP2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbGrP2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LbGrP2.Location = new System.Drawing.Point(74, 57);
            this.LbGrP2.Name = "LbGrP2";
            this.LbGrP2.Size = new System.Drawing.Size(118, 48);
            this.LbGrP2.TabIndex = 19;
            this.LbGrP2.Text = "0";
            this.LbGrP2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbSbNotesP2
            // 
            this.LbSbNotesP2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbSbNotesP2.Location = new System.Drawing.Point(290, 240);
            this.LbSbNotesP2.Name = "LbSbNotesP2";
            this.LbSbNotesP2.Size = new System.Drawing.Size(124, 48);
            this.LbSbNotesP2.TabIndex = 24;
            this.LbSbNotesP2.Text = "0";
            this.LbSbNotesP2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTotalScore
            // 
            this.LbTotalScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LbTotalScore.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbTotalScore.Location = new System.Drawing.Point(770, 118);
            this.LbTotalScore.Name = "LbTotalScore";
            this.LbTotalScore.Size = new System.Drawing.Size(168, 48);
            this.LbTotalScore.TabIndex = 25;
            this.LbTotalScore.Text = "0.00%";
            this.LbTotalScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LbTotalScore.Click += new System.EventHandler(this.LbTotalScore_Click);
            // 
            // LbISRESULT
            // 
            this.LbISRESULT.AutoSize = true;
            this.LbISRESULT.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbISRESULT.Location = new System.Drawing.Point(12, 298);
            this.LbISRESULT.Name = "LbISRESULT";
            this.LbISRESULT.Size = new System.Drawing.Size(118, 48);
            this.LbISRESULT.TabIndex = 26;
            this.LbISRESULT.Text = "FALSE";
            this.LbISRESULT.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(566, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 48);
            this.label2.TabIndex = 27;
            this.label2.Text = "総合スコア：";
            // 
            // BtResultOutput
            // 
            this.BtResultOutput.Location = new System.Drawing.Point(656, 333);
            this.BtResultOutput.Name = "BtResultOutput";
            this.BtResultOutput.Size = new System.Drawing.Size(282, 62);
            this.BtResultOutput.TabIndex = 28;
            this.BtResultOutput.Text = "リザルトファイル手動出力\r\n（飾りです）";
            this.BtResultOutput.UseVisualStyleBackColor = true;
            this.BtResultOutput.Click += new System.EventHandler(this.BtResultOutput_Click);
            // 
            // LbExScore
            // 
            this.LbExScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LbExScore.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbExScore.Location = new System.Drawing.Point(190, 118);
            this.LbExScore.Name = "LbExScore";
            this.LbExScore.Size = new System.Drawing.Size(116, 48);
            this.LbExScore.TabIndex = 30;
            this.LbExScore.Text = "0";
            this.LbExScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label6.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(12, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 48);
            this.label6.TabIndex = 29;
            this.label6.Text = "ExScore：";
            // 
            // LbMAXMinus
            // 
            this.LbMAXMinus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.LbMAXMinus.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbMAXMinus.Location = new System.Drawing.Point(434, 118);
            this.LbMAXMinus.Name = "LbMAXMinus";
            this.LbMAXMinus.Size = new System.Drawing.Size(118, 48);
            this.LbMAXMinus.TabIndex = 32;
            this.LbMAXMinus.Text = "0";
            this.LbMAXMinus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label7.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(324, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 48);
            this.label7.TabIndex = 31;
            this.label7.Text = "MAX-";
            // 
            // LbGaugeStatusP1
            // 
            this.LbGaugeStatusP1.AutoSize = true;
            this.LbGaugeStatusP1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbGaugeStatusP1.Location = new System.Drawing.Point(622, 9);
            this.LbGaugeStatusP1.Name = "LbGaugeStatusP1";
            this.LbGaugeStatusP1.Size = new System.Drawing.Size(0, 48);
            this.LbGaugeStatusP1.TabIndex = 33;
            // 
            // LbGaugeStatusP2
            // 
            this.LbGaugeStatusP2.AutoSize = true;
            this.LbGaugeStatusP2.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbGaugeStatusP2.Location = new System.Drawing.Point(622, 57);
            this.LbGaugeStatusP2.Name = "LbGaugeStatusP2";
            this.LbGaugeStatusP2.Size = new System.Drawing.Size(0, 48);
            this.LbGaugeStatusP2.TabIndex = 34;
            // 
            // BtResultCopy
            // 
            this.BtResultCopy.Location = new System.Drawing.Point(656, 266);
            this.BtResultCopy.Name = "BtResultCopy";
            this.BtResultCopy.Size = new System.Drawing.Size(282, 62);
            this.BtResultCopy.TabIndex = 35;
            this.BtResultCopy.Text = "現在の結果を\r\nグリップボードにコピー";
            this.BtResultCopy.UseVisualStyleBackColor = true;
            this.BtResultCopy.Click += new System.EventHandler(this.BtResultCopy_Click);
            // 
            // cbHardGauge
            // 
            this.cbHardGauge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHardGauge.FormattingEnabled = true;
            this.cbHardGauge.Items.AddRange(new object[] {
            "通常ハードゲージ",
            "超ハードゲージ",
            "バカハードゲージ",
            "回復なしモード",
            "フレアゲージ",
            "フレアEXゲージ",
            "可のみゲージ"});
            this.cbHardGauge.Location = new System.Drawing.Point(656, 227);
            this.cbHardGauge.Name = "cbHardGauge";
            this.cbHardGauge.Size = new System.Drawing.Size(282, 33);
            this.cbHardGauge.TabIndex = 36;
            // 
            // CbIfDowned
            // 
            this.CbIfDowned.AutoSize = true;
            this.CbIfDowned.Location = new System.Drawing.Point(577, 192);
            this.CbIfDowned.Name = "CbIfDowned";
            this.CbIfDowned.Size = new System.Drawing.Size(361, 29);
            this.CbIfDowned.TabIndex = 37;
            this.CbIfDowned.Text = "ハードゲージで片方が落ちた時に強制終了する";
            this.CbIfDowned.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(950, 410);
            this.Controls.Add(this.CbIfDowned);
            this.Controls.Add(this.cbHardGauge);
            this.Controls.Add(this.BtResultCopy);
            this.Controls.Add(this.LbGaugeStatusP2);
            this.Controls.Add(this.LbGaugeStatusP1);
            this.Controls.Add(this.LbMAXMinus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LbExScore);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BtResultOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LbISRESULT);
            this.Controls.Add(this.LbTotalScore);
            this.Controls.Add(this.LbSbNotesP2);
            this.Controls.Add(this.LbScoreP2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LbBdP2);
            this.Controls.Add(this.LbGdP2);
            this.Controls.Add(this.LbGrP2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LbScoreP1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LbRating);
            this.Controls.Add(this.Rat);
            this.Controls.Add(this.LbSbNotesP1);
            this.Controls.Add(this.LbBdP1);
            this.Controls.Add(this.LbGdP1);
            this.Controls.Add(this.LbGrP1);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DP Judge Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label4;
        private Label LbGrP1;
        private Label LbGdP1;
        private Label LbBdP1;
        private Label LbSbNotesP1;
        private System.Windows.Forms.Timer timer1;
        private Label Rat;
        private Label LbRating;
        private Label label1;
        private Label LbScoreP1;
        private Label label3;
        private Label LbScoreP2;
        private Label label8;
        private Label LbBdP2;
        private Label LbGdP2;
        private Label LbGrP2;
        private Label LbSbNotesP2;
        private Label LbTotalScore;
        private Label LbISRESULT;
        private Label label2;
        private Button BtResultOutput;
        private Label LbExScore;
        private Label label6;
        private Label LbMAXMinus;
        private Label label7;
        private Label LbGaugeStatusP1;
        private Label LbGaugeStatusP2;
        private Button BtResultCopy;
        private ComboBox cbHardGauge;
        private CheckBox CbIfDowned;
    }
}