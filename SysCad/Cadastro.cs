using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace SysCad
{
    class Cadastro
    {
        string cmdconect =ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
        public void cadastro(string comando)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = cmdconect;

            try
            {
                conexao.Open();
                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public bool verificaSeTrue(string comando)
        {
            bool existe = false;

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = cmdconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();
                existe = reader.HasRows;

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            return existe;
        }

        public void visitantePreench(TextBox text, TextBox nome, TextBox documento, TextBox descricao)
        {
            string cmdSelect = @"SELECT * FROM CAD WHERE DOCUMENTO ='" + text.Text + "';";

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = cmdconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(cmdSelect, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    nome.Text = reader["NOME"].ToString();
                    documento.Text = reader["DOCUMENTO"].ToString();
                    descricao.Text = reader["DESCRICAO"].ToString();
                }

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public void listaTable(string comando, DataGridView datagrid)
        {
            datagrid.Columns.Clear();

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = cmdconect;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataAdapter data = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();

                data.Fill(table);
                datagrid.DataSource = table;

                datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                datagrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
    }
}
