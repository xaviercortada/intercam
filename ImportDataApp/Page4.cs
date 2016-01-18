using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizardDatos
{
    public partial class Page4 : UserControl
    {
        public Page4()
        {
            InitializeComponent();
        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new Pen(Color.Gray, 1);
            Graphics g = e.Graphics;
            g.DrawLine(blackpen, 0, label1.Top - 10, Width, label1.Top - 10);
            g.Dispose();

        }
    }
}
