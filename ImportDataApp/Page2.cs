using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace WizardDatos
{

    public partial class Page2 : UserControl, ISeleccion
    {

        public event SelectionEventHandler Changed;

        String pathDatos;
        String BS_version;

        public Page2()
        {
            InitializeComponent();
        }

        protected virtual void OnChanged(SelectionEvent e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewChanged();
        }

        private void listViewChanged()
        {
            ListView.SelectedListViewItemCollection sel = listView1.SelectedItems;
            if (sel.Count > 0)
            {
                pathDatos = sel[0].Text;
                OnChanged(new SelectionEvent() { status = Selection.Ready });

            }
            else
            {
                OnChanged(new SelectionEvent() { status = Selection.Pending });
            }
        }

        private void Page2_Load(object sender, EventArgs e)
        {
            string[] arr = new string[3];

            pathDatos = FindBS_Path()+@"\datos";

            BS_version = FindBS_Version();

            arr[2] = "";

            if (Directory.Exists(pathDatos))
            {
                DateTime fecha = Directory.GetCreationTime(pathDatos);
                arr[2] = fecha.ToShortDateString() + " " + fecha.ToShortTimeString();
            }

            arr[0] = pathDatos;
            arr[1] = BS_version;


            ListViewItem item = new ListViewItem(arr);
            listView1.Items.Add(item);
        }

        private String FindBS_Path()
        {
            String s = "";

            using(RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\BS\BS ONLINE 2014"))
            {
                if (key != null)
                {
                    Object o = key.GetValue("Path");
                    if (o != null)
                    {
                        s = o as String;

                    }
                }

            }

            return s;

        }

        private String FindBS_Version()
        {
            String s = "";

            using(RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\BS\OnLine\Products\BS ONLINE 2014"))
            {
                if (key != null)
                {
                    Object o = key.GetValue("version");
                    if (o != null)
                    {
                        s = o as String;

                    }
                }

            }

            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                pathDatos = textBox1.Text;
                OnChanged(new SelectionEvent() { status = Selection.Ready });
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton2.Checked;
            button1.Enabled = radioButton2.Checked;

            if (radioButton2.Checked)
            {
                if (textBox1.Text.Length > 0)
                {
                    pathDatos = textBox1.Text;
                    OnChanged(new SelectionEvent() { status = Selection.Ready });
                }
                else
                {
                    OnChanged(new SelectionEvent() { status = Selection.Pending });
                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listView1.Enabled = radioButton1.Checked;

            if (radioButton1.Checked)
            {
                listViewChanged();
            }
        }

        public string GetFolder()
        {
            return pathDatos;
        }

        private void Page2_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new Pen(Color.Gray, 1);
            Graphics g = e.Graphics;
            g.DrawLine(blackpen, 0, label2.Top - 10, Width, label2.Top - 10);
            g.Dispose();
        }
    }
}
