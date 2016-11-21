namespace FlyController
{
    partial class ConsoleUi
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.checkBoxTime = new System.Windows.Forms.CheckBox();
            this.checkBoxDown = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(617, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(599, 20);
            this.textBox1.TabIndex = 1;
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.Location = new System.Drawing.Point(12, 38);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.Size = new System.Drawing.Size(715, 446);
            this.listBoxMessage.TabIndex = 2;
            // 
            // checkBoxTime
            // 
            this.checkBoxTime.AutoSize = true;
            this.checkBoxTime.Location = new System.Drawing.Point(13, 505);
            this.checkBoxTime.Name = "checkBoxTime";
            this.checkBoxTime.Size = new System.Drawing.Size(49, 17);
            this.checkBoxTime.TabIndex = 3;
            this.checkBoxTime.Text = "Time";
            this.checkBoxTime.UseVisualStyleBackColor = true;
            // 
            // checkBoxDown
            // 
            this.checkBoxDown.AutoSize = true;
            this.checkBoxDown.Location = new System.Drawing.Point(13, 528);
            this.checkBoxDown.Name = "checkBoxDown";
            this.checkBoxDown.Size = new System.Drawing.Size(76, 17);
            this.checkBoxDown.TabIndex = 4;
            this.checkBoxDown.Text = "AutoDown";
            this.checkBoxDown.UseVisualStyleBackColor = true;
            // 
            // ConsoleUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 557);
            this.Controls.Add(this.checkBoxDown);
            this.Controls.Add(this.checkBoxTime);
            this.Controls.Add(this.listBoxMessage);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "ConsoleUi";
            this.Text = "ConsoleUi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.CheckBox checkBoxTime;
        private System.Windows.Forms.CheckBox checkBoxDown;
    }
}