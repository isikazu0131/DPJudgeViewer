﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiroJudgeViewer {
    public partial class JiroDead : Form {
        public JiroDead() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void JiroDead_Shown(object sender, EventArgs e) {
            System.Media.SystemSounds.Hand.Play();
        }

        private void JiroDead_FormClosed(object sender, FormClosedEventArgs e) {
            this.DialogResult = DialogResult.OK;
        }
    }
}
