﻿using Alphaleonis.Win32.Filesystem;

using kopator.Properties;

using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kopator
{
    public partial class CollectControl : UserControl, IKopatorControl
    {
        private KopatorMainForm _parentForm => FindForm() as KopatorMainForm;

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
            var sPath = _parentForm.GetFullPath(tbCollect.Text);

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
            _parentForm.TokenSource = new CancellationTokenSource();

            var oProgress = new Progress<bool>(x => _parentForm.ProgressBar.PerformStep());

            // get sub dirs
            var a_sDirs = Directory.GetDirectories(sPath);
            var a_sFilesNotIncluded = tbIgnore.Text.Split(',')
                .Select(t => t.Trim(' '))
                .Select(t => Directory.GetFiles(sPath, t, System.IO.SearchOption.AllDirectories))
                .SelectMany(s => s)
                .ToArray();

            var a_sAllFiles = Directory.GetFiles(sPath, "*", System.IO.SearchOption.AllDirectories);
            var a_sFiles = a_sAllFiles.Except(a_sFilesNotIncluded).ToArray();

            // set progressbar
            _parentForm.ProgressBar.Maximum = a_sFiles.Length;

            await Task.Run(() => _parentForm.DoProcessing(oProgress, true, sPath, a_sFiles), _parentForm.TokenSource.Token).ConfigureAwait(true);
            await Task.Run(() => a_sDirs.ToList().ForEach(d => _parentForm.DeleteDirectory(d))).ConfigureAwait(true);

            _parentForm.Invoke(new Action(() => 
            {
                _parentForm.SetProcessInWork(false);

                if (_parentForm.TokenSource?.IsCancellationRequested ?? true)
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
