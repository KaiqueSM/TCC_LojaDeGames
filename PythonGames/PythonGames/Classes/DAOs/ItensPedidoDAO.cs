using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class ItensPedidoDAO
    {

        private Conexao conexao = new Conexao();



        public List<ItensPedido> Listar(int cd)
        {
            string strQuery = string.Format("select * from vw_itemPedido " +
                "where cd_pedido = {0}", cd); MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeItensPedido(retorno);
        }



        public ItensPedido ListarPorCds(int cd_pedido, int cd_produto)
        {
            string strQuery = string.Format("select * from vw_itemPedido " +
                "where cd_pedido = {0} and cd_produto = {1}", cd_pedido, cd_produto);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeItensPedido(retorno).FirstOrDefault();
        }



        private List<ItensPedido> ListaDeItensPedido(MySqlDataReader retorno)
        {
            var itens = new List<ItensPedido>();

            while (retorno.Read())
            {
                var tempItem = new ItensPedido()
                {
                    cd_pedido = int.Parse(retorno["cd_pedido"].ToString()),
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    qt_prod = uint.Parse(retorno["qt_prod"].ToString()),
                    nm_prod = retorno["nm_prod"].ToString(),
                    link_img = retorno["link_img"].ToString()
                };
                itens.Add(tempItem);
            }

            retorno.Close();
            return itens;
        }



        public void Insert(ItensPedido ip)
        {
            string strQuery = string.Format("insert into tbl_itensPedido" +
                "(cd_pedido,cd_produto,qt_prod)" +
                " values({0},{1},{2})",
                ip.cd_pedido,
                ip.cd_produto,
                ip.qt_prod);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(ItensPedido ip)
        {
            string strQuery = "update tbl_itensPedido set ";
            strQuery += string.Format("qt_prod = {0} ", ip.qt_prod);
            strQuery += string.Format("where cd_produto = {0} and cd_pedido = {1}"
                , ip.cd_produto, ip.cd_pedido);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd_pedido, int cd_produto)
        {
            string strQuery = string.Format("delete from tbl_itensPedido " +
                "where cd_pedido = {0} and cd_produto = {1}", cd_pedido, cd_produto);

            conexao.ExecutaComando(strQuery);
        }
    }
}
