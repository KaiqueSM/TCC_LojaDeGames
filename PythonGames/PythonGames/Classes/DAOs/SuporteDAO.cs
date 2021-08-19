using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class SuporteDAO
    {

        private Conexao conexao = new Conexao();



        public List<Suporte> Listar()
        {
            string strQuery = "select * from vw_suporte order by dt_sup desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeSuporte(retorno);
        }



        public Suporte ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from vw_suporte " +
                "where cd_suporte = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeSuporte(retorno).FirstOrDefault();
        }



        public List<Suporte> ListarPorCliente(string cpf)
        {
            string strQuery = string.Format("select * from vw_suporte " +
                "where cpf_cli = '{0}'", cpf);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeSuporte(retorno);
        }



        public List<Suporte> ListarPorCpf(string cpf)
        {
            string strQuery = string.Format("select * from vw_suporte " +
                "where cpf_cli like '{0}'", cpf);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeSuporte(retorno);
        }



        private List<Suporte> ListaDeSuporte(MySqlDataReader retorno)
        {
            var sups = new List<Suporte>();

            while (retorno.Read())
            {
                var tempsup = new Suporte()
                {
                    cd_suporte = int.Parse(retorno["cd_suporte"].ToString()),
                    cd_produto = int.Parse(retorno["cd_produto"].ToString()),
                    cd_carrinho = int.Parse(retorno["cd_carrinho"].ToString()),
                    sup_status = int.Parse(retorno["sup_status"].ToString()),
                    nm_cli = retorno["nm_cli"].ToString(),
                    cpf_cli = retorno["cpf_cli"].ToString(),
                    dt_sup = DateTime.Parse(retorno["dt_sup"].ToString()),
                    sup_descricao = retorno["sup_descricao"].ToString(),
                    nm_prod = retorno["nm_prod"].ToString(),
                    nm_email = retorno["nm_email"].ToString(),
                    link_img = retorno["link_img"].ToString()
                };
                sups.Add(tempsup);
            }

            retorno.Close();
            return sups;
        }



        public void Insert(Suporte sup)
        {
            string strQuery = string.Format("insert into tbl_suporte" +
                "(cd_produto,cd_carrinho,sup_descricao,dt_sup)" +
                " values({0},{1},'{2}','{3}')",
                sup.cd_produto,
                sup.cd_carrinho,
                sup.sup_descricao,
                sup.dt_sup.ToString("yyyy-MM-dd"));

            conexao.ExecutaComando(strQuery);
        }



        public void Update(Suporte sup)
        {
            string strQuery = "update tbl_suporte set ";
            strQuery += string.Format("cd_produto = {0}, ", sup.cd_produto);
            strQuery += string.Format("cd_carrinho = {0}, ", sup.cd_carrinho);
            strQuery += string.Format("sup_descricao = '{0}', ", sup.sup_descricao);
            strQuery += string.Format("dt_sup = '{0}' "
                , sup.dt_sup.ToString("yyyy-MM-dd"));
            strQuery += string.Format("where cd_suporte = {0}", sup.cd_suporte);

            conexao.ExecutaComando(strQuery);
        }



        public void UpdateStatus(int cd)
        {
            string strQuery = String.Format("update tbl_suporte set " +
                "sup_status = 1 where cd_suporte = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd)
        {
            string strQuery = string.Format("delete from tbl_suporte " +
                "where cd_suporte = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }
    }
}
