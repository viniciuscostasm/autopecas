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
using System.Net.Mail;


namespace SystemPecAuto
{
    public partial class frmRecuperarSenha : Form
    {
        ConnectionString cs = new ConnectionString();
        public frmRecuperarSenha()
        {
            InitializeComponent();
        }

        private void btnEnviarSenha_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Informe o seu email ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Password FROM Registration Where Email = '" + txtEmail.Text + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MailMessage Msg = new MailMessage();
                    // Email do remetente
                    Msg.From = new MailAddress("vinicius.costasm@gmail.com");
                    // Recipient e-mail address.
                    Msg.To.Add(txtEmail.Text);
                    Msg.Subject = "Informaçoes";
                    Msg.Body = "Sua Senha: " + Convert.ToString(ds.Tables[0].Rows[0]["Password"]) + "";
                    Msg.IsBodyHtml = true;
                    // informe o ip do seu servidor SMTP
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("vinicius.costasm@gmail.com", "123456");
                    smtp.EnableSsl = true;
                    smtp.Send(Msg);
                    MessageBox.Show(("Senha enviada com sucesso " + ("\r\n" + " Verifique o seu email")), "Obrigado", MessageBoxButtons.OK, MessageBoxIcon.Information); this.Hide();
                    frmLogin LoginForm1 = new frmLogin();
                    LoginForm1.Show();
                    LoginForm1.txtUsuario.Text = "";
                    LoginForm1.txtSenha.Text = "";
                    LoginForm1.progressBar1.Visible = false;
                    LoginForm1.txtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmRecuperarSenha_Load(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }

        private void frmRecuperarSenha_FormClosing(object sender, FormClosingEventArgs e)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }


    }
}
