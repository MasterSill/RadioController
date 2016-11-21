using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlyController
{
    public partial class ConsoleUi : Form
    {
        public ConsoleUi()
        {
            InitializeComponent();
            ComController.writeMessage += ComController_writeMessage;
            ComController.receiveMessage += ComController_receiveMessage;
        }

        private void ComController_receiveMessage(string message)
        {
            try
            {
                string data = ComController.GetInfo()+ ": " + message;
                data = checkBoxTime.Checked ? GetTime()+ " " + data : data;
                listBoxMessage.Invoke(new Action(() => { listBoxMessage.Items.Add(data); }));
            }
            catch
            {

            }
        }

        private string GetTime()
        {
            return DateTime.Now.TimeOfDay.Hours.ToString() + "." +
                DateTime.Now.TimeOfDay.Minutes.ToString() + "." +
                DateTime.Now.TimeOfDay.Seconds.ToString();
        }

        private void ComController_writeMessage(string message)
        {
            try
            {
                string data = "Computer: " + message;
                data = checkBoxTime.Checked ? GetTime()+" " + data : data;
                listBoxMessage.Items.Add(data);
            }
            catch(Exception e)
            {
                listBoxMessage.Invoke(new Action(() => { listBoxMessage.Items.Add(e.ToString()); }));
            }
            //listBoxMessage.Items[listBoxMessage.Items.Count]
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComController.Write(textBox1.Text);
        }

        
    }
}
