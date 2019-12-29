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
        public kopatorMode Mode { get; set; }

        private void kopatorMainForm_Load(object oSender, EventArgs oArgs)
        {
            cbMove.Checked = Settings.Default.Cut;
            tbSource.Text = Settings.Default.Source;
            tbDestiny.Text = Settings.Default.Destiny;
            tbCollect.Text = Settings.Default.Collect;
            tbIgnore.Text = Settings.Default.Ignore;
            oTabControl.SelectTab(Settings.Default.TabPage);
        }

        private void kopatorMainForm_FormClosing(object oSender, FormClosingEventArgs oArgs)
        {
            if (bProcessing)
            {
                oArgs.Cancel = true;
                return;
            }

            Settings.Default.Cut = cbMove.Checked;
            Settings.Default.Source = tbSource.Text;
            Settings.Default.Destiny = tbDestiny.Text;
            Settings.Default.Collect = tbCollect.Text;
            Settings.Default.Ignore = tbIgnore.Text;
            Settings.Default.TabPage = oTabControl.SelectedIndex;
            Settings.Default.Save();

            oTokenSource?.Dispose();
        }

        private async void btCopy_Click(object oSender, EventArgs oArgs)
        {
            try
            {
                oTokenSource = oTokenSource ?? new CancellationTokenSource();

                if (bProcessing)
                {
                    oTokenSource?.Cancel();
                    return;
                }

                switch(Mode)
                {
                    case kopatorMode.Copy:
                        await Copy().ConfigureAwait(true);
                        break;

                    case kopatorMode.Collect:
                        await Collect().ConfigureAwait(true);
                        break;
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

        private async Task Copy()
        {
            var bMove = cbMove.Checked;
            var sSourcePath = Path.GetFullPath(tbSource.Text);
            var sDestinyPath = Path.GetFullPath(tbDestiny.Text);

            // check for null/empty
            if (string.IsNullOrEmpty(sSourcePath) || string.IsNullOrEmpty(sDestinyPath))
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

            // create directory if it is not existing
            if (!Directory.Exists(sDestinyPath))
                Directory.CreateDirectory(sDestinyPath);

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
            oProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => DoProcessing(oProgress, bMove, sDestinyPath, a_sFiles), oTokenSource.Token).ConfigureAwait(false);
            SetProcessInWork(false);

            if (oTokenSource?.IsCancellationRequested ?? true)
                MessageBox.Show("Kopiervorgang abgebrochen!");
            else
            {
                MessageBox.Show("Kopiervorgang abgeschlossen!");
                Close();
            }
        }

        private async Task Collect()
        {
            var sPath = Path.GetFullPath(tbCollect.Text);

            // check for null/empty
            if (string.IsNullOrEmpty(sPath))
            {
                MessageBox.Show("Bitte Ziel- und Quellordner angeben!");
                return;
            }

            // check path
            if (!CheckDirectory(sPath, true, true))
            {
                MessageBox.Show("Keine Lese- und/oder Schreibrechte für den Quellordner!");
                return;
            }

            // init process start
            SetProcessInWork(true);
            oTokenSource = new CancellationTokenSource();

            var oProgress = new Progress<bool>(x => oProgressBar.PerformStep());

            // get sub dirs
            var a_sDirs = Directory.GetDirectories(sPath);
            var a_sFilesNotIncluded = tbIgnore.Text.Split(',').Select(t => t.Trim(' ')).Select(t => Directory.GetFiles(sPath, t, System.IO.SearchOption.AllDirectories)).SelectMany(s => s).ToArray();
            var a_sAllFiles = Directory.GetFiles(sPath, "*", System.IO.SearchOption.AllDirectories);
            var a_sFiles = a_sAllFiles.Except(a_sFilesNotIncluded).ToArray();

            // set progressbar
            oProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => DoProcessing(oProgress, true, sPath, a_sFiles), oTokenSource.Token).ConfigureAwait(true);
            await Task.Run(() => a_sDirs.ToList().ForEach(d => DeleteDirectory(d))).ConfigureAwait(true);
            SetProcessInWork(false);

            if (oTokenSource?.IsCancellationRequested ?? true)
                MessageBox.Show("Sammelvorgang abgebrochen!");
            else
            {
                MessageBox.Show("Sammelvorgang abgeschlossen!");
                Close();
            }
        }

        
        private void btClose_Click(object oSender, EventArgs oArgs) => Close();
        private void cbMove_CheckedChanged(object oSender, EventArgs oArgs) => btCopy.Text = GetBtCopyText();

        private void btDestinyDotDotDot_Click(object oSender, EventArgs oArgs) => HandleFolderBrowser("Quelle auswählen.", false, tbDestiny);
        private void btSourceDotDotDot_Click(object oSender, EventArgs oArgs) => HandleFolderBrowser("Ziel auswählen.", true, tbSource);
        private void btCollectDotDotDot_Click(object oSender, EventArgs oArgs) => HandleFolderBrowser("Sammelziel auswählen.", false, tbCollect);

        private void HandleFolderBrowser(string sDescription, bool bShowNewFolderButton, TextBox oDestinyBox)
        {
            oFolderBrowserDialog.Description = sDescription;
            oFolderBrowserDialog.ShowNewFolderButton = bShowNewFolderButton;
            oFolderBrowserDialog.SelectedPath = tbDestiny.Text;

            if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                oDestinyBox.Text = oFolderBrowserDialog.SelectedPath;
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

        private string GetBtCopyText() => Mode == kopatorMode.Collect ? "Sammeln" : cbMove.Checked ? "Verschieben" : "Kopieren";

        public static bool CheckDirectory(string sDirPath, bool bReadable, bool bWritable)
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

        private void DoProcessing(IProgress<bool> oProgress, bool bCut, string sDestinyPath, string[] a_sFiles)
        {
            foreach (var sFile in a_sFiles)
            {
                try
                {
                    if (oTokenSource?.IsCancellationRequested ?? true)
                        return;

                    var sOldPath = Path.Combine(Path.GetDirectoryName(sFile), Path.GetFileName(sFile));
                    var sNewPath = Path.Combine(sDestinyPath, Path.GetFileName(sFile));

                    if (sOldPath == sNewPath)
                        continue;

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
                catch
                {

                }
            }
        }

        private void DeleteDirectory(string sRoot)
        {
            foreach (var sDir in Directory.GetDirectories(sRoot))
                DeleteDirectory(sDir);

            try
            {
                Directory.Delete(sRoot, true);
            }
            catch (IOException)
            {
                Directory.Delete(sRoot, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(sRoot, true);
            }
        }

        private void oTabControl_Selected(object ooSender, TabControlEventArgs oArgs)
        {
            Mode = (kopatorMode)oArgs.TabPageIndex;
            cbMove.Enabled = Mode == kopatorMode.Copy;
            btCopy.Text = GetBtCopyText();
        }
    }
}
