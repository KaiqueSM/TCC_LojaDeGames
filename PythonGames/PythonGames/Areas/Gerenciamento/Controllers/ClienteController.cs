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
    public class ClienteController : Controller
    {
        ClienteDAO cliDAO = new ClienteDAO();
        // GET: Gerenciamento/Cliente
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(cliDAO.Listar());
            else
                return RedirectToAction("Login","Funcionario");
        }

        [HttpPost]
        public ActionResult Index(string cpf_cli)
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



        public ActionResult ListaInativos()
        {
            if (Session["FuncionarioLogado"] != null)
                return View(cliDAO.ListarInativos());
            else
                return RedirectToAction("Login", "Funcionario");
        }

        [HttpPost]
        public ActionResult ListaInativos(string cpf_cli)
        {
            if (Session["FuncionarioLogado"] != null)
            {
                if (cpf_cli == "")
                    cpf_cli = "___.___.___-__";
                return View(cliDAO.ListarPorCpfInativos(cpf_cli));
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
        public ActionResult Cadastrar(Cliente cliente)
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
                            ViewBag.ErroMsg = "Este Email já está registrado!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ErroMsg = "CPF Inválido!";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ErroMsg = "Este CPF já está registrado!";
                    return View();
                }
            }
            else
                return View();
        }



        public ActionResult Alterar(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login","Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            return View(cliDAO.ListarPorCd(cd));
        }

        [HttpPost]
        public ActionResult Alterar(Cliente cliente)
        {
            Cliente antCli = cliDAO.ListarPorCd(cliente.cd_cliente);
            if (ModelState.IsValid)
            {
                if (antCli.cpf_cli == cliente.cpf_cli || cliDAO.ChecaCPF(cliente.cpf_cli))
                {
                    if (antCli.cpf_cli == cliente.cpf_cli || Validacoes.ValidaCPF(cliente.cpf_cli))
                    {
                        if (antCli.nm_email == cliente.nm_email || cliDAO.ChecaEmail(cliente.nm_email))
                        {
                            try
                            {
                                cliDAO.Update(cliente);
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
                            ViewBag.ErroMsg = "Este Email já está registrado!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ErroMsg = "CPF Inválido!";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ErroMsg = "Este CPF já está registrado!";
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
                cliDAO.Inativar(cd);
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
                cliDAO.Reativar(cd);
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}