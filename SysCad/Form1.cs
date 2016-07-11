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
    public partial class FormCadSys : Form
    {
        Cadastro cadastro = new Cadastro();
        Funcao funcao = new Funcao();
        public FormCadSys()
        {
            InitializeComponent();
            toolStripLabel1.Text = DateTime.Now.ToString();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            if(textBoxSelDocumento.Text!="" && funcao.isok(textBoxSelDocumento.Text, "numero")==true)
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
                textBoxDocumento.Clear();
                MessageBox.Show("O valor para documento não deve ser nulo na busca.", "Erro busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            if(textBoxNome.Text!="" && textBoxDocumento.Text!="" && funcao.isok(textBoxNome.Text, "nome")==true && funcao.isok(textBoxDocumento.Text, "numero")==true)
            {
                string data = Convert.ToDateTime(DateTime.Now).ToString();
                string cmdInsert = @"INSERT INTO CAD VALUES ('" + textBoxNome.Text + "', '" + textBoxDocumento.Text + "', '" + data + "', '" + textBoxDescricao.Text +
                    "');";

                if (textBoxDescricao.Text!="" && funcao.isok(textBoxDescricao.Text, "nome")==true)
                {
                    cadastro.cadastro(cmdInsert);
                }
                else if(textBoxDescricao.Text=="")
                {
                    cadastro.cadastro(cmdInsert);
                }
                else
                {
                    MessageBox.Show("A descrição não corresponde com os valores esperados.", "Erro Descrição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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
            
            if(radioButton1.Checked==true && funcao.isok(maskedTextBoxDia.Text, "data")==true)
            {
                cmdSelect += "BETWEEN '" + maskedTextBoxDia.Text + " 00:00:00' AND '" + maskedTextBoxDia.Text + " 23:59:59';";
                cadastro.listaTable(cmdSelect, dataGridView1);
            }
            else if(radioButton2.Checked==true && funcao.isok(maskedTextBoxDia.Text, "data")==true && funcao.isok(maskedTextBoxFiltro.Text, "data")==true)
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

        private void textBoxSelDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.keyEnter(e, buttonBuscar);
        }

        private void maskedTextBoxDia_KeyDown(object sender, KeyEventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                funcao.keyEnter(e, button1);
            }
        }

        private void maskedTextBoxFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if(radioButton2.Checked==true)
            {
                funcao.keyEnter(e, button1);
            }
        }

        private void maskedTextBoxFiltro_DoubleClick(object sender, EventArgs e)
        {
            maskedTextBoxFiltro.Text = DateTime.Now.ToString();
        }
    }
}
