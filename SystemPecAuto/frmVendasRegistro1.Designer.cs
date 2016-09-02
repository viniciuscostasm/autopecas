namespace SystemPecAuto
{
    partial class frmVendasRegistro1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDataFaturaAte = new System.Windows.Forms.DateTimePicker();
            this.dtpDataFaturaDe = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnObterDados = new System.Windows.Forms.Button();
            this.btnResetar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTipoUsuario = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDataFaturaAte);
            this.groupBox1.Controls.Add(this.dtpDataFaturaDe);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 87);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data da Fatura";
            // 
            // dtpDataFaturaAte
            // 
            this.dtpDataFaturaAte.CustomFormat = "dd/MMM/yyyy";
            this.dtpDataFaturaAte.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataFaturaAte.Location = new System.Drawing.Point(190, 42);
            this.dtpDataFaturaAte.Name = "dtpDataFaturaAte";
            this.dtpDataFaturaAte.Size = new System.Drawing.Size(120, 24);
            this.dtpDataFaturaAte.TabIndex = 107;
            // 
            // dtpDataFaturaDe
            // 
            this.dtpDataFaturaDe.CustomFormat = "dd/MMM/yyyy";
            this.dtpDataFaturaDe.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataFaturaDe.Location = new System.Drawing.Point(24, 42);
            this.dtpDataFaturaDe.Name = "dtpDataFaturaDe";
            this.dtpDataFaturaDe.Size = new System.Drawing.Size(120, 24);
            this.dtpDataFaturaDe.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "ATÉ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "DE";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportarExcel);
            this.groupBox2.Controls.Add(this.btnObterDados);
            this.groupBox2.Controls.Add(this.btnResetar);
            this.groupBox2.Location = new System.Drawing.Point(358, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 87);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Location = new System.Drawing.Point(218, 26);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(126, 40);
            this.btnExportarExcel.TabIndex = 2;
            this.btnExportarExcel.Text = "&Exportar p/Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnObterDados
            // 
            this.btnObterDados.Location = new System.Drawing.Point(18, 26);
            this.btnObterDados.Name = "btnObterDados";
            this.btnObterDados.Size = new System.Drawing.Size(94, 40);
            this.btnObterDados.TabIndex = 0;
            this.btnObterDados.Text = "&Obter Dados";
            this.btnObterDados.UseVisualStyleBackColor = true;
            this.btnObterDados.Click += new System.EventHandler(this.btnObterDados_Click);
            // 
            // btnResetar
            // 
            this.btnResetar.Location = new System.Drawing.Point(118, 26);
            this.btnResetar.Name = "btnResetar";
            this.btnResetar.Size = new System.Drawing.Size(94, 40);
            this.btnResetar.TabIndex = 1;
            this.btnResetar.Text = "&Resetar";
            this.btnResetar.UseVisualStyleBackColor = true;
            this.btnResetar.Click += new System.EventHandler(this.btnResetar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 98);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(1044, 564);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // lblTipoUsuario
            // 
            this.lblTipoUsuario.AutoSize = true;
            this.lblTipoUsuario.Location = new System.Drawing.Point(768, 32);
            this.lblTipoUsuario.Name = "lblTipoUsuario";
            this.lblTipoUsuario.Size = new System.Drawing.Size(76, 17);
            this.lblTipoUsuario.TabIndex = 118;
            this.lblTipoUsuario.Text = "TipoUsuario";
            this.lblTipoUsuario.Visible = false;
            // 
            // frmVendasRegistro1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1058, 664);
            this.Controls.Add(this.lblTipoUsuario);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmVendasRegistro1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendas Consultas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVendasRegistro1_FormClosing);
            this.Load += new System.EventHandler(this.frmVendasRegistro1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.DateTimePicker dtpDataFaturaDe;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker dtpDataFaturaAte;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.Button btnExportarExcel;
        internal System.Windows.Forms.Button btnObterDados;
        internal System.Windows.Forms.Button btnResetar;
        public System.Windows.Forms.Label lblTipoUsuario;
    }
}