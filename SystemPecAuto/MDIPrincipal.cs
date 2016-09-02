using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemPecAuto
{
    public partial class mdiPrincipal : Form
    {
        

        public mdiPrincipal()
        {
            InitializeComponent();
        }

        
        /*private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
         }
        */

        private void registroToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frmCadUsuario = new frmCadUsuario();
            frmCadUsuario.MdiParent = this;
            frmCadUsuario.Show();
        }
        
        
        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSobre sobre = new frmSobre();
            sobre.Show();              
        }

        private void registrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmCadUsuario = new frmCadUsuario();
            frmCadUsuario.MdiParent = this;
            frmCadUsuario.Show();
        }
        
        private void produtosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (UsuarioTipo.Text == "ADMIN")
            {
                frmCadProduto frm = new frmCadProduto();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = true;
                frm.MdiParent = this;
                frm.Show();
            }
            if (UsuarioTipo.Text == "SUPERVISOR")
            {
                frmCadProduto frm = new frmCadProduto();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = false;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void wordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Wordpad.exe");
        }

        private void gerenciadorDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TaskMgr.exe");
        }

        private void mSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Winword.exe");
        }
        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmCat = new frmCategoria();
            frmCat.MdiParent = this;
            frmCat.Show();
        }
        
        
        private void subCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form subCategoria = new frmSubCategoria();
            subCategoria.MdiParent = this;
            subCategoria.Show();
        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCategoria o1 = new frmCategoria();
            o1.Hide();
            frmSubCategoria o2 = new frmSubCategoria();
            o2.Hide();
            frmCadProduto o3 = new frmCadProduto();
            o3.Hide();
            frmRegistroUsuariosDetalhes o4 = new frmRegistroUsuariosDetalhes();
            o4.Hide();
            frmCadUsuario o5 = new frmCadUsuario();
            o5.Hide();
            frmProdutosRegistro2 o9 = new frmProdutosRegistro2();
            o9.Hide();
            frmVendasRegistro o10 = new frmVendasRegistro();
            o10.Hide();
            frmEstoqueInfo o11 = new frmEstoqueInfo();
            o11.Hide();
            frmLogin frm = new frmLogin();
            frm.NivelAcessoComboBox.SelectedIndex = -1;
            frm.txtUsuario.Text = "";
            frm.txtSenha.Text = "";
            frm.progressBar1.Visible = false;
            frm.NivelAcessoComboBox.Focus();
            frm.Show();
        }
        private void mdiPrincipal_Load(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToString();
        }
        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsuarioTipo.Text == "ADMIN")
            {
                frmCadProduto frm = new frmCadProduto();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = true;
                frm.MdiParent = this;
                frm.Show();
            }
            if (UsuarioTipo.Text == "SUPERVISOR")
            {
                frmCadProduto frm = new frmCadProduto();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = false;
                frm.MdiParent = this;
                frm.Show();
            }
        }
        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form consultaProdutos = new frmProdutosRegistro2();
            consultaProdutos.MdiParent = this;
            consultaProdutos.Show();
        }
        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsuarioTipo.Text == "ADMIN")
            {
                frmEstoque frm = new frmEstoque();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = true;
                frm.MdiParent = this;
                frm.Show();
            }
            if (UsuarioTipo.Text == "SUPERVISOR")
            {
                frmEstoque frm = new frmEstoque();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = false;
                frm.MdiParent = this;
                frm.Show();
            }

        }
        private void estoqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (UsuarioTipo.Text == "ADMIN")
            {
                frmEstoque frm = new frmEstoque();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = true;
                frm.MdiParent = this;
                frm.Show();
            }
            if (UsuarioTipo.Text == "SUPERVISOR")
            {
                frmEstoque frm = new frmEstoque();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = false;
                frm.MdiParent = this;
                frm.Show();
            }
        }
        private void faturamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsuarioTipo.Text == "ADMIN")
            {
                frmVendas frm = new frmVendas();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = true;
                frm.MdiParent = this;
                frm.Show();
            }
            if (UsuarioTipo.Text == "VENDEDOR")
            {
                frmVendas frm = new frmVendas();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = false;
                frm.MdiParent = this;
                frm.Show();
            }
        }
        private void vendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UsuarioTipo.Text == "ADMIN")
            {
                frmVendas frm = new frmVendas();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = true;
                frm.MdiParent = this;
                frm.Show();
            }
            if (UsuarioTipo.Text == "VENDEDOR")
            {
                frmVendas frm = new frmVendas();
                frm.lblTipoUsuario.Text = lblUsuario.Text;
                frm.btnObterDados.Enabled = false;
                frm.MdiParent = this;
                frm.Show();
            }
        }
        private void vendasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form vendasRegistro = new frmVendasRegistro();
            vendasRegistro.MdiParent = this;
            vendasRegistro.Show();
        }
        
        private void loginDetalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form loginDetalhes = new frmLoginDetalhes();
            loginDetalhes.MdiParent = this;
            loginDetalhes.Show();
        }
        private void mdiPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
        private void estoquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form estoque = new frmEstoque();
            estoque.MdiParent = this;
            estoque.Show();
        }

        private void estoqueInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstoqueInfo estoqueInfo = new frmEstoqueInfo();
            estoqueInfo.MdiParent = this;
            estoqueInfo.Show();
         }

        

        

        

        
  
    }
}
