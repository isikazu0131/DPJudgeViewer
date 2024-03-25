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
    public partial class AutoZuruForm : Form {
        public AutoZuruForm() {
            InitializeComponent();
        }

        private void BtOK_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AutoZuruForm_Load(object sender, EventArgs e) {

        }

        private void AutoZuruForm_Shown(object sender, EventArgs e) {
            System.Media.SystemSounds.Hand.Play();
        }
    }
}
