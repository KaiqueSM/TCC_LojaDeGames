using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class EntregaDAO
    {
        private Conexao conexao = new Conexao();



        public List<Entrega> Listar()
        {
            string strQuery = "select * from tbl_entrega;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeEntrega(retorno);
        }



        public Entrega ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from tbl_entrega " +
                "where cd_carrinho = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeEntrega(retorno).FirstOrDefault();
        }



        private List<Entrega> ListaDeEntrega(MySqlDataReader retorno)
        {
            var entsentregas = new List<Entrega>();

            while (retorno.Read())
            {
                var tempEnt = new Entrega()
                {
                    cd_carrinho = int.Parse(retorno["cd_carrinho"].ToString()),
                    dt_entrega = DateTime.Parse(retorno["dt_entrega"].ToString()),
                    vl_frete = double.Parse(retorno["vl_frete"].ToString()),
                    no_cep = retorno["no_cep"].ToString(),
                    nm_estado = retorno["nm_estado"].ToString(),
                    nm_cidade = retorno["nm_cidade"].ToString(),
                    nm_bairro = retorno["nm_bairro"].ToString(),
                    nm_rua = retorno["nm_rua"].ToString(),
                    no_end = int.Parse(retorno["no_end"].ToString()),
                    nm_complemento = retorno["nm_complemento"].ToString()
                };
                entsentregas.Add(tempEnt);
            }

            retorno.Close();
            return entsentregas;
        }



        public void Insert(Entrega ent)
        {
            string strQuery = string.Format("insert into tbl_entrega" +
                "(cd_carrinho," +
                "dt_entrega," +
                "vl_frete," +
                "no_cep," +
                "nm_estado," +
                "nm_cidade," +
                "nm_bairro," +
                "nm_rua," +
                "no_end," +
                "nm_complemento)" +
                " values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}')",
                ent.cd_carrinho,
                ent.dt_entrega.ToString("yyyy-MM-dd"),
                ent.vl_frete.ToString().Replace(",", "."),
                ent.no_cep,
                ent.nm_estado,
                ent.nm_cidade,
                ent.nm_bairro,
                ent.nm_rua,
                ent.no_end,
                ent.nm_complemento);

            conexao.ExecutaComando(strQuery);
        }



        public void Update(Entrega ent)
        {
            string strQuery = "update tbl_entrega set ";
            strQuery += string.Format("vl_frete = '{0}', ", ent.vl_frete.ToString().Replace(",", "."));
            strQuery += string.Format("no_cep = '{0}', ", ent.no_cep);
            strQuery += string.Format("nm_estado = '{0}', ", ent.nm_estado);
            strQuery += string.Format("nm_cidade = '{0}', ", ent.nm_cidade);
            strQuery += string.Format("nm_bairro = '{0}', ", ent.nm_bairro);
            strQuery += string.Format("nm_rua = '{0}', ", ent.nm_rua);
            strQuery += string.Format("no_end = {0}, ", ent.no_end);
            strQuery += string.Format("nm_complemento = '{0}', ", ent.nm_complemento);
            strQuery += string.Format("dt_entrega = '{0}' "
                , ent.dt_entrega.ToString("yyyy-MM-dd"));
            strQuery += string.Format("where cd_carrinho = {0}", ent.cd_carrinho);

            conexao.ExecutaComando(strQuery);
        }



        public void Delete(int cd)
        {
            string strQuery = string.Format("delete from tbl_entrega " +
                "where cd_carrinho = {0}", cd);

            conexao.ExecutaComando(strQuery);
        }
    }
}
