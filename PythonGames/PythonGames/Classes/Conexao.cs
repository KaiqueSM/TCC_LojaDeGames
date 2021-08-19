using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PythonGames.Classes
{
    public class Conexao : IDisposable
    {
        private readonly MySqlConnection conexao;
        public Conexao()
        {
            conexao = new MySqlConnection("Server=localhost; DataBase=db_pythongames; user=root;pwd=79737406");
        }

        public void ExecutaComando(string strQuery)
        {
            if (conexao.State == System.Data.ConnectionState.Closed)
                conexao.Open();

            var vComando = new MySqlCommand(strQuery, conexao);
            vComando.ExecuteNonQuery();
            conexao.Close();
        }

        public MySqlDataReader RetornaComando(string strQuery)
        {
            if (conexao.State == System.Data.ConnectionState.Closed)
                conexao.Open();
            var vComando = new MySqlCommand(strQuery, conexao);
            return vComando.ExecuteReader();
        }

        public void Dispose()
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}