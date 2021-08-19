using PythonGames.Classes;
using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Controllers
{
    public class HomeController : Controller
    {
        ClienteDAO cliDAO = new ClienteDAO();
        ProdutoDAO prodDAO = new ProdutoDAO();

        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });

            return View(prodDAO.Listar());
        }



        public ActionResult Login()
        {
            if (Session["ClienteLogado"] == null & Session["FuncionarioLogado"] == null)
                return View();
            else if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(Cliente cliente)
        {
            Cliente clienteLogado = cliDAO.Login(cliente);

            if (clienteLogado != null)
            {
                Session["ClienteLogado"] = clienteLogado;
                Session["CdClienteLogado"] = clienteLogado.cd_cliente;
                Session["CpfClienteLogado"] = clienteLogado.cpf_cli;
                Session["FuncionarioLogado"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErroMsg = "Usuário e/ou Senha incorreto(s)";
                return View();
            }
        }



        public ActionResult Logout()
        {
            Session["ClienteLogado"] = null;
            Session["CdClienteLogado"] = null;
            Session["CpfClienteLogado"] = null;
            Session["FuncionarioLogado"] = null;
            Session["CarrinhoGerenciamento"] = null;
            Session["ItensCarrinhoGerenciamento"] = null;
            return RedirectToAction("Index");
        }



        public ActionResult CadastrarCliente()
        {
            if (Session["ClienteLogado"] == null & Session["FuncionarioLogado"] == null)
                return View();
            else if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CadastrarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliDAO.ChecaCPF(cliente.cpf_cli))
                {
                    if (Validacoes.ValidaCPF(cliente.cpf_cli))
                    {
                        if (cliDAO.ChecaEmail(cliente.nm_email))
                        {
                            try
                            {
                                cliDAO.Insert(cliente);
                                return RedirectToAction("Login");
                            }
                            catch
                            {
                                ViewBag.ErroMsg = "Algo deu Errado );";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.ErroMsg = "Este Email já está registrado!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.CPFMsg = "CPF Inválido!";
                        return View();
                    }

                }
                else
                {
                    ViewBag.CPFMsg = "Este CPF já está registrado!";
                    return View();
                }
            }
            else
                return View();
        }



        [HttpPost]
        public ActionResult Pesquisa(string busca)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });

            ViewBag.Busca = busca;

            if (busca == "")
                return Redirect(Request.UrlReferrer.ToString());

            return View(prodDAO.ListarPorBusca(busca));
        }



        public ActionResult Categoria(string cat)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });

            List<Produto> produtos = prodDAO.ListarPorCategoria(cat);
            if (produtos == null || produtos.Count() == 0)
                return Redirect(Request.UrlReferrer.ToString());

            ViewBag.Categoria = cat;

            return View(produtos);
        }
    }
}