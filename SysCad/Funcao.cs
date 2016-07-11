using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SysCad
{
    class Funcao
    {
        public void keyEnter(KeyEventArgs e, Button botaox)
        {
            if(e.KeyCode== Keys.Enter)
            {
                botaox.PerformClick();
            }
        }

        public bool isok(string texto, string codigo)
        {
            bool math = false;

            Regex data = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
            Regex numero = new Regex(@"^[0-9]*$");
            Regex usuario = new Regex(@"^[A-Za-z]*$");
            Regex nome = new Regex(@"^[A-Z\s]*$");

            if (data.IsMatch(texto) && codigo == "data")
            {
                math = true;
            }
            else if (numero.IsMatch(texto) && codigo == "numero")
            {
                math = true;
            }
            else if (usuario.IsMatch(texto) && codigo == "usuario")
            {
                math = true;
            }
            else if (nome.IsMatch(texto) && codigo == "nome")
            {
                math = true;
            }

            return math;
        }
    }
}
