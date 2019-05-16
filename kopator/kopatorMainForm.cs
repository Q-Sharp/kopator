using kopator.Properties;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kopator
{
    public partial class kopatorMainForm : Form
    {
        private bool bProcessing;
        private CancellationTokenSource oTokenSource = new CancellationTokenSource();

        public kopatorMainForm() => InitializeComponent();

        private void kopatorMainForm_Load(object sender, EventArgs e)
        {
            cbMove.Checked = Settings.Default.Cut;
            tbSource.Text = Settings.Default.Source;
            tbDestiny.Text = Settings.Default.Destiny;
        }

        private void kopatorMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bProcessing)
            {
                e.Cancel = true;
                return;
            }

            Settings.Default.Cut = cbMove.Checked;
            Settings.Default.Source = tbSource.Text;
            Settings.Default.Destiny = tbDestiny.Text;
            Settings.Default.Save();
        }

        private async void btCopy_Click(object sender, EventArgs e)
        {
            try
            {
                oTokenSource = oTokenSource ?? new CancellationTokenSource();

                if (bProcessing)
                {
                    oTokenSource?.Cancel();
                    return;
                }

                var bMove = cbMove.Checked;
                var sSourcePath = Path.GetFullPath(tbSource.Text);
                var sDestinyPath = Path.GetFullPath(tbDestiny.Text);

                // check for null/empty
                if(string.IsNullOrEmpty(sSourcePath) || string.IsNullOrEmpty(sDestinyPath))
                {
                    MessageBox.Show("Bitte Ziel- und Quellordner angeben!");
                    return;
                }

                // check source
                if (!CheckDirectory(sSourcePath, true, bMove))
                {
                    MessageBox.Show("Keine Lese- und/oder Schreibrechte für den Quellordner!");
                    return;
                }

                // check destiny
                if (!CheckDirectory(sDestinyPath, false, true))
                {
                    MessageBox.Show("Keine Schreibrechte für den Zielordner!");
                    return;
                }

                // init process start
                SetProcessInWork(true);
                oTokenSource = new CancellationTokenSource();

                var oProgress = new Progress<bool>(x => oProgressBar.PerformStep());

                // get files
                var a_sFiles = Directory.GetFiles(sSourcePath);

                // set progressbar
                oProgressBar.Maximum = a_sFiles.Count();

                await Task.Run(() => DoProcessing(oProgress, bMove, sSourcePath, sDestinyPath, a_sFiles), oTokenSource.Token);
                SetProcessInWork(false);

                if (oTokenSource?.IsCancellationRequested ?? true)
                    MessageBox.Show("Kopiervorgang abgebrochen!");
                else
                {
                    MessageBox.Show("Kopiervorgang abgeschlossen!");
                    Close();
                }
            }
            catch (Exception oException)
            {
                MessageBox.Show(oException.ToString());
            }
            finally
            {
                oProgressBar.ResetText();
                oTokenSource?.Dispose();
            }
        }

        private void btClose_Click(object sender, EventArgs e) => Close();
        private void cbMove_CheckedChanged(object sender, EventArgs e) => btCopy.Text = GetBtCopyText();

        private void btSourceDotDotDot_Click(object sender, EventArgs e)
        {
            oFolderBrowserDialog.Description = "Quelle auswählen.";
            oFolderBrowserDialog.ShowNewFolderButton = false;
            oFolderBrowserDialog.SelectedPath = tbSource.Text;

            if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                tbSource.Text = oFolderBrowserDialog.SelectedPath;
        }

        private void btDestinyDotDotDot_Click(object sender, EventArgs e)
        {
            oFolderBrowserDialog.Description = "Ziel auswählen.";
            oFolderBrowserDialog.ShowNewFolderButton = true;
            oFolderBrowserDialog.SelectedPath = tbDestiny.Text;

            if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                tbDestiny.Text = oFolderBrowserDialog.SelectedPath;
        }

        private void SetProcessInWork(bool bProcessing)
        {
            this.bProcessing = bProcessing;

            btClose.Enabled = !bProcessing;
            //btCopy.Enabled = !bProcessing;
            btCopy.Text = bProcessing ? "Stop" : GetBtCopyText();
            btSourceDotDotDot.Enabled = !bProcessing;
            btDestinyDotDotDot.Enabled = !bProcessing;
            tbSource.Enabled = !bProcessing;
            tbDestiny.Enabled = !bProcessing;
            cbMove.Enabled = !bProcessing;

            oProgressBar.Enabled = bProcessing;
        }

        private string GetBtCopyText() => cbMove.Checked ? "Verschieben" : "Kopieren";

        public bool CheckDirectory(string sDirPath, bool bReadable, bool bWritable)
        {
            try
            {
                if (bWritable)
                {
                    using (var fs = File.Create(Path.Combine(sDirPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                    {
                    }
                    return true;
                }

                if(bReadable)
                    Directory.GetFiles(sDirPath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DoProcessing(IProgress<bool> oProgress, bool bCut, string sSourcePath, string sDestinyPath, string[] a_sFiles)
        {
            foreach (var sFile in a_sFiles)
            {
                try
                {
                    if (oTokenSource?.IsCancellationRequested ?? true)
                        return;

                    var sNewPath = Path.Combine(sDestinyPath, Path.GetFileName(sFile));

                    if (bCut)
                    {
                        if (File.Exists(sNewPath))
                            File.Delete(sNewPath);

                        File.Move(sFile, sNewPath);
                    } 
                    else
                        File.Copy(sFile, sNewPath, true);

                    oProgress?.Report(true);

                    if (oTokenSource?.IsCancellationRequested ?? true)
                        return;
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
