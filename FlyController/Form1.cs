using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace FlyController
{
    public partial class Form1 : Form
    {
        FilterInfoCollection fic;
        VideoCaptureDevice videoSorce;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdatePorts();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        ConsoleUi console;

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (console == null)
                console = new ConsoleUi();
            if (console.IsDisposed)
                console = new ConsoleUi();
            console.Show();
            console.Text = ComController.GetInfo();
        }

        private void UpdatePorts()
        {
            portToolStripMenuItem.DropDownItems.Clear();
            try
            {
                foreach (var s in ComController.GetOpenPorts())
                    portToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(s, null, new EventHandler((object a, EventArgs b) => { ComController.Init(s); ComController.Open(); })));
            }
            catch
            {

            }
            portToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem("Update", null, new EventHandler((object a, EventArgs s) => { UpdatePorts(); })));
        }


        private void Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            string[] names = { "Servo", "Motor", "GPS", "Compas", "Accelerometer", "Gyroscope", "Barometer" };
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Add");
            item.DropDownItems.Add(names[0], null, new EventHandler((object a, EventArgs b) => { AddPanelDeviece(new ControlDeviceUi.ServoPanel()); }));
            item.DropDownItems.Add(names[1], null, new EventHandler((object a, EventArgs b) => { }));
            item.DropDownItems.Add(names[2], null, new EventHandler((object a, EventArgs b) => { }));
            item.DropDownItems.Add(names[3], null, new EventHandler((object a, EventArgs b) => { }));
            item.DropDownItems.Add(names[4], null, new EventHandler((object a, EventArgs b) => { }));
            item.DropDownItems.Add(names[5], null, new EventHandler((object a, EventArgs b) => { }));
            item.DropDownItems.Add(names[6], null, new EventHandler((object a, EventArgs b) => { }));
            menu.Items.Add(item);

            item = new ToolStripMenuItem("Visializate");
            item.DropDownItems.Add("3", null, new EventHandler((object a, EventArgs b) => { colvo = 3; UpdatePanelDevice(); }));
            item.DropDownItems.Add("6", null, new EventHandler((object a, EventArgs b) => { colvo = 6; UpdatePanelDevice(); }));
            menu.Items.Add(item);
            menu.Show(MousePosition);
        }

        private void panelDeviceControls_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            string[] names = { "Button", "Fixeted button", "Potentiometer", "Joystick", "Light" };
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Add");
            item.DropDownItems.Add(names[0], null, new EventHandler((object a, EventArgs b) => { AddPanel(new ControlUi.ButtonPanel()); }));
            item.DropDownItems.Add(names[1], null, new EventHandler((object a, EventArgs b) => { AddPanel(new ControlUi.ButtonSwithPanel()); }));
            item.DropDownItems.Add(names[2], null, new EventHandler((object a, EventArgs b) => { AddPanel(new ControlUi.PotentiometerPanel()); }));
            item.DropDownItems.Add(names[3], null, new EventHandler((object a, EventArgs b) => { }));
            item.DropDownItems.Add(names[4], null, new EventHandler((object a, EventArgs b) => { }));
            menu.Items.Add(item);
            menu.Show(MousePosition);
        }

        int colvo = 3;
        List<UserControl> DeviceControls = new List<UserControl>();
        private void AddPanelDeviece(UserControl control)
        {
            if (DeviceControls.Count + 1 > colvo)
            {
                MessageBox.Show("You must remove the object, to add one more.");
                return;
            }
            Point[] points = { new Point(3, 3), new Point(3, 159), new Point(3, 315), new Point(159, 3), new Point(159, 159), new Point(159, 315) };

            control.Location = points[DeviceControls.Count];
            if (colvo == 3)
                SetBig(control);
            else SetSmall(control);
            control.MouseClick += ControlDevice_Click;
            foreach (var v in control.Controls)
                (v as Control).MouseClick += new MouseEventHandler((object obj, MouseEventArgs evarg) => { ControlDevice_Click(control, evarg); });

            PanelDevice.Controls.Add(control);
            DeviceControls.Add(control);
            //  servoPanel.Init("Servoprivod one","serv1");
        }
        private void ControlDevice_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Init", null, new EventHandler((object a, EventArgs b) => { Init((sender as UserControl)); }));
            menu.Items.Add(item);
            item = new ToolStripMenuItem("Delete", null, new EventHandler((object a, EventArgs b) => { DeleteDevicePanel((sender as UserControl)); }));
            menu.Items.Add(item);
            menu.Show(MousePosition);
        }
        private void DeleteDevicePanel(UserControl control)
        {
            PanelDevice.Controls.Remove(control);
            DeviceControls.Remove(control);
            control.Dispose();
            control = null;
            UpdatePanelDevice();
        }
        private void UpdatePanelDevice()
        {
            Point[] points = { new Point(3, 3), new Point(3, 159), new Point(3, 315), new Point(159, 3), new Point(159, 159), new Point(159, 315) };

            if (DeviceControls.Count > colvo)
            {
                for (int i = DeviceControls.Count - 1; i > colvo; i--)
                    DeleteDevicePanel(DeviceControls[i]);
            }

            for (int i = 0; i < DeviceControls.Count; i++)
            {
                if (colvo == 3)
                {
                    SetBig(DeviceControls[i]);
                }
                else
                {
                    SetSmall(DeviceControls[i]);
                }
                DeviceControls[i].Location = points[i];
            }
        }

        List<UserControl> ControlsUi = new List<UserControl>();
        private void AddPanel(UserControl control)
        {
            if (ControlsUi.Count + 1 > 5)
            {
                MessageBox.Show("You must remove the object, to add one more.");
                return;
            }
            Point[] points = { new Point(3, 3), new Point(159, 3), new Point(315, 3), new Point(471, 3), new Point(627, 3) };

            control.Location = points[ControlsUi.Count];

            control.MouseClick += Control_Click;
            foreach (var v in control.Controls)
                (v as Control).MouseClick += new MouseEventHandler((object obj, MouseEventArgs evarg) => { Control_Click(control, evarg); });
            panelControls.Controls.Add(control);
            ControlsUi.Add(control);
        }
        private void Control_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Init", null, new EventHandler((object a, EventArgs b) => { Init((sender as UserControl)); }));
            menu.Items.Add(item);
            item = new ToolStripMenuItem("Delete", null, new EventHandler((object a, EventArgs b) => { DeletePanel((sender as UserControl)); }));
            menu.Items.Add(item);
            menu.Show(MousePosition);
        }
        private void DeletePanel(UserControl control)
        {
            panelControls.Controls.Remove(control);
            ControlsUi.Remove(control);
            control.Dispose();
            control = null;
            UpdatePanel();
        }
        private void UpdatePanel()
        {
            Point[] points = { new Point(3, 3), new Point(159, 3), new Point(315, 3), new Point(471, 3), new Point(627, 3) };

            if (ControlsUi.Count > colvo)
            {
                for (int i = ControlsUi.Count - 1; i > colvo; i--)
                    DeletePanel(ControlsUi[i]);
            }

            for (int i = 0; i < ControlsUi.Count; i++)
            {
                ControlsUi[i].Location = points[i];
            }
        }

        private void SetBig(UserControl control)
        {
            if (control is ControlDeviceUi.ServoPanel)
                (control as ControlDeviceUi.ServoPanel).DisineBig();
        }
        private void SetSmall(UserControl control)
        {
            if (control is ControlDeviceUi.ServoPanel)
                (control as ControlDeviceUi.ServoPanel).DisineSmall();
        }
        private void Init(UserControl control)
        {
            if (control is ControlDeviceUi.ServoPanel)
                (control as ControlDeviceUi.ServoPanel).Init();

            if (control is ControlUi.ButtonPanel)
                (control as ControlUi.ButtonPanel).Init();
            if (control is ControlUi.ButtonSwithPanel)
                (control as ControlUi.ButtonSwithPanel).Init();
            if (control is ControlUi.PotentiometerPanel)
                (control as ControlUi.PotentiometerPanel).Init();

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            (menuToolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).DropDownItems.Clear();
            foreach (FilterInfo device in fic)
            {
                (menuToolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).DropDownItems.Add(device.Name, null, new EventHandler((object o, EventArgs arg) => { StartVideo(device); }));
            }

            videoSorce = new VideoCaptureDevice();
        }

        void StartVideo(FilterInfo f)
        {
            if (videoSorce.IsRunning)
            {
                videoSorce.Stop();
                pictureBoxVideo.Image = null;
                pictureBoxVideo.Invalidate();
            }
            else
            {
                videoSorce = new VideoCaptureDevice(f.MonikerString);
                videoSorce.NewFrame += VideoSorce_NewFrame;
                videoSorce.Start();
            }
        }

        private void VideoSorce_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            pictureBoxVideo.Image = image;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSorce.Stop();
        }
    }

}
