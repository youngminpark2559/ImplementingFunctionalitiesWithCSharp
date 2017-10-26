namespace AnalyzingTextTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnGetStats = new System.Windows.Forms.Button();
            this.txtBook = new System.Windows.Forms.TextBox();
            this.textURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfChar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numberOfWords = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 539);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(157, 23);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Get text on above textbox";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnGetStats
            // 
            this.btnGetStats.Location = new System.Drawing.Point(12, 794);
            this.btnGetStats.Name = "btnGetStats";
            this.btnGetStats.Size = new System.Drawing.Size(157, 23);
            this.btnGetStats.TabIndex = 1;
            this.btnGetStats.Text = "Analyze";
            this.btnGetStats.UseVisualStyleBackColor = true;
            this.btnGetStats.Click += new System.EventHandler(this.btnGetStats_Click);
            // 
            // txtBook
            // 
            this.txtBook.Location = new System.Drawing.Point(12, 12);
            this.txtBook.Multiline = true;
            this.txtBook.Name = "txtBook";
            this.txtBook.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBook.Size = new System.Drawing.Size(567, 373);
            this.txtBook.TabIndex = 2;
            // 
            // textURL
            // 
            this.textURL.Location = new System.Drawing.Point(14, 426);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(567, 21);
            this.textURL.TabIndex = 3;
            this.textURL.Text = "http://www.gutenberg.org/files/98/98-8.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(507, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Put the URL of a text source(i.e. http://www.http://www.gutenberg.org/files/98/98" +
    "-8.txt)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 471);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Put the text file from local PC";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 497);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(455, 21);
            this.textBox2.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(473, 497);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(106, 21);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 603);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 36);
            this.label3.TabIndex = 8;
            this.label3.Text = "Put a number\r\nand you can get frequently used words\r\ncomposed of number of charac" +
    "ters you put";
            // 
            // numberOfChar
            // 
            this.numberOfChar.Location = new System.Drawing.Point(12, 655);
            this.numberOfChar.Name = "numberOfChar";
            this.numberOfChar.Size = new System.Drawing.Size(48, 21);
            this.numberOfChar.TabIndex = 9;
            this.numberOfChar.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 578);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(581, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "---------------------------------------------------------------------------------" +
    "---------------";
            // 
            // numberOfWords
            // 
            this.numberOfWords.Location = new System.Drawing.Point(12, 741);
            this.numberOfWords.Name = "numberOfWords";
            this.numberOfWords.Size = new System.Drawing.Size(48, 21);
            this.numberOfWords.TabIndex = 11;
            this.numberOfWords.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 692);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 36);
            this.label5.TabIndex = 12;
            this.label5.Text = "Put a number\r\nand you can get units of word\r\nyou put";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(291, 655);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 21);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 603);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 36);
            this.label6.TabIndex = 14;
            this.label6.Text = "Put a number for matching words\r\nYou put 3 and you get such as \"one\", \"way\"\r\nin o" +
    "rder of frequency descending ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 829);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numberOfWords);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numberOfChar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textURL);
            this.Controls.Add(this.txtBook);
            this.Controls.Add(this.btnGetStats);
            this.Controls.Add(this.btnDownload);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnGetStats;
        private System.Windows.Forms.TextBox txtBook;
        private System.Windows.Forms.TextBox textURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox numberOfChar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numberOfWords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
    }
}

