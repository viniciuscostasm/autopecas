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
    public partial class frmCadUsuario : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public frmCadUsuario()
        {
            InitializeComponent();
        }

        private void frmCadUsuario_Load(object sender, EventArgs e)
        {
            Autocomplete();
        }

        private void Resetar()
        {
            txtNomeUsuario.Text = "";
            comboBoxTipoUsuario.SelectedIndex = -1;
            txtSenha.Text = "";
            txtNumContato.Text = "";
            txtNome.Text = "";
            txtEmail_Endereco.Text = "";
            btnRegistrar.Enabled = true;
            btnDelete.Enabled = false;
            btnAtualizarRegistro.Enabled = false;
            txtNomeUsuario.Focus();
        }

        private void btnNovoRegistro_Click(object sender, EventArgs e)
        {
            Resetar();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNomeUsuario.Text == "")
            {
                MessageBox.Show("Informe o login do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNomeUsuario.Focus();
                return;
            }
            if (comboBoxTipoUsuario.Text == "")
            {
                MessageBox.Show("Selecione o tipo do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxTipoUsuario.Focus();
                return;
            }
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Informe a senha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
                return;
            }
            if (txtNome.Text == "")
            {
                MessageBox.Show("Infome o nome do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
                return;
            }
            if (txtNumContato.Text == "")
            {
                MessageBox.Show("Informe o número de contato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumContato.Focus();
                return;
            }
            if (txtEmail_Endereco.Text == "")
            {
                MessageBox.Show("Informe o email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail_Endereco.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select username from registration where username=@find";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 30, "username"));
                cmd.Parameters["@find"].Value = txtNomeUsuario.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Nome do usuário ja existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomeUsuario.Text = "";
                    txtNomeUsuario.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct1 = "select Email from registration where Email='" + txtEmail_Endereco.Text + "'";

                cmd = new SqlCommand(ct1);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Email já existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail_Endereco.Text = "";
                    txtEmail_Endereco.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert into Registration(Username,UsertYpe,Password,ContactNo,Email,Name,JoiningDate) VALUES ('" + txtNomeUsuario.Text + "','" + comboBoxTipoUsuario.Text + "','" + txtSenha.Text + "','" + txtNumContato.Text + "','" + txtEmail_Endereco.Text + "','" + txtNome.Text + "','" + System.DateTime.Now + "')";

                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Registrado com sucesso", "Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnRegistrar.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerificarDisponibilidade_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNomeUsuario.Text == "")
                {
                    MessageBox.Show("Informe o nome do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomeUsuario.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select username from registration where username='" + txtNomeUsuario.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Nome de usuário não disponível", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!rdr.Read())
                {
                    MessageBox.Show("Nome de usuário disponível", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNomeUsuario.Focus();

                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmail_Endereco_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmail_Endereco.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmail_Endereco.Text))
                {
                    MessageBox.Show("Email inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail_Endereco.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtNomeUsuario_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_]");
            if (txtNomeUsuario.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtNomeUsuario.Text))
                {
                    MessageBox.Show("somente letras, numeros e sublinhado é permitido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomeUsuario.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void btnObterDetalhes_Click(object sender, EventArgs e)
        {
            frmRegistroUsuariosDetalhes frm = new frmRegistroUsuariosDetalhes();
            frm.dataGridView1.DataSource = frm.ObterDados();
            frm.Show();
        }

        private void txtNomeUsuario_TextChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
            btnAtualizarRegistro.Enabled = true;
            try
            {
                txtNomeUsuario.Text = txtNomeUsuario.Text.TrimEnd();
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT Usertype,password,name,contactno,email FROM registration WHERE username = '" + txtNomeUsuario.Text.Trim() + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    comboBoxTipoUsuario.Text = (rdr.GetString(0).Trim());
                    txtSenha.Text = (rdr.GetString(1).Trim());
                    txtNome.Text = (rdr.GetString(2).Trim());
                    txtNumContato.Text = (rdr.GetString(3).Trim());
                    txtEmail_Endereco.Text = (rdr.GetString(4).Trim());
                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT username FROM registration", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "registration");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["Username"].ToString());

                }
                txtNomeUsuario.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtNomeUsuario.AutoCompleteCustomSource = col;
                txtNomeUsuario.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizarRegistro_Click(object sender, EventArgs e)
        {
            if (txtNomeUsuario.Text == "")
            {
                MessageBox.Show("Informe o nome do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNomeUsuario.Focus();
                return;
            }
            if (comboBoxTipoUsuario.Text == "")
            {
                MessageBox.Show("Selecione o tipo do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxTipoUsuario.Focus();
                return;
            }
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Informe a senha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
                return;
            }
            if (txtNome.Text == "")
            {
                MessageBox.Show("Infome o nome do usuário", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
                return;
            }
            if (txtNumContato.Text == "")
            {
                MessageBox.Show("Informe o no de contato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumContato.Focus();
                return;
            }
            if (txtEmail_Endereco.Text == "")
            {
                MessageBox.Show("Informe o email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail_Endereco.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "update registration set usertype='" + comboBoxTipoUsuario.Text + "', password='" + txtSenha.Text + "',contactno='" + txtNumContato.Text + "',email='" + txtEmail_Endereco.Text + "',name='" + txtNome.Text + "' where username='" + txtNomeUsuario.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                MessageBox.Show("Atualizado com sucesso", "Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnAtualizarRegistro.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja deletar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                deletar_registros();
            }
        }
        private void deletar_registros()
        {

            try
            {
                if (txtNomeUsuario.Text == "admin")
                {
                    MessageBox.Show("A conta Admin não pode ser deletada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int registrosAfetados = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "delete from Registration where Username='" + txtNomeUsuario.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                if (registrosAfetados > 0)
                {
                    MessageBox.Show("Deletado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Autocomplete();
                    Resetar();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Resetar();
                    Autocomplete();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumContato_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

