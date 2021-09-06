namespace AsyncProgramming
{
    partial class SyncForm
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
            this.receiveDataButton = new System.Windows.Forms.Button();
            this.executionTimeTextBox = new System.Windows.Forms.TextBox();
            this.summaryContentLengthTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAsync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // receiveDataButton
            // 
            this.receiveDataButton.Location = new System.Drawing.Point(44, 110);
            this.receiveDataButton.Name = "receiveDataButton";
            this.receiveDataButton.Size = new System.Drawing.Size(75, 23);
            this.receiveDataButton.TabIndex = 0;
            this.receiveDataButton.Text = "Получить";
            this.receiveDataButton.UseVisualStyleBackColor = true;
            this.receiveDataButton.Click += new System.EventHandler(this.receiveDataButton_Click);
            // 
            // executionTimeTextBox
            // 
            this.executionTimeTextBox.Location = new System.Drawing.Point(180, 22);
            this.executionTimeTextBox.Name = "executionTimeTextBox";
            this.executionTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.executionTimeTextBox.TabIndex = 1;
            // 
            // summaryContentLengthTextBox
            // 
            this.summaryContentLengthTextBox.Location = new System.Drawing.Point(180, 62);
            this.summaryContentLengthTextBox.Name = "summaryContentLengthTextBox";
            this.summaryContentLengthTextBox.Size = new System.Drawing.Size(100, 20);
            this.summaryContentLengthTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Время выполнения (мс):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Общий размер страниц (байт):";
            // 
            // buttonAsync
            // 
            this.buttonAsync.Location = new System.Drawing.Point(158, 110);
            this.buttonAsync.Name = "buttonAsync";
            this.buttonAsync.Size = new System.Drawing.Size(75, 23);
            this.buttonAsync.TabIndex = 5;
            this.buttonAsync.Text = "Async";
            this.buttonAsync.UseVisualStyleBackColor = true;
            this.buttonAsync.Click += new System.EventHandler(this.buttonAsync_Click);
            // 
            // SyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 178);
            this.Controls.Add(this.buttonAsync);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.summaryContentLengthTextBox);
            this.Controls.Add(this.executionTimeTextBox);
            this.Controls.Add(this.receiveDataButton);
            this.Name = "SyncForm";
            this.Text = "Получение информации от веб-узлов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button receiveDataButton;
        private System.Windows.Forms.TextBox executionTimeTextBox;
        private System.Windows.Forms.TextBox summaryContentLengthTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAsync;
    }
}

