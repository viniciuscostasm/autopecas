using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SystemPecAuto
{
    public partial class frmLogin : Form
    {
        ConnectionString cs = new ConnectionString();
        mdiPrincipal mdi = new mdiPrincipal();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (NivelAcessoComboBox.Text == "")
            {
                MessageBox.Show("Selecione o nível de acesso do usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NivelAcessoComboBox.Focus();
                return;
            }
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Informe o nome do usuario para login", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
                return;
            }
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Informe a senha do usuario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
                return;
            }
            try
            {
                SqlConnection connection = default(SqlConnection);
                connection = new SqlConnection(cs.DBConn);
                SqlCommand cmd = default(SqlCommand);

                cmd = new SqlCommand("SELECT UserType,Username,password FROM Registration WHERE UserType = @usertype AND Username = @username AND password = @UserPassword", connection);
                SqlParameter uType = new SqlParameter("@usertype", SqlDbType.NChar);
                SqlParameter uName = new SqlParameter("@username", SqlDbType.NChar);
                SqlParameter uPassword = new SqlParameter("@UserPassword", SqlDbType.NChar);

                uType.Value = NivelAcessoComboBox.Text;
                uName.Value = txtUsuario.Text;
                uPassword.Value = txtSenha.Text;

                cmd.Parameters.Add(uType);
                cmd.Parameters.Add(uName);
                cmd.Parameters.Add(uPassword);

                cmd.Connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (myReader.Read() == true)
                {
                    int i;
                    progressBar1.Visible = true;
                    progressBar1.Maximum = 5000;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 4;
                    progressBar1.Step = 1;

                    for (i = 0; i <= 5000; i++)
                    {
                        progressBar1.PerformStep();
                    }
                    if (NivelAcessoComboBox.Text == "GERENTE")
                    {
                        this.Hide();
                        mdi.cadastrosToolStripMenuItem.Enabled = false;
                        mdi.categoriaToolStripMenuItem.Enabled = false;
                        mdi.subCategoriaToolStripMenuItem.Enabled = false;
                        mdi.produtoToolStripMenuItem.Enabled = false;

                        mdi.usuáriosToolStripMenuItem.Enabled = false;
                        mdi.registroToolStripMenuItem.Enabled = false;
                        mdi.loginDetalhesToolStripMenuItem.Enabled = false;

                        mdi.produtosToolStripMenuItem.Enabled = false;
                        mdi.estoqueToolStripMenuItem.Enabled = false;
                        mdi.vendaToolStripMenuItem.Enabled = false;

                        mdi.estoquesToolStripMenuItem.Enabled = false;
                        mdi.produtosToolStripMenuItem1.Enabled = false;
                        mdi.vendasToolStripMenuItem.Enabled = true;

                        mdi.registrosToolStripMenuItem.Enabled = false;
                        mdi.produtosToolStripMenuItem2.Enabled = false;
                        mdi.estoqueToolStripMenuItem1.Enabled = false;
                        mdi.estoqueInfoToolStripMenuItem.Enabled = false;
                        mdi.faturamentoToolStripMenuItem.Enabled = false;

                        mdi.lblUsuario.Text = txtUsuario.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.Show();
                    }
                    if (NivelAcessoComboBox.Text == "ADMIN")
                    {
                        this.Hide();
                        mdi.cadastrosToolStripMenuItem.Enabled = true;
                        mdi.categoriaToolStripMenuItem.Enabled = true;
                        mdi.subCategoriaToolStripMenuItem.Enabled = true;
                        mdi.produtoToolStripMenuItem.Enabled = true;

                        mdi.usuáriosToolStripMenuItem.Enabled = true;
                        mdi.registroToolStripMenuItem.Enabled = true;
                        mdi.loginDetalhesToolStripMenuItem.Enabled = true;

                        mdi.produtosToolStripMenuItem.Enabled = true;
                        mdi.estoqueToolStripMenuItem.Enabled = true;
                        mdi.vendaToolStripMenuItem.Enabled = true;

                        mdi.estoquesToolStripMenuItem.Enabled = true;
                        mdi.produtosToolStripMenuItem1.Enabled = true;
                        mdi.vendasToolStripMenuItem.Enabled = true;

                        mdi.registrosToolStripMenuItem.Enabled = true;
                        mdi.produtosToolStripMenuItem2.Enabled = true;
                        mdi.estoqueToolStripMenuItem1.Enabled = true;
                        mdi.estoqueInfoToolStripMenuItem.Enabled = true;
                        mdi.faturamentoToolStripMenuItem.Enabled = true;

                        mdi.lblUsuario.Text = txtUsuario.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.Show();
                    }

                    if (NivelAcessoComboBox.Text == "VENDEDOR")
                    {
                        this.Hide();
                        mdi.cadastrosToolStripMenuItem.Enabled = false;
                        mdi.categoriaToolStripMenuItem.Enabled = false;
                        mdi.subCategoriaToolStripMenuItem.Enabled = false;
                        mdi.produtoToolStripMenuItem.Enabled = false;

                        mdi.usuáriosToolStripMenuItem.Enabled = false;
                        mdi.registroToolStripMenuItem.Enabled = false;
                        mdi.loginDetalhesToolStripMenuItem.Enabled = false;

                        mdi.produtosToolStripMenuItem.Enabled = false;
                        mdi.estoqueToolStripMenuItem.Enabled = false;
                        mdi.vendaToolStripMenuItem.Enabled = true;

                        mdi.estoquesToolStripMenuItem.Enabled = true;
                        mdi.produtosToolStripMenuItem1.Enabled = true;
                        mdi.vendasToolStripMenuItem.Enabled = false;

                        mdi.registrosToolStripMenuItem.Enabled = false;
                        mdi.produtosToolStripMenuItem2.Enabled = false;
                        mdi.estoqueToolStripMenuItem1.Enabled = false;
                        mdi.estoqueInfoToolStripMenuItem.Enabled = true;
                        mdi.faturamentoToolStripMenuItem.Enabled = true;

                        mdi.lblUsuario.Text = txtUsuario.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.Show();
                    }
                    if (NivelAcessoComboBox.Text == "SUPERVISOR")
                    {
                        this.Hide();
                        mdi.cadastrosToolStripMenuItem.Enabled = true;
                        mdi.categoriaToolStripMenuItem.Enabled = false;
                        mdi.subCategoriaToolStripMenuItem.Enabled = false;
                        mdi.produtoToolStripMenuItem.Enabled = true;

                        mdi.usuáriosToolStripMenuItem.Enabled = false;
                        mdi.registroToolStripMenuItem.Enabled = false;
                        mdi.loginDetalhesToolStripMenuItem.Enabled = false;

                        mdi.produtosToolStripMenuItem.Enabled = true;
                        mdi.estoqueToolStripMenuItem.Enabled = true;
                        mdi.vendaToolStripMenuItem.Enabled = false;

                        mdi.estoquesToolStripMenuItem.Enabled = true;
                        mdi.produtosToolStripMenuItem1.Enabled = true;
                        mdi.vendasToolStripMenuItem.Enabled = true;

                        mdi.registrosToolStripMenuItem.Enabled = false;
                        mdi.produtosToolStripMenuItem2.Enabled = true;
                        mdi.estoqueToolStripMenuItem1.Enabled = true;
                        mdi.estoqueInfoToolStripMenuItem.Enabled = true;
                        mdi.faturamentoToolStripMenuItem.Enabled = false;

                        mdi.lblUsuario.Text = txtUsuario.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.UsuarioTipo.Text = NivelAcessoComboBox.Text;
                        mdi.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Login falhou...Tente Novamente !", "login Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Clear();
                    txtSenha.Clear();
                    txtUsuario.Focus();
                }
                if (connection.State == ConnectionState.Open)
                {
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            NivelAcessoComboBox.Focus();
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmMudancaSenha frm = new frmMudancaSenha();
            frm.Show();
            frm.txtNomeUsuario.Text = "";
            frm.txtNovaSenha.Text = "";
            frm.txtSenhaAnterior.Text = "";
            frm.txtConfirmSenha.Text = "";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmRecuperarSenha frm = new frmRecuperarSenha();
            frm.txtEmail.Focus();
            frm.Show();
        }
    }
}
