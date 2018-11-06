using RoinCableTester.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoinCableTester.Dialog {
    public partial class TestInfoDialog : Form {
        /*
        private const int WS_SYSMENU = 0x80000;
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }
        */
        public TestInfoDialog() {
            InitializeComponent();

            this.ButtonCancel.Text = IniFile.IniReadValue("Button", "No");
            this.ButtonAccept.Text = IniFile.IniReadValue("Button", "Yes");
            this.LabelX1.Text = IniFile.IniReadValue("TestInfoDialog", "CustomerName");
            this.labelX2.Text = IniFile.IniReadValue("TestInfoDialog", "ProductName");
            this.labelX3.Text = IniFile.IniReadValue("TestInfoDialog", "TestTotal");
            this.labelX4.Text = IniFile.IniReadValue("TestInfoDialog", "OrderNo");
            this.labelX5.Text = IniFile.IniReadValue("TestInfoDialog", "Operator");
            this.labelX6.Text = IniFile.IniReadValue("TestInfoDialog", "Validator");
            this.labelX7.Text = IniFile.IniReadValue("TestInfoDialog", "TestMachine");
            this.labelX11.Text = IniFile.IniReadValue("TestInfoDialog", "ProductRev");
            this.labelX12.Text = IniFile.IniReadValue("TestInfoDialog", "PrinterName");
            this.labelX13.Text = IniFile.IniReadValue("TestInfoDialog", "DtpDate");
            this.labelX10.Text = IniFile.IniReadValue("TestInfoDialog", "PrinterActive");
            this.radioButton1.Text = IniFile.IniReadValue("Button", "Start");
            this.radioButton2.Text = IniFile.IniReadValue("Button", "Stop");
            this.Text = IniFile.IniReadValue("TestInfoDialog", "SetTestInfo");

            this.txtProductName.Select();
        }

        private void ButtonAccept_Click(object sender, EventArgs e) {
            if (!ValidateInput()) {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(IniFile.IniReadValue("Message", "MustRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private bool ValidateInput() {
            //if (string.IsNullOrWhiteSpace(txtTestMachine.Text)) {
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(txtProductName.Text)) {
                return false; // 派工單號
            }
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text)) {
                return false; // 產品規格
            }
            if (string.IsNullOrWhiteSpace(txtTestTotal.Text)) {
                return false;
            }
            //if (string.IsNullOrWhiteSpace(txtProductRev.Text)) {
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(txtOperator.Text)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(dtDtpDate.Text)) {
                return false;
            }
            return true;
        }
    }
}
