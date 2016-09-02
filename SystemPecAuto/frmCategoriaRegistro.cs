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
    public partial class frmCategoriaRegistro : Form
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        public frmCategoriaRegistro()
        {
            InitializeComponent();
        }

       
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmCategoria frm = new frmCategoria();
            frm.Show();
            frm.txtID.Text = dr.Cells[0].Value.ToString();
            frm.txtCategoriaNome.Text = dr.Cells[1].Value.ToString();
            frm.btnDelete.Enabled = true;
            frm.btnAtualizar.Enabled = true;
            frm.txtCategoriaNome.Focus();
            frm.btnSalvar.Enabled = false;
        }
        public void ObterDados()
        {
            try 
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "Select * from Category order by CategoryName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read()== true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCategoriaRegistro_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmCategoria frm = new frmCategoria();
            frm.Show();
        }
        private void frmCategoriaRegistro_Load(object sender, EventArgs e)
        {
            ObterDados();
        }
    }
}
