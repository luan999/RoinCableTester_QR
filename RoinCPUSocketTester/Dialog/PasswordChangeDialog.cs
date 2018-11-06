using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoinCableTester.Utils {
    public partial class PasswordChangeDialog : Form {
        public PasswordChangeDialog() {
            InitializeComponent();

            TextOldPassword.Text = "";
            TextNewPassword.Text = "";
            TextRePassword.Text = "";
            TextOldPassword.Focus();
        }

        private void ButtonAccept_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.None;
            if (string.IsNullOrWhiteSpace(TextOldPassword.Text) || string.IsNullOrWhiteSpace(TextNewPassword.Text) || string.IsNullOrWhiteSpace(TextRePassword.Text)) {
                MessageBox.Show(IniFile.IniReadValue("Message", "MustRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (TextNewPassword.Text.Length < 8) {
                MessageBox.Show(IniFile.IniReadValue("Message", "NewPasswordLengthError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (TextNewPassword.Text != TextRePassword.Text) {
                MessageBox.Show(IniFile.IniReadValue("Message", "NewPasswordConfirmError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
