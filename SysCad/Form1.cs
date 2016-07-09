using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SysCad
{
    public partial class FormCadSys : Form
    {
        Cadastro cadastro = new Cadastro();
        public FormCadSys()
        {
            InitializeComponent();
            toolStripLabel1.Text = DateTime.Now.ToString();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            if(textBoxSelDocumento.Text!="")
            {
                limpaCampo();

                string cmdSelect = @"SELECT * FROM CAD WHERE DOCUMENTO='" + textBoxSelDocumento.Text + "';";

                if (cadastro.verificaSeTrue(cmdSelect) == true)
                {
                    cadastro.visitantePreench(textBoxSelDocumento, textBoxNome, textBoxDocumento, textBoxDescricao);
                }
                else
                {
                    MessageBox.Show("Registro não encontrado no sistema", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("O valor para documento não deve ser nulo na busca.", "Erro busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            if(textBoxNome.Text!="" && textBoxDocumento.Text!="")
            {
                string data = Convert.ToDateTime(DateTime.Now).ToString();
                string cmdInsert = @"INSERT INTO CAD VALUES ('" + textBoxNome.Text + "', '" + textBoxDocumento.Text + "', '" + data + "', '" + textBoxDescricao.Text +
                    "');";

                cadastro.cadastro(cmdInsert);

                limpaCampo();
            }
            else
            {
                MessageBox.Show("Os valores de Nome e Documento não podem ser nulos.", "Erro Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                maskedTextBoxFiltro.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked==true)
            {
                maskedTextBoxFiltro.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmdSelect = @"SELECT DATA, DOCUMENTO, NOME FROM CAD WHERE DATA ";
            
            if(radioButton1.Checked==true && isok(maskedTextBoxDia)==true)
            {
                cmdSelect += "BETWEEN '" + maskedTextBoxDia.Text + " 00:00:00' AND '" + maskedTextBoxDia.Text + " 23:59:59';";
                cadastro.listaTable(cmdSelect, dataGridView1);
            }
            else if(radioButton2.Checked==true && isok(maskedTextBoxDia)==true && isok(maskedTextBoxFiltro)==true)
            {
                cmdSelect += "BETWEEN '" + maskedTextBoxDia.Text + " 00:00:00' AND '" + maskedTextBoxFiltro.Text + " 23:59:59';";
                cadastro.listaTable(cmdSelect, dataGridView1);
            }
        }

        private void limpaCampo()
        {
            textBoxNome.Clear();
            textBoxDocumento.Clear();
            textBoxDescricao.Clear();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox sobre = new AboutBox();
            sobre.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripLabel1.Text = DateTime.Now.ToString();
        }

        private bool isok(MaskedTextBox mask)
        {
            bool math = false;

            Regex data = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");

            if (data.IsMatch(mask.Text))
            {
                math = true;
            }
            else
            {
                MessageBox.Show("Verifique a DATA, pois não corresponde ao esperado.", "Erro DATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return math;
        }
    }
}
