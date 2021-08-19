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
    public class FuncionarioController : Controller
    {
        // GET: Gerenciamento/Funcionario
        FuncionarioDAO funcDAO = new FuncionarioDAO();
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
            {
                Funcionario func = (Funcionario) Session["FuncionarioLogado"];
                return View(funcDAO.ListarPorCd(func.cd_funcionario));
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Index(Funcionario func)
        {
            Funcionario antFunc = funcDAO.ListarPorCd(func.cd_funcionario);
            if (ModelState.IsValid)
            {
                if (funcDAO.ChecaCPF(func.cpf_func) || func.cpf_func == antFunc.cpf_func)
                {
                    if (Validacoes.ValidaCPF(func.cpf_func))
                    {
                        if (funcDAO.ChecaUsuario(func.nm_usu) || func.nm_usu == antFunc.nm_usu)
                        {
                            try
                            {
                                funcDAO.Update(func);
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
                            ViewBag.ErroMsg = "Nome de Usuário indisponível";
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
            {
                return View();
            }
        }



        public ActionResult Listar()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            return View(funcDAO.Listar());
        }

        [HttpPost]
        public ActionResult Listar(string cpf_func)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            if (cpf_func == "")
                cpf_func = "___.___.___-__";
            return View(funcDAO.ListarPorCpf(cpf_func));
        }



        public ActionResult ListaInativos()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            return View(funcDAO.ListarInativos());
        }

        [HttpPost]
        public ActionResult ListaInativos(string cpf_func)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            if (cpf_func == "")
                cpf_func = "___.___.___-__";
            return View(funcDAO.ListarPorCpfInativos(cpf_func));
        }



        public ActionResult Cadastrar()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            return View();
        }
        
        [HttpPost]
        public ActionResult Cadastrar(Funcionario func)
        {
            if (ModelState.IsValid)
            {
                if(funcDAO.ChecaCPF(func.cpf_func))
                {
                    if (Validacoes.ValidaCPF(func.cpf_func))
                    {
                        if (funcDAO.ChecaUsuario(func.nm_usu))
                        {
                            try
                            {
                                funcDAO.Insert(func);
                                return RedirectToAction("Listar");
                            }
                            catch
                            {
                                ViewBag.ErroMsg = "Algo deu Errado );";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.ErroMsg = "Nome de Usuário indisponível";
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
            {
                return View();
            }
        }



        public ActionResult Alterar(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index");

            Funcionario func = funcDAO.ListarPorCd(cd);

            if (func.func_acesso == 0)
                return View(func);
            else
                return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Alterar(Funcionario func)
        {
            Funcionario antFunc = funcDAO.ListarPorCd(func.cd_funcionario);
            if (ModelState.IsValid)
            {
                if (func.cpf_func == antFunc.cpf_func || funcDAO.ChecaCPF(func.cpf_func))
                {
                    if (func.cpf_func == antFunc.cpf_func || Validacoes.ValidaCPF(func.cpf_func))
                    {
                        if (func.nm_usu == antFunc.nm_usu || funcDAO.ChecaUsuario(func.nm_usu))
                        {
                            try
                            {
                                funcDAO.Update(func);
                                return RedirectToAction("Listar");
                            }
                            catch
                            {
                                ViewBag.ErroMsg = "Algo deu Errado );";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.ErroMsg = "Nome de Usuário indisponível";
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
            {
                return View();
            }
        }



        public ActionResult Desativar(int cd)
        {
            try
            {
                funcDAO.Inativar(cd);
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
                funcDAO.Reativar(cd);
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }



        public ActionResult Login()
        {
            if (Session["FuncionarioLogado"] == null)
                return View();
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(Funcionario func)
        {
            Funcionario funcLogado = funcDAO.Login(func);

            if (funcLogado != null)
            {
                Session["FuncionarioLogado"] = funcLogado;
                Session["CdFuncionarioLogado"] = funcLogado.cd_funcionario;
                Session["AcFuncionarioLogado"] = funcLogado.func_acesso;
                Session["ClienteLogado"] = null;
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
            Session["FuncionarioLogado"] = null;
            Session["CdFuncionarioLogado"] = null;
            Session["AcFuncionarioLogado"] = null;
            Session["ClienteLogado"] = null;
            return RedirectToAction("Login");
        }

    }
}