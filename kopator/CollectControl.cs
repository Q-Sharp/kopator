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
    public partial class CollectControl : UserControl, IkopatorControl
    {
        private kopatorMainForm _parentForm => FindForm() as kopatorMainForm;

        public CollectControl()
        {
            InitializeComponent();
        }

        private void CollectControl_Load(object sender, EventArgs e)
        {
            tbCollect.Text = Settings.Default.Collect;
            tbIgnore.Text = Settings.Default.Ignore;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Settings.Default.Collect = tbCollect.Text;
            Settings.Default.Ignore = tbIgnore.Text;
            Settings.Default.Save();
            base.OnHandleDestroyed(e);
        }

        private void btCollectDotDotDot_Click(object oSender, EventArgs oArgs) => _parentForm.HandleFolderBrowser("Sammelziel auswählen.", false, tbCollect);


        public async Task Run()
        {
            var sPath = Path.GetFullPath(tbCollect.Text);

            // check for null/empty
            if (string.IsNullOrEmpty(sPath))
            {
                MessageBox.Show("Bitte Ziel- und Quellordner angeben!");
                return;
            }

            // check path
            if (!_parentForm.CheckDirectory(sPath, true, true))
            {
                MessageBox.Show("Keine Lese- und/oder Schreibrechte für den Quellordner!");
                return;
            }

            // init process start
            _parentForm.SetProcessInWork(true);
            _parentForm.oTokenSource = new CancellationTokenSource();

            var oProgress = new Progress<bool>(x => _parentForm.oProgressBar.PerformStep());

            // get sub dirs
            var a_sDirs = Directory.GetDirectories(sPath);
            var a_sFilesNotIncluded = tbIgnore.Text.Split(',').Select(t => t.Trim(' ')).Select(t => Directory.GetFiles(sPath, t, SearchOption.AllDirectories)).SelectMany(s => s).ToArray();
            var a_sAllFiles = Directory.GetFiles(sPath, "*", SearchOption.AllDirectories);
            var a_sFiles = a_sAllFiles.Except(a_sFilesNotIncluded).ToArray();

            // set progressbar
            _parentForm.oProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => _parentForm.DoProcessing(oProgress, true, sPath, a_sFiles), _parentForm.oTokenSource.Token).ConfigureAwait(true);
            await Task.Run(() => a_sDirs.ToList().ForEach(d => _parentForm.DeleteDirectory(d))).ConfigureAwait(true);

            _parentForm.Invoke(new Action(() => 
            {
                _parentForm.SetProcessInWork(false);

                if (_parentForm.oTokenSource?.IsCancellationRequested ?? true)
                    MessageBox.Show("Sammelvorgang abgebrochen!");
                else
                {
                    MessageBox.Show("Sammelvorgang abgeschlossen!");
                    _parentForm.Close();
                }
            }));
        }
    }
}
