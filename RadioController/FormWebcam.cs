using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video.DirectShow;
using AForge.Video;
namespace RadioController
{
    public partial class FormWebcam : Form
    {
        FilterInfoCollection fic;
        VideoCaptureDevice videoSorce;

        public FormWebcam()
        {
            InitializeComponent();
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            if (videoSorce.IsRunning)
            {
                videoSorce.Stop();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else
            {
                videoSorce = new VideoCaptureDevice(fic[comboBox1.SelectedIndex].MonikerString);
                videoSorce.NewFrame += VideoSorce_NewFrame;
                videoSorce.Start();
            }
        }

        private void VideoSorce_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = image;
        }

        private void FormWebcam_Load(object sender, EventArgs e)
        {
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in fic)
            {
                comboBox1.Items.Add(device.Name);
            }

            videoSorce = new VideoCaptureDevice();
        }
    }
}
