using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzingTextTool
{
    public partial class Form1 : Form
    {
        Form2 f2 = new Form2();

        string theEBook;

        string informations = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(textURL.Text))
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += (s, eArgs) =>
                {
                    theEBook = eArgs.Result;
                    txtBook.Text = theEBook;
                };

                wc.DownloadStringAsync(new Uri(textURL.Text));
            }
            else if (!String.IsNullOrEmpty(textBox2.Text))
            {
                //txtBook.Text = (new System.IO.StreamReader(textBox2.Text)).ToString();
                string stringText = File.ReadAllText(textBox2.Text);

                txtBook.Text = stringText;
                theEBook = txtBook.Text;

            }

        }

        private void btnGetStats_Click(object sender, EventArgs e)
        {
            // Get the words from the text.
            string[] words = theEBook.Split(
              new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/' },
              StringSplitOptions.RemoveEmptyEntries);
            string[] tenMostCommon = null;
            string longestWord = string.Empty;
            string[] shortestWord = null;
            //string informations = string.Empty;

            // Process the task by using all available CPUs on the host machine
            Parallel.Invoke(
                () =>
                {
                    // Now, find the ten most common words.
                    tenMostCommon = FindTenMostCommon(words);
                    //StringBuilder strBd = new StringBuilder();
                    //strBd.Append($"Current working context is {Thread.CurrentContext.ContextID} --- ");
                    //strBd.Append($"Current working thread is {Thread.CurrentThread.Name} --- ");
                    //strBd.Append($"Current working AppDomain is {AppDomain.CurrentDomain.FriendlyName} --- ");
                    //informations = strBd.ToString();
                },
                () =>
                {
                    // Get the longest word.
                    longestWord = FindLongestWord(words);
                },
                () =>
                {
                    // Get the shortest word.
                    shortestWord = FindShortestWord(words);
                }
                );

            // Now that all tasks are complete, build a string to show all
            // stats in a message box.
            StringBuilder bookStats = new StringBuilder("Most Common Words are:\n");
            bookStats.AppendLine();
            foreach (string s in tenMostCommon)
            {
                bookStats.AppendLine(s);
            }
            bookStats.AppendLine();
            bookStats.AppendLine();
            bookStats.AppendFormat("Longest word is: {0}", longestWord);
            bookStats.AppendLine();
            bookStats.AppendFormat("Informations : {0}", informations);
            bookStats.AppendLine();
            bookStats.AppendFormat("Most common words composed of\n number of character you put are:");
            bookStats.AppendLine();
            foreach (string s in shortestWord)
            {
                bookStats.AppendLine(s);
            }

            bookStats.AppendLine();
            f2.Show();
            f2.textBoxList.Text = bookStats.ToString();
            f2.textBoxList.Show();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            OpenFileDialog fdl = new OpenFileDialog();
            DialogResult dr = fdl.ShowDialog();

            if (dr == DialogResult.OK)
            {
                textBox2.Text = fdl.FileName;

            }
        }


        private string[] FindTenMostCommon(string[] words)
        {
            
            

            var frequencyOrder = from word in words
                                 where word.Length > Int32.Parse(numberOfChar.Text)
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;
            string[] commonWords = (frequencyOrder.Take(Int32.Parse(numberOfWords.Text))).ToArray();

            // Not sure but, confirmation code to show current context, thread, AppDomain looks more right to be in the block of method invoked
            // However, it's showing same output
            // Need to check whether I had wrong concept or I applied test code in the wrong way
            StringBuilder strBd = new StringBuilder();
            strBd.Append($"Current working context is {Thread.CurrentContext.ContextID} --- ");
            strBd.Append($"Current working thread is {Thread.CurrentThread.Name} --- ");
            strBd.Append($"Current working AppDomain is {AppDomain.CurrentDomain.FriendlyName} --- ");
            informations = strBd.ToString();

            return commonWords;
            
        }
        private string FindLongestWord(string[] words)
        {
            return (from w in words
                    orderby w.Length descending
                    select w).FirstOrDefault();
        }
        private string[] FindShortestWord(string[] words)
        {
            //return (from w in words
            //        orderby w.Length ascending
            //        select w).FirstOrDefault();

            //return words.OrderBy(x=>x.Length).FirstOrDefault();



            var shortestWordOrder = from word in words
                                    where word.Length == Int32.Parse(textBox1.Text)
                                    group word by word into g
                                    orderby g.Count() descending
                                    select g.Key;
            

            string[] shortWords = (shortestWordOrder.Take(10)).ToArray();
            return shortWords;


        }
    }
}
