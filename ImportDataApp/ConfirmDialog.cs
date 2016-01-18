using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;

namespace WizardDatos
{
    public enum moveResult { Undefined, Overwrite, Keep, Both, Cancel};

    partial class ConfirmDialog : Form
    {

        private moveResult MyResult;

        private FileInfo file;
        String newFile;
        String target;

        public moveResult ConfirmResult
        {
            get { return MyResult; }
        }

        public Boolean ApplyToAll
        {
            get { return checkBox1.Checked; }
        }


        public ConfirmDialog(FileInfo file, String newFile, String target)
        {
            this.file = file;
            this.newFile = newFile;
            this.target = target;


            InitializeComponent();

            askFile1.Clicked += new ClickEventHandler(AskFile1Clicked);
            askFile2.Clicked += new ClickEventHandler(AskFile2Clicked);
            moveAndKeep1.Clicked += new ClickEventHandler(MoveAndKeepClicked);
        }

        private void MoveAndKeepClicked(object sender, EventArgs e)
        {
            MyResult = moveResult.Both;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void AskFile2Clicked(object sender, EventArgs e)
        {
            MyResult = moveResult.Keep;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void AskFile1Clicked(object sender, EventArgs e)
        {
            MyResult = moveResult.Overwrite;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void ConfirmDialog_Load(object sender, EventArgs e)
        {
            askFile2.Title = "No mover";
            askFile2.SubTitle = "No se cambiará ningún archivo. Conservar este archivo en la carpeta de destino.";
            askFile2.FileName =  file.Name;
            askFile2.FilePath = String.Format("{0} ({1})", Path.GetFileNameWithoutExtension(file.Name), target);
            askFile2.FileSize = String.Format("Tamaño: {0} KB", file.Length);
            askFile2.FileDate = String.Format("Fecha de modificación: {0:d} {0:t}",file.LastWriteTime);

            askFile1.FileName = file.Name;
            askFile1.FilePath = String.Format("{0} ({1})", Path.GetFileNameWithoutExtension(file.Name), file.Directory.FullName);
            askFile1.FileSize = String.Format("Tamaño: {0} KB", file.Length);
            askFile1.FileDate = String.Format("Fecha de modificación: {0:d} {0:t}", file.LastWriteTime);

            moveAndKeep1.RenameFileAs = newFile;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            MyResult = moveResult.Cancel;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }


    }
}
