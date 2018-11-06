using System.Drawing;
using System.Drawing.Printing;

namespace Utils.LabelPrinting {
    public class AFCSPrinter {
        /*頁面打印委託*/
        public delegate void DoPrintDelegate(Graphics g, ref bool hasMorePage);

        PrintDocument iSPrinter = null;
        bool m_bUseDefaultPaperSetting = false;

        DoPrintDelegate DoPrint = null;

        public AFCSPrinter() {
            iSPrinter = new PrintDocument();
            iSPrinter.PrintPage += new PrintPageEventHandler(this.OnPrintPage);
        }

        public void Dispose() {
            if (iSPrinter != null)
                iSPrinter.Dispose();
            iSPrinter = null;
        }

        /*設置打印機名*/
        public string PrinterName {
            get { return iSPrinter.PrinterSettings.PrinterName; }
            set { iSPrinter.PrinterSettings.PrinterName = value; }
        }

        /*設置打印文檔名*/
        public string DocumentName {
            get { return iSPrinter.DocumentName; }
            set { iSPrinter.DocumentName = value; }
        }

        /*設置是否使用缺省紙張*/
        public bool UseDefaultPaper {
            get { return m_bUseDefaultPaperSetting; }
            set {
                m_bUseDefaultPaperSetting = value;
                if (!m_bUseDefaultPaperSetting) {
                    //如果不適用缺省紙張則創建一個自定義紙張，注意，必須使用這個版本的構造函數才是自定義的紙張
                    PaperSize ps = new PaperSize("Custom Size 1", 827, 1169);
                    //將缺省的紙張設置為新建的自定義紙張
                    iSPrinter.DefaultPageSettings.PaperSize = ps;
                }
            }
        }

        /*紙張邊界*/
        public float MarginsLeft {
            get {
                return (int)(iSPrinter.DefaultPageSettings.Margins.Left / 100f * 25.4f);
            }
            set {
                //注意，只有自定義紙張才能修改該屬性，否則將導致異常
                if (iSPrinter.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPrinter.DefaultPageSettings.Margins.Left = (int)(value / 25.4 * 100);
            }
        }

        /*紙張邊界*/
        public float MarginsRight {
            get {
                return (int)(iSPrinter.DefaultPageSettings.Margins.Right / 100f * 25.4f);
            }
            set {
                //注意，只有自定義紙張才能修改該屬性，否則將導致異常
                if (iSPrinter.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPrinter.DefaultPageSettings.Margins.Right = (int)(value / 25.4 * 100);
            }
        }

        /*紙張邊界*/
        public float MarginsTop {
            get {
                return (int)(iSPrinter.DefaultPageSettings.Margins.Top / 100f * 25.4f);
            }
            set {
                //注意，只有自定義紙張才能修改該屬性，否則將導致異常
                if (iSPrinter.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPrinter.DefaultPageSettings.Margins.Top = (int)(value / 25.4 * 100);
            }
        }

        /*紙張邊界*/
        public float MarginsBottom {
            get {
                return (int)(iSPrinter.DefaultPageSettings.Margins.Bottom / 100f * 25.4f);
            }
            set {
                //注意，只有自定義紙張才能修改該屬性，否則將導致異常
                if (iSPrinter.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPrinter.DefaultPageSettings.Margins.Bottom = (int)(value / 25.4 * 100);
            }
        }

        /*紙張寬度 單位定義為毫米mm*/
        public float PaperWidth {
            get { return iSPrinter.DefaultPageSettings.PaperSize.Width / 100f * 25.4f; }
            set {
                //注意，只有自定義紙張才能修改該屬性，否則將導致異常
                if (iSPrinter.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPrinter.DefaultPageSettings.PaperSize.Width = (int)(value / 25.4 * 100);
            }
        }

        /*紙張高度 單位定義為毫米mm*/
        public float PaperHeight {
            get { return (int)iSPrinter.PrinterSettings.DefaultPageSettings.PaperSize.Height / 100f * 25.4f; }
            set {
                //注意，只有自定義紙張才能修改該屬性，否則將導致異常
                if (iSPrinter.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                    iSPrinter.DefaultPageSettings.PaperSize.Height = (int)(value / 25.4 * 100);
            }
        }

        /*頁面打印*/
        private void OnPrintPage(object sender, PrintPageEventArgs ev) {
            //調用委託繪製打印內容
            if (DoPrint != null) {
                bool bHadMore = false;
                DoPrint(ev.Graphics, ref bHadMore);
                ev.HasMorePages = bHadMore;
            }
        }

        /* 開始打印*/
        public void Print(DoPrintDelegate doPrint) {
            DoPrint = doPrint;
            this.iSPrinter.Print();
        }
    }
}