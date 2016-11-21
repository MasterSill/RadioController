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
    public partial class PotentiometerPanel : UserControl
    {
        int _rot = 0;
        string prefix;
        string name;

        public PotentiometerPanel()
        {
            InitializeComponent();
        }

        public void Init()
        {
            InitEditFrame();
            if (!pictureBox1.Controls.Contains(this.pictureBox2)) this.pictureBox1.Controls.Add(this.pictureBox2);
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
            textBox1.Text = name == "" ? name : "Potentiometer";
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
            textBox3.Location = new Point(82, 59);
            textBox3.Text = _rot.ToString();
            textBox3.Size = new Size(100, 20);
            textBox3.TabIndex = 2;
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
            label3.Text = "Rotate";
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
                name = textBox1.Text;
                prefix = textBox2.Text;
                int rot = _rot;
                try
                {
                    rot = int.Parse(textBox3.Text);


                    this.label1.Text = "Name " + name;
                    Rotate(rot);
                    form.Close();
                }
                catch { MessageBox.Show("Error angle"); }
            });

            form.Show();
        }

        private void ComControllerReceive(string message)
        {
            try
            {
                if (message.Split(' ')[0] != prefix)
                    return;
                Rotate(int.Parse(message.Split(' ')[1]));
            }
            catch
            {

            }
        }

        public void Rotate(int rotate)
        {

            pictureBox2.Image = RotateImage(pictureBox2.Image, new PointF(125, 125), rotate - _rot);
            _rot = rotate % 360;
            label2.Text = "Angle " + _rot.ToString();
        }

        public Bitmap RotateImage(Image image, PointF offset, int angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics g = Graphics.FromImage(rotatedBmp);
            g.TranslateTransform(offset.X, offset.Y);
            g.RotateTransform(angle);
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }
    }
}
