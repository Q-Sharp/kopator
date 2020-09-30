using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kopator.Properties;
using System.IO;
using System.Threading;

namespace kopator
{
    public partial class CatalogControl : UserControl, IkopatorControl
    {
        private kopatorMainForm _parentForm => FindForm() as kopatorMainForm;
        public CatalogControl() =>  InitializeComponent();

        private void CatalogControl_Load(object sender, EventArgs e)
        {
            tbCSV.Text = Settings.Default.CatCSV;
            tbSource.Text = Settings.Default.CatSource;
            tbType.Text = Settings.Default.CatType;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Settings.Default.CatCSV = tbCSV.Text;
            Settings.Default.CatSource = tbSource.Text;
            Settings.Default.CatType = tbType.Text;
            Settings.Default.Save();
            base.OnHandleDestroyed(e);
        }

        private void btSourceDotDotDot_Click(object sender, EventArgs e) => _parentForm.HandleFolderBrowser("Quelle auswählen.", false, tbSource);
        private void btCSVDotDotDot_Click(object sender, EventArgs e) => _parentForm.HandleFileBrowser("Quelle auswählen.", "export", "csv", tbCSV);

        public async Task Run()
        {
            var sSourcePath = _parentForm.GetFullPath(tbSource.Text);
            var sCSVPath =_parentForm.GetFullPath(tbCSV.Text);
            var sFileType = tbType.Text;

            // check for null/empty
            if (string.IsNullOrEmpty(sSourcePath) || string.IsNullOrEmpty(sCSVPath))
            {
                MessageBox.Show("Bitte CSV- und Quellordner angeben!");
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
            var a_sFiles = Directory.GetFiles(sSourcePath, $"*{(!string.IsNullOrWhiteSpace(sFileType) ? $".{sFileType}" : string.Empty)}", SearchOption.AllDirectories);

            // set progressbar
            _parentForm.ProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => DoProcessing(oProgress, sCSVPath, a_sFiles), _parentForm.TokenSource.Token).ConfigureAwait(false);

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

        private void DoProcessing(IProgress<bool> oProgress, string sCSVPath, string[] a_sFiles)
        {
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();

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

            File.WriteAllText(sCSVPath, sbOutput.ToString());
        }
    }
}
