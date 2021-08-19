using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Areas.Gerenciamento.Controllers
{
    public class SuporteController : Controller
    {
        CarrinhoDAO carDAO = new CarrinhoDAO();
        ItensCarrinhoDAO icDAO = new ItensCarrinhoDAO();
        SuporteDAO supDAO = new SuporteDAO();
        // GET: Gerenciamento/Suporte
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            return View(supDAO.Listar());
        }

        [HttpPost]
        public ActionResult Index(string cpf_cli)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (cpf_cli == "")
                cpf_cli = "___.___.___-__";
            return View(supDAO.ListarPorCpf(cpf_cli));
        }



        public ActionResult EscolherCarrinho()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            return View(carDAO.Listar());
        }

        [HttpPost]
        public ActionResult EscolherCarrinho(string cpf_cli)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if (cpf_cli == "")
                cpf_cli = "___.___.___-__";
            return View(carDAO.ListarPorCpf(cpf_cli));
        }



        public ActionResult VendaEscolhida(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            return View(icDAO.Listar(cd));
        }



        public ActionResult Cadastrar(int cd_produto, int cd_carrinho)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            Suporte suporte = new Suporte()
            {
                cd_carrinho = cd_carrinho,
                cd_produto = cd_produto
            };

            return View(suporte);
        }

        [HttpPost]
        public ActionResult Cadastrar(Suporte sup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    supDAO.Insert(sup);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.ErroMsg = "Algo Deu Errado :(";
                    return View();
                }
            }
            else
                return View();
        }



        public ActionResult Detalhes(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            Suporte sup = supDAO.ListarPorCd(cd);
            ViewBag.Cd = cd;
            ViewBag.Status = sup.sup_status;

            return View(sup);
        }



        public ActionResult AlterarStatus(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            supDAO.UpdateStatus(cd);
            return RedirectToAction("Detalhes", new { cd = cd });
        }



        public ActionResult Alterar(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            Suporte sup = supDAO.ListarPorCd(cd);
            ViewBag.Cd = cd;

            return View(sup);
        }

        [HttpPost]
        public ActionResult Alterar(Suporte sup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    supDAO.Update(sup);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.ErroMsg = "Algo Deu Errado :(";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }



        public ActionResult Deletar(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            supDAO.Delete(cd);

            return RedirectToAction("Index");
        }
    }
}