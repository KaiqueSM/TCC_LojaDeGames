using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class PedidoDeCompraDAO
    {

        private Conexao conexao = new Conexao();



        public List<PedidoDeCompra> Listar()
        {
            string strQuery = "select * from vw_pedido order by dt_pedido desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDePedidoDeCompra(retorno);
        }



        public List<PedidoDeCompra> ListarPendentes()
        {
            string strQuery = "select * from vw_pedido where ped_status = 0 " +
                "order by dt_pedido desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDePedidoDeCompra(retorno);
        }



        public PedidoDeCompra ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from vw_pedido " +
                "where cd_pedido = {0} order by dt_pedido desc", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDePedidoDeCompra(retorno).FirstOrDefault();
        }



        public List<PedidoDeCompra> ListarPorCnpj(string no_cnpj)
        {
            string strQuery = string.Format("select * from vw_pedido " +
                "where no_cnpj like '{0}' " +
                "order by dt_pedido desc", no_cnpj);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDePedidoDeCompra(retorno);
        }



        public List<PedidoDeCompra> ListarPorCnpjPendentes(string no_cnpj)
        {
            string strQuery = string.Format("select * from vw_pedido " +
                "where no_cnpj like '{0}' " +
                "and ped_status = 0 " +
                "order by dt_pedido desc", no_cnpj);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDePedidoDeCompra(retorno);
        }



        private List<PedidoDeCompra> ListaDePedidoDeCompra(MySqlDataReader retorno)
        {
            var peds = new List<PedidoDeCompra>();

            while (retorno.Read())
            {
                var tempPed = new PedidoDeCompra()
                {
                    cd_pedido = int.Parse(retorno["cd_pedido"].ToString()),
                    cd_funcionario = int.Parse(retorno["cd_funcionario"].ToString()),
                    cd_fornecedor = int.Parse(retorno["cd_fornecedor"].ToString()),
                    ped_status = int.Parse(retorno["ped_status"].ToString()),
                    dt_pedido = DateTime.Parse(retorno["dt_pedido"].ToString()),
                    cpf_func = retorno["cpf_func"].ToString(),
                    no_cnpj = retorno["no_cnpj"].ToString(),
                    nm_fornecedor = retorno["nm_fornecedor"].ToString()
                };
                peds.Add(tempPed);
            }

            retorno.Close();
            return peds;
        }



        public void Insert(PedidoDeCompra ped)
        {
            string strQuery = string.Format("insert into tbl_pedido" +
                "(cd_funcionario,cd_fornecedor,dt_pedido)" +
                " values({0},{1},'{2}')",
                ped.cd_funcionario,
                ped.cd_fornecedor,
                ped.dt_pedido.ToString("yyyy-MM-dd"));

            conexao.ExecutaComando(strQuery);
        }



        public void UpdateStatus(int cd)
        {
            string strQuery = "update tbl_pedido set ped_status = 1 ";
            strQuery += string.Format("where cd_pedido = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd)
        {
            string strQuery = string.Format("delete from tbl_itenspedido " +
                "where cd_pedido = {0}", cd);

            conexao.ExecutaComando(strQuery);

            strQuery = string.Format("delete from tbl_pedido " +
                "where cd_pedido = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }
    }
}
