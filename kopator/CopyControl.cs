using kopator.Properties;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kopator
{
    public partial class CopyControl : UserControl, IkopatorControl
    {
        private kopatorMainForm _parentForm => FindForm() as kopatorMainForm;
        public CopyControl() => InitializeComponent();

        private void CopyControl_Load(object sender, EventArgs e)
        {
            tbSource.Text = Settings.Default.Source;
            tbDestiny.Text = Settings.Default.Destiny;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Settings.Default.Source = tbSource.Text;
            Settings.Default.Destiny = tbDestiny.Text;
            Settings.Default.Save();
            base.OnHandleDestroyed(e);
        }

        public async Task Run()
        {
            var bMove = _parentForm.cbMove.Checked;
            var sSourcePath = _parentForm.GetFullPath(tbSource.Text);
            var sDestinyPath = _parentForm.GetFullPath(tbDestiny.Text);

            // check for null/empty
            if (string.IsNullOrEmpty(sSourcePath) || string.IsNullOrEmpty(sDestinyPath))
            {
                MessageBox.Show("Bitte Ziel- und Quellordner angeben!");
                return;
            }

            // check source
            if (!_parentForm.CheckDirectory(sSourcePath, true, bMove))
            {
                MessageBox.Show("Keine Lese- und/oder Schreibrechte für den Quellordner!");
                return;
            }

            // create directory if it is not existing
            if (!Directory.Exists(sDestinyPath))
                Directory.CreateDirectory(sDestinyPath);

            // check destiny
            if (!_parentForm.CheckDirectory(sDestinyPath, false, true))
            {
                MessageBox.Show("Keine Schreibrechte für den Zielordner!");
                return;
            }

            // init process start
            _parentForm.SetProcessInWork(true);
            _parentForm.TokenSource = new CancellationTokenSource();

            var oProgress = new Progress<bool>(x => _parentForm.ProgressBar.PerformStep());

            // get files
            var a_sFiles = Directory.GetFiles(sSourcePath);

            // set progressbar
            _parentForm.ProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => _parentForm.DoProcessing(oProgress, bMove, sDestinyPath, a_sFiles), _parentForm.TokenSource.Token).ConfigureAwait(false);

            _parentForm.Invoke(new Action(() => 
            {
                _parentForm.SetProcessInWork(false);

                if (_parentForm.TokenSource?.IsCancellationRequested ?? true)
                    MessageBox.Show("Kopiervorgang abgebrochen!");
                else
                {
                    MessageBox.Show("Kopiervorgang abgeschlossen!");
                    _parentForm.Close();
                }
            }));
        }

        private void btDestinyDotDotDot_Click(object oSender, EventArgs oArgs) => _parentForm.HandleFolderBrowser("Quelle auswählen.", false, tbDestiny);
        private void btSourceDotDotDot_Click(object oSender, EventArgs oArgs) => _parentForm.HandleFolderBrowser("Ziel auswählen.", true, tbSource);
    }
}
