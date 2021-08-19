using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class DetalheGameDAO
    {

        private Conexao conexao = new Conexao();



        public DetalheGame ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_detalheGame " +
                "where cd_produto = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeDetalheGame(retorno).FirstOrDefault();
        }



        private List<DetalheGame> ListaDeDetalheGame(MySqlDataReader retorno)
        {
            var detalhes = new List<DetalheGame>();

            while (retorno.Read())
            {
                var tempDetalheGame = new DetalheGame()
                {
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    nm_genero = retorno["nm_genero"].ToString(),
                    vl_indicacao = retorno["vl_indicacao"].ToString()
                };
                detalhes.Add(tempDetalheGame);
            }

            retorno.Close();
            return detalhes;
        }



        public void Insert(DetalheGame DetalheGame)
        {
            string strQuery = string.Format("insert into tbl_detalheGame" +
                "(cd_produto,nm_genero,vl_indicacao)" +
                " values({0},'{1}','{2}')",
                DetalheGame.cd_produto,
                DetalheGame.nm_genero,
                DetalheGame.vl_indicacao);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(DetalheGame DetalheGame)
        {
            string strQuery = "update tbl_detalheGame set ";
            strQuery += string.Format("nm_genero = '{0}', ", DetalheGame.nm_genero);
            strQuery += string.Format("vl_indicacao = '{0}' ", DetalheGame.vl_indicacao);
            strQuery += string.Format("where cd_produto = {0}", DetalheGame.cd_produto);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd)
        {
            string strQuery = string.Format("delete from tbl_detalheGame " +
                "where cd_produto = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }
    }
}
