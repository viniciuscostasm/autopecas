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
    public partial class frmMudancaSenha : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        public frmMudancaSenha()
        {
            InitializeComponent();
        }

        private void frmMudancaSenha_Load(object sender, EventArgs e)
        {

        }

        private void frmMudancaSenha_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.NivelAcessoComboBox.SelectedIndex = -1;
            frm.txtUsuario.Text = "";
            frm.txtSenha.Text = "";
            frm.progressBar1.Visible = false;
            frm.NivelAcessoComboBox.Focus();
            frm.Show();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            try
            {
                int registrosAfetados = 0;
                if ((txtNomeUsuario.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Informe o login do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomeUsuario.Focus();
                    return;
                }
                if ((txtSenhaAnterior.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Informe a senha Anterior", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenhaAnterior.Focus();
                    return;
                }
                if ((txtNovaSenha.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Informe a nova senha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNovaSenha.Focus();
                    return;
                }
                if ((txtConfirmSenha.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Confirm a nova senha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmSenha.Focus();
                    return;
                }
                if ((txtNovaSenha.TextLength < 5))
                {
                    MessageBox.Show("A nova senha deve ter no mínimo 5 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNovaSenha.Text = "";
                    txtConfirmSenha.Text = "";
                    txtNovaSenha.Focus();
                    return;
                }
                else if ((txtNovaSenha.Text != txtConfirmSenha.Text))
                {
                    MessageBox.Show("As senhas não conferem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNovaSenha.Text = "";
                    txtSenhaAnterior.Text = "";
                    txtConfirmSenha.Text = "";
                    txtSenhaAnterior.Focus();
                    return;
                }
                else if ((txtSenhaAnterior.Text == txtNovaSenha.Text))
                {
                    MessageBox.Show("A nova senha é a mesma usada como senha anterior, Informe uma senha diferente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNovaSenha.Text = "";
                    txtConfirmSenha.Text = "";
                    txtNovaSenha.Focus();
                    return;
                }

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string co = "Update Registration set Password = '" + txtNovaSenha.Text + "'where UserName='" + txtNomeUsuario.Text + "' and Password = '" + txtNovaSenha.Text + "'";

                cmd = new SqlCommand(co);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                if ((registrosAfetados > 0))
                {
                    MessageBox.Show("Alteração realizada com sucesso", "Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    txtNomeUsuario.Text = "";
                    txtNovaSenha.Text = "";
                    txtSenhaAnterior.Text = "";
                    txtConfirmSenha.Text = "";
                    frmLogin LoginForm1 = new frmLogin();
                    this.Hide();
                    frmLogin frm = new frmLogin();
                    frm.NivelAcessoComboBox.SelectedIndex = -1;
                    frm.txtUsuario.Text = "";
                    frm.txtSenha.Text = "";
                    frm.progressBar1.Visible = false;
                    frm.NivelAcessoComboBox.Focus();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomeUsuario.Text = "";
                    txtNovaSenha.Text = "";
                    txtSenhaAnterior.Text = "";
                    txtConfirmSenha.Text = "";
                    txtNomeUsuario.Focus();
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

