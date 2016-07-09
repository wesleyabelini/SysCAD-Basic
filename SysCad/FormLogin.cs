using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysCad
{
    public partial class FormLogin : Form
    {
        Cadastro cadastro = new Cadastro();

        public FormLogin()
        {
            InitializeComponent();
            verifUsers();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string cmdSelect = "SELECT * FROM SYSLOGIN WHERE USUARIO='" + textBoxLogin.Text + "' AND SENHA='" + textBoxSenha.Text + "';";

            if(cadastro.verificaSeTrue(cmdSelect)==true)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void verifUsers()
        {
            string cmdSelect = @"SELECT * FROM SYSLOGIN;";

            if(cadastro.verificaSeTrue(cmdSelect)==false)
            {
                string cmdInsert = @"INSERT INTO SYSLOGIN VALUES ('syscad', 'syscad', 0);";
                cadastro.cadastro(cmdInsert);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
