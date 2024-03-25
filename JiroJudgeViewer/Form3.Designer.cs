namespace JiroJudgeViewer {
    partial class AutoZuruForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.BtOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 96F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(42, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(828, 1408);
            this.label1.TabIndex = 0;
            this.label1.Text = "オートをつけ\r\nるのはズ\r\nルだろうが！\r\n！！！\r\n！！！！\r\n！！！！\r\n！！！\r\n！\r\n！\r\n\r\n！";
            // 
            // BtOK
            // 
            this.BtOK.Font = new System.Drawing.Font("ＭＳ ゴシック", 144F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BtOK.Location = new System.Drawing.Point(432, 338);
            this.BtOK.Name = "BtOK";
            this.BtOK.Size = new System.Drawing.Size(496, 394);
            this.BtOK.TabIndex = 1;
            this.BtOK.Text = "はい";
            this.BtOK.UseVisualStyleBackColor = true;
            this.BtOK.Click += new System.EventHandler(this.BtOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::JiroJudgeViewer.Properties.Resources.ng;
            this.pictureBox1.Location = new System.Drawing.Point(12, 353);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(271, 329);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::JiroJudgeViewer.Properties.Resources.kusa_diie;
            this.pictureBox2.Location = new System.Drawing.Point(691, 207);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(74, 56);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(675, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "＼ﾅﾝﾃｺｯﾀｲ／";
            // 
            // AutoZuruForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 582);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtOK);
            this.Controls.Add(this.label1);
            this.Name = "AutoZuruForm";
            this.Text = "何やってんだお前ェ！bot";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AutoZuruForm_Load);
            this.Shown += new System.EventHandler(this.AutoZuruForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button BtOK;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label2;
    }
}