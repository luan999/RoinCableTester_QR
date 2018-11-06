using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoinCableTester.Utils {
    public partial class PasswordDialog : Form {
        public PasswordDialog() {
            InitializeComponent();

            TextPassword.Text = "";
            TextPassword.Focus();
        }
    }
}
