using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class AbastecimentoDAO
    {

        private Conexao conexao = new Conexao();



        public List<Abastecimento> Listar()
        {
            string strQuery = "select * from vw_abastecimento order by dt_abastecimento desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeAbastecimento(retorno);
        }



        public Abastecimento ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from vw_abastecimento " +
                "where cd_abastecimento = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeAbastecimento(retorno).FirstOrDefault();
        }



        public List<Abastecimento> ListarPorCnpj(string no_cnpj)
        {
            string strQuery = string.Format("select * from vw_abastecimento " +
                "where no_cnpj like '{0}' " +
                "order by dt_abastecimento desc;",no_cnpj);
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeAbastecimento(retorno);
        }



        private List<Abastecimento> ListaDeAbastecimento(MySqlDataReader retorno)
        {
            var abss = new List<Abastecimento>();

            while (retorno.Read())
            {
                var tempAbs = new Abastecimento()
                {
                    cd_abastecimento = int.Parse(retorno["cd_abastecimento"].ToString()),
                    cd_fornecedor = int.Parse(retorno["cd_fornecedor"].ToString()),
                    dt_abastecimento = DateTime.Parse(retorno["dt_abastecimento"].ToString()),
                    no_cnpj = retorno["no_cnpj"].ToString(),
                    nm_fornecedor = retorno["nm_fornecedor"].ToString()
                };
                abss.Add(tempAbs);
            }

            retorno.Close();
            return abss;
        }



        public int Insert(Abastecimento abs)
        {
            string strQuery = string.Format("insert into tbl_abastecimento(cd_fornecedor, dt_abastecimento)" +
                "values ({0},'{1}');" +
                "select max(cd_abastecimento) from tbl_abastecimento;",
                abs.cd_fornecedor,
                abs.dt_abastecimento.ToString("yyyy-MM-dd"));


            MySqlDataReader retorno = conexao.RetornaComando(strQuery);

            int cd = 0;

            if(retorno.Read())
            {
                cd = int.Parse(retorno["max(cd_abastecimento)"].ToString());
            }
            retorno.Close();

            return cd;
        }



        public void Update(Abastecimento abs)
        {
            string strQuery = "update tbl_abastecimento set ";
            strQuery += string.Format("cd_fornecedor = {0}, ", abs.cd_fornecedor);
            strQuery += string.Format("dt_abastecimento = '{0}' "
                , abs.dt_abastecimento.ToString("yyyy-MM-dd"));
            strQuery += string.Format("where cd_abastecimento = {0}", abs.cd_abastecimento);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd)
        {
            string strQuery = string.Format("delete from tbl_itensAbs " +
                "where cd_abastecimento = {0}", cd);

            conexao.ExecutaComando(strQuery);

            strQuery = string.Format("delete from tbl_abastecimento " +
                "where cd_abastecimento = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }
    }
}
