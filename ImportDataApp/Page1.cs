using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardDatos
{
    public partial class Page1 : UserControl
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Page1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new Pen(Color.Gray, 1);
            Graphics g = e.Graphics;
            g.DrawLine(blackpen, 0, label2.Top - 10, Width, label2.Top - 10);
            g.Dispose();
        }
    }
}
