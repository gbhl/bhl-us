using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MOBOT.IAFileGenerator.GUI
{
    public partial class ViewLog : Form
    {
        public ViewLog()
        {
            InitializeComponent();
        }

        private void ViewLog_Load(object sender, EventArgs e)
        {
            textBox1.Text = Generator.GetLogText();
            textBox1.Select(0, 0);            
        }
    }
}
