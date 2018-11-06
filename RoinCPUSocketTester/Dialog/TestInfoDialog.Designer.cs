using RoinCableTester.Utils;

namespace RoinCableTester.Dialog {
    partial class TestInfoDialog {
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
            this.txtCustomerName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtProductName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtTestTotal = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtOrderNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOperator = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtValidator = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtTestMachine = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtProductRev = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cbPrinterName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.dtDtpDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.container = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtpDate)).BeginInit();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonCancel.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonCancel.Location = new System.Drawing.Point(439, 211);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(115, 32);
            this.ButtonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonCancel.TabIndex = 32;
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.Visible = false;
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonAccept.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonAccept.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.ButtonAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonAccept.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ButtonAccept.Location = new System.Drawing.Point(297, 211);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(115, 32);
            this.ButtonAccept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ButtonAccept.TabIndex = 31;
            this.ButtonAccept.Text = "確定";
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // LabelX1
            // 
            // 
            // 
            // 
            this.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LabelX1.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LabelX1.Location = new System.Drawing.Point(358, 53);
            this.LabelX1.Name = "LabelX1";
            this.LabelX1.Size = new System.Drawing.Size(143, 35);
            this.LabelX1.TabIndex = 20;
            this.LabelX1.Text = "料號<font color=\'red\'>＊</font>";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtCustomerName.Border.Class = "TextBoxBorder";
            this.txtCustomerName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCustomerName.ButtonCustom.Tooltip = "";
            this.txtCustomerName.ButtonCustom2.Tooltip = "";
            this.txtCustomerName.DisabledBackColor = System.Drawing.Color.White;
            this.txtCustomerName.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCustomerName.ForeColor = System.Drawing.Color.Black;
            this.txtCustomerName.Location = new System.Drawing.Point(497, 53);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.PreventEnterBeep = true;
            this.txtCustomerName.Size = new System.Drawing.Size(187, 35);
            this.txtCustomerName.TabIndex = 16;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX2.Location = new System.Drawing.Point(12, 53);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(155, 35);
            this.labelX2.TabIndex = 21;
            this.labelX2.Text = "派工單號<font color=\'red\'>＊</font>";
            // 
            // txtProductName
            // 
            this.txtProductName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtProductName.Border.Class = "TextBoxBorder";
            this.txtProductName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtProductName.ButtonCustom.Tooltip = "";
            this.txtProductName.ButtonCustom2.Tooltip = "";
            this.txtProductName.DisabledBackColor = System.Drawing.Color.White;
            this.txtProductName.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtProductName.ForeColor = System.Drawing.Color.Black;
            this.txtProductName.Location = new System.Drawing.Point(161, 53);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PreventEnterBeep = true;
            this.txtProductName.Size = new System.Drawing.Size(191, 35);
            this.txtProductName.TabIndex = 17;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX3.Location = new System.Drawing.Point(12, 362);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(155, 35);
            this.labelX3.TabIndex = 23;
            this.labelX3.Text = "測試總數<font color=\'red\'>＊</font>";
            this.labelX3.Visible = false;
            // 
            // txtTestTotal
            // 
            this.txtTestTotal.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTestTotal.Border.Class = "TextBoxBorder";
            this.txtTestTotal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTestTotal.ButtonCustom.Tooltip = "";
            this.txtTestTotal.ButtonCustom2.Tooltip = "";
            this.txtTestTotal.DisabledBackColor = System.Drawing.Color.White;
            this.txtTestTotal.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtTestTotal.ForeColor = System.Drawing.Color.Black;
            this.txtTestTotal.Location = new System.Drawing.Point(161, 362);
            this.txtTestTotal.Name = "txtTestTotal";
            this.txtTestTotal.PreventEnterBeep = true;
            this.txtTestTotal.Size = new System.Drawing.Size(191, 35);
            this.txtTestTotal.TabIndex = 18;
            this.txtTestTotal.Visible = false;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX4.Location = new System.Drawing.Point(358, 403);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(155, 35);
            this.labelX4.TabIndex = 25;
            this.labelX4.Text = "訂單編號";
            this.labelX4.Visible = false;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtOrderNo.Border.Class = "TextBoxBorder";
            this.txtOrderNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOrderNo.ButtonCustom.Tooltip = "";
            this.txtOrderNo.ButtonCustom2.Tooltip = "";
            this.txtOrderNo.DisabledBackColor = System.Drawing.Color.White;
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtOrderNo.ForeColor = System.Drawing.Color.Black;
            this.txtOrderNo.Location = new System.Drawing.Point(497, 403);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.PreventEnterBeep = true;
            this.txtOrderNo.Size = new System.Drawing.Size(187, 35);
            this.txtOrderNo.TabIndex = 26;
            this.txtOrderNo.Visible = false;
            // 
            // txtOperator
            // 
            this.txtOperator.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtOperator.Border.Class = "TextBoxBorder";
            this.txtOperator.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOperator.ButtonCustom.Tooltip = "";
            this.txtOperator.ButtonCustom2.Tooltip = "";
            this.txtOperator.DisabledBackColor = System.Drawing.Color.White;
            this.txtOperator.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtOperator.ForeColor = System.Drawing.Color.Black;
            this.txtOperator.Location = new System.Drawing.Point(497, 12);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.PreventEnterBeep = true;
            this.txtOperator.Size = new System.Drawing.Size(187, 35);
            this.txtOperator.TabIndex = 28;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX5.Location = new System.Drawing.Point(358, 12);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(143, 35);
            this.labelX5.TabIndex = 27;
            this.labelX5.Text = "測試人員<font color=\'red\'>＊</font>";
            // 
            // txtValidator
            // 
            this.txtValidator.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtValidator.Border.Class = "TextBoxBorder";
            this.txtValidator.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtValidator.ButtonCustom.Tooltip = "";
            this.txtValidator.ButtonCustom2.Tooltip = "";
            this.txtValidator.DisabledBackColor = System.Drawing.Color.White;
            this.txtValidator.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtValidator.ForeColor = System.Drawing.Color.Black;
            this.txtValidator.Location = new System.Drawing.Point(161, 403);
            this.txtValidator.Name = "txtValidator";
            this.txtValidator.PreventEnterBeep = true;
            this.txtValidator.Size = new System.Drawing.Size(191, 35);
            this.txtValidator.TabIndex = 30;
            this.txtValidator.Visible = false;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX6.Location = new System.Drawing.Point(12, 403);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(147, 35);
            this.labelX6.TabIndex = 29;
            this.labelX6.Text = "檢准人員";
            this.labelX6.Visible = false;
            // 
            // txtTestMachine
            // 
            this.txtTestMachine.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTestMachine.Border.Class = "TextBoxBorder";
            this.txtTestMachine.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTestMachine.ButtonCustom.Tooltip = "";
            this.txtTestMachine.ButtonCustom2.Tooltip = "";
            this.txtTestMachine.DisabledBackColor = System.Drawing.Color.White;
            this.txtTestMachine.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtTestMachine.ForeColor = System.Drawing.Color.Black;
            this.txtTestMachine.Location = new System.Drawing.Point(161, 12);
            this.txtTestMachine.Name = "txtTestMachine";
            this.txtTestMachine.PreventEnterBeep = true;
            this.txtTestMachine.Size = new System.Drawing.Size(191, 35);
            this.txtTestMachine.TabIndex = 19;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX7.Location = new System.Drawing.Point(12, 12);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(147, 35);
            this.labelX7.TabIndex = 22;
            this.labelX7.Text = "測試機編號<font color=\'red\'>＊</font>";
            // 
            // txtProductRev
            // 
            this.txtProductRev.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtProductRev.Border.Class = "TextBoxBorder";
            this.txtProductRev.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtProductRev.ButtonCustom.Tooltip = "";
            this.txtProductRev.ButtonCustom2.Tooltip = "";
            this.txtProductRev.DisabledBackColor = System.Drawing.Color.White;
            this.txtProductRev.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.txtProductRev.ForeColor = System.Drawing.Color.Black;
            this.txtProductRev.Location = new System.Drawing.Point(161, 321);
            this.txtProductRev.Name = "txtProductRev";
            this.txtProductRev.PreventEnterBeep = true;
            this.txtProductRev.Size = new System.Drawing.Size(191, 35);
            this.txtProductRev.TabIndex = 34;
            this.txtProductRev.Visible = false;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX11.Location = new System.Drawing.Point(12, 321);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(155, 35);
            this.labelX11.TabIndex = 33;
            this.labelX11.Text = "設變";
            this.labelX11.Visible = false;
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX12.Location = new System.Drawing.Point(12, 157);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(151, 35);
            this.labelX12.TabIndex = 35;
            this.labelX12.Text = "印表機名稱<font color=\'red\'>＊</font>";
            // 
            // cbPrinterName
            // 
            this.cbPrinterName.DisplayMember = "Text";
            this.cbPrinterName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPrinterName.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.cbPrinterName.FormattingEnabled = true;
            this.cbPrinterName.ItemHeight = 29;
            this.cbPrinterName.Location = new System.Drawing.Point(161, 157);
            this.cbPrinterName.Name = "cbPrinterName";
            this.cbPrinterName.Size = new System.Drawing.Size(527, 35);
            this.cbPrinterName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPrinterName.TabIndex = 36;
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX13.Location = new System.Drawing.Point(358, 116);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(143, 35);
            this.labelX13.TabIndex = 35;
            this.labelX13.Text = "製造日期<font color=\'red\'>＊</font>";
            // 
            // dtDtpDate
            // 
            // 
            // 
            // 
            this.dtDtpDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtDtpDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDtpDate.ButtonClear.Tooltip = "";
            this.dtDtpDate.ButtonCustom.Tooltip = "";
            this.dtDtpDate.ButtonCustom2.Tooltip = "";
            this.dtDtpDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtDtpDate.ButtonDropDown.Tooltip = "";
            this.dtDtpDate.ButtonDropDown.Visible = true;
            this.dtDtpDate.ButtonFreeText.Tooltip = "";
            this.dtDtpDate.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.dtDtpDate.IsPopupCalendarOpen = false;
            this.dtDtpDate.Location = new System.Drawing.Point(501, 117);
            // 
            // 
            // 
            this.dtDtpDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDtpDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDtpDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtDtpDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtDtpDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDtpDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtDtpDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtDtpDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDtpDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtDtpDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDtpDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtDtpDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtDtpDate.MonthCalendar.TodayButtonVisible = true;
            this.dtDtpDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtDtpDate.Name = "dtDtpDate";
            this.dtDtpDate.Size = new System.Drawing.Size(187, 35);
            this.dtDtpDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtDtpDate.TabIndex = 37;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.labelX10.Location = new System.Drawing.Point(12, 116);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(155, 35);
            this.labelX10.TabIndex = 23;
            this.labelX10.Text = "標籤列印";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.radioButton1.Location = new System.Drawing.Point(21, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 31);
            this.radioButton1.TabIndex = 38;
            this.radioButton1.Text = "啟用";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F);
            this.radioButton2.Location = new System.Drawing.Point(99, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 31);
            this.radioButton2.TabIndex = 38;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "停用";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // container
            // 
            this.container.Controls.Add(this.radioButton1);
            this.container.Controls.Add(this.radioButton2);
            this.container.Location = new System.Drawing.Point(161, 106);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(191, 47);
            this.container.TabIndex = 39;
            this.container.TabStop = false;
            // 
            // TestInfoDialog
            // 
            this.AcceptButton = this.ButtonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(712, 262);
            this.Controls.Add(this.txtTestTotal);
            this.Controls.Add(this.container);
            this.Controls.Add(this.dtDtpDate);
            this.Controls.Add(this.cbPrinterName);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.txtProductRev);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtTestMachine);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.txtValidator);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.txtOperator);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.LabelX1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "測試資料設定";
            ((System.ComponentModel.ISupportInitialize)(this.dtDtpDate)).EndInit();
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevComponents.DotNetBar.ButtonX ButtonCancel;
        internal DevComponents.DotNetBar.ButtonX ButtonAccept;
        internal DevComponents.DotNetBar.LabelX LabelX1;
        public DevComponents.DotNetBar.Controls.TextBoxX txtCustomerName;
        internal DevComponents.DotNetBar.LabelX labelX2;
        public DevComponents.DotNetBar.Controls.TextBoxX txtProductName;
        internal DevComponents.DotNetBar.LabelX labelX3;
        public DevComponents.DotNetBar.Controls.TextBoxX txtTestTotal;
        internal DevComponents.DotNetBar.LabelX labelX4;
        public DevComponents.DotNetBar.Controls.TextBoxX txtOrderNo;
        public DevComponents.DotNetBar.Controls.TextBoxX txtOperator;
        internal DevComponents.DotNetBar.LabelX labelX5;
        public DevComponents.DotNetBar.Controls.TextBoxX txtValidator;
        internal DevComponents.DotNetBar.LabelX labelX6;
        internal DevComponents.DotNetBar.LabelX labelX7;
        public DevComponents.DotNetBar.Controls.TextBoxX txtTestMachine;
        public DevComponents.DotNetBar.Controls.TextBoxX txtProductRev;
        internal DevComponents.DotNetBar.LabelX labelX11;
        internal DevComponents.DotNetBar.LabelX labelX12;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cbPrinterName;
        internal DevComponents.DotNetBar.LabelX labelX13;
        public DevComponents.Editors.DateTimeAdv.DateTimeInput dtDtpDate;
        internal DevComponents.DotNetBar.LabelX labelX10;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.GroupBox container;
    }
}