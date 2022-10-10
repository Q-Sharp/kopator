using kopator.Properties;
using System;
using Alphaleonis.Win32.Filesystem;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace kopator
{
    public partial class KopatorMainForm : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool DeleteFile(string path);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool RemoveDirectory(string path);


        private IKopatorControl _currentControl => oTabControl?.SelectedTab?.Controls?.OfType<IKopatorControl>()?.FirstOrDefault();
        public bool IsProcessing { get; set; }
        public CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public KopatorMainForm() => InitializeComponent();
        public KopatorMode Mode { get; set; }

        private void kopatorMainForm_Load(object oSender, EventArgs oArgs)
        {
            cbMove.Checked = Settings.Default.Cut;
            oTabControl.SelectTab(Settings.Default.TabPage);
            SetTabPageControl(Settings.Default.TabPage);
        }

        private void kopatorMainForm_FormClosing(object oSender, FormClosingEventArgs oArgs)
        {
            if (IsProcessing)
            {
                oArgs.Cancel = true;
                return;
            }

            Settings.Default.Cut = cbMove.Checked;
            Settings.Default.TabPage = oTabControl.SelectedIndex;
            Settings.Default.Save();
            
            TokenSource?.Dispose();
        }

        internal void HandleFileBrowser(string sTitle, string fileName, string defaultExtension, TextBox oDestiny)
        {
            oSaveFileDialog.Title = sTitle;
            oSaveFileDialog.FileName = fileName;

            if(!string.IsNullOrWhiteSpace(oDestiny.Text))
                oSaveFileDialog.InitialDirectory = Path.GetDirectoryName(oDestiny.Text);
            
            oSaveFileDialog.DefaultExt = defaultExtension;
            oSaveFileDialog.Filter = $"*.{defaultExtension.ToLower()}|";

            if (oSaveFileDialog.ShowDialog() == DialogResult.OK)
                oDestiny.Text = oSaveFileDialog.FileName;
        }

        public async void btCopy_Click(object oSender, EventArgs oArgs)
        {
            try
            {
                TokenSource ??= new CancellationTokenSource();

                if (IsProcessing)
                {
                    TokenSource?.Cancel();
                    return;
                }

                await _currentControl.Run().ConfigureAwait(true);
            }
            catch (Exception oException)
            {
                MessageBox.Show(oException.ToString());
            }
            finally
            {
                ProgressBar.ResetText();
                TokenSource?.Dispose();
            }
        }

        private void btClose_Click(object oSender, EventArgs oArgs) => Close();
        private void cbMove_CheckedChanged(object oSender, EventArgs oArgs) => btCopy.Text = GetBtCopyText();
        
        public void HandleFolderBrowser(string sDescription, bool bShowNewFolderButton, TextBox oDestinyBox)
        {
            oFolderBrowserDialog.Description = sDescription;
            oFolderBrowserDialog.ShowNewFolderButton = bShowNewFolderButton;
            oFolderBrowserDialog.SelectedPath = oDestinyBox.Text;

            if (oFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                oDestinyBox.Text = oFolderBrowserDialog.SelectedPath;
        }

        public void SetProcessInWork(bool bProcessing)
        {
            IsProcessing = bProcessing;

            btClose.Enabled = !bProcessing;
            btCopy.Text = bProcessing ? "Stop" : GetBtCopyText();
            oTabControl.Enabled = !bProcessing;
            cbMove.Enabled = !bProcessing;

            ProgressBar.Enabled = bProcessing;
        }

        public string GetBtCopyText() => Mode switch
        {
            KopatorMode.Copy => cbMove.Checked ? "Verschieben" : "Kopieren",
            KopatorMode.Collect => "Sammeln",
            KopatorMode.Catalog => "Katalogisieren",
            _ => string.Empty,
        };

        public bool CheckDirectory(string sDirPath, bool bReadable, bool bWritable)
        {
            try
            {
                if (bWritable)
                {
                    using (var fs = File.Create(Path.Combine(sDirPath, Path.GetRandomFileName()), 1, System.IO.FileOptions.DeleteOnClose))
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

        public void DoProcessing(IProgress<bool> oProgress, bool bCut, string sDestinyPath, string[] a_sFiles)
        {
            foreach (var sFile in a_sFiles)
            {
                try
                {
                    if (TokenSource?.IsCancellationRequested ?? true)
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

                    if (TokenSource?.IsCancellationRequested ?? true)
                        return;
                }
                catch
                {

                }
            }
        }

        public void DeleteDirectory(string sRoot)
        {
            foreach (var sDir in Directory.GetDirectories(sRoot))
                DeleteDirectory(sDir);

            try
            {
                Directory.Delete(sRoot, true);
            }
            catch (Exception)
            {
                var files = Directory.GetFiles(sRoot);

                foreach (var f in files)
                    DeleteFile(f);

                RemoveDirectory(sRoot);
            }
        }

        private void oTabControl_Selected(object oSender, TabControlEventArgs oArgs)
        {
            if(oArgs.TabPageIndex < 0)
                return;

            SetTabPageControl(oArgs.TabPageIndex);
        }

        private void SetTabPageControl(int tabPageIndex)
        {
            Mode = (KopatorMode)tabPageIndex;
            
            cbMove.Enabled = Mode == KopatorMode.Copy;
            btCopy.Text = GetBtCopyText();

            if(_currentControl != null)
                return;

            Control cToAdd = null;
            switch(Mode)
            {
                case KopatorMode.Copy: cToAdd = new CopyControl(); break;
                case KopatorMode.Collect: cToAdd = new CollectControl(); break;
                case KopatorMode.Catalog: cToAdd = new CatalogControl(); break;
            }

            oTabControl.SelectedTab.Controls.Add(cToAdd);
            oTabControl.PerformLayout();
        }

        public string GetFullPath(string path) => string.IsNullOrWhiteSpace(path) ? null : Path.GetFullPath(path);
    }
}
