using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WizardDatos
{

    public partial class Page3 : UserControl
    {
        public event ChangedEventHandler Changed;

        enum copyType {file, folder};

        enum copyResult { Success = 0, Renamed, Error, Exist, Unhautorized };

        private struct progressdata
        {
            public copyType type;
        };

        String targetPath;
        String sourcePath;
        Thread t1;
        private static int i = 0;
        private int iFiles = 0;
        private int iFolders = 0;

        private int iFilesOk = 0;
        private int iFoldersOk = 0;

        private bool Canceled = false;
        private moveResult MoveResult = moveResult.Undefined;

        private StreamWriter w;

        public Page3(String sourcePath)
        {
            targetPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\BS\BS Online 2014\Datos";
            this.sourcePath = sourcePath;

            InitializeComponent();

            textBox1.Text = sourcePath;
            textBox4.Text = targetPath;
        }

        protected virtual void OnChanged(ProcessEvent e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        private void Page3_Paint(object sender, PaintEventArgs e)
        {
            Pen blackpen = new Pen(Color.Gray, 1);
            Graphics g = e.Graphics;
            g.DrawLine(blackpen, 0, label5.Top - 10, Width, label5.Top - 10);
            g.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            button1.Enabled = false;

            w = File.CreateText("log.html");
            w.WriteLine("<!DOCTYPE html>");
            w.WriteLine("<html>");
            w.WriteLine("<body>");
            w.WriteLine("<table border='1'>");
            w.WriteLine("<tr>");
            w.WriteLine("<th>file name</>");
            w.WriteLine("<th>file size</>");
            w.WriteLine("<th>file modified</>");
            w.WriteLine("<th>result</>");
            w.WriteLine("</tr>");

            backgroundWorker2.RunWorkerAsync();
        }

        private void Log(FileInfo file, String target, copyResult result = copyResult.Success)
        {

            switch (result)
            {
                case copyResult.Success:
                    {
                        w.WriteLine("<tr><td>{0}</td><td>{1}KB</td><td>{2:d} {2:t}</td><td style='color: green;'>Correcto</td></tr>", target, file.Length, file.LastWriteTime);
                        break;
                    }
                case copyResult.Renamed:
                    {
                        w.WriteLine("<tr><td>{0}</td><td>{1}KB</td><td>{2:d} {2:t}</td><td style='color: orange;'>Renombrado</td></tr>", target, file.Length, file.LastWriteTime);
                        break;
                    }
                case copyResult.Error:
                    {
                        w.WriteLine("<tr><td>{0}</td><td>{1}KB</td><td>{2:d} {2:t}</td><td style='color: red;'>Error</></tr>", target, file.Length, file.LastWriteTime);
                        break;
                    }
                case copyResult.Unhautorized:
                    {
                        w.WriteLine("<tr><td>{0}</td><td>{1}KB</td><td>{2:d} {2:t}</td><td style='color: red;'>Permiso denegado</></tr>", target, file.Length, file.LastWriteTime);
                        break;
                    }
                case copyResult.Exist:
                    {
                        w.WriteLine("<tr><td>{0}</td><td>{1}KB</td><td>{2:d} {2:t}</td><td style='color: blue;'>Omitir</></tr>", target, file.Length, file.LastWriteTime);
                        break;
                    }
            }
        }



        private void CopyData()
        {
            DirectoryCopy(sourcePath, targetPath, true);
        }

        private delegate void InvokeDelegate(Form parent);

        public bool AskForConfirmation(Form parent)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }

            return false;

        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);

                try
                {
                    file.CopyTo(temppath, false);
                    Log(file, temppath);
                }
                catch (System.UnauthorizedAccessException)
                {
                    Log(file,  temppath, copyResult.Unhautorized);
                }
                catch (System.IO.IOException e)
                {
                    String newFile = RenameFile(file, temppath);
                    if (MoveResult == moveResult.Undefined)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            var sfd = new ConfirmDialog(file, Path.GetFileName(newFile), temppath);
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                if (sfd.ApplyToAll)
                                {
                                    MoveResult = sfd.ConfirmResult;
                                }

                                if (sfd.ConfirmResult == moveResult.Overwrite)
                                {
                                    try
                                    {
                                        file.CopyTo(temppath, true);
                                        Log(file, temppath);
                                    }
                                    catch (System.UnauthorizedAccessException)
                                    {
                                        Log(file, temppath, copyResult.Unhautorized);
                                    }
                                }
                                else if (sfd.ConfirmResult == moveResult.Both)
                                {
                                    file.CopyTo(newFile, true);
                                    Log(file, newFile, copyResult.Renamed);
                                }
                                else
                                {
                                    Log(file, temppath, copyResult.Exist);
                                }
                            
                            }
                            else
                            {
                                backgroundWorker1.CancelAsync();
                                Canceled = true;
                            }
                                                

                        });
                    }
                    else
                    {
                        if (MoveResult == moveResult.Overwrite)
                        {
                            try
                            {
                                file.CopyTo(temppath, true);
                                Log(file, temppath);
                            }
                            catch (System.UnauthorizedAccessException)
                            {
                                Log(file, temppath, copyResult.Unhautorized);
                            }
                        }
                        else if (MoveResult == moveResult.Both)
                        {
                            file.CopyTo(newFile, true);
                            Log(file,newFile, copyResult.Renamed);
                        }
                        else
                        {
                            Log(file, temppath, copyResult.Exist);
                        }
                    }                

                    if (Canceled) break;
                }
            }
                
            

            if (Canceled) return;


            progressdata data = new progressdata();
            data.type = copyType.file;
            backgroundWorker1.ReportProgress(iFilesOk++, data);

            
        
            progressdata data2 = new progressdata();
            data2.type = copyType.folder;
            backgroundWorker1.ReportProgress(iFoldersOk++, data2);

            // If copying subdirectories, copy them and their contents to new location.
            if (!Canceled && copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);

                }
            }
        }

        private String RenameFile(FileInfo file, String targetFolder)
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(file.Name);
            string extension = Path.GetExtension(file.Name);
            string path = Path.GetDirectoryName(targetFolder);
            string newFullPath = targetFolder;

            while (File.Exists(newFullPath))
            {
                String tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }

            return newFullPath;

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryCopy(sourcePath, targetPath, true);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (iFoldersOk <= 1)
            {
                OnChanged(new ProcessEvent() { state = State.Started });
            }

            progressdata data = (progressdata)e.UserState;

            if (data.type == copyType.folder)
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            else
            {
                progressBar2.Value = e.ProgressPercentage;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Finished();
        }

        private void Finished()
        {
            label6.Text = "Proceso de copia finalizado";

            OnChanged(new ProcessEvent() { state = State.Finished});

            w.WriteLine("</table></body></html>");
            w.Flush();
            w.Close();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            PrepareDirectoryCopy(sourcePath, true);
        }

        private void PrepareDirectoryCopy(string sourceDirName, bool copySubDirs)
        {

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();


            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();

            iFiles += files.Length;

            iFolders++;

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    PrepareDirectoryCopy(subdir.FullName, copySubDirs);
                }
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Maximum = iFolders-1;
            progressBar2.Maximum = iFiles-1;

            iFoldersOk = 0;
            iFilesOk = 0;

            backgroundWorker1.RunWorkerAsync();
        }

    }
}
