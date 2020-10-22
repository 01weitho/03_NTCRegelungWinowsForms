namespace _03_NTCRegelungWinowsForms
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_aktTemp = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_aktSollwert = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_aktHysterese = new System.Windows.Forms.Label();
            this.cb_aendernWerte = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_aendern = new System.Windows.Forms.TextBox();
            this.btn_aendernWerte = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Ports = new System.Windows.Forms.ComboBox();
            this.btn_Ports = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.PictureBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Recieved = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_sent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "aktuelle Temeratur:";
            // 
            // lbl_aktTemp
            // 
            this.lbl_aktTemp.AutoSize = true;
            this.lbl_aktTemp.Location = new System.Drawing.Point(160, 9);
            this.lbl_aktTemp.Name = "lbl_aktTemp";
            this.lbl_aktTemp.Size = new System.Drawing.Size(0, 17);
            this.lbl_aktTemp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "aktueller Sollwert:";
            // 
            // lbl_aktSollwert
            // 
            this.lbl_aktSollwert.AutoSize = true;
            this.lbl_aktSollwert.Location = new System.Drawing.Point(160, 49);
            this.lbl_aktSollwert.Name = "lbl_aktSollwert";
            this.lbl_aktSollwert.Size = new System.Drawing.Size(0, 17);
            this.lbl_aktSollwert.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "aktuelle Hysterese:";
            // 
            // lbl_aktHysterese
            // 
            this.lbl_aktHysterese.AutoSize = true;
            this.lbl_aktHysterese.Location = new System.Drawing.Point(160, 89);
            this.lbl_aktHysterese.Name = "lbl_aktHysterese";
            this.lbl_aktHysterese.Size = new System.Drawing.Size(0, 17);
            this.lbl_aktHysterese.TabIndex = 5;
            // 
            // cb_aendernWerte
            // 
            this.cb_aendernWerte.FormattingEnabled = true;
            this.cb_aendernWerte.Location = new System.Drawing.Point(15, 155);
            this.cb_aendernWerte.Name = "cb_aendernWerte";
            this.cb_aendernWerte.Size = new System.Drawing.Size(121, 24);
            this.cb_aendernWerte.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "ändern der/des:[°C]";
            // 
            // txb_aendern
            // 
            this.txb_aendern.Location = new System.Drawing.Point(15, 197);
            this.txb_aendern.Name = "txb_aendern";
            this.txb_aendern.Size = new System.Drawing.Size(116, 22);
            this.txb_aendern.TabIndex = 8;
            // 
            // btn_aendernWerte
            // 
            this.btn_aendernWerte.Location = new System.Drawing.Point(15, 226);
            this.btn_aendernWerte.Name = "btn_aendernWerte";
            this.btn_aendernWerte.Size = new System.Drawing.Size(73, 31);
            this.btn_aendernWerte.TabIndex = 9;
            this.btn_aendernWerte.Text = "aendern";
            this.btn_aendernWerte.UseVisualStyleBackColor = true;
            this.btn_aendernWerte.Click += new System.EventHandler(this.btn_aendernWerte_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Com ändern:";
            // 
            // cb_Ports
            // 
            this.cb_Ports.FormattingEnabled = true;
            this.cb_Ports.Location = new System.Drawing.Point(18, 355);
            this.cb_Ports.Name = "cb_Ports";
            this.cb_Ports.Size = new System.Drawing.Size(121, 24);
            this.cb_Ports.TabIndex = 11;
            this.cb_Ports.MouseEnter += new System.EventHandler(this.cb_Ports_Enter);
            // 
            // btn_Ports
            // 
            this.btn_Ports.BackColor = System.Drawing.Color.Red;
            this.btn_Ports.Location = new System.Drawing.Point(18, 386);
            this.btn_Ports.Name = "btn_Ports";
            this.btn_Ports.Size = new System.Drawing.Size(113, 36);
            this.btn_Ports.TabIndex = 12;
            this.btn_Ports.Text = "OPEN";
            this.btn_Ports.UseVisualStyleBackColor = false;
            this.btn_Ports.Click += new System.EventHandler(this.btn_Ports_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(18, 613);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(113, 32);
            this.btn_close.TabIndex = 13;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(222, 13);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(925, 527);
            this.pb.TabIndex = 14;
            this.pb.TabStop = false;
            this.pb.SizeChanged += new System.EventHandler(this.pb_SizeChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 450);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Recieved Data:";
            // 
            // lbl_Recieved
            // 
            this.lbl_Recieved.AutoSize = true;
            this.lbl_Recieved.Location = new System.Drawing.Point(15, 470);
            this.lbl_Recieved.Name = "lbl_Recieved";
            this.lbl_Recieved.Size = new System.Drawing.Size(0, 17);
            this.lbl_Recieved.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 500);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Sent Data:";
            // 
            // lbl_sent
            // 
            this.lbl_sent.AutoSize = true;
            this.lbl_sent.Location = new System.Drawing.Point(15, 520);
            this.lbl_sent.Name = "lbl_sent";
            this.lbl_sent.Size = new System.Drawing.Size(0, 17);
            this.lbl_sent.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 590);
            this.Controls.Add(this.lbl_sent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbl_Recieved);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_Ports);
            this.Controls.Add(this.cb_Ports);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_aendernWerte);
            this.Controls.Add(this.txb_aendern);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_aendernWerte);
            this.Controls.Add(this.lbl_aktHysterese);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_aktSollwert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_aktTemp);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_aktTemp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_aktSollwert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_aktHysterese;
        private System.Windows.Forms.ComboBox cb_aendernWerte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_aendern;
        private System.Windows.Forms.Button btn_aendernWerte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_Ports;
        private System.Windows.Forms.Button btn_Ports;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.PictureBox pb;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Recieved;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_sent;
    }
}

