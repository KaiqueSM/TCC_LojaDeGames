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
    public class FornecedorController : Controller
    {
        FornecedorDAO fornDAO = new FornecedorDAO();
        // GET: Gerenciamento/Fornecedor
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(fornDAO.Listar());
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult Index(string no_cnpj)
        {
            if (Session["FuncionarioLogado"] != null)
            {
                if (no_cnpj == "")
                    no_cnpj = "__.___.___/____-__";
                return View(fornDAO.ListarPorCnpj(no_cnpj));
            }
            else
                return RedirectToAction("Login", "Funcionario");
        }



        public ActionResult ListaInativos()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(fornDAO.ListarInativos());
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult ListaInativos(string no_cnpj)
        {
            if (Session["FuncionarioLogado"] != null)
            {
                if (no_cnpj == "")
                    no_cnpj = "__.___.___/____-__";
                return View(fornDAO.ListarPorCnpjInativos(no_cnpj));
            }
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
        public ActionResult Cadastrar(Fornecedor forn)
        {
            if (ModelState.IsValid)
            {
                if (fornDAO.ChecaCNPJ(forn.no_cnpj))
                {
                    if (Validacoes.ValidaCNPJ(forn.no_cnpj))
                    {
                        try
                        {
                            fornDAO.Insert(forn);
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
                        ViewBag.ErroMsg = "CNPJ inválido!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErroMsg = "CNPJ Já Registrado!";
                    return View();
                }
            }
            else
                return View();
        }



        public ActionResult Alterar(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            return View(fornDAO.ListarPorCd(cd));
        }

        [HttpPost]
        public ActionResult Alterar(Fornecedor forn)
        {
            Fornecedor antForn = fornDAO.ListarPorCd(forn.cd_fornecedor);
            if (ModelState.IsValid)
            {
                if (antForn.no_cnpj == forn.no_cnpj || fornDAO.ChecaCNPJ(forn.no_cnpj))
                {
                    if (antForn.no_cnpj == forn.no_cnpj || Validacoes.ValidaCNPJ(forn.no_cnpj))
                    {
                        try
                        {
                            fornDAO.Update(forn);
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
                        ViewBag.ErroMsg = "CNPJ inválido!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErroMsg = "CNPJ Já Registrado!";
                    return View();
                }
            }
            else
                return View();
        }



        public ActionResult Desativar(int cd)
        {
            try
            {
                fornDAO.Inativar(cd);
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
                fornDAO.Reativar(cd);
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

    }
}