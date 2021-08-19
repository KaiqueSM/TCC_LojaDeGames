using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class ProdutoDAO
    {

        private Conexao conexao = new Conexao();



        public List<Produto> Listar()
        {
            string strQuery = "select * from tbl_produto where flag = 0;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }



        public List<Produto> ListarInativos()
        {
            string strQuery = "select * from tbl_produto;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }



        public Produto ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_produto " +
                "where cd_Produto = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno).FirstOrDefault();
        }



        public List<Produto> ListarPorBusca(string busca)
        {
            string strQuery = string.Format("select * from tbl_produto where flag = 0 " +
                "and nm_prod like '%{0}%' or nm_categoria like '%{0}%' or prod_desc like '%{0}%';", busca);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }



        public List<Produto> ListarGamePorBusca(string busca)
        {
            string strQuery = string.Format("select * from vw_game where flag = 0 " +
                "and nm_prod like '%{0}%' or nm_categoria like '%{0}%' or prod_desc like '%{0}%' or nm_genero like '%{0}%';", busca);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }



        public List<Produto> ListarPorBuscaInativos(string busca)
        {
            string strQuery = string.Format("select * from tbl_produto where " +
                "nm_prod like '%{0}%';", busca);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }



        public Produto ListarGame(int cd)
        {
            string strQuery = string.Format("select * from vw_game " +
                "where cd_Produto = {0} and flag = 0", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);

            var prods = new List<Produto>();

            while (retorno.Read())
            {
                var tempProd = new Produto()
                {
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    nm_prod = retorno["nm_prod"].ToString(),
                    link_img = retorno["link_img"].ToString(),
                    nm_categoria = retorno["nm_categoria"].ToString(),
                    vl_prod = double.Parse(retorno["vl_prod"].ToString()),
                    qt_estoque = int.Parse(retorno["qt_estoque"].ToString()),
                    prod_desc = retorno["prod_desc"].ToString(),
                    nm_genero = retorno["nm_genero"].ToString(),
                    vl_indicacao = retorno["vl_indicacao"].ToString(),
                    flag = int.Parse(retorno["flag"].ToString())

                };
                prods.Add(tempProd);
            }

            retorno.Close();

            return prods.FirstOrDefault();
        }



        public List<Produto> ListarPorCategoria(string cat)
        {
            string strQuery = string.Format("select * from tbl_produto " +
                "where nm_categoria = '{0}' and flag = 0", cat);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }



        private List<Produto> ListaDeProduto(MySqlDataReader retorno)
        {
            var prods = new List<Produto>();

            while (retorno.Read())
            {
                var tempProd = new Produto()
                {
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    nm_prod = retorno["nm_prod"].ToString(),
                    link_img = retorno["link_img"].ToString(),
                    nm_categoria = retorno["nm_categoria"].ToString(),
                    vl_prod = double.Parse(retorno["vl_prod"].ToString()),
                    qt_estoque = int.Parse(retorno["qt_estoque"].ToString()),
                    prod_desc = retorno["prod_desc"].ToString(),
                    flag = int.Parse(retorno["flag"].ToString())

                };
                prods.Add(tempProd);
            }

            retorno.Close();
            return prods;
        }



        public void Insert(Produto prod)
        {
            string strQuery = string.Format("insert into tbl_Produto" +
                "(nm_prod,link_img,nm_categoria,vl_prod,qt_estoque,prod_desc)" +
                " values('{0}','{1}','{2}','{3}',{4},'{5}')",
                prod.nm_prod,
                prod.link_img,
                prod.nm_categoria,
                prod.vl_prod.ToString().Replace(",", "."),
                prod.qt_estoque,
                prod.prod_desc);

            conexao.ExecutaComando(strQuery);
        }



        public void InsertGame(Produto prod)
        {
            string strQuery = string.Format("call sp_prod_insgame" +
                "('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}');",
                prod.nm_prod,
                prod.link_img,
                prod.nm_categoria,
                prod.vl_prod.ToString().Replace(",", "."),
                prod.qt_estoque,
                prod.prod_desc,
                prod.nm_genero,
                prod.vl_indicacao);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(Produto prod)
        {
            string strQuery = string.Format("call sp_prod_altprod" +
                "('{0}','{1}','{2}','{3}',{4},'{5}','{6}');",
                prod.cd_produto,
                prod.nm_prod,
                prod.link_img,
                prod.nm_categoria,
                prod.vl_prod.ToString().Replace(",", "."),
                prod.qt_estoque,
                prod.prod_desc);

            conexao.ExecutaComando(strQuery);
        }



        public void UpdateGame(Produto prod)
        {
            string strQuery = string.Format("call sp_prod_altgame" +
                "('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}');",
                prod.cd_produto,
                prod.nm_prod,
                prod.link_img,
                prod.nm_categoria,
                prod.vl_prod.ToString().Replace(",", "."),
                prod.qt_estoque,
                prod.prod_desc,
                prod.nm_genero,
                prod.vl_indicacao);

            conexao.ExecutaComando(strQuery);
        }



        public void Inativar(int cd)
        {
            string strQuery = "update tbl_Produto set flag = 1 ";
            strQuery += string.Format("where cd_produto = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }



        public void Reativar(int cd)
        {
            string strQuery = "update tbl_Produto set flag = 0 ";
            strQuery += string.Format("where cd_produto = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }
    }
}
