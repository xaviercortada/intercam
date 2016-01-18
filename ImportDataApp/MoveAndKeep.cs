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
    public partial class MoveAndKeep : AskControl
    {
        public event ClickEventHandler Clicked;

        public String RenameFileAs
        {
            set
            {
                label2.Text = String.Format("El nombre del archivo que esta moviendo se cambiará a: {0}", value);
            }
        }

        public MoveAndKeep()
        {
            InitializeComponent();

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
    }
}
