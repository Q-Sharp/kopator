using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kopator.Properties;
using Alphaleonis.Win32.Filesystem;
using System.Threading;
using System.Windows.Forms;

namespace kopator
{
    public partial class CatalogControl : UserControl, IKopatorControl
    {
        private KopatorMainForm _parentForm => FindForm() as KopatorMainForm;
        public CatalogControl() =>  InitializeComponent();

        private void CatalogControl_Load(object sender, EventArgs e)
        {
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;

            tbDestinyFile.Text = Settings.Default.CatDestinyFile;
            tbSource.Text = Settings.Default.CatSource;
            tbType.Text = Settings.Default.CatType;
            cbType.Text = Settings.Default.CatExportType;

            cbType.SelectedItem = Settings.Default.CatType;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Settings.Default.CatDestinyFile = tbDestinyFile.Text;
            Settings.Default.CatSource = tbSource.Text;
            Settings.Default.CatType = tbType.Text;
            Settings.Default.CatExportType = cbType.Text;
            Settings.Default.Save();
            base.OnHandleDestroyed(e);
        }

        private void btSourceDotDotDot_Click(object sender, EventArgs e) => _parentForm.HandleFolderBrowser("Quelle auswählen.", false, tbSource);
        private void btCSVDotDotDot_Click(object sender, EventArgs e) => _parentForm.HandleFileBrowser("Quelle auswählen.", "export", cbType.Text, tbDestinyFile);

        public async Task Run()
        {
            var sSourcePath = _parentForm.GetFullPath(tbSource.Text);
            var sCSVPath =_parentForm.GetFullPath(tbDestinyFile.Text);
            var sFileType = tbType.Text;
            Enum.TryParse<CatalogExportType>(cbType.Text, out var ExportType);

            // check for null/empty
            if (string.IsNullOrEmpty(sSourcePath) || string.IsNullOrEmpty(sCSVPath))
            {
                MessageBox.Show($"Bitte {sFileType}- und Quellordner angeben!");
                return;
            }

            // check source
            if (!_parentForm.CheckDirectory(sSourcePath, true, false))
            {
                MessageBox.Show("Keine Leserechte für den Quellordner!");
                return;
            }

            // init process start
            _parentForm.SetProcessInWork(true);
            _parentForm.TokenSource = new CancellationTokenSource();

            var oProgress = new Progress<bool>(x => _parentForm.ProgressBar.PerformStep());

            // get files
            var a_sFiles = Directory.GetFiles(sSourcePath, $"*{(!string.IsNullOrWhiteSpace(sFileType) ? $".{sFileType}" : string.Empty)}", System.IO.SearchOption.AllDirectories);

            // set progressbOutputar
            _parentForm.ProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => DoProcessing(oProgress, sCSVPath, ExportType, a_sFiles), _parentForm.TokenSource.Token).ConfigureAwait(false);

            _parentForm.Invoke(new Action(() => 
            {
                 _parentForm.SetProcessInWork(false);

                 if (_parentForm.TokenSource?.IsCancellationRequested ?? true)
                     MessageBox.Show("Katalogisiervorgang abgebrochen!");
                 else
                 {
                     MessageBox.Show("Katalogisiervorgang abgeschlossen!");
                     _parentForm.Close();
                 }
            }));
        }

        private void DoProcessing(IProgress<bool> oProgress, string filePath, CatalogExportType export, string[] a_sFiles)
        {
            var sbOutput = new StringBuilder();

            if (export == CatalogExportType.CSV)
            {
                string strSeperator = ",";

                sbOutput.AppendLine(string.Join(strSeperator, "Datei", "Datum"));
                foreach (var file in a_sFiles.Select(x => new { file = x, time = File.GetLastWriteTime(x) }).OrderBy(x => x.time))
                {
                    try
                    {
                        if (_parentForm.TokenSource?.IsCancellationRequested ?? true)
                            return;

                        var ct = file.time;
                        var name = Path.GetFileName(file.file);

                        sbOutput.AppendLine(string.Join(strSeperator, name, ct));
                        oProgress?.Report(true);

                        if (_parentForm.TokenSource?.IsCancellationRequested ?? true)
                            return;
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                var ic = new ImageConverter();

                sbOutput.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\">");
                sbOutput.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                sbOutput.AppendLine("<head>");
                sbOutput.AppendLine("<title>Katalog</title>");
                sbOutput.AppendLine("</head>");
                sbOutput.AppendLine("<body>");
                sbOutput.AppendLine("<table style=\"width:100%\">");
                sbOutput.AppendLine("<tr>");
                sbOutput.AppendLine("<th>Datei</th>");
                sbOutput.AppendLine("<th>Datum</th>");
                sbOutput.AppendLine("<th>Vorschau</th>");
                sbOutput.AppendLine("</tr>");

                foreach (var file in a_sFiles.Select(x => new { file = x, time = File.GetLastWriteTime(x) }).OrderBy(x => x.time))
                {
                    try
                    {
                        if (_parentForm.TokenSource?.IsCancellationRequested ?? true)
                            return;

                        sbOutput.AppendLine("<tr>");

                        var ct = file.time;
                        var name = Path.GetFileName(file.file);
                        var addTN = false;
                        string imagestring = null;

                        addTN = file.file.IsImage(out var thumbnail);
                        sbOutput.AppendLine($"<td>{name}</td>");
                        sbOutput.AppendLine($"<td>{ct}</td>");

                        if (addTN)
                        {
                            var buffer = (byte[])ic.ConvertTo(thumbnail, typeof(byte[]));
                            imagestring = Convert.ToBase64String(buffer, Base64FormattingOptions.InsertLineBreaks);
                            sbOutput.AppendLine($"<td><img src=\"data:image/png;base64,{imagestring}\"/></td>");
                        }
                        else
                            sbOutput.AppendLine($"<td></td>");

                        sbOutput.AppendLine("</tr>");
                    }
                    catch
                    {

                    }
                }

                sbOutput.AppendLine("</body>");
                sbOutput.AppendLine("</html>");
            }

             File.WriteAllText(filePath, sbOutput.ToString());
        }
    }
}
