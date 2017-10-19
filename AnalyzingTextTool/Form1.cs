using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzingTextTool
{
    public partial class Form1 : Form
    {
        Form2 f2 = new Form2();

        string theEBook;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, eArgs) =>
            {
                theEBook = eArgs.Result;
                txtBook.Text = theEBook;
            };
            
            wc.DownloadStringAsync(new Uri(textURL.Text));
        }

        private void btnGetStats_Click(object sender, EventArgs e)
        {
            // Get the words from the text.
            string[] words = theEBook.Split(
              new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/' },
              StringSplitOptions.RemoveEmptyEntries);
            string[] tenMostCommon = null;
            string longestWord = string.Empty;
            string shortestWord = string.Empty;

            // Process the task by using all available CPUs on the host machine
            Parallel.Invoke(
                () =>
                {
                    // Now, find the ten most common words.
                    tenMostCommon = FindTenMostCommon(words);
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
            StringBuilder bookStats = new StringBuilder("Ten Most Common Words are:\n");
            bookStats.AppendLine();
            foreach (string s in tenMostCommon)
            {
                bookStats.AppendLine(s);
            }
            bookStats.AppendLine();
            bookStats.AppendFormat("Longest word is: {0}", longestWord);
            bookStats.AppendLine();
            bookStats.AppendLine();
            bookStats.AppendFormat("Shortest word is: {0}", shortestWord);
            bookStats.AppendLine();
            f2.Show();
            f2.textBoxList.Text = bookStats.ToString();
            f2.textBoxList.Show();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }


        private string[] FindTenMostCommon(string[] words)
        {
            var frequencyOrder = from word in words
                                 where word.Length > Int32.Parse(numberOfChar.Text)
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;
            string[] commonWords = (frequencyOrder.Take(Int32.Parse(numberOfWords.Text))).ToArray();
            return commonWords;
        }
        private string FindLongestWord(string[] words)
        {
            return (from w in words
                    orderby w.Length descending
                    select w).FirstOrDefault();
        }
        private string FindShortestWord(string[] words)
        {
            return (from w in words
                    orderby w.Length ascending
                    select w).FirstOrDefault();
        }


    }
}
