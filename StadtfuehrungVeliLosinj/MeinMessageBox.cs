using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StadtfuehrungVeliLosinj
{
   
    public partial class MeinMessageBox : Form
    { 
        private Timer _timer;
        public MeinMessageBox(string _message, int _milliseconds)
        {
            InitializeComponent();
            labelMessage.Text = _message;

            _timer = new Timer();
            _timer.Interval = _milliseconds;
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                this.Close();
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _timer.Start();

        } 
        
        public static void ShowMessage(string _message,int _milliseconds  = 3000)
        {
            MeinMessageBox box = new MeinMessageBox(_message, _milliseconds);
            box.ShowDialog();
        }

        private void MeinMessageBox_Load(object sender, EventArgs e)
        {

        }
    }
}
