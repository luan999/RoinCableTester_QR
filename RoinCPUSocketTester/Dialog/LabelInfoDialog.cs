using DevComponents.DotNetBar;
using RoinCableTester.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoinCableTester.Dialog {
    public partial class LabelInfoDialog : Form {
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
        public LabelInfoDialog() {
            InitializeComponent();

            this.ButtonCancel.Text = IniFile.IniReadValue("Button", "No");
            this.ButtonAccept.Text = IniFile.IniReadValue("Button", "Yes");
            this.labelX1.Text = IniFile.IniReadValue("LabelInfoDialog", "Title");
            this.labelX2.Text = IniFile.IniReadValue("LabelInfoDialog", "Product");
            this.labelX3.Text = IniFile.IniReadValue("LabelInfoDialog", "Revision");
            this.labelX4.Text = IniFile.IniReadValue("LabelInfoDialog", "ManufacturerCode");
            this.labelX8.Text = IniFile.IniReadValue("LabelInfoDialog", "PlantCode");
            this.cbPlantCode.Items.Add(new ComboBoxItem("SZ", "SZ"));
            this.cbPlantCode.Items.Add(new ComboBoxItem("KS", "KS"));
            this.labelX5.Text = IniFile.IniReadValue("LabelInfoDialog", "Part");
            this.labelX6.Text = IniFile.IniReadValue("LabelInfoDialog", "PlantVendor");
            this.labelX7.Text = IniFile.IniReadValue("LabelInfoDialog", "TestedBy");
            this.labelX9.Text = IniFile.IniReadValue("LabelInfoDialog", "QRCodeSize");
            string qrCodeInfo = "";
            for (int i = 0; !string.IsNullOrEmpty(qrCodeInfo = Util.GetProperty("QRCodeInfo" + i)); i++) {
                this.cbQRCodeSize.Items.Add(new ComboBoxItem("S"+i, qrCodeInfo.Split(',')[0]));
            }
            this.labelX10.Text = IniFile.IniReadValue("LabelInfoDialog", "PrintQRCode");
            this.radioButton1.Text = IniFile.IniReadValue("Button", "Start");
            this.radioButton2.Text = IniFile.IniReadValue("Button", "Stop");
            this.labelX11.Text = IniFile.IniReadValue("LabelInfoDialog", "LabelFont1");
            this.labelX12.Text = IniFile.IniReadValue("LabelInfoDialog", "LabelFont2");
            this.labelX13.Text = IniFile.IniReadValue("LabelInfoDialog", "LabelWidth");
            this.labelX14.Text = IniFile.IniReadValue("LabelInfoDialog", "LabelHeight");
            this.btnSelectFont1.Text = IniFile.IniReadValue("LabelInfoDialog", "FontSelect");
            this.btnSelectFont2.Text = IniFile.IniReadValue("LabelInfoDialog", "FontSelect");
            this.btnSelectFont3.Text = IniFile.IniReadValue("LabelInfoDialog", "FontSelect");
            this.Text = IniFile.IniReadValue("LabelInfoDialog", "SetLabelInfo");

            this.lblSwitch.Text = IniFile.IniReadValue("LabelInfoDialog", "LabelFormat");
            this.cbSwitch.Items.Add(new ComboBoxItem("0", IniFile.IniReadValue("LabelInfoDialog", "LabelFormat1")));
            this.cbSwitch.Items.Add(new ComboBoxItem("1", IniFile.IniReadValue("LabelInfoDialog", "LabelFormat2")));
            //this.cbSwitch.Items.Add(new ComboBoxItem("2", IniFile.IniReadValue("LabelInfoDialog", "LabelFormat3")));
            this.cbSwitch.SelectedIndexChanged += new System.EventHandler(cbSwitch_SelectedIndexChanged);

            this.txtTitle.Focus();
        }

        private void cbSwitch_SelectedIndexChanged(object sender, EventArgs e) {
            switch (this.cbSwitch.SelectedIndex) {
                case 0:
                    this.txtTitle.Enabled = true;
                    this.txtPlantVendor.Enabled = true;
                    this.txtRevision.Enabled = true;
                    this.txtTestedBy.Enabled = true;
                    this.txtLabelWidth.Text = "80";
                    this.txtLabelHeight.Text = "25";
                    this.txtFont1.Text = "Arial,8,Regular";
                    this.txtFont2.Text = "Arial,8,Regular";
                    break;
                case 1:
                    this.txtTitle.Text = ""; this.txtTitle.Enabled = false;
                    this.txtPlantVendor.Text = ""; this.txtPlantVendor.Enabled = false;
                    this.txtRevision.Text = ""; this.txtRevision.Enabled = false;
                    this.txtTestedBy.Text = ""; this.txtTestedBy.Enabled = false;
                    this.txtLabelWidth.Text = "110";
                    this.txtLabelHeight.Text = "25";
                    this.txtFont1.Text = "Arial Narrow,6,Bold";
                    this.txtFont2.Text = "Arial Narrow,8,Bold";
                    break;
                case 2:
                    this.txtTitle.Text = ""; this.txtTitle.Enabled = false;
                    this.txtPlantVendor.Text = ""; this.txtPlantVendor.Enabled = false;
                    this.txtRevision.Text = ""; this.txtRevision.Enabled = false;
                    this.txtTestedBy.Text = ""; this.txtTestedBy.Enabled = false;
                    this.txtLabelWidth.Text = "51";
                    this.txtLabelHeight.Text = "51";
                    this.txtFont1.Text = "Arial Narrow,6,Bold";
                    this.txtFont2.Text = "Arial Narrow,8,Bold";
                    break;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e) {
            if (!ValidateInput()) {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(IniFile.IniReadValue("Message", "MustRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } else {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = this.txtProduct.Text + ".txt";
                savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (savefile.ShowDialog() == DialogResult.OK) {
                    using (StreamWriter sw = new StreamWriter(savefile.FileName)) {
                        sw.WriteLine(this.txtTitle.Text);
                        sw.WriteLine(this.txtProduct.Text);
                        sw.WriteLine(this.txtRevision.Text);
                        sw.WriteLine(this.txtManufacturerCode.Text);
                        sw.WriteLine(this.cbPlantCode.SelectedValue);
                        sw.WriteLine(this.txtPart.Text);
                        sw.WriteLine(this.txtPlantVendor.Text);
                        sw.WriteLine(this.txtTestedBy.Text);
                        sw.WriteLine(this.cbSwitch.SelectedIndex);
                        sw.WriteLine(this.txtLabelWidth.Text);
                        sw.WriteLine(this.txtLabelHeight.Text);
                        sw.WriteLine(this.txtFont1.Text);
                        sw.WriteLine(this.txtFont2.Text);
                    }
                }
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.DefaultExt = "txt";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            if (dialog.ShowDialog() == DialogResult.OK) {
                string[] lines = File.ReadAllLines(dialog.FileName);
                this.txtTitle.Text = lines[0];
                this.txtProduct.Text = lines[1];
                this.txtRevision.Text = lines[2];
                this.txtManufacturerCode.Text = lines[3];
                this.cbPlantCode.SelectedValue = lines[4];
                this.txtPart.Text = lines[5];
                this.txtPlantVendor.Text = lines[6];
                this.txtTestedBy.Text = lines[7];
                this.cbSwitch.SelectedIndex = Convert.ToInt16(lines[8]);
                this.txtLabelWidth.Text = lines[9];
                this.txtLabelHeight.Text = lines[10];
                this.txtFont1.Text = lines[11];
                this.txtFont2.Text = lines[12];
            }
        }

        private void ButtonAccept_Click(object sender, EventArgs e) {
            if (!ValidateInput()) {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(IniFile.IniReadValue("Message", "MustRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        //TRIM MARNESS,CP INLET,M3
        private bool ValidateInput() {
            if (this.cbSwitch.SelectedIndex == 0 && string.IsNullOrWhiteSpace(this.txtTitle.Text)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtProduct.Text)) {
                return false;
            }
            if (this.cbSwitch.SelectedIndex == 0 && string.IsNullOrWhiteSpace(this.txtRevision.Text)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtManufacturerCode.Text)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtPart.Text)) {
                return false;
            }
            if (this.cbSwitch.SelectedIndex == 0 && string.IsNullOrWhiteSpace(this.txtPlantVendor.Text)) {
                return false;
            }
            if (this.cbSwitch.SelectedIndex == 0 && string.IsNullOrWhiteSpace(this.txtTestedBy.Text)) {
                return false;
            }
            return true;
        }

        private void btnSelectFont1_Click(object sender, EventArgs e) {
            string font = GetFont(this.txtFont1.Text);
            if (font != null) {
                this.txtFont1.Text = font;
            }
        }

        private void btnSelectFont2_Click(object sender, EventArgs e) {
            string font = GetFont(this.txtFont2.Text);
            if (font != null) {
                this.txtFont2.Text = font;
            }
        }

        private void btnSelectFont3_Click(object sender, EventArgs e) {
            string font = GetFont(this.txtFont3.Text);
            if (font != null) {
                this.txtFont3.Text = font;
            }
        }

        private string GetFont(string font) {
            FontDialog fd = new FontDialog();
            fd.AllowVerticalFonts = false;
            fd.AllowVectorFonts = true;
            fd.ShowColor = false;
            fd.ShowEffects = false;
            fd.AllowScriptChange = true;
            if (!string.IsNullOrEmpty(font)) {
                var f = font.Split(',');
                fd.Font = new Font(f[0], Convert.ToSingle(f[1]), f[2] == "Regular" ? FontStyle.Regular : FontStyle.Bold );
            }
            if (fd.ShowDialog() == DialogResult.OK) {
                return fd.Font.Name + "," + Convert.ToInt32(Math.Round(fd.Font.Size)) + "," + fd.Font.Style.ToString();
            }
            return null;
        }
    }
}
