namespace WizardDatos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FootPanel = new System.Windows.Forms.Panel();
            this.PrevButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.CenterPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.FootPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.27711F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.72289F));
            this.tableLayoutPanel1.Controls.Add(this.FootPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CenterPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LeftPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(664, 330);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FootPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.FootPanel, 2);
            this.FootPanel.Controls.Add(this.PrevButton);
            this.FootPanel.Controls.Add(this.NextButton);
            this.FootPanel.Controls.Add(this.CancelButton);
            this.FootPanel.Controls.Add(this.HelpButton);
            this.FootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FootPanel.Location = new System.Drawing.Point(3, 283);
            this.FootPanel.Name = "FootPanel";
            this.FootPanel.Size = new System.Drawing.Size(658, 44);
            this.FootPanel.TabIndex = 0;
            this.FootPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FootPanel_Paint);
            // 
            // PrevButton
            // 
            this.PrevButton.Enabled = false;
            this.PrevButton.Location = new System.Drawing.Point(328, 10);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(75, 23);
            this.PrevButton.TabIndex = 3;
            this.PrevButton.Text = "< &Atrás";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(405, 10);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 2;
            this.NextButton.Text = "Siguien&te >";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(491, 10);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancelar";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(575, 10);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(75, 23);
            this.HelpButton.TabIndex = 0;
            this.HelpButton.Text = "Ayuda";
            this.HelpButton.UseVisualStyleBackColor = true;
            // 
            // LeftPanel
            // 
            this.LeftPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LeftPanel.BackgroundImage = global::WizardDatos.Properties.Resources.update_data_1;
            this.LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LeftPanel.Location = new System.Drawing.Point(3, 9);
            this.LeftPanel.Name = "LeftPanel";
            this.tableLayoutPanel1.SetRowSpan(this.LeftPanel, 2);
            this.LeftPanel.Size = new System.Drawing.Size(121, 262);
            this.LeftPanel.TabIndex = 1;
            // 
            // CenterPanel
            // 
            this.CenterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CenterPanel.Location = new System.Drawing.Point(131, 3);
            this.CenterPanel.Name = "CenterPanel";
            this.tableLayoutPanel1.SetRowSpan(this.CenterPanel, 2);
            this.CenterPanel.Size = new System.Drawing.Size(530, 274);
            this.CenterPanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 330);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Importación de ficheros";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.FootPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel FootPanel;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel CenterPanel;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button HelpButton;

    }
}

