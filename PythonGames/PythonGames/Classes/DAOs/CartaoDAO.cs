using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class CartaoDAO
    {

        private Conexao conexao = new Conexao();



        public string ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_cartao " +
                "where cd_carrinho = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            var cartoes = new List<Cartao>();
            string no_cartao = "";

            if (retorno.Read())
            {
                no_cartao = retorno["no_cartao"].ToString();
            }

            retorno.Close();
            return no_cartao;
        }



        public void Insert(Cartao cartao)
        {
            string strQuery = string.Format("insert into tbl_cartao" +
                "(cd_carrinho,nm_cartao,no_cartao,no_cvv,dt_validade)" +
                " values({0},'{1}','{2}','{3}','{4}')",
                cartao.cd_carrinho,
                cartao.nm_cartao,
                cartao.no_cartao,
                cartao.no_cvv,
                cartao.dt_validade.ToString("yyyy-MM-dd"));

            conexao.ExecutaComando(strQuery);
        }
    }
}
