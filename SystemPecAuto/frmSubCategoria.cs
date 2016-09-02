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
    public partial class frmSubCategoria : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frmSubCategoria()
        {
            InitializeComponent();
        }

        private void Resetar()
        {
            txtSubCategoria.Text = "";

            btnSalvarSubCategoria.Enabled = true;
            btnDeleteSubCategoria.Enabled = false;
            btnAtualizarSubCategoria.Enabled = false;
            txtSubCategoria.Focus();
        }

        private void btnNovoSubCategoria_Click(object sender, EventArgs e)
        {
            Resetar();
        }

        private void btnSalvarSubCategoria_Click(object sender, EventArgs e)
        {
            if (txtSubCategoria.Text == "")
            {
                MessageBox.Show("Informe a sub categoria", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubCategoria.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select SubCategoryName from SubCategory where SubCategoryName='" + txtSubCategoria.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Nome da sub categoria já existe", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSubCategoria.Text = "";
                    txtSubCategoria.Focus();

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "Insert into SubCategory(SubCategoryName,CategoryID) VALUES ('" + txtSubCategoria.Text + "'," + txtCategoriaID.Text + ")";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Salvo com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnSalvarSubCategoria.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct SubCategoryName from SubCategory", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SubCategory");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["SubCategoryName"].ToString());
                }
                txtSubCategoria.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSubCategoria.AutoCompleteCustomSource = col;
                txtSubCategoria.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteSubCategoria_Click(object sender, EventArgs e)
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
                int registrosAfetados = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from SubCategory where ID=" + txtSubCategoriaID.Text + "";
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
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizarSubCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategoria.Text == "")
                {
                    MessageBox.Show("Informe a sub categoria", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSubCategoria.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "update SubCategory set SubCategoryName='" + txtSubCategoria.Text + "',CategoryID=" + txtCategoriaID.Text + "where ID=" + txtSubCategoriaID.Text + "";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Atualizado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnAtualizarSubCategoria.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDadosSubCategoria_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSubCategoriaRegistro frm = new frmSubCategoriaRegistro();
            frm.Show();
            frm.ObterDados();
        }
        private void frmSubCategoria_Load(object sender, EventArgs e)
        {
            Autocomplete();
            FillCombo();
        }

        public void FillCombo()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(CategoryName) from Category order by CategoryName";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    comboBCategoria.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "Select ID from Category WHERE CategoryName = '" + comboBCategoria.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtCategoriaID.Text = rdr.GetInt32(0).ToString().Trim();
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
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
