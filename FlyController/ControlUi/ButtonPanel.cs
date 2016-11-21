using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlyController.ControlUi
{
    public partial class ButtonPanel : UserControl
    {
        bool enable = true;
        string prefix;
        string name;

        public ButtonPanel()
        {
            InitializeComponent();
        }

        public void Init()
        {
            InitEditFrame();
            ComController.receiveMessage += ComControllerReceive;
        }

        private void InitEditFrame()
        {
            Form form = new Form();
            TextBox textBox1 = new TextBox();
            Button button1 = new Button();
            TextBox textBox2 = new TextBox();
            TextBox textBox3 = new TextBox();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();

            form.SuspendLayout();
            textBox1.Location = new Point(82, 6);
            textBox1.Name = name;
            textBox1.Size = new Size(100, 20);
            textBox1.TabIndex = 0;
            textBox1.Text = name == "" ? "Name" : name;
            button1.Location = new Point(197, 6);
            button1.Name = "EnterButton";
            button1.Size = new Size(75, 73);
            button1.TabIndex = 4;
            button1.Text = "Enter";
            button1.UseVisualStyleBackColor = true;
            textBox2.Location = new Point(82, 33);
            textBox2.Name = prefix;
            textBox2.Size = new Size(100, 20);
            textBox2.TabIndex = 1;
            textBox2.Text = prefix;
            textBox3.Location = new Point(82, 59);
            textBox3.Name = enable.ToString();
            textBox3.Size = new Size(100, 20);
            textBox3.TabIndex = 2;
            textBox3.Text = enable.ToString();
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(35, 13);
            label1.Text = "Name";
            label2.AutoSize = true;
            label2.Location = new Point(12, 36);
            label2.Name = "label2";
            label2.Size = new Size(35, 13);

            label2.Text = "Prefix";
            label3.AutoSize = true;
            label3.Location = new Point(12, 62);
            label3.Name = "label3";
            label3.Size = new Size(35, 13);
            label3.Text = "Param";
            form.AutoScaleDimensions = new SizeF(6F, 13F);
            form.AutoScaleMode = AutoScaleMode.Font;
            form.ClientSize = new Size(284, 87);
            form.Controls.Add(label3);
            form.Controls.Add(label2);
            form.Controls.Add(label1);
            form.Controls.Add(textBox3);
            form.Controls.Add(textBox2);
            form.Controls.Add(button1);
            form.Controls.Add(textBox1);
            form.Name = "Init";
            form.Text = "Init";
            form.ResumeLayout(false);
            form.PerformLayout();
            button1.Click += new EventHandler((object o, EventArgs e) =>
            {
                try
                {
                    name = textBox1.Text;
                    this.prefix = textBox2.Text;

                    enable = bool.Parse(textBox3.Text);

                    this.label1.Text = name;
                    Switch(enable);
                    form.Close();
                }
                catch { MessageBox.Show("Error, 'True' or 'False'"); }
            });

            form.Show();
        }

        private void ComControllerReceive(string message)
        {
            try
            {
                if (message.Split(' ')[0] != prefix)
                    return;
                Switch(bool.Parse(message.Split(' ')[1]));
            }
            catch
            {

            }
        }

        private void Switch(bool str)
        {
                pictureBox1.Visible = !str;
                pictureBox2.Visible = str;
        }
    }
}