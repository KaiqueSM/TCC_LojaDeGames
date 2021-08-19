using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class TesteDeGameDAO
    {

        private Conexao conexao = new Conexao();



        public List<TesteDeGame> Listar()
        {
            string strQuery = "select * from vw_teste order by cd_teste desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeTesteDeGame(retorno);
        }



        public TesteDeGame ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from vw_teste " +
                "where cd_teste = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeTesteDeGame(retorno).FirstOrDefault();
        }



        public List<TesteDeGame> ListarPorCliente(int cd)
        {
            string strQuery = string.Format("select * from vw_teste " +
                "where cd_cliente = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeTesteDeGame(retorno);
        }



        public List<TesteDeGame> ListarPorCpf(string cpf_cli)
        {
            string strQuery = string.Format("select * from vw_teste " +
                "where cpf_cli like '{0}'", cpf_cli);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeTesteDeGame(retorno);
        }



        public bool ChecaDisp(TesteDeGame teste)
        {
            string strQuery = string.Format("select * from vw_teste " +
                "where hr_teste = '{0}' and dt_teste = '{1}'", 
                teste.dt_teste,teste.dt_teste.ToString("yyyy-MM-dd"));

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            TesteDeGame testeDeGame = ListaDeTesteDeGame(retorno).FirstOrDefault();

            if (testeDeGame == null)
                return true;
            else
                return false;
        }



        private List<TesteDeGame> ListaDeTesteDeGame(MySqlDataReader retorno)
        {
            var testes = new List<TesteDeGame>();

            while (retorno.Read())
            {
                var tempTeste = new TesteDeGame()
                {
                    cd_teste = int.Parse(retorno["cd_teste"].ToString()),
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    cd_cliente = int.Parse(retorno["cd_cliente"].ToString()),
                    nm_cli = retorno["nm_cli"].ToString(),
                    cpf_cli = retorno["cpf_cli"].ToString(),
                    dt_teste = DateTime.Parse(retorno["dt_teste"].ToString()),
                    hr_teste = retorno["hr_teste"].ToString(),
                    nm_prod = retorno["nm_prod"].ToString(),
                    link_img = retorno["link_img"].ToString()
                };
                testes.Add(tempTeste);
            }

            retorno.Close();
            return testes;
        }



        public void Insert(TesteDeGame teste)
        {
            string strQuery = string.Format("insert into tbl_teste" +
                "(cd_produto,cd_cliente,hr_teste,dt_teste)" +
                " values({0},{1},'{2}','{3}')",
                teste.cd_produto,
                teste.cd_cliente,
                teste.hr_teste,
                teste.dt_teste.ToString("yyyy-MM-dd"));

            conexao.ExecutaComando(strQuery);
        }



        public void Update(TesteDeGame teste)
        {
            string strQuery = "update tbl_teste set ";
            strQuery += string.Format("hr_teste = '{0}', ", teste.hr_teste);
            strQuery += string.Format("dt_teste = '{0}' "
                , teste.dt_teste.ToString("yyyy-MM-dd"));
            strQuery += string.Format("where cd_teste = {0}", teste.cd_teste);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd)
        {
            string strQuery = string.Format("delete from tbl_teste " +
                "where cd_teste = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }
    }
}
