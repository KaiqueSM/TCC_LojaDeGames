using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class ItensAbsDAO
    {

        private Conexao conexao = new Conexao();



        public List<ItensAbs> Listar(int cd)
        {
            string strQuery = string.Format("select * from vw_itemAbs " +
                "where cd_abastecimento = {0}", cd);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeItensAbs(retorno);
        }



        public ItensAbs ListarPorCds(int cd_abastecimento, int cd_produto)
        {
            string strQuery = string.Format("select * from vw_itemAbs " +
                "where cd_abastecimento = {0} and cd_produto = {1}", cd_abastecimento, cd_produto);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeItensAbs(retorno).FirstOrDefault();
        }



        private List<ItensAbs> ListaDeItensAbs(MySqlDataReader retorno)
        {
            var itens = new List<ItensAbs>();

            while (retorno.Read())
            {
                var tempItem = new ItensAbs()
                {
                    cd_abastecimento = int.Parse(retorno["cd_abastecimento"].ToString()),
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



        public void Insert(ItensAbs ip)
        {
            string strQuery = string.Format("insert into tbl_itensAbs" +
                "(cd_abastecimento,cd_produto,qt_prod)" +
                " values({0},{1},{2})",
                ip.cd_abastecimento,
                ip.cd_produto,
                ip.qt_prod);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(ItensAbs ip)
        {
            string strQuery = "update tbl_itensAbs set ";
            strQuery += string.Format("qt_prod = {0} ", ip.qt_prod);
            strQuery += string.Format("where cd_produto = {0} and cd_abastecimento = {1}"
                , ip.cd_produto, ip.cd_abastecimento);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd_abastecimento, int cd_produto)
        {
            string strQuery = string.Format("delete from tbl_itensAbs " +
                "where cd_abastecimento = {0} and cd_produto = {1}", cd_abastecimento, cd_produto);

            conexao.ExecutaComando(strQuery);
        }
    }
}
