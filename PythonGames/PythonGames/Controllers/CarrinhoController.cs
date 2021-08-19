using PythonGames.Classes;
using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using PythonGames.ServiceReferenceCorreios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Controllers
{
    public class CarrinhoController : Controller
    {
        CarrinhoDAO carDAO = new CarrinhoDAO();
        CartaoDAO cartaoDAO = new CartaoDAO();
        EntregaDAO entDAO = new EntregaDAO();
        FormaDePagamentoDAO frmDAO = new FormaDePagamentoDAO();
        ItensCarrinhoDAO icDAO = new ItensCarrinhoDAO();
        ProdutoDAO prodDAO = new ProdutoDAO();
        // GET: Carrinho
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            Carrinho carrinho = (Carrinho)Session["CarrinhoCliente"];
            if (carrinho == null)
            {
                carrinho = new Carrinho();
                carrinho.cd_cliente = (int)Session["CdClienteLogado"];
                Session["CarrinhoCliente"] = carrinho;
            }
            ViewBag.ValorCarrinho = carrinho.vl_total;

            List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoCliente"];
            if (lista == null)
            {
                lista = new List<ItensCarrinho>();
                Session["ItensCarrinhoCliente"] = lista;
            }

            return View(lista);
        }



        public ActionResult AdicionarItem(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });

            Produto prod = prodDAO.ListarPorCd(cd);
            ItensCarrinho item = new ItensCarrinho();

            item.cd_produto = prod.cd_produto;
            ViewBag.CdProduto = prod.cd_produto;
            ViewBag.NmProd = prod.nm_prod;
            ViewBag.LinkImg = prod.link_img;
            ViewBag.NmCat = prod.nm_categoria;
            ViewBag.VlProd = prod.vl_prod;
            ViewBag.QtEstoque = prod.qt_estoque;
            ViewBag.ProdDesc = prod.prod_desc;

            return View(item);
        }

        [HttpPost]
        public ActionResult AdicionarItem(ItensCarrinho item)
        {
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Login", "Home");

            if (item.qt_item > 0)
            {
                Carrinho carrinho = (Carrinho)Session["CarrinhoCliente"];
                if (carrinho == null)
                {
                    carrinho = new Carrinho();
                    carrinho.cd_cliente = (int)Session["CdClienteLogado"];
                    Session["CarrinhoCliente"] = carrinho;
                }
                ViewBag.ValorCarrinho = carrinho.vl_total;

                List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoCliente"];
                if (lista == null)
                {
                    lista = new List<ItensCarrinho>();
                    Session["ItensCarrinhoCliente"] = lista;
                }

                //deletando possivel existencia de outro item do mesmo produto
                var itemExclusao = lista.FirstOrDefault(i => i.cd_produto == item.cd_produto);
                lista.Remove(itemExclusao);

                Produto prod = prodDAO.ListarPorCd(item.cd_produto);

                item.cd_produto = prod.cd_produto;
                item.nm_prod = prod.nm_prod;
                item.link_img = prod.link_img;
                item.nm_categoria = prod.nm_categoria;
                item.vl_prod = prod.vl_prod;
                item.qt_estoque = prod.qt_estoque;
                item.prod_desc = prod.prod_desc;

                lista.Add(item);
                Session["ItensCarrinhoGerenciamento"] = lista;

                carrinho.vl_total = 0;
                foreach (ItensCarrinho ic in lista)
                {
                    carrinho.vl_total += ic.vl_item;
                }
                Session["CarrinhoGerenciamento"] = carrinho;

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("AdicionarItem", new { cd = item.cd_produto });
        }



        public ActionResult RemoverItem(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            Carrinho carrinho = (Carrinho)Session["CarrinhoCliente"];
            if (carrinho == null)
            {
                carrinho = new Carrinho();
                carrinho.cd_cliente = (int)Session["CdClienteLogado"];
                Session["CarrinhoCliente"] = carrinho;
            }

            List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoCliente"];
            if (lista == null)
            {
                lista = new List<ItensCarrinho>();
                Session["ItensCarrinhoCliente"] = lista;
            }

            var itemExclusao = lista.FirstOrDefault(i => i.cd_produto == cd);
            lista.Remove(itemExclusao);

            Session["ItensCarrinhoGerenciamento"] = lista;

            carrinho.vl_total = 0;
            foreach (ItensCarrinho ic in lista)
            {
                carrinho.vl_total += ic.vl_item;
            }
            Session["CarrinhoGerenciamento"] = carrinho;

            return RedirectToAction("Index");
        }



        public ActionResult ConfirmarCompra()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            Carrinho carrinho = (Carrinho)Session["CarrinhoCliente"];
            List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoCliente"];

            if (carrinho == null || lista == null || lista.Count == 0)
                return RedirectToAction("Index");

            ViewBag.Formas = frmDAO.ListarParaEcommerce();

            return View();
        }

        [HttpPost]
        public ActionResult ConfirmarCompra(Entrega entrega)
        {
            // Checando integridade do carrinho e dos itens do carrinho e checando se o carrinho não está vazio
            Carrinho carrinho = (Carrinho)Session["CarrinhoCliente"];
            List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoCliente"];
            if (carrinho == null || lista == null || lista.Count == 0)
            {
                ViewBag.ErroMsg = "Carrinho Vazio";
                ViewBag.Formas = frmDAO.ListarParaEcommerce();
                return View();
            }

            // Checando e passando valor do frete para o objeto entrega
            double dValorFrete;
            string sValorFrete = Request["vl_frete"];
            sValorFrete = sValorFrete.Replace("R$", string.Empty);
            //sValorFrete.Replace(",", ".");

            try
            {
                dValorFrete = Convert.ToDouble(sValorFrete);
                entrega.vl_frete = dValorFrete;
            }
            catch
            {
                ViewBag.ErroMsg = "Erro no cálculo do frete";
                ViewBag.Formas = frmDAO.ListarParaEcommerce();
                return View();
            }

            // Checando e passando o prazo de entrega e passando a data de previsão para o objeto entrega
            string sDtEntrega = Request["dt_entrega"];
            sDtEntrega = sDtEntrega.Replace(" dias", string.Empty);

            try
            {                
                entrega.dt_entrega = DateTime.Now.AddDays(int.Parse(sDtEntrega));
            }
            catch
            {
                ViewBag.ErroMsg = "Erro na Data de entrega";
                ViewBag.Formas = frmDAO.ListarParaEcommerce();
                return View();
            }

            // Passando código da forma para o carrinho
            int cdForma = int.Parse(Request["cd_forma"]);
            carrinho.cd_forma = cdForma;

            // Checando Dados do cartão e passando-os para um objeto
            string nm_cartao = Request["nm_cartao"];
            string no_cartao = Request["no_cartao"];
            string no_cvv = Request["no_cvv"];
            DateTime dt_validade = DateTime.Parse(Request["dt_validade"]);

            // Checando dados do cartão
            Cartao cartao = ChecaCartao(nm_cartao, no_cartao, no_cvv, dt_validade);
            if (cartao == null)
            {
                ViewBag.Formas = frmDAO.ListarParaEcommerce();
                return View();
            }

            //Inserindo dados no Banco e zerando Sessions
            carrinho.dt_venda = DateTime.Now;
            int cdIns = carDAO.InsertCliente(carrinho);
            foreach (ItensCarrinho item in lista)
            {
                item.cd_carrinho = cdIns;
                icDAO.Insert(item);
            }
            cartao.cd_carrinho = cdIns;
            cartaoDAO.Insert(cartao);
            entrega.cd_carrinho = cdIns;
            entDAO.Insert(entrega);
            Session["CarrinhoCliente"] = null;
            Session["ItensCarrinhoCliente"] = null;

            return RedirectToAction("Compras","Usuario");
        }



        public JsonResult CalculaFrete(string cep)
        {
            string cepArmazem = "06454911";

            CalcPrecoPrazoWSSoapClient wsCorreios = new CalcPrecoPrazoWSSoapClient();
            cResultado retornoCorreios = wsCorreios.CalcPrecoPrazo(
                string.Empty, string.Empty,"41106", cepArmazem, cep,
                "1",1,20,20,20,0,"N",0,"N"
            );

            string[] result = new string[2];
            result[0] = retornoCorreios.Servicos[0].PrazoEntrega;
            result[1] = retornoCorreios.Servicos[0].Valor;

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public Cartao ChecaCartao(string nm_cartao, string no_cartao, string no_cvv, DateTime dt_validade)
        {
            // validacoes cartao
            if (!Validacoes.ValidaCartao(no_cartao))
            {
                ViewBag.ErroMsg = "Número do Cartão inválido";
                return null;
            }
            if (DateTime.Now > dt_validade)
            {
                ViewBag.ErroMsg = "Data De Validade Do Cartão Inválida";
                return null;
            }
            if (no_cvv.Length != 3)
            {
                ViewBag.ErroMsg = "CVV Do Cartão Inválido";
                return null;
            }

            Cartao cartao = new Cartao();
            cartao.nm_cartao = nm_cartao;
            cartao.no_cartao = no_cartao;
            cartao.no_cvv = no_cvv;
            cartao.dt_validade = dt_validade;

            return cartao;
        }
    }
}