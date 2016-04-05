namespace ProjectManager
{
    partial class ManForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textProjectName = new System.Windows.Forms.TextBox();
            this.textCompanyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.projectHomeDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCreateProject = new System.Windows.Forms.Button();
            this.flag_CreateTorsionProj = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.textHomeFolder = new System.Windows.Forms.TextBox();
            this.textRootFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSelectRootFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelectHomeFolder = new System.Windows.Forms.Button();
            this.projectRootDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // textProjectName
            // 
            this.textProjectName.Location = new System.Drawing.Point(85, 6);
            this.textProjectName.Name = "textProjectName";
            this.textProjectName.Size = new System.Drawing.Size(240, 20);
            this.textProjectName.TabIndex = 0;
            // 
            // textCompanyName
            // 
            this.textCompanyName.Location = new System.Drawing.Point(85, 32);
            this.textCompanyName.Name = "textCompanyName";
            this.textCompanyName.Size = new System.Drawing.Size(240, 20);
            this.textCompanyName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project name";
            // 
            // buttonCreateProject
            // 
            this.buttonCreateProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateProject.Location = new System.Drawing.Point(5, 186);
            this.buttonCreateProject.Name = "buttonCreateProject";
            this.buttonCreateProject.Size = new System.Drawing.Size(99, 23);
            this.buttonCreateProject.TabIndex = 7;
            this.buttonCreateProject.Text = "Create Project";
            this.buttonCreateProject.UseVisualStyleBackColor = true;
            this.buttonCreateProject.Click += new System.EventHandler(this.buttonCreateProject_Click);
            // 
            // flag_CreateTorsionProj
            // 
            this.flag_CreateTorsionProj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flag_CreateTorsionProj.AutoSize = true;
            this.flag_CreateTorsionProj.Checked = true;
            this.flag_CreateTorsionProj.CheckState = System.Windows.Forms.CheckState.Checked;
            this.flag_CreateTorsionProj.Location = new System.Drawing.Point(10, 164);
            this.flag_CreateTorsionProj.Name = "flag_CreateTorsionProj";
            this.flag_CreateTorsionProj.Size = new System.Drawing.Size(131, 17);
            this.flag_CreateTorsionProj.TabIndex = 6;
            this.flag_CreateTorsionProj.Text = "Create Torsion Project";
            this.flag_CreateTorsionProj.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Company name";
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(3, 211);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(35, 13);
            this.labelTotal.TabIndex = 6;
            this.labelTotal.Text = "label3";
            this.labelTotal.Visible = false;
            // 
            // textHomeFolder
            // 
            this.textHomeFolder.Location = new System.Drawing.Point(85, 133);
            this.textHomeFolder.Name = "textHomeFolder";
            this.textHomeFolder.Size = new System.Drawing.Size(240, 20);
            this.textHomeFolder.TabIndex = 4;
            // 
            // textRootFolder
            // 
            this.textRootFolder.Location = new System.Drawing.Point(85, 83);
            this.textRootFolder.Name = "textRootFolder";
            this.textRootFolder.Size = new System.Drawing.Size(240, 20);
            this.textRootFolder.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Torque 2D root folder";
            // 
            // buttonSelectRootFolder
            // 
            this.buttonSelectRootFolder.Location = new System.Drawing.Point(5, 81);
            this.buttonSelectRootFolder.Name = "buttonSelectRootFolder";
            this.buttonSelectRootFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectRootFolder.TabIndex = 3;
            this.buttonSelectRootFolder.Text = "Select folder";
            this.buttonSelectRootFolder.UseVisualStyleBackColor = true;
            this.buttonSelectRootFolder.Click += new System.EventHandler(this.buttonSelectRootFolder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Your project home folder";
            // 
            // buttonSelectHomeFolder
            // 
            this.buttonSelectHomeFolder.Location = new System.Drawing.Point(5, 131);
            this.buttonSelectHomeFolder.Name = "buttonSelectHomeFolder";
            this.buttonSelectHomeFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectHomeFolder.TabIndex = 5;
            this.buttonSelectHomeFolder.Text = "Select folder";
            this.buttonSelectHomeFolder.UseVisualStyleBackColor = true;
            this.buttonSelectHomeFolder.Click += new System.EventHandler(this.buttonSelectHomeFolder_Click);
            // 
            // ManForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 229);
            this.Controls.Add(this.buttonSelectHomeFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonSelectRootFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textHomeFolder);
            this.Controls.Add(this.textRootFolder);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flag_CreateTorsionProj);
            this.Controls.Add(this.buttonCreateProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCompanyName);
            this.Controls.Add(this.textProjectName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Manager for Torque 2D by yurembo";
            this.Load += new System.EventHandler(this.ManForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textProjectName;
        private System.Windows.Forms.TextBox textCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog projectHomeDialog;
        private System.Windows.Forms.Button buttonCreateProject;
        private System.Windows.Forms.CheckBox flag_CreateTorsionProj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.TextBox textHomeFolder;
        private System.Windows.Forms.TextBox textRootFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSelectRootFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSelectHomeFolder;
        private System.Windows.Forms.FolderBrowserDialog projectRootDialog;
    }
}

