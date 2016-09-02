﻿using System;
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
    public partial class frmSubCategoriaRegistro : Form
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
 
        public frmSubCategoriaRegistro()
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
            frmSubCategoria frm = new frmSubCategoria();
            frm.Show();
            frm.txtSubCategoriaID.Text = dr.Cells[0].Value.ToString();
            frm.txtSubCategoria.Text = dr.Cells[1].Value.ToString();
            frm.txtCategoriaID.Text = dr.Cells[2].Value.ToString();
            frm.comboBCategoria.Text = dr.Cells[3].Value.ToString();
            frm.btnDeleteSubCategoria.Enabled = true;
            frm.btnAtualizarSubCategoria.Enabled = true;
            frm.txtSubCategoria.Focus();
            frm.btnSalvarSubCategoria.Enabled = false;
        }

        private void frmSubCategoriaRegistro_Load(object sender, EventArgs e)
        {
            ObterDados();
        }
        public void ObterDados()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT RTRIM(SubCategory.ID),RTRIM(SubCategoryName),RTRIM(CategoryID),RTRIM(CategoryName) from Category,SubCategory where Category.ID=SubCategory.CategoryID order by SubCategoryName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSubCategoriaRegistro_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmSubCategoria frm = new frmSubCategoria();
            frm.Show();
        }

        private void txtSubCategoria_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT RTRIM(SubCategory.ID),RTRIM(SubCategoryName),RTRIM(CategoryID),RTRIM(CategoryName) from Category,SubCategory where Category.ID=SubCategory.CategoryID and SubCategoryName like '" + txtSubCategoria.Text + "%' order by SubCategoryName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
 