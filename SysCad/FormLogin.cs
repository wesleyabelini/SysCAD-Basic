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
        Funcao funcao = new Funcao();

        public FormLogin()
        {
            InitializeComponent();
            verifUsers();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(funcao.isok(textBoxLogin.Text, "usuario")==true && funcao.isok(textBoxSenha.Text, "usuario")==true)
            {
                string cmdSelect = "SELECT * FROM SYSLOGIN WHERE USUARIO='" + textBoxLogin.Text + "' AND SENHA='" + textBoxSenha.Text + "';";

                if (cadastro.verificaSeTrue(cmdSelect) == true)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Usuario e senha não confirmam.", "Erro Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                textBoxLogin.Clear();
                textBoxSenha.Clear();

                MessageBox.Show("Dados inseridos não estão válidos.\nCaracteres permitidos: MAIÚSCULA, minúscula.", "Erro Caracteres", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
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

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.keyEnter(e, buttonLogin);
        }

        private void textBoxSenha_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.keyEnter(e, buttonLogin);
        }
    }
}
