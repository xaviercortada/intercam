using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizardDatos
{
    class Utilities
    {
    }

    public delegate void ClickEventHandler(object sender, EventArgs e);

    public delegate void SelectionEventHandler(object sender, SelectionEvent e);

    public enum Selection { Ready, Pending };

    public class SelectionEvent : EventArgs
    {
        private Selection currentStatus;
        public Selection status
        {
            set
            {
                currentStatus = value;
            }
            get
            {
                return this.currentStatus;
            }
        }
    }

    public delegate void ChangedEventHandler(object sender, ProcessEvent e);

    public enum State { Started, Error, Canceled, Finished };

    public class ProcessEvent : EventArgs
    {
        private State currentState;
        public State state
        {
            set
            {
                currentState = value;
            }
            get
            {
                return this.currentState;
            }
        }
    }

    public interface IAskControl
    {
        Boolean IsSelected();
        void OnMouseEnter();
        void OnMouseLeave();
        void OnClick();

    }

    public class AskControl : UserControl, IAskControl
    {
        protected Boolean Selected = false;

        public Boolean IsSelected()
        {
            return Selected;
        }

        public void OnMouseEnter()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ChangeBackColor(this, Color.Azure);

            Selected = true;

        }

        public void OnMouseLeave()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ChangeBackColor(this, Color.White);

            Selected = false;
        }

        public virtual void OnClick()
        {

        }

        protected void ChangeBackColor(Control target, Color color)
        {
            target.BackColor = color;
            foreach (Control ctrl in target.Controls)
            {
                ChangeBackColor(ctrl, color);
            }

        }
    }

    public class MessageFilter : IMessageFilter
    {
        AskControl askFile;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MOUSECLICK = 0x0201;

        public MessageFilter(AskControl askFile)
        {
            this.askFile = askFile;
        }

        public bool PreFilterMessage(ref Message objMessage)
        {
            if (objMessage.Msg == WM_MOUSEMOVE)
            {
                Point point = askFile.PointToClient(System.Windows.Forms.Control.MousePosition);

                if (point.Y >= 0 && point.Y < askFile.Height && point.X >= 0 && point.X < askFile.Width)
                {
                    if (!askFile.IsSelected())
                    {
                        askFile.OnMouseEnter();
                    }
                    return true;
                }

                if (askFile.IsSelected())
                {
                    askFile.OnMouseLeave();
                }
            }
            else if (objMessage.Msg == WM_MOUSECLICK)
            {
                Point point = askFile.PointToClient(System.Windows.Forms.Control.MousePosition);

                if (point.Y >= 0 && point.Y <= askFile.Height && point.X >= 0 && point.X < askFile.Width)
                {
                    askFile.OnClick();
                    return true;
                }
            }


            return false;
        }
    }


}
