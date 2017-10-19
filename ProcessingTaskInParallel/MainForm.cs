using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataParallelismWithForEach
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Field for cancellation
        private CancellationTokenSource cancelToken = new CancellationTokenSource();

        private void btnProcessImages_Click(object sender, EventArgs e)
        {
            // Start a new "task" to process the files
            Task.Factory.StartNew(() =>
            {
                ProcessFiles();
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // This will be used to tell all the worker threads to stop
            cancelToken.Cancel();
        }




        // Upside down images which have jpg extension 
        private void ProcessFiles()
        {
            // Use ParallelOptions instance to store the CancellationToken.
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles
                (@"C:\PicturesWillbeRotated", "*.jpg",
                  SearchOption.AllDirectories);
            string newDir = @"C:\PicturesHaveBeenRotated";
            Directory.CreateDirectory(newDir);
            try
            {
                // Process the image data in a parallel manner
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();

                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));
                        this.Invoke((Action)delegate
                        {
                            textBox1.Text  /*this.Text*/ = string.Format("Processing {0} on thread {1}", filename,
                              Thread.CurrentThread.ManagedThreadId);
                        }
                        );
                    }
                }
                );
                this.Invoke((Action)delegate
                {
                    textBox1.Text  /*this.Text*/ = "Done!";
                });
            }
            catch (OperationCanceledException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Text = ex.Message;
                });
            }
        }




    }
}
