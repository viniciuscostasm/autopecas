namespace SystemPecAuto
{
    partial class frmSubCategoria
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDadosSubCategoria = new System.Windows.Forms.Button();
            this.btnAtualizarSubCategoria = new System.Windows.Forms.Button();
            this.btnDeleteSubCategoria = new System.Windows.Forms.Button();
            this.btnSalvarSubCategoria = new System.Windows.Forms.Button();
            this.btnNovoSubCategoria = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBCategoria = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubCategoria = new System.Windows.Forms.TextBox();
            this.txtCategoriaID = new System.Windows.Forms.TextBox();
            this.txtSubCategoriaID = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDadosSubCategoria);
            this.panel1.Controls.Add(this.btnAtualizarSubCategoria);
            this.panel1.Controls.Add(this.btnDeleteSubCategoria);
            this.panel1.Controls.Add(this.btnSalvarSubCategoria);
            this.panel1.Controls.Add(this.btnNovoSubCategoria);
            this.panel1.Location = new System.Drawing.Point(25, 157);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 62);
            this.panel1.TabIndex = 3;
            // 
            // btnDadosSubCategoria
            // 
            this.btnDadosSubCategoria.Location = new System.Drawing.Point(309, 14);
            this.btnDadosSubCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDadosSubCategoria.Name = "btnDadosSubCategoria";
            this.btnDadosSubCategoria.Size = new System.Drawing.Size(68, 32);
            this.btnDadosSubCategoria.TabIndex = 4;
            this.btnDadosSubCategoria.Text = "&Dados";
            this.btnDadosSubCategoria.UseVisualStyleBackColor = true;
            this.btnDadosSubCategoria.Click += new System.EventHandler(this.btnDadosSubCategoria_Click);
            // 
            // btnAtualizarSubCategoria
            // 
            this.btnAtualizarSubCategoria.Enabled = false;
            this.btnAtualizarSubCategoria.Location = new System.Drawing.Point(235, 14);
            this.btnAtualizarSubCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAtualizarSubCategoria.Name = "btnAtualizarSubCategoria";
            this.btnAtualizarSubCategoria.Size = new System.Drawing.Size(68, 32);
            this.btnAtualizarSubCategoria.TabIndex = 3;
            this.btnAtualizarSubCategoria.Text = "&Atualizar";
            this.btnAtualizarSubCategoria.UseVisualStyleBackColor = true;
            this.btnAtualizarSubCategoria.Click += new System.EventHandler(this.btnAtualizarSubCategoria_Click);
            // 
            // btnDeleteSubCategoria
            // 
            this.btnDeleteSubCategoria.Enabled = false;
            this.btnDeleteSubCategoria.Location = new System.Drawing.Point(161, 14);
            this.btnDeleteSubCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteSubCategoria.Name = "btnDeleteSubCategoria";
            this.btnDeleteSubCategoria.Size = new System.Drawing.Size(68, 32);
            this.btnDeleteSubCategoria.TabIndex = 2;
            this.btnDeleteSubCategoria.Text = "&Deletar";
            this.btnDeleteSubCategoria.UseVisualStyleBackColor = true;
            this.btnDeleteSubCategoria.Click += new System.EventHandler(this.btnDeleteSubCategoria_Click);
            // 
            // btnSalvarSubCategoria
            // 
            this.btnSalvarSubCategoria.Location = new System.Drawing.Point(87, 14);
            this.btnSalvarSubCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSalvarSubCategoria.Name = "btnSalvarSubCategoria";
            this.btnSalvarSubCategoria.Size = new System.Drawing.Size(68, 32);
            this.btnSalvarSubCategoria.TabIndex = 1;
            this.btnSalvarSubCategoria.Text = "&Salvar";
            this.btnSalvarSubCategoria.UseVisualStyleBackColor = true;
            this.btnSalvarSubCategoria.Click += new System.EventHandler(this.btnSalvarSubCategoria_Click);
            // 
            // btnNovoSubCategoria
            // 
            this.btnNovoSubCategoria.Location = new System.Drawing.Point(13, 14);
            this.btnNovoSubCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNovoSubCategoria.Name = "btnNovoSubCategoria";
            this.btnNovoSubCategoria.Size = new System.Drawing.Size(68, 32);
            this.btnNovoSubCategoria.TabIndex = 0;
            this.btnNovoSubCategoria.Text = "&Novo";
            this.btnNovoSubCategoria.UseVisualStyleBackColor = true;
            this.btnNovoSubCategoria.Click += new System.EventHandler(this.btnNovoSubCategoria_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBCategoria);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSubCategoria);
            this.groupBox1.Location = new System.Drawing.Point(25, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(396, 123);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sub Categoria Informação";
            // 
            // comboBCategoria
            // 
            this.comboBCategoria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBCategoria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBCategoria.FormattingEnabled = true;
            this.comboBCategoria.Location = new System.Drawing.Point(143, 74);
            this.comboBCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBCategoria.Name = "comboBCategoria";
            this.comboBCategoria.Size = new System.Drawing.Size(235, 25);
            this.comboBCategoria.TabIndex = 3;
            this.comboBCategoria.SelectedIndexChanged += new System.EventHandler(this.comboBCategoria_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Categoria";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sub Categoria";
            // 
            // txtSubCategoria
            // 
            this.txtSubCategoria.Location = new System.Drawing.Point(143, 36);
            this.txtSubCategoria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSubCategoria.Name = "txtSubCategoria";
            this.txtSubCategoria.Size = new System.Drawing.Size(235, 24);
            this.txtSubCategoria.TabIndex = 0;
            // 
            // txtCategoriaID
            // 
            this.txtCategoriaID.Location = new System.Drawing.Point(123, -4);
            this.txtCategoriaID.Name = "txtCategoriaID";
            this.txtCategoriaID.Size = new System.Drawing.Size(143, 24);
            this.txtCategoriaID.TabIndex = 5;
            this.txtCategoriaID.Visible = false;
            // 
            // txtSubCategoriaID
            // 
            this.txtSubCategoriaID.Location = new System.Drawing.Point(272, -4);
            this.txtSubCategoriaID.Name = "txtSubCategoriaID";
            this.txtSubCategoriaID.Size = new System.Drawing.Size(143, 24);
            this.txtSubCategoriaID.TabIndex = 4;
            this.txtSubCategoriaID.Visible = false;
            // 
            // frmSubCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(447, 234);
            this.Controls.Add(this.txtSubCategoriaID);
            this.Controls.Add(this.txtCategoriaID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSubCategoria";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerenciamento de Sub Categoria";
            this.Load += new System.EventHandler(this.frmSubCategoria_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnDadosSubCategoria;
        public System.Windows.Forms.Button btnAtualizarSubCategoria;
        public System.Windows.Forms.Button btnDeleteSubCategoria;
        public System.Windows.Forms.Button btnSalvarSubCategoria;
        public System.Windows.Forms.Button btnNovoSubCategoria;
        public System.Windows.Forms.TextBox txtCategoriaID;
        public System.Windows.Forms.TextBox txtSubCategoriaID;
        public System.Windows.Forms.ComboBox comboBCategoria;
        public System.Windows.Forms.TextBox txtSubCategoria;

    }
}