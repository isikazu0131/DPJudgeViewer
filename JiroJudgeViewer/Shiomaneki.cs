using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiroJudgeViewer {
    public partial class Shiomaneki : Form {
        public Shiomaneki() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Shiomaneki_Shown(object sender, EventArgs e) {
            this.TopMost = true;
            System.Media.SystemSounds.Hand.Play();
        }
    }
}
