using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class ItensCarrinhoDAO
    {

        private Conexao conexao = new Conexao();



        public List<ItensCarrinho> Listar(int cd)
        {
            string strQuery = string.Format("select * from vw_itemCarrinho " +
            "where cd_carrinho = {0} ", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeItensCarrinho(retorno);
        }



        public ItensCarrinho ListarPorCds(int cd_carrinho, int cd_produto)
        {
            string strQuery = string.Format("select * from vw_itemCarrinho " +
                "where cd_carrinho = {0} and cd_produto = {1}", cd_carrinho, cd_produto);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeItensCarrinho(retorno).FirstOrDefault();
        }



        private List<ItensCarrinho> ListaDeItensCarrinho(MySqlDataReader retorno)
        {
            var itens = new List<ItensCarrinho>();

            while (retorno.Read())
            {
                var tempItem = new ItensCarrinho()
                {
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    nm_prod = retorno["nm_prod"].ToString(),
                    link_img = retorno["link_img"].ToString(),
                    nm_categoria = retorno["nm_categoria"].ToString(),
                    vl_prod = double.Parse(retorno["vl_prod"].ToString()),
                    qt_estoque = int.Parse(retorno["qt_estoque"].ToString()),
                    prod_desc = retorno["prod_desc"].ToString(),
                    cd_carrinho = int.Parse(retorno["cd_carrinho"].ToString()),
                    qt_item = uint.Parse(retorno["qt_item"].ToString()),
                    vl_item = double.Parse(retorno["vl_item"].ToString())
                };
                itens.Add(tempItem);
            }

            retorno.Close();
            return itens;
        }



        public void Insert(ItensCarrinho ic)
        {
            string strQuery = string.Format("insert into tbl_ItensCarrinho" +
                "(cd_carrinho,cd_produto,qt_item,vl_item)" +
                " values({0},{1},{2},'{3}')",
                ic.cd_carrinho,
                ic.cd_produto,
                ic.qt_item,
                ic.vl_item.ToString().Replace(",", "."));

            conexao.ExecutaComando(strQuery);
        }



        public void Update(ItensCarrinho ic)
        {
            string strQuery = "update tbl_ItensCarrinho set ";
            strQuery += string.Format("qt_item = {0}, ", ic.qt_item);
            strQuery += string.Format("vl_item = '{0}' ", ic.vl_item.ToString().Replace(",", "."));
            strQuery += string.Format("where cd_carrinho = {0} and cd_produto = {1}"
                , ic.cd_carrinho, ic.cd_produto);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd_carrinho, int cd_produto)
        {
            string strQuery = string.Format("delete from tbl_ItensCarrinho " +
                "where cd_carrinho = {0} and cd_produto = {1}", cd_carrinho, cd_produto);

            conexao.ExecutaComando(strQuery);
        }
    }
}
