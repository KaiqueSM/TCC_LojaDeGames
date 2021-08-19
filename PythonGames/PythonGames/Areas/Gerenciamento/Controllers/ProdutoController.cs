using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Areas.Gerenciamento.Controllers
{
    public class ProdutoController : Controller
    {
        ProdutoDAO prodDAO = new ProdutoDAO();
        // GET: Gerenciamento/Produto
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(prodDAO.Listar());
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult Index(string busca)
        {
            if (Session["FuncionarioLogado"] != null)
                return View(prodDAO.ListarPorBusca(busca));
            else
                return RedirectToAction("Login", "Funcionario");
        }



        public ActionResult ListaInativos()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(prodDAO.ListarInativos());
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult ListaInativos(string busca)
        {
            if (Session["FuncionarioLogado"] != null)
                return View(prodDAO.ListarPorBuscaInativos(busca));
            else
                return RedirectToAction("Login", "Funcionario");
        }



        public ActionResult Cadastrar()
        {
            if (Session["FuncionarioLogado"] != null)
                return View();
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.nm_categoria == "Games")
                {
                    try
                    {
                        prodDAO.InsertGame(produto);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.ErroMsg = "Algo deu Errado );";
                        return View();
                    }
                }
                else
                {
                    try
                    {
                        prodDAO.Insert(produto);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.ErroMsg = "Algo deu Errado );";
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }



        public ActionResult Alterar(int cd, string nm_categoria)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            Produto prod;

            if (nm_categoria == "Games")
                prod = prodDAO.ListarGame(cd);
            else
                prod = prodDAO.ListarPorCd(cd);

            ViewBag.cat = prod.nm_categoria;

            return View(prod);

        }

        [HttpPost]
        public ActionResult Alterar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.nm_categoria == "Games")
                {
                    try
                    {
                        prodDAO.UpdateGame(produto);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.ErroMsg = "Algo deu Errado );";
                        return View();
                    }
                }
                else
                {
                    try
                    {
                        prodDAO.Update(produto);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.ErroMsg = "Algo deu Errado );";
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }



        public ActionResult Detalhes(int cd, string nm_categoria)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            Produto prod;

            if (nm_categoria == "Games")
                prod = prodDAO.ListarGame(cd);
            else
                prod = prodDAO.ListarPorCd(cd);

            return View(prod);
        }



        public ActionResult Desativar(int cd)
        {
            try
            {
                prodDAO.Inativar(cd);
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }



        public ActionResult Reativar(int cd)
        {
            try
            {
                prodDAO.Reativar(cd);
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}