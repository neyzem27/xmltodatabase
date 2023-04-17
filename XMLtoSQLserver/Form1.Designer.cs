namespace XMLtoSQLserver
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
            this.btnGucluToXML = new System.Windows.Forms.Button();
            this.lblDataWrittenToXML = new System.Windows.Forms.Label();
            this.lblXMLtoDatabase = new System.Windows.Forms.Label();
            this.btnWriteXMLtoDatabase = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnBigpoint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGucluToXML
            // 
            this.btnGucluToXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGucluToXML.Location = new System.Drawing.Point(12, 84);
            this.btnGucluToXML.Name = "btnGucluToXML";
            this.btnGucluToXML.Size = new System.Drawing.Size(143, 23);
            this.btnGucluToXML.TabIndex = 0;
            this.btnGucluToXML.Text = "XML GÜÇLÜ";
            this.btnGucluToXML.UseVisualStyleBackColor = true;
            this.btnGucluToXML.Click += new System.EventHandler(this.btnWriteToXML_Click);
            // 
            // lblDataWrittenToXML
            // 
            this.lblDataWrittenToXML.AutoSize = true;
            this.lblDataWrittenToXML.Location = new System.Drawing.Point(12, 166);
            this.lblDataWrittenToXML.Name = "lblDataWrittenToXML";
            this.lblDataWrittenToXML.Size = new System.Drawing.Size(0, 13);
            this.lblDataWrittenToXML.TabIndex = 1;
            this.lblDataWrittenToXML.Visible = false;
            // 
            // lblXMLtoDatabase
            // 
            this.lblXMLtoDatabase.AutoSize = true;
            this.lblXMLtoDatabase.Location = new System.Drawing.Point(12, 89);
            this.lblXMLtoDatabase.Name = "lblXMLtoDatabase";
            this.lblXMLtoDatabase.Size = new System.Drawing.Size(0, 13);
            this.lblXMLtoDatabase.TabIndex = 2;
            // 
            // btnWriteXMLtoDatabase
            // 
            this.btnWriteXMLtoDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnWriteXMLtoDatabase.Location = new System.Drawing.Point(12, 49);
            this.btnWriteXMLtoDatabase.Name = "btnWriteXMLtoDatabase";
            this.btnWriteXMLtoDatabase.Size = new System.Drawing.Size(169, 23);
            this.btnWriteXMLtoDatabase.TabIndex = 3;
            this.btnWriteXMLtoDatabase.Text = "XML KADIOĞLU";
            this.btnWriteXMLtoDatabase.UseVisualStyleBackColor = true;
            this.btnWriteXMLtoDatabase.Click += new System.EventHandler(this.btnWriteXMLtoDatabase_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(15, 13);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(255, 20);
            this.txtFilePath.TabIndex = 4;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBrowse.Location = new System.Drawing.Point(288, 13);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(112, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Xml dosyayı seç";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 209);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(399, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // btnBigpoint
            // 
            this.btnBigpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBigpoint.Location = new System.Drawing.Point(12, 123);
            this.btnBigpoint.Name = "btnBigpoint";
            this.btnBigpoint.Size = new System.Drawing.Size(143, 23);
            this.btnBigpoint.TabIndex = 7;
            this.btnBigpoint.Text = "XML BİGPOİNT";
            this.btnBigpoint.UseVisualStyleBackColor = true;
            this.btnBigpoint.Click += new System.EventHandler(this.BtnBigpoint_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 234);
            this.Controls.Add(this.btnBigpoint);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnWriteXMLtoDatabase);
            this.Controls.Add(this.lblXMLtoDatabase);
            this.Controls.Add(this.lblDataWrittenToXML);
            this.Controls.Add(this.btnGucluToXML);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGucluToXML;
        private System.Windows.Forms.Label lblDataWrittenToXML;
        private System.Windows.Forms.Label lblXMLtoDatabase;
        private System.Windows.Forms.Button btnWriteXMLtoDatabase;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnBigpoint;
    }
}

