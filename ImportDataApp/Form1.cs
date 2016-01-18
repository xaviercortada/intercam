using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardDatos
{
    public partial class Form1 : Form
    {
        private const int numPages = 4;

        private Control currentPanel;

        private Page1 page1;
        private Page2 page2;
        private Page3 page3;
        private Page4 page4;

        int index = 0;

        public Form1()
        {
            InitializeComponent();

            page1 = new Page1();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterPanel.Controls.Add(page1);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {

            switch (index)
            {
                case 0: 
                    index++;
                    PrevButton.Enabled = true;
                    Seleccionar();
                    break;
                case 1:
                    index++;
                    NextButton.Enabled = false;
                    Actualizar();
                    break;
                case 2:
                    index++;
                    NextButton.Enabled = false;
                    Fin();
                    break;
                case 3:
                    Close();
                    break;
            }
        }

        private void Presentación()
        {
            LeftPanel.BackgroundImage = Properties.Resources.update_data_1;

            CenterPanel.Controls.RemoveAt(0);

            CenterPanel.Controls.Add(currentPanel = new Page1() { Dock = DockStyle.Fill});


        }

        private void Seleccionar()
        {
            NextButton.Enabled = false;

            LeftPanel.BackgroundImage = Properties.Resources.update_data_2;

            CenterPanel.Controls.RemoveAt(0);

            CenterPanel.Controls.Add(page2 = new Page2() { Dock = DockStyle.Fill });

            page2.Changed += new SelectionEventHandler(SelectionChanged);

        }

        private void SelectionChanged(object sender,SelectionEvent e)
        {
            if (e.status == Selection.Ready)
            {
                NextButton.Enabled = true;
            }
            else
            {
                NextButton.Enabled = false;
            }

        }
        
        private void Actualizar()
        {
            ISeleccion seleccion = page2;

            LeftPanel.BackgroundImage = Properties.Resources.update_data_3;

            CenterPanel.Controls.RemoveAt(0);

            CenterPanel.Controls.Add(page3 = new Page3(page2.GetFolder()) { Dock = DockStyle.Fill });

            page3.Changed += new ChangedEventHandler(UpdateChanged);

        }

        private void UpdateChanged(object sender, ProcessEvent e)
        {
            if (e.state == State.Started)
            {
                PrevButton.Enabled = false;
                NextButton.Enabled = false;
                CancelButton.Enabled = false;
                HelpButton.Enabled = false;
                FootPanel.Enabled = false;
            }
            else if (e.state == State.Finished)
            {
                FootPanel.Enabled = true;
                NextButton.Enabled = true;
            }
        }

        private void Fin()
        {

            LeftPanel.BackgroundImage = Properties.Resources.update_data_4;

            CenterPanel.Controls.RemoveAt(0);

            NextButton.Text = "Finalizar";
            NextButton.Enabled = true;

            CenterPanel.Controls.Add(page4 = new Page4() { Dock = DockStyle.Fill });

        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            switch (index)
            {
                case 1:
                    index--;
                    PrevButton.Enabled = false;
                    NextButton.Enabled = true;
                    Presentación();
                    break;
                case 2:
                    index--;
                    Seleccionar();
                    break;
                case 3:
                    index--;
                    NextButton.Enabled = true;
                    Actualizar();
                    break;
            }

        }

        private void ActivateButton(Button bt){
            bt.BackColor = Color.LightSteelBlue;

        }


        private void DeactivateButton(Button bt)
        {
            bt.BackColor = Color.Azure;

        }

        private void FootPanel_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new Pen(Color.Gray, 1);
            Graphics g = e.Graphics;
            g.DrawLine(blackpen, 0, 1, Width, 1);
            g.Dispose();

        }


    }
}
