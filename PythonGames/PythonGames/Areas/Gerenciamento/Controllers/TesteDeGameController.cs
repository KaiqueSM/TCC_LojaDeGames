using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Areas.Gerenciamento.Controllers
{
    public class TesteDeGameController : Controller
    {
        TesteDeGameDAO testeDAO = new TesteDeGameDAO();
        ClienteDAO cliDAO = new ClienteDAO();
        ProdutoDAO prodDAO = new ProdutoDAO();

        // GET: Gerenciamento/TesteDeGame
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
            {
                Session["TesteNmCli"] = null;
                Session["TesteCdCli"] = null;
                Session["TesteNmProd"] = null;
                Session["TesteCdProd"] = null;

                return View(testeDAO.Listar());
            }
            else
                return RedirectToAction("Login", "Funcionario");
        }        

        [HttpPost]
        public ActionResult Index(string cpf_cli)
        {
            if (Session["FuncionarioLogado"] != null)
            {
                Session["TesteNmCli"] = null;
                Session["TesteCdCli"] = null;
                Session["TesteNmProd"] = null;
                Session["TesteCdProd"] = null;

                if (cpf_cli == "")
                    cpf_cli = "___.___.___-__";
                return View(testeDAO.ListarPorCpf(cpf_cli));
            }
            else
                return RedirectToAction("Login", "Funcionario");
        }



        public ActionResult Cadastrar()
        {
            if (Session["FuncionarioLogado"] != null)
            {
                TesteDeGame teste = new TesteDeGame();
                if (Session["TesteCdCli"] != null)
                    teste.cd_cliente = (int) Session["TesteCdCli"];

                if (Session["TesteCdProd"] != null)
                    teste.cd_produto = (int)Session["TesteCdProd"];

                if (Session["TesteNmCli"] != null)
                    teste.nm_cli = (string)Session["TesteNmCli"];

                if (Session["TesteNmProd"] != null)
                    teste.nm_prod = (string)Session["TesteNmProd"];

                return View(teste);
            }
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult Cadastrar(TesteDeGame teste)
        {
            if (ModelState.IsValid)
            {
                if (testeDAO.ChecaDisp(teste))
                {
                    try
                    {
                        testeDAO.Insert(teste);
                        Session["TesteNmCli"] = null;
                        Session["TesteCdCli"] = null;
                        Session["TesteNmProd"] = null;
                        Session["TesteCdProd"] = null;
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
                    ViewBag.ErroMsg = "Horário indisponível!";
                    return View();
                }
            }
            else
                return View();
        }



        public ActionResult Alterar(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return View(testeDAO.ListarPorCd(cd));
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult Alterar(TesteDeGame teste)
        {
            if (ModelState.IsValid)
            {
                if (testeDAO.ChecaDisp(teste))
                {
                    try
                    {
                        testeDAO.Update(teste);
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
                    ViewBag.ErroMsg = "Horário indisponível!";
                    return View();
                }
            }
            else
                return View();
        }



        public ActionResult Deletar(int cd)
        {
            try
            {
                testeDAO.Delete(cd);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
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
            Session["TesteNmCli"] = nm;
            Session["TesteCdCli"] = cd;
            return RedirectToAction("Cadastrar");
        }



        public ActionResult EscolherProduto()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(prodDAO.ListarPorCategoria("Games"));
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult EscolherProduto(string busca)
        {
            if (Session["FuncionarioLogado"] != null)
                return View(prodDAO.ListarGamePorBusca(busca));
            else
                return RedirectToAction("Login", "Funcionario");
        }



        public ActionResult ProdutoEscolhido(int cd, string nm)
        {
            Session["TesteNmProd"] = nm;
            Session["TesteCdProd"] = cd;
            return RedirectToAction("Cadastrar");
        }
    }
}