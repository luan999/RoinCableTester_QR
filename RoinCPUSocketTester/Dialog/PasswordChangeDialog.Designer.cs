namespace RoinCableTester.Utils {
    partial class PasswordChangeDialog {
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
            this.LabelX3 = new DevComponents.DotNetBar.LabelX();
            this.LabelX2 = new DevComponents.DotNetBar.LabelX();
            this.LabelX1 = new DevComponents.DotNetBar.LabelX();
            this.TextRePassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.TextNewPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.TextOldPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonCancel.Location = new System.Drawing.Point(179, 150);
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
            this.ButtonAccept.Location = new System.Drawing.Point(61, 151);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(104, 29);
            this.ButtonAccept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonAccept.TabIndex = 14;
            this.ButtonAccept.Text = IniFile.IniReadValue("Button", "Yes");
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // LabelX3
            // 
            // 
            // 
            // 
            this.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX3.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabelX3.Location = new System.Drawing.Point(12, 100);
            this.LabelX3.Name = "LabelX3";
            this.LabelX3.Size = new System.Drawing.Size(129, 33);
            this.LabelX3.TabIndex = 13;
            this.LabelX3.Text = IniFile.IniReadValue("PasswordChangeDialog", "RePassword");
            // 
            // LabelX2
            // 
            // 
            // 
            // 
            this.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX2.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabelX2.Location = new System.Drawing.Point(12, 58);
            this.LabelX2.Name = "LabelX2";
            this.LabelX2.Size = new System.Drawing.Size(129, 34);
            this.LabelX2.TabIndex = 12;
            this.LabelX2.Text = IniFile.IniReadValue("PasswordChangeDialog", "NewPassword");
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
            this.LabelX1.Text = IniFile.IniReadValue("PasswordChangeDialog", "OldPassword");
            // 
            // TextRePassword
            // 
            this.TextRePassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.TextRePassword.Border.Class = "TextBoxBorder";
            this.TextRePassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TextRePassword.ButtonCustom.Tooltip = "";
            this.TextRePassword.ButtonCustom2.Tooltip = "";
            this.TextRePassword.DisabledBackColor = System.Drawing.Color.White;
            this.TextRePassword.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TextRePassword.ForeColor = System.Drawing.Color.Black;
            this.TextRePassword.Location = new System.Drawing.Point(147, 98);
            this.TextRePassword.Name = "TextRePassword";
            this.TextRePassword.PasswordChar = '*';
            this.TextRePassword.PreventEnterBeep = true;
            this.TextRePassword.Size = new System.Drawing.Size(202, 35);
            this.TextRePassword.TabIndex = 10;
            this.TextRePassword.UseSystemPasswordChar = true;
            // 
            // TextNewPassword
            // 
            this.TextNewPassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.TextNewPassword.Border.Class = "TextBoxBorder";
            this.TextNewPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TextNewPassword.ButtonCustom.Tooltip = "";
            this.TextNewPassword.ButtonCustom2.Tooltip = "";
            this.TextNewPassword.DisabledBackColor = System.Drawing.Color.White;
            this.TextNewPassword.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TextNewPassword.ForeColor = System.Drawing.Color.Black;
            this.TextNewPassword.Location = new System.Drawing.Point(147, 57);
            this.TextNewPassword.Name = "TextNewPassword";
            this.TextNewPassword.PasswordChar = '*';
            this.TextNewPassword.PreventEnterBeep = true;
            this.TextNewPassword.Size = new System.Drawing.Size(202, 35);
            this.TextNewPassword.TabIndex = 9;
            this.TextNewPassword.UseSystemPasswordChar = true;
            // 
            // TextOldPassword
            // 
            this.TextOldPassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.TextOldPassword.Border.Class = "TextBoxBorder";
            this.TextOldPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TextOldPassword.ButtonCustom.Tooltip = "";
            this.TextOldPassword.ButtonCustom2.Tooltip = "";
            this.TextOldPassword.DisabledBackColor = System.Drawing.Color.White;
            this.TextOldPassword.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TextOldPassword.ForeColor = System.Drawing.Color.Black;
            this.TextOldPassword.Location = new System.Drawing.Point(147, 16);
            this.TextOldPassword.Name = "TextOldPassword";
            this.TextOldPassword.PasswordChar = '*';
            this.TextOldPassword.PreventEnterBeep = true;
            this.TextOldPassword.Size = new System.Drawing.Size(202, 35);
            this.TextOldPassword.TabIndex = 8;
            this.TextOldPassword.UseSystemPasswordChar = true;
            // 
            // PasswordChangeDialog
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(367, 195);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.LabelX3);
            this.Controls.Add(this.LabelX2);
            this.Controls.Add(this.LabelX1);
            this.Controls.Add(this.TextRePassword);
            this.Controls.Add(this.TextNewPassword);
            this.Controls.Add(this.TextOldPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordChangeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = IniFile.IniReadValue("PasswordChangeDialog", "PasswordChange");
            this.ResumeLayout(false);

        }

        #endregion

        internal DevComponents.DotNetBar.ButtonX ButtonCancel;
        internal DevComponents.DotNetBar.ButtonX ButtonAccept;
        internal DevComponents.DotNetBar.LabelX LabelX3;
        internal DevComponents.DotNetBar.LabelX LabelX2;
        internal DevComponents.DotNetBar.LabelX LabelX1;
        internal DevComponents.DotNetBar.Controls.TextBoxX TextRePassword;
        internal DevComponents.DotNetBar.Controls.TextBoxX TextNewPassword;
        internal DevComponents.DotNetBar.Controls.TextBoxX TextOldPassword;
    }
}