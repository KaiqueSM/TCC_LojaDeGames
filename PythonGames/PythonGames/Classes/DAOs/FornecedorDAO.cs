using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class FornecedorDAO
    {

        private Conexao conexao = new Conexao();



        public List<Fornecedor> Listar()
        {
            string strQuery = "select * from tbl_fornecedor where flag = 0 order by cd_fornecedor desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFornecedor(retorno);
        }



        public List<Fornecedor> ListarInativos()
        {
            string strQuery = "select * from tbl_fornecedor";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFornecedor(retorno);
        }



        public Fornecedor ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_fornecedor " +
                "where cd_fornecedor = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFornecedor(retorno).FirstOrDefault();
        }



        public List<Fornecedor> ListarPorCnpj(string no_cnpj)
        {
            string strQuery = string.Format("select * from tbl_fornecedor " +
                "where no_cnpj like '{0}' " +
                "and flag = 0 " +
                "order by cd_fornecedor desc", no_cnpj);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFornecedor(retorno);
        }



        public List<Fornecedor> ListarPorCnpjInativos(string no_cnpj)
        {
            string strQuery = string.Format("select * from tbl_fornecedor " +
                "where no_cnpj like '{0}' " +
                "order by cd_fornecedor desc", no_cnpj);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFornecedor(retorno);
        }



        public Boolean ChecaCNPJ(string cnpj)
        {
            string strQuery = string.Format("select * from tbl_fornecedor " +
                "where no_cnpj = '{0}'", cnpj);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            Fornecedor fotn = ListaDeFornecedor(retorno).FirstOrDefault();

            if (fotn == null)
                return true;
            else
                return false;
        }



        private List<Fornecedor> ListaDeFornecedor(MySqlDataReader retorno)
        {
            var fornecedors = new List<Fornecedor>();

            while (retorno.Read())
            {
                var tempFornecedor = new Fornecedor()
                {
                    cd_fornecedor = int.Parse(retorno["cd_fornecedor"].ToString()),
                    nm_fornecedor = retorno["nm_fornecedor"].ToString(),
                    no_cnpj = retorno["no_cnpj"].ToString(),
                    flag = int.Parse(retorno["flag"].ToString())
                };
                fornecedors.Add(tempFornecedor);
            }

            retorno.Close();
            return fornecedors;
        }



        public void Insert(Fornecedor fornecedor)
        {
            string strQuery = string.Format("insert into tbl_fornecedor" +
                "(nm_fornecedor,no_cnpj)" +
                " values('{0}','{1}')",
                fornecedor.nm_fornecedor,
                fornecedor.no_cnpj);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(Fornecedor fornecedor)
        {
            string strQuery = "update tbl_fornecedor set ";
            strQuery += string.Format("nm_fornecedor = '{0}', ", fornecedor.nm_fornecedor);
            strQuery += string.Format("no_cnpj = '{0}' ", fornecedor.no_cnpj);
            strQuery += string.Format("where cd_fornecedor = {0}", fornecedor.cd_fornecedor);

            conexao.ExecutaComando(strQuery);
        }



        public void Inativar(int cd)
        {
            string strQuery = "update tbl_fornecedor set flag = 1 ";
            strQuery += string.Format("where cd_fornecedor = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }



        public void Reativar(int cd)
        {
            string strQuery = "update tbl_fornecedor set flag = 0 ";
            strQuery += string.Format("where cd_fornecedor = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }
    }
}
