namespace MultitabledDataSetApp
{
    partial class MainForm
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
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.dataGridViewInventory = new System.Windows.Forms.DataGridView();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCustomers
            // 
            this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(12, 267);
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.RowTemplate.Height = 23;
            this.dataGridViewCustomers.Size = new System.Drawing.Size(735, 149);
            this.dataGridViewCustomers.TabIndex = 0;
            // 
            // dataGridViewInventory
            // 
            this.dataGridViewInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInventory.Location = new System.Drawing.Point(12, 55);
            this.dataGridViewInventory.Name = "dataGridViewInventory";
            this.dataGridViewInventory.RowTemplate.Height = 23;
            this.dataGridViewInventory.Size = new System.Drawing.Size(735, 149);
            this.dataGridViewInventory.TabIndex = 1;
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 471);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.RowTemplate.Height = 23;
            this.dataGridViewOrders.Size = new System.Drawing.Size(735, 149);
            this.dataGridViewOrders.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Inventory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Current Customers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 439);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current Orders";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 632);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.dataGridViewInventory);
            this.Controls.Add(this.dataGridViewCustomers);
            this.Name = "MainForm";
            this.Text = "AutoLot Database Manipulator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.DataGridView dataGridViewInventory;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

