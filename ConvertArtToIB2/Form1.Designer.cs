namespace ConvertArtToIB2
{
    partial class Form1
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
            this.btnCreateAll = new System.Windows.Forms.Button();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.btnSourceFolder = new System.Windows.Forms.Button();
            this.btnTargetFolder = new System.Windows.Forms.Button();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnCreateAll
            // 
            this.btnCreateAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateAll.Location = new System.Drawing.Point(166, 91);
            this.btnCreateAll.Name = "btnCreateAll";
            this.btnCreateAll.Size = new System.Drawing.Size(179, 39);
            this.btnCreateAll.TabIndex = 4;
            this.btnCreateAll.Text = "Create All Tokens";
            this.btnCreateAll.UseVisualStyleBackColor = true;
            this.btnCreateAll.Click += new System.EventHandler(this.btnCreateAll_Click);
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Location = new System.Drawing.Point(85, 17);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.ReadOnly = true;
            this.txtSourceFolder.Size = new System.Drawing.Size(360, 20);
            this.txtSourceFolder.TabIndex = 5;
            // 
            // btnSourceFolder
            // 
            this.btnSourceFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSourceFolder.Location = new System.Drawing.Point(452, 13);
            this.btnSourceFolder.Name = "btnSourceFolder";
            this.btnSourceFolder.Size = new System.Drawing.Size(33, 28);
            this.btnSourceFolder.TabIndex = 6;
            this.btnSourceFolder.Text = "...";
            this.btnSourceFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSourceFolder.UseVisualStyleBackColor = true;
            this.btnSourceFolder.Click += new System.EventHandler(this.btnSourceFolder_Click);
            // 
            // btnTargetFolder
            // 
            this.btnTargetFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTargetFolder.Location = new System.Drawing.Point(452, 48);
            this.btnTargetFolder.Name = "btnTargetFolder";
            this.btnTargetFolder.Size = new System.Drawing.Size(33, 28);
            this.btnTargetFolder.TabIndex = 8;
            this.btnTargetFolder.Text = "...";
            this.btnTargetFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTargetFolder.UseVisualStyleBackColor = true;
            this.btnTargetFolder.Click += new System.EventHandler(this.btnTargetFolder_Click);
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Location = new System.Drawing.Point(85, 52);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.ReadOnly = true;
            this.txtTargetFolder.Size = new System.Drawing.Size(360, 20);
            this.txtTargetFolder.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Target Folder:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 146);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTargetFolder);
            this.Controls.Add(this.txtTargetFolder);
            this.Controls.Add(this.btnSourceFolder);
            this.Controls.Add(this.txtSourceFolder);
            this.Controls.Add(this.btnCreateAll);
            this.Name = "Form1";
            this.Text = "Convert FRUA PCX Combat Tokens to IceBlink 2 Format";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAll;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.Button btnSourceFolder;
        private System.Windows.Forms.Button btnTargetFolder;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
    }
}

