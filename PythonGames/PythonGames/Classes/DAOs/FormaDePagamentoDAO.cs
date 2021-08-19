using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class FormaDePagamentoDAO
    {

        private Conexao conexao = new Conexao();



        public List<FormaDePagamento> ListarParaGerenciamento()
        {
            string strQuery = "select * from tbl_forma where cd_forma >2;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCartao(retorno);
        }


        public List<FormaDePagamento> ListarParaEcommerce()
        {
            string strQuery = "select * from tbl_forma limit 2;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCartao(retorno);
        }



        private List<FormaDePagamento> ListaDeCartao(MySqlDataReader retorno)
        {
            var formas = new List<FormaDePagamento>();

            while (retorno.Read())
            {
                var tempForma = new FormaDePagamento()
                {
                    cd_forma = int.Parse(retorno["cd_forma"].ToString()),
                    nm_forma = retorno["nm_forma"].ToString()
                };
                formas.Add(tempForma);
            }

            retorno.Close();
            return formas;
        }
    }
}
