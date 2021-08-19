using MySql.Data.MySqlClient;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.DAOs
{
    public class CarrinhoDAO
    {

        private Conexao conexao = new Conexao();



        public List<Carrinho> Listar()
        {
            string strQuery = "select * from vw_carrinho order by dt_venda desc;";
            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCarrinho(retorno);
        }



        public Carrinho ListarPorCd(int cd)
        {
            string strQuery = string.Format("select * from vw_carrinho " +
                "where cd_Carrinho = {0}", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCarrinho(retorno).FirstOrDefault();
        }



        public List<Carrinho> ListarPorCliente(int cd)
        {
            string strQuery = string.Format("select * from vw_carrinho " +
                "where cd_cliente = {0} order by dt_venda desc", cd);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCarrinho(retorno);
        }



        public List<Carrinho> ListarPorCpf(string cpf_cli)
        {
            string strQuery = string.Format("select * from vw_carrinho " +
                "where cpf_cli like '{0}' order by dt_venda desc", cpf_cli);

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);
            return ListaDeCarrinho(retorno);
        }



        private List<Carrinho> ListaDeCarrinho(MySqlDataReader retorno)
        {
            var cars = new List<Carrinho>();

            while (retorno.Read())
            {
                var tempCar = new Carrinho()
                {
                    cd_carrinho = int.Parse(retorno["cd_carrinho"].ToString()),
                    cd_cliente = int.Parse(retorno["cd_cliente"].ToString()),
                    cd_forma = int.Parse(retorno["cd_forma"].ToString()),
                    dt_venda = DateTime.Parse(retorno["dt_venda"].ToString()),
                    vl_total = double.Parse(retorno["vl_total"].ToString()),
                    cpf_cli = retorno["cpf_cli"].ToString(),
                    nm_cli = retorno["nm_cli"].ToString(),
                    nm_forma = retorno["nm_forma"].ToString()
                };
                cars.Add(tempCar);
            }

            retorno.Close();
            return cars;
        }



        public int InsertGerenciamento(Carrinho car)
        {
            string strQuery = string.Format("insert into tbl_carrinho" +
                "(cd_funcionario,cd_cliente,cd_forma,vl_total,dt_venda)" +
                " values({0},{1},{2},'{3}','{4}');" +
                "select max(cd_carrinho) from tbl_carrinho;",
                car.cd_funcionario,
                car.cd_cliente,
                car.cd_forma,
                car.vl_total.ToString().Replace(",", "."),
                car.dt_venda.ToString("yyyy-MM-dd"));

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);

            int cd = 0;

            if (retorno.Read())
            {
                cd = int.Parse(retorno["max(cd_carrinho)"].ToString());
            }
            retorno.Close();

            return cd;
        }



        public int InsertCliente(Carrinho car)
        {
            string strQuery = string.Format("insert into tbl_carrinho" +
                "(cd_cliente,cd_forma,vl_total,dt_venda)" +
                " values({0},{1},'{2}','{3}');" +
                "select max(cd_carrinho) from tbl_carrinho;",
                car.cd_cliente,
                car.cd_forma,
                car.vl_total.ToString().Replace(",", "."),
                car.dt_venda.ToString("yyyy-MM-dd"));

            MySqlDataReader retorno = conexao.RetornaComando(strQuery);

            int cd = 0;

            if (retorno.Read())
            {
                cd = int.Parse(retorno["max(cd_carrinho)"].ToString());
            }
            retorno.Close();

            return cd;
        }
    }
}
