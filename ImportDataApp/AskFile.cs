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
    public partial class AskFile : AskControl
    {
        public event ClickEventHandler Clicked;

        public String Title
        {
            set
            {
                label1.Text = value;
            }
        }

        public String SubTitle
        {
            set
            {
                label2.Text = value;
            }
        }

        public String FileName
        {
            set
            {
                label3.Text = value;
            }
        }

        public String FilePath
        {
            set
            {
                label4.Text = value;
            }
        }

        public String FileSize
        {
            set
            {
                label5.Text = value;
            }
        }

        public String FileDate
        {
            set
            {
                label6.Text = value;
            }
        }

        public AskFile()
        {
            InitializeComponent();

            Selected = false;

            Application.AddMessageFilter(new MessageFilter(this));
        }


        public override void OnClick()
        {
            if (Clicked != null)
            {
                //Application.RemoveMessageFilter(msgFilter);
                Clicked(this, new EventArgs());
            }
        }

        private void AskFile_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave();
        }
    }
}
