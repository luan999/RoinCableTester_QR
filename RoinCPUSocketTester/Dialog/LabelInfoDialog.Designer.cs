using RoinCableTester.Utils;

namespace RoinCableTester.Dialog {
    partial class LabelInfoDialog {
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
            this.ButtonSave = new DevComponents.DotNetBar.ButtonX();
            this.ButtonLoad = new DevComponents.DotNetBar.ButtonX();
            this.ButtonCancel = new DevComponents.DotNetBar.ButtonX();
            this.ButtonAccept = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtTitle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblSwitch = new DevComponents.DotNetBar.LabelX();
            this.cbSwitch = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtProduct = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtRevision = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtManufacturerCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPart = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtPlantVendor = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtTestedBy = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cbPlantCode = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.cbQRCodeSize = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.container = new System.Windows.Forms.GroupBox();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.txtFont1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtFont2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtFont3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.txtLabelWidth = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLabelHeight = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnSelectFont3 = new DevComponents.DotNetBar.ButtonX();
            this.btnSelectFont2 = new DevComponents.DotNetBar.ButtonX();
            this.btnSelectFont1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.container.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonSave
            // 
            this.ButtonSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonSave.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonSave.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonSave.Location = new System.Drawing.Point(180, 506);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(115, 32);
            this.ButtonSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonSave.TabIndex = 33;
            this.ButtonSave.Text = "儲存";
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonLoad.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonLoad.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonLoad.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonLoad.Location = new System.Drawing.Point(30, 506);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(115, 32);
            this.ButtonLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonLoad.TabIndex = 34;
            this.ButtonLoad.Text = "讀取";
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonCancel.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonCancel.Location = new System.Drawing.Point(480, 506);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(115, 32);
            this.ButtonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonCancel.TabIndex = 32;
            this.ButtonCancel.Text = "取消";
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonAccept.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonAccept.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonAccept.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonAccept.Location = new System.Drawing.Point(330, 506);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(115, 32);
            this.ButtonAccept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonAccept.TabIndex = 31;
            this.ButtonAccept.Text = "確定";
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(16, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(147, 35);
            this.labelX1.TabIndex = 20;
            this.labelX1.Text = "客戶";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTitle.Border.Class = "TextBoxBorder";
            this.txtTitle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTitle.ButtonCustom.Tooltip = "";
            this.txtTitle.ButtonCustom2.Tooltip = "";
            this.txtTitle.DisabledBackColor = System.Drawing.Color.White;
            this.txtTitle.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTitle.Location = new System.Drawing.Point(161, 17);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PreventEnterBeep = true;
            this.txtTitle.Size = new System.Drawing.Size(236, 35);
            this.txtTitle.TabIndex = 16;
            // 
            // lblSwitch
            // 
            // 
            // 
            // 
            this.lblSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSwitch.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSwitch.Location = new System.Drawing.Point(403, 20);
            this.lblSwitch.Name = "lblSwitch";
            this.lblSwitch.Size = new System.Drawing.Size(55, 35);
            this.lblSwitch.TabIndex = 17;
            this.lblSwitch.Text = "標籤";
            // 
            // cbSwitch
            // 
            this.cbSwitch.DisplayMember = "Text";
            this.cbSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSwitch.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.cbSwitch.FormattingEnabled = true;
            this.cbSwitch.ItemHeight = 29;
            this.cbSwitch.Location = new System.Drawing.Point(470, 20);
            this.cbSwitch.Name = "cbSwitch";
            this.cbSwitch.Size = new System.Drawing.Size(127, 35);
            this.cbSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbSwitch.TabIndex = 18;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX2.Location = new System.Drawing.Point(16, 58);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(151, 35);
            this.labelX2.TabIndex = 21;
            this.labelX2.Text = "料號";
            // 
            // txtProduct
            // 
            this.txtProduct.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtProduct.Border.Class = "TextBoxBorder";
            this.txtProduct.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtProduct.ButtonCustom.Tooltip = "";
            this.txtProduct.ButtonCustom2.Tooltip = "";
            this.txtProduct.DisabledBackColor = System.Drawing.Color.White;
            this.txtProduct.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtProduct.ForeColor = System.Drawing.Color.Black;
            this.txtProduct.Location = new System.Drawing.Point(161, 58);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.PreventEnterBeep = true;
            this.txtProduct.ReadOnly = true;
            this.txtProduct.Size = new System.Drawing.Size(236, 35);
            this.txtProduct.TabIndex = 17;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX3.Location = new System.Drawing.Point(403, 58);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(133, 35);
            this.labelX3.TabIndex = 23;
            this.labelX3.Text = "版本號碼";
            // 
            // txtRevision
            // 
            this.txtRevision.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtRevision.Border.Class = "TextBoxBorder";
            this.txtRevision.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRevision.ButtonCustom.Tooltip = "";
            this.txtRevision.ButtonCustom2.Tooltip = "";
            this.txtRevision.DisabledBackColor = System.Drawing.Color.White;
            this.txtRevision.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtRevision.ForeColor = System.Drawing.Color.Black;
            this.txtRevision.Location = new System.Drawing.Point(528, 58);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.PreventEnterBeep = true;
            this.txtRevision.Size = new System.Drawing.Size(69, 35);
            this.txtRevision.TabIndex = 18;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX4.Location = new System.Drawing.Point(16, 99);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(155, 35);
            this.labelX4.TabIndex = 25;
            this.labelX4.Text = "公司代碼";
            // 
            // txtManufacturerCode
            // 
            this.txtManufacturerCode.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtManufacturerCode.Border.Class = "TextBoxBorder";
            this.txtManufacturerCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtManufacturerCode.ButtonCustom.Tooltip = "";
            this.txtManufacturerCode.ButtonCustom2.Tooltip = "";
            this.txtManufacturerCode.DisabledBackColor = System.Drawing.Color.White;
            this.txtManufacturerCode.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtManufacturerCode.ForeColor = System.Drawing.Color.Black;
            this.txtManufacturerCode.Location = new System.Drawing.Point(161, 99);
            this.txtManufacturerCode.Name = "txtManufacturerCode";
            this.txtManufacturerCode.PreventEnterBeep = true;
            this.txtManufacturerCode.Size = new System.Drawing.Size(162, 35);
            this.txtManufacturerCode.TabIndex = 19;
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPart.Border.Class = "TextBoxBorder";
            this.txtPart.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPart.ButtonCustom.Tooltip = "";
            this.txtPart.ButtonCustom2.Tooltip = "";
            this.txtPart.DisabledBackColor = System.Drawing.Color.White;
            this.txtPart.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtPart.ForeColor = System.Drawing.Color.Black;
            this.txtPart.Location = new System.Drawing.Point(161, 140);
            this.txtPart.Name = "txtPart";
            this.txtPart.PreventEnterBeep = true;
            this.txtPart.Size = new System.Drawing.Size(436, 35);
            this.txtPart.TabIndex = 20;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX5.Location = new System.Drawing.Point(16, 140);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(147, 35);
            this.labelX5.TabIndex = 27;
            this.labelX5.Text = "部件名稱";
            // 
            // txtPlantVendor
            // 
            this.txtPlantVendor.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPlantVendor.Border.Class = "TextBoxBorder";
            this.txtPlantVendor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPlantVendor.ButtonCustom.Tooltip = "";
            this.txtPlantVendor.ButtonCustom2.Tooltip = "";
            this.txtPlantVendor.DisabledBackColor = System.Drawing.Color.White;
            this.txtPlantVendor.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtPlantVendor.ForeColor = System.Drawing.Color.Black;
            this.txtPlantVendor.Location = new System.Drawing.Point(161, 181);
            this.txtPlantVendor.Name = "txtPlantVendor";
            this.txtPlantVendor.PreventEnterBeep = true;
            this.txtPlantVendor.Size = new System.Drawing.Size(436, 35);
            this.txtPlantVendor.TabIndex = 21;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX6.Location = new System.Drawing.Point(16, 181);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(147, 35);
            this.labelX6.TabIndex = 29;
            this.labelX6.Text = "工廠/廠商";
            // 
            // txtTestedBy
            // 
            this.txtTestedBy.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTestedBy.Border.Class = "TextBoxBorder";
            this.txtTestedBy.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTestedBy.ButtonCustom.Tooltip = "";
            this.txtTestedBy.ButtonCustom2.Tooltip = "";
            this.txtTestedBy.DisabledBackColor = System.Drawing.Color.White;
            this.txtTestedBy.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtTestedBy.ForeColor = System.Drawing.Color.Black;
            this.txtTestedBy.Location = new System.Drawing.Point(161, 222);
            this.txtTestedBy.Name = "txtTestedBy";
            this.txtTestedBy.PreventEnterBeep = true;
            this.txtTestedBy.Size = new System.Drawing.Size(436, 35);
            this.txtTestedBy.TabIndex = 22;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX7.Location = new System.Drawing.Point(16, 222);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(147, 35);
            this.labelX7.TabIndex = 22;
            this.labelX7.Text = "測試單位";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX8.Location = new System.Drawing.Point(329, 99);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(152, 35);
            this.labelX8.TabIndex = 33;
            this.labelX8.Text = "工廠代碼";
            // 
            // cbPlantCode
            // 
            this.cbPlantCode.DisplayMember = "Text";
            this.cbPlantCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPlantCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPlantCode.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.cbPlantCode.FormattingEnabled = true;
            this.cbPlantCode.ItemHeight = 29;
            this.cbPlantCode.Location = new System.Drawing.Point(476, 99);
            this.cbPlantCode.Name = "cbPlantCode";
            this.cbPlantCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPlantCode.TabIndex = 23;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX9.Location = new System.Drawing.Point(296, 48);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(173, 35);
            this.labelX9.TabIndex = 33;
            this.labelX9.Text = "QRCode尺寸";
            // 
            // cbQRCodeSize
            // 
            this.cbQRCodeSize.DisplayMember = "Text";
            this.cbQRCodeSize.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbQRCodeSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbQRCodeSize.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.cbQRCodeSize.FormattingEnabled = true;
            this.cbQRCodeSize.ItemHeight = 29;
            this.cbQRCodeSize.Location = new System.Drawing.Point(455, 48);
            this.cbQRCodeSize.Name = "cbQRCodeSize";
            this.cbQRCodeSize.Size = new System.Drawing.Size(131, 35);
            this.cbQRCodeSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbQRCodeSize.TabIndex = 36;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX10.Location = new System.Drawing.Point(4, 48);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(151, 35);
            this.labelX10.TabIndex = 40;
            this.labelX10.Text = "列印QRCode";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.radioButton1.Location = new System.Drawing.Point(6, 11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 31);
            this.radioButton1.TabIndex = 38;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "啟用";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.radioButton2.Location = new System.Drawing.Point(72, 11);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 31);
            this.radioButton2.TabIndex = 38;
            this.radioButton2.Text = "停用";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // container
            // 
            this.container.Controls.Add(this.radioButton2);
            this.container.Controls.Add(this.radioButton1);
            this.container.Location = new System.Drawing.Point(146, 39);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(146, 47);
            this.container.TabIndex = 41;
            this.container.TabStop = false;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX11.Location = new System.Drawing.Point(4, 90);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(147, 35);
            this.labelX11.TabIndex = 42;
            this.labelX11.Text = "標籤字型1";
            // 
            // txtFont1
            // 
            this.txtFont1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtFont1.Border.Class = "TextBoxBorder";
            this.txtFont1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFont1.ButtonCustom.Tooltip = "";
            this.txtFont1.ButtonCustom2.Tooltip = "";
            this.txtFont1.DisabledBackColor = System.Drawing.Color.White;
            this.txtFont1.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtFont1.ForeColor = System.Drawing.Color.Black;
            this.txtFont1.Location = new System.Drawing.Point(269, 90);
            this.txtFont1.Name = "txtFont1";
            this.txtFont1.PreventEnterBeep = true;
            this.txtFont1.Size = new System.Drawing.Size(316, 35);
            this.txtFont1.TabIndex = 17;
            // 
            // txtFont2
            // 
            this.txtFont2.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtFont2.Border.Class = "TextBoxBorder";
            this.txtFont2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFont2.ButtonCustom.Tooltip = "";
            this.txtFont2.ButtonCustom2.Tooltip = "";
            this.txtFont2.DisabledBackColor = System.Drawing.Color.White;
            this.txtFont2.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtFont2.ForeColor = System.Drawing.Color.Black;
            this.txtFont2.Location = new System.Drawing.Point(269, 131);
            this.txtFont2.Name = "txtFont2";
            this.txtFont2.PreventEnterBeep = true;
            this.txtFont2.Size = new System.Drawing.Size(316, 35);
            this.txtFont2.TabIndex = 17;
            // 
            // txtFont3
            // 
            this.txtFont3.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtFont3.Border.Class = "TextBoxBorder";
            this.txtFont3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFont3.ButtonCustom.Tooltip = "";
            this.txtFont3.ButtonCustom2.Tooltip = "";
            this.txtFont3.DisabledBackColor = System.Drawing.Color.White;
            this.txtFont3.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtFont3.ForeColor = System.Drawing.Color.Black;
            this.txtFont3.Location = new System.Drawing.Point(269, 172);
            this.txtFont3.Name = "txtFont3";
            this.txtFont3.PreventEnterBeep = true;
            this.txtFont3.Size = new System.Drawing.Size(316, 35);
            this.txtFont3.TabIndex = 17;
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX12.Location = new System.Drawing.Point(4, 131);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(147, 35);
            this.labelX12.TabIndex = 42;
            this.labelX12.Text = "標籤字型2";
            // 
            // labelX17
            // 
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX17.Location = new System.Drawing.Point(4, 172);
            this.labelX17.Name = "labelX12";
            this.labelX17.Size = new System.Drawing.Size(147, 35);
            this.labelX17.TabIndex = 42;
            this.labelX17.Text = "標籤字型3";
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX13.Location = new System.Drawing.Point(4, 8);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(147, 35);
            this.labelX13.TabIndex = 21;
            this.labelX13.Text = "標籤寬度";
            // 
            // labelX14
            // 
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX14.Location = new System.Drawing.Point(302, 8);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(147, 35);
            this.labelX14.TabIndex = 23;
            this.labelX14.Text = "標籤高度";
            // 
            // txtLabelWidth
            // 
            this.txtLabelWidth.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLabelWidth.Border.Class = "TextBoxBorder";
            this.txtLabelWidth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLabelWidth.ButtonCustom.Tooltip = "";
            this.txtLabelWidth.ButtonCustom2.Tooltip = "";
            this.txtLabelWidth.DisabledBackColor = System.Drawing.Color.White;
            this.txtLabelWidth.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtLabelWidth.ForeColor = System.Drawing.Color.Black;
            this.txtLabelWidth.Location = new System.Drawing.Point(149, 8);
            this.txtLabelWidth.Name = "txtLabelWidth";
            this.txtLabelWidth.PreventEnterBeep = true;
            this.txtLabelWidth.Size = new System.Drawing.Size(101, 35);
            this.txtLabelWidth.TabIndex = 17;
            // 
            // txtLabelHeight
            // 
            this.txtLabelHeight.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLabelHeight.Border.Class = "TextBoxBorder";
            this.txtLabelHeight.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLabelHeight.ButtonCustom.Tooltip = "";
            this.txtLabelHeight.ButtonCustom2.Tooltip = "";
            this.txtLabelHeight.DisabledBackColor = System.Drawing.Color.White;
            this.txtLabelHeight.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.txtLabelHeight.ForeColor = System.Drawing.Color.Black;
            this.txtLabelHeight.Location = new System.Drawing.Point(455, 8);
            this.txtLabelHeight.Name = "txtLabelHeight";
            this.txtLabelHeight.PreventEnterBeep = true;
            this.txtLabelHeight.Size = new System.Drawing.Size(85, 35);
            this.txtLabelHeight.TabIndex = 18;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.Color.Transparent;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
            this.panelEx1.Controls.Add(this.btnSelectFont1);
            this.panelEx1.Controls.Add(this.btnSelectFont2);
            this.panelEx1.Controls.Add(this.btnSelectFont3);
            this.panelEx1.Controls.Add(this.cbQRCodeSize);
            this.panelEx1.Controls.Add(this.labelX12);
            this.panelEx1.Controls.Add(this.txtLabelWidth);
            this.panelEx1.Controls.Add(this.labelX16);
            this.panelEx1.Controls.Add(this.labelX15);
            this.panelEx1.Controls.Add(this.labelX13);
            this.panelEx1.Controls.Add(this.labelX14);
            this.panelEx1.Controls.Add(this.labelX17);
            this.panelEx1.Controls.Add(this.txtLabelHeight);
            this.panelEx1.Controls.Add(this.labelX11);
            this.panelEx1.Controls.Add(this.container);
            this.panelEx1.Controls.Add(this.txtFont1);
            this.panelEx1.Controls.Add(this.txtFont2);
            this.panelEx1.Controls.Add(this.txtFont3);
            this.panelEx1.Controls.Add(this.labelX9);
            this.panelEx1.Controls.Add(this.labelX10);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Location = new System.Drawing.Point(12, 263);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(594, 227);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.SeaShell;
            this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.SeaShell;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 43;
            // 
            // btnSelectFont3
            // 
            this.btnSelectFont3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFont3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelectFont3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFont3.Location = new System.Drawing.Point(149, 172);
            this.btnSelectFont3.Name = "btnSelectFont3";
            this.btnSelectFont3.Size = new System.Drawing.Size(114, 35);
            this.btnSelectFont3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelectFont3.TabIndex = 44;
            this.btnSelectFont3.Text = "選擇字型";
            this.btnSelectFont3.Click += new System.EventHandler(this.btnSelectFont3_Click);
            // 
            // btnSelectFont2
            // 
            this.btnSelectFont2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFont2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelectFont2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFont2.Location = new System.Drawing.Point(149, 131);
            this.btnSelectFont2.Name = "btnSelectFont2";
            this.btnSelectFont2.Size = new System.Drawing.Size(114, 35);
            this.btnSelectFont2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelectFont2.TabIndex = 44;
            this.btnSelectFont2.Text = "選擇字型";
            this.btnSelectFont2.Click += new System.EventHandler(this.btnSelectFont2_Click);
            // 
            // btnSelectFont1
            // 
            this.btnSelectFont1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFont1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelectFont1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFont1.Location = new System.Drawing.Point(149, 90);
            this.btnSelectFont1.Name = "btnSelectFont1";
            this.btnSelectFont1.Size = new System.Drawing.Size(114, 35);
            this.btnSelectFont1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelectFont1.TabIndex = 44;
            this.btnSelectFont1.Text = "選擇字型";
            this.btnSelectFont1.Click += new System.EventHandler(this.btnSelectFont1_Click);
            // 
            // labelX16
            // 
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX16.Location = new System.Drawing.Point(541, 9);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(54, 35);
            this.labelX16.TabIndex = 21;
            this.labelX16.Text = "mm";
            // 
            // labelX15
            // 
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.labelX15.Location = new System.Drawing.Point(250, 9);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(49, 35);
            this.labelX15.TabIndex = 21;
            this.labelX15.Text = "mm";
            // 
            // LabelInfoDialog
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(619, 559);
            this.Controls.Add(this.txtTestedBy);
            this.Controls.Add(this.cbPlantCode);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.txtRevision);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.txtPlantVendor);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtPart);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtManufacturerCode);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblSwitch);
            this.Controls.Add(this.cbSwitch);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LabelInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "標籤資料設定";
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.GroupBox container;
        public DevComponents.DotNetBar.ButtonX ButtonSave;
        public DevComponents.DotNetBar.ButtonX ButtonLoad;
        public DevComponents.DotNetBar.ButtonX ButtonCancel;
        public DevComponents.DotNetBar.ButtonX ButtonAccept;
        public DevComponents.DotNetBar.LabelX labelX1;
        public DevComponents.DotNetBar.Controls.TextBoxX txtTitle;
        public DevComponents.DotNetBar.LabelX lblSwitch;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cbSwitch;
        public DevComponents.DotNetBar.LabelX labelX2;
        public DevComponents.DotNetBar.Controls.TextBoxX txtProduct;
        public DevComponents.DotNetBar.LabelX labelX3;
        public DevComponents.DotNetBar.Controls.TextBoxX txtRevision;
        public DevComponents.DotNetBar.LabelX labelX4;
        public DevComponents.DotNetBar.Controls.TextBoxX txtManufacturerCode;
        public DevComponents.DotNetBar.Controls.TextBoxX txtPart;
        public DevComponents.DotNetBar.LabelX labelX5;
        public DevComponents.DotNetBar.Controls.TextBoxX txtPlantVendor;
        public DevComponents.DotNetBar.LabelX labelX6;
        public DevComponents.DotNetBar.LabelX labelX7;
        public DevComponents.DotNetBar.Controls.TextBoxX txtTestedBy;
        public DevComponents.DotNetBar.LabelX labelX8;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cbPlantCode;
        public DevComponents.DotNetBar.LabelX labelX9;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cbQRCodeSize;
        public DevComponents.DotNetBar.LabelX labelX10;
        public DevComponents.DotNetBar.LabelX labelX11;
        public DevComponents.DotNetBar.Controls.TextBoxX txtFont1;
        public DevComponents.DotNetBar.Controls.TextBoxX txtLabelWidth;
        public DevComponents.DotNetBar.Controls.TextBoxX txtFont2;
        public DevComponents.DotNetBar.Controls.TextBoxX txtFont3;
        public DevComponents.DotNetBar.LabelX labelX12;
        public DevComponents.DotNetBar.LabelX labelX13;
        public DevComponents.DotNetBar.LabelX labelX14;
        public DevComponents.DotNetBar.LabelX labelX17;
        public DevComponents.DotNetBar.Controls.TextBoxX txtLabelHeight;
        public DevComponents.DotNetBar.PanelEx panelEx1;
        public DevComponents.DotNetBar.ButtonX btnSelectFont3;
        public DevComponents.DotNetBar.ButtonX btnSelectFont2;
        public DevComponents.DotNetBar.ButtonX btnSelectFont1;
        public DevComponents.DotNetBar.LabelX labelX16;
        public DevComponents.DotNetBar.LabelX labelX15;
    }
}