using PythonGames.Classes;
using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Areas.Gerenciamento.Controllers
{
    public class VendaController : Controller
    {
        CarrinhoDAO carDAO = new CarrinhoDAO();
        CartaoDAO cartaoDAO = new CartaoDAO();
        ClienteDAO cliDAO = new ClienteDAO();
        FormaDePagamentoDAO frmDAO = new FormaDePagamentoDAO();
        ItensCarrinhoDAO icDAO = new ItensCarrinhoDAO();
        ProdutoDAO prodDAO = new ProdutoDAO();
        // GET: Gerenciamento/Venda
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            Session["CarrinhoGerenciamento"] = null;
            Session["ItensCarrinhoGerenciamento"] = null;
            Session["CarrinhoNmCli"] = null;
            Session["CarrinhoNmCli"] = null;

            return View(carDAO.Listar());
        }

        [HttpPost]
        public ActionResult Index(string cpf_cli)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (cpf_cli == "")
                cpf_cli = "___.___.___-__";
            return View(carDAO.ListarPorCpf(cpf_cli));
        }



        public ActionResult Cadastrar()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            ViewBag.Formas = frmDAO.ListarParaGerenciamento();

            Carrinho carrinho = new Carrinho();
            if (Session["CarrinhoCdCli"] != null)
                carrinho.cd_cliente = (int)Session["CarrinhoCdCli"];

            if (Session["CarrinhoNmCli"] != null)
                carrinho.nm_cli = (string)Session["CarrinhoNmCli"];

            return View(carrinho);
        }

        [HttpPost]
        public ActionResult Cadastrar(Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                carrinho.cd_funcionario = (int)Session["CdFuncionarioLogado"];
                carrinho.vl_total = 0;
                carrinho.dt_venda = DateTime.Now;
                Session["CarrinhoGerenciamento"] = carrinho;

                Session["ItensCarrinhoGerenciamento"] = new List<ItensCarrinho>();

                return RedirectToAction("ItensCarrinho");
            }
            else
                return View();
        }



        public ActionResult ItensCarrinho()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (Session["CarrinhoGerenciamento"] == null || Session["ItensCarrinhoGerenciamento"] == null)
                return RedirectToAction("Index");

            Carrinho carrinho = (Carrinho)Session["CarrinhoGerenciamento"];
            ViewBag.ValorCarrinho = carrinho.vl_total;
            ViewBag.FrmPag = carrinho.cd_forma;

            List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoGerenciamento"];
            return View(lista);
        }



        [HttpPost]
        public ActionResult ItensCarrinho(string nm_cartao, string no_cartao, string no_cvv)
        {
            List<ItensCarrinho> itens = (List<ItensCarrinho>)Session["ItensCarrinhoGerenciamento"];
            Carrinho carrinho = (Carrinho)Session["CarrinhoGerenciamento"];

            if (itens.Count == 0)
            {
                ViewBag.ErroMsg = "Não Há Itens Neste Carrinho";
                ViewBag.ValorCarrinho = carrinho.vl_total;
                ViewBag.FrmPag = carrinho.cd_forma;

                return View(itens);
            }

            Cartao cartao = new Cartao();
            if (carrinho.cd_forma != 3)
            {
                DateTime dt_validade = DateTime.Parse(Request["dt_validade"]);
                cartao = ChecaCartao(nm_cartao, no_cartao, no_cvv.Replace("_",string.Empty), dt_validade);
                if (cartao == null)
                {
                    ViewBag.ValorCarrinho = carrinho.vl_total;
                    ViewBag.FrmPag = carrinho.cd_forma;

                    return View(itens);
                }
            }

            try
            {
                int cd = carDAO.InsertGerenciamento(carrinho);
                foreach (ItensCarrinho item in itens)
                {
                    item.cd_carrinho = cd;
                    icDAO.Insert(item);
                }

                if (carrinho.cd_forma != 3)
                {
                    cartao.cd_carrinho = cd;
                    cartaoDAO.Insert(cartao);
                }

                Session["CarrinhoGerenciamento"] = null;
                Session["ItensCarrinhoGerenciamento"] = null;
            }
            catch
            {
                ViewBag.ErroMsg = "Algo Deu Errado :(";
                ViewBag.ValorCarrinho = carrinho.vl_total;
                ViewBag.FrmPag = carrinho.cd_forma;

                return View(itens);
            }

            return RedirectToAction("Index");
        }

        public Cartao ChecaCartao(string nm_cartao, string no_cartao, string no_cvv, DateTime dt_validade)
        {
            // validacoes cartao
            if(!Validacoes.ValidaCartao(no_cartao))
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



        public ActionResult Detalhes(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            Carrinho carrinho = carDAO.ListarPorCd(cd);
            if (carrinho.cd_forma != 3)
            {
                string no_cartao = cartaoDAO.ListarPorCd(cd);
                ViewBag.NoCartao = no_cartao.Substring(no_cartao.Length-4); ;
            }

            return View(carrinho);
        }



        public ActionResult ItensVenda(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            ViewBag.Cd = cd;
            return View(icDAO.Listar(cd));
        }



        public ActionResult EscolherProduto()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (Session["CarrinhoGerenciamento"] == null || Session["ItensCarrinhoGerenciamento"] == null)
                return RedirectToAction("Index");

            return View(prodDAO.Listar());
        }

        [HttpPost]
        public ActionResult EscolherProduto(string busca)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (Session["CarrinhoGerenciamento"] == null || Session["ItensCarrinhoGerenciamento"] == null)
                return RedirectToAction("Index");

            return View(prodDAO.ListarPorBusca(busca));
        }



        public ActionResult AdicionarItem(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (Session["CarrinhoGerenciamento"] == null || Session["ItensCarrinhoGerenciamento"] == null)
                return RedirectToAction("Index");

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
            if (item.qt_item > 0)
            {
                List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoGerenciamento"];
                
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

                Carrinho carrinho = (Carrinho)Session["CarrinhoGerenciamento"];
                carrinho.vl_total = 0;
                foreach (ItensCarrinho ic in lista)
                {
                    carrinho.vl_total += ic.vl_item;
                }
                Session["CarrinhoGerenciamento"] = carrinho;

                return RedirectToAction("ItensCarrinho");
            }
            else
                return RedirectToAction("AdicionarItem", new { cd = item.cd_produto });
        }



        public ActionResult DeletarItem(int cd)
        {
            List<ItensCarrinho> lista = (List<ItensCarrinho>)Session["ItensCarrinhoGerenciamento"];
            var itemExclusao = lista.FirstOrDefault(i => i.cd_produto == cd);
            lista.Remove(itemExclusao);

            Session["ItensCarrinhoGerenciamento"] = lista;

            Carrinho carrinho = (Carrinho)Session["CarrinhoGerenciamento"];
            carrinho.vl_total = 0;
            foreach (ItensCarrinho ic in lista)
            {
                carrinho.vl_total += ic.vl_item;
            }
            Session["CarrinhoGerenciamento"] = carrinho;

            return RedirectToAction("ItensCarrinho");
        }




        public ActionResult EscolherCliente()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(cliDAO.Listar());
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult EscolherCliente(string cpf_cli)
        {
            if (Session["FuncionarioLogado"] != null)
            {
                if (cpf_cli == "")
                    cpf_cli = "___.___.___-__";
                return View(cliDAO.ListarPorCpf(cpf_cli));
            }
            else
                return RedirectToAction("Login", "Funcionario");
        }



        public ActionResult ClienteEscolhido(int cd, string nm)
        {
            Session["CarrinhoNmCli"] = nm;
            Session["CarrinhoCdCli"] = cd;
            return RedirectToAction("Cadastrar");
        }
    }

}