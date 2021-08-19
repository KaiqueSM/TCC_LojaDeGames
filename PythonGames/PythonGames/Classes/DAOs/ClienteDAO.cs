using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class ClienteDAO
    {

        private Conexao conexao = new Conexao();



        public List<Cliente> Listar()
        {
            string strQuery = "select * from tbl_cliente where flag = 0  order by cd_cliente desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCliente(retorno);
        }



        public List<Cliente> ListarInativos()
        {
            string strQuery = "select * from tbl_cliente";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCliente(retorno);
        }



        public List<Cliente> ListarPorCpf(string cpf_cli)
        {
            string strQuery = string.Format("select * from tbl_cliente " +
                "where flag = 0 " +
                "and cpf_cli like '{0}'" +
                "order by cd_cliente desc", cpf_cli); 
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCliente(retorno);
        }



        public List<Cliente> ListarPorCpfInativos(string cpf_cli)
        {
            string strQuery = string.Format("select * from tbl_cliente " +
                "where cpf_cli like '{0}'" +
                "order by cd_cliente desc", cpf_cli);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCliente(retorno);
        }



        public Cliente ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_cliente " +
                "where cd_cliente = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCliente(retorno).FirstOrDefault();
        }



        public Cliente Login(Cliente cliente)
        {
            string strQuery = string.Format("select * from tbl_cliente " +
                "where nm_email = '{0}' and senha_cli = '{1}' and flag = 0", 
                cliente.nm_email, cliente.senha_cli) ;

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCliente(retorno).FirstOrDefault();
        }



        public Boolean ChecaCPF(string cpf)
        {
            string strQuery = string.Format("select * from tbl_cliente " +
                "where cpf_cli = '{0}'", cpf);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            Cliente cli = ListaDeCliente(retorno).FirstOrDefault();

            if (cli == null)
                return true;
            else
                return false;
        }



        public Boolean ChecaEmail(string email)
        {
            string strQuery = string.Format("select * from tbl_cliente " +
                "where nm_email = '{0}'", email);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            Cliente cli = ListaDeCliente(retorno).FirstOrDefault();

            if (cli == null)
                return true;
            else
                return false;
        }



        private List<Cliente> ListaDeCliente(MySqlDataReader retorno)
        {
            var clientes = new List<Cliente>();

            while (retorno.Read())
            {
                var tempCliente = new Cliente()
                {
                    cd_cliente = int.Parse(retorno["cd_cliente"].ToString()),
                    nm_cli = retorno["nm_cli"].ToString(),
                    cpf_cli = retorno["cpf_cli"].ToString(),
                    nm_email = retorno["nm_email"].ToString(),
                    senha_cli = retorno["senha_cli"].ToString(),
                    flag = int.Parse(retorno["flag"].ToString())
                };
                clientes.Add(tempCliente);
            }

            retorno.Close();
            return clientes;
        }



        public void Insert(Cliente cliente)
        {
            string strQuery = string.Format("insert into tbl_Cliente" +
                "(nm_cli,cpf_cli,nm_email,senha_cli)" +
                " values('{0}','{1}','{2}','{3}')",
                cliente.nm_cli,
                cliente.cpf_cli,
                cliente.nm_email,
                cliente.senha_cli);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(Cliente cliente)
        {
            string strQuery = "update tbl_cliente set ";
            strQuery += string.Format("nm_cli = '{0}', ", cliente.nm_cli);
            strQuery += string.Format("cpf_cli = '{0}', ", cliente.cpf_cli);
            strQuery += string.Format("nm_email = '{0}', ", cliente.nm_email);
            strQuery += string.Format("senha_cli = '{0}' ", cliente.senha_cli);
            strQuery += string.Format("where cd_cliente = {0}", cliente.cd_cliente);

            conexao.ExecutaComando(strQuery);
        }



        public void Inativar(int cd)
        {
            string strQuery = "update tbl_cliente set flag = 1 ";
            strQuery += string.Format("where cd_cliente = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }



        public void Reativar(int cd)
        {
            string strQuery = "update tbl_cliente set flag = 0 ";
            strQuery += string.Format("where cd_cliente = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }
    }
}
