using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class FuncionarioDAO
    {

        private Conexao conexao = new Conexao();



        public List<Funcionario> Listar()
        {
            string strQuery = "select * from tbl_funcionario where flag = 0 order by cd_funcionario desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFuncionario(retorno);
        }



        public List<Funcionario> ListarInativos()
        {
            string strQuery = "select * from tbl_funcionario order by cd_funcionario desc";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFuncionario(retorno);
        }



        public Funcionario ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_funcionario " +
                "where cd_funcionario = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFuncionario(retorno).FirstOrDefault();
        }



        public List<Funcionario> ListarPorCpf(string cpf_func)
        {
            string strQuery = string.Format("select * from tbl_funcionario " +
                "where cpf_func like '{0}' " +
                "and flag = 0 " +
                "order by cd_funcionario desc", cpf_func); 
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFuncionario(retorno);
        }



        public List<Funcionario> ListarPorCpfInativos(string cpf_func)
        {
            string strQuery = string.Format("select * from tbl_funcionario " +
                "where cpf_func like '{0}' " +
                "order by cd_funcionario desc", cpf_func);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFuncionario(retorno);
        }



        public Funcionario Login(Funcionario func)
        {
            string strQuery = string.Format("select * from tbl_funcionario " +
                "where nm_usu = '{0}' and senha_usu = '{1}' and flag = 0", 
                func.nm_usu, func.senha_usu);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeFuncionario(retorno).FirstOrDefault();
        }



        public Boolean ChecaCPF(string cpf)
        {
            string strQuery = string.Format("select * from tbl_funcionario " +
                "where cpf_func = '{0}'", cpf);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            Funcionario func = ListaDeFuncionario(retorno).FirstOrDefault();

            if (func == null)
                return true;
            else
                return false;
        }



        public Boolean ChecaUsuario(string usu)
        {
            string strQuery = string.Format("select * from tbl_funcionario " +
                "where nm_usu = '{0}'", usu);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            Funcionario func = ListaDeFuncionario(retorno).FirstOrDefault();

            if (func == null)
                return true;
            else
                return false;
        }



        private List<Funcionario> ListaDeFuncionario(MySqlDataReader retorno)
        {
            var funcionarios = new List<Funcionario>();

            while (retorno.Read())
            {
                var tempFuncionario = new Funcionario()
                {
                    cd_funcionario = int.Parse(retorno["cd_funcionario"].ToString()),
                    nm_func = retorno["nm_func"].ToString(),
                    cpf_func = retorno["cpf_func"].ToString(),
                    nm_usu = retorno["nm_usu"].ToString(),
                    senha_usu = retorno["senha_usu"].ToString(),
                    func_acesso = int.Parse(retorno["func_acesso"].ToString()),
                    flag = int.Parse(retorno["flag"].ToString())
                };
                funcionarios.Add(tempFuncionario);
            }

            retorno.Close();
            return funcionarios;
        }



        public void Insert(Funcionario funcionario)
        {
            string strQuery = string.Format("insert into tbl_funcionario" +
                "(nm_func,cpf_func,nm_usu,senha_usu,func_acesso)" +
                " values('{0}','{1}','{2}','{3}',{4})",
                funcionario.nm_func,
                funcionario.cpf_func,
                funcionario.nm_usu,
                funcionario.senha_usu,
                funcionario.func_acesso);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(Funcionario funcionario)
        {
            string strQuery = "update tbl_funcionario set ";
            strQuery += string.Format("nm_func = '{0}', ", funcionario.nm_func);
            strQuery += string.Format("cpf_func = '{0}', ", funcionario.cpf_func);
            strQuery += string.Format("nm_usu = '{0}', ", funcionario.nm_usu);
            strQuery += string.Format("func_acesso = {0}, ", funcionario.func_acesso);
            strQuery += string.Format("senha_usu = '{0}' ", funcionario.senha_usu);
            strQuery += string.Format("where cd_funcionario = {0}", funcionario.cd_funcionario);

            conexao.ExecutaComando(strQuery);
        }



        public void Inativar(int cd)
        {
            string strQuery = "update tbl_funcionario set flag = 1 ";
            strQuery += string.Format("where cd_funcionario = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }



        public void Reativar(int cd)
        {
            string strQuery = "update tbl_funcionario set flag = 0 ";
            strQuery += string.Format("where cd_funcionario = {0}", cd);
            conexao.ExecutaComando(strQuery);
        }
    }
}
