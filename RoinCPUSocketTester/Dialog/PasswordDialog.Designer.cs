namespace RoinCableTester.Utils {
    partial class PasswordDialog {
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
            this.ButtonCancel = new DevComponents.DotNetBar.ButtonX();
            this.ButtonAccept = new DevComponents.DotNetBar.ButtonX();
            this.LabelX1 = new DevComponents.DotNetBar.LabelX();
            this.TextPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonCancel.Location = new System.Drawing.Point(181, 65);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(104, 29);
            this.ButtonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonCancel.TabIndex = 15;
            this.ButtonCancel.Text = IniFile.IniReadValue("Button", "No");
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonAccept.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonAccept.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonAccept.Location = new System.Drawing.Point(63, 66);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(104, 29);
            this.ButtonAccept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonAccept.TabIndex = 14;
            this.ButtonAccept.Text = IniFile.IniReadValue("Button", "Yes");
            // 
            // LabelX1
            // 
            // 
            // 
            // 
            this.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX1.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabelX1.Location = new System.Drawing.Point(12, 16);
            this.LabelX1.Name = "LabelX1";
            this.LabelX1.Size = new System.Drawing.Size(129, 35);
            this.LabelX1.TabIndex = 11;
            this.LabelX1.Text = IniFile.IniReadValue("PasswordDialog", "Password");
            // 
            // TextPassword
            // 
            this.TextPassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.TextPassword.Border.Class = "TextBoxBorder";
            this.TextPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TextPassword.ButtonCustom.Tooltip = "";
            this.TextPassword.ButtonCustom2.Tooltip = "";
            this.TextPassword.DisabledBackColor = System.Drawing.Color.White;
            this.TextPassword.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TextPassword.ForeColor = System.Drawing.Color.Black;
            this.TextPassword.Location = new System.Drawing.Point(147, 16);
            this.TextPassword.Name = "TextPassword";
            this.TextPassword.PasswordChar = '*';
            this.TextPassword.PreventEnterBeep = true;
            this.TextPassword.Size = new System.Drawing.Size(202, 35);
            this.TextPassword.TabIndex = 8;
            this.TextPassword.UseSystemPasswordChar = true;
            // 
            // PasswordDialog
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(367, 110);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.LabelX1);
            this.Controls.Add(this.TextPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.Text = IniFile.IniReadValue("PasswordDialog", "PasswordTitle");
        }

        #endregion

        internal DevComponents.DotNetBar.ButtonX ButtonCancel;
        internal DevComponents.DotNetBar.ButtonX ButtonAccept;
        internal DevComponents.DotNetBar.LabelX LabelX1;
        internal DevComponents.DotNetBar.Controls.TextBoxX TextPassword;
    }
}