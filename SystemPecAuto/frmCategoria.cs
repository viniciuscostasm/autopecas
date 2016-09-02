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
    public partial class frmCategoria : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            Autocomplete();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(txtCategoriaNome.Text == "")
            {
                MessageBox.Show("Informe o nome da categoria.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCategoriaNome.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select CategoryName from Category where CategoryName='" + txtCategoriaNome.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Categoria já existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCategoriaNome.Text = "";
                    txtCategoriaNome.Focus();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert into Category(CategoryName) VALUES ('" + txtCategoriaNome.Text + "')";

                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Salvo com sucesso.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnSalvar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja deletar este registro ?", "Confirma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                deletar_registros();
            }
        }
        private void deletar_registros()
        {
            try
            {
                int registrosAfetados = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Category where ID=" + txtID.Text + "";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                if (registrosAfetados > 0)
                {
                    MessageBox.Show("Deletado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Resetar();
                    Autocomplete();
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
        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT distinct Categoryname FROM Category", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Category");

                AutoCompleteStringCollection col = new AutoCompleteStringCollection();

                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["Categoryname"].ToString());
                }
                txtCategoriaNome.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCategoriaNome.AutoCompleteCustomSource = col;
                txtCategoriaNome.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtCategoriaNome.Text == "")
                {
                    MessageBox.Show("Informe o nome da categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCategoriaNome.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "update Category set CategoryName='" + txtCategoriaNome.Text + "' where ID=" + txtID.Text + "";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Atualizado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnAtualizar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Resetar()
        {
            txtCategoriaNome.Text = "";
            btnSalvar.Enabled = true;
            btnDelete.Enabled = false;
            btnAtualizar.Enabled = false;
            txtCategoriaNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Resetar();
        }

        private void btnObterDados_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCategoriaRegistro frm = new frmCategoriaRegistro();
            frm.Show();
            frm.ObterDados();
        }

    }
}
