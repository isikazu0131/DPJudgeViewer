namespace JiroJudgeViewer {
    partial class ResultViewer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultViewer));
            this.DgvResult = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LEVEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClearMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Great1P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Good1P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bad1P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Great2P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Good2P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bad2P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAXMinus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score1P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score2P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtFolderSetting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvResult
            // 
            this.DgvResult.AllowUserToAddRows = false;
            this.DgvResult.AllowUserToDeleteRows = false;
            this.DgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.LEVEL,
            this.ClearMode,
            this.Great1P,
            this.Good1P,
            this.Bad1P,
            this.Great2P,
            this.Good2P,
            this.Bad2P,
            this.Score,
            this.EXScore,
            this.MAXMinus,
            this.Score1P,
            this.Score2P});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvResult.Location = new System.Drawing.Point(12, 43);
            this.DgvResult.Name = "DgvResult";
            this.DgvResult.RowTemplate.Height = 25;
            this.DgvResult.Size = new System.Drawing.Size(911, 413);
            this.DgvResult.TabIndex = 0;
            // 
            // Title
            // 
            this.Title.HeaderText = "曲名";
            this.Title.Name = "Title";
            // 
            // LEVEL
            // 
            this.LEVEL.HeaderText = "LEVEL";
            this.LEVEL.Name = "LEVEL";
            // 
            // ClearMode
            // 
            this.ClearMode.HeaderText = "クリアランプ";
            this.ClearMode.Name = "ClearMode";
            // 
            // Great1P
            // 
            this.Great1P.HeaderText = "良(1P)";
            this.Great1P.Name = "Great1P";
            // 
            // Good1P
            // 
            this.Good1P.HeaderText = "可(1P)";
            this.Good1P.Name = "Good1P";
            // 
            // Bad1P
            // 
            this.Bad1P.HeaderText = "不可(1P)";
            this.Bad1P.Name = "Bad1P";
            // 
            // Great2P
            // 
            this.Great2P.HeaderText = "良(2P)";
            this.Great2P.Name = "Great2P";
            // 
            // Good2P
            // 
            this.Good2P.HeaderText = "可(2P)";
            this.Good2P.Name = "Good2P";
            // 
            // Bad2P
            // 
            this.Bad2P.HeaderText = "不可(2P)";
            this.Bad2P.Name = "Bad2P";
            // 
            // Score
            // 
            this.Score.HeaderText = "総合スコア";
            this.Score.Name = "Score";
            // 
            // EXScore
            // 
            this.EXScore.HeaderText = "EX";
            this.EXScore.Name = "EXScore";
            // 
            // MAXMinus
            // 
            this.MAXMinus.HeaderText = "MAX-";
            this.MAXMinus.Name = "MAXMinus";
            // 
            // Score1P
            // 
            this.Score1P.HeaderText = "スコア(1P)";
            this.Score1P.Name = "Score1P";
            // 
            // Score2P
            // 
            this.Score2P.HeaderText = "スコア(2P)";
            this.Score2P.Name = "Score2P";
            // 
            // BtFolderSetting
            // 
            this.BtFolderSetting.Location = new System.Drawing.Point(12, 12);
            this.BtFolderSetting.Name = "BtFolderSetting";
            this.BtFolderSetting.Size = new System.Drawing.Size(216, 23);
            this.BtFolderSetting.TabIndex = 1;
            this.BtFolderSetting.Text = "読み込むフォルダの設定";
            this.BtFolderSetting.UseVisualStyleBackColor = true;
            this.BtFolderSetting.Click += new System.EventHandler(this.BtFolderSetting_Click);
            // 
            // ResultViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 468);
            this.Controls.Add(this.BtFolderSetting);
            this.Controls.Add(this.DgvResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResultViewer";
            this.Text = "ResultViewer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ResultViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView DgvResult;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn LEVEL;
        private DataGridViewTextBoxColumn ClearMode;
        private DataGridViewTextBoxColumn Great1P;
        private DataGridViewTextBoxColumn Good1P;
        private DataGridViewTextBoxColumn Bad1P;
        private DataGridViewTextBoxColumn Great2P;
        private DataGridViewTextBoxColumn Good2P;
        private DataGridViewTextBoxColumn Bad2P;
        private DataGridViewTextBoxColumn Score;
        private DataGridViewTextBoxColumn EXScore;
        private DataGridViewTextBoxColumn MAXMinus;
        private DataGridViewTextBoxColumn Score1P;
        private DataGridViewTextBoxColumn Score2P;
        private Button BtFolderSetting;
    }
}