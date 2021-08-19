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
    public class UsuarioController : Controller
    {
        CarrinhoDAO carDAO = new CarrinhoDAO();
        ClienteDAO cliDAO = new ClienteDAO();
        EntregaDAO entDAO = new EntregaDAO();
        ItensCarrinhoDAO icDAO = new ItensCarrinhoDAO();
        ProdutoDAO prodDAO = new ProdutoDAO();
        SuporteDAO supDAO = new SuporteDAO();
        TesteDeGameDAO testeDAO = new TesteDeGameDAO();

        // GET: Usuario

        // Página de Alterar dados
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index","Home");

            int cd = (int)Session["CdClienteLogado"];

            return View(cliDAO.ListarPorCd(cd));
        }

        [HttpPost]
        public ActionResult Index(Cliente cliente)
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



        // Lista de compras
        public ActionResult Compras()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            int cd = (int)Session["CdClienteLogado"];

            return View(carDAO.ListarPorCliente(cd));
        }

        public ActionResult ItensCompra(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            return View(icDAO.Listar(cd));
        }



        public ActionResult DadosEntrega(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            Entrega ent = entDAO.ListarPorCd(cd);
            ViewBag.Complemento = ent.nm_complemento;

            return View(ent);
        }



        // Suporte
        public ActionResult Suporte(int cd_carrinho, int cd_produto)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            Suporte sup = new Suporte();
            sup.cd_carrinho = cd_carrinho;
            sup.cd_produto = cd_produto;

            return View(sup);
        }

        [HttpPost]
        public ActionResult Suporte(Suporte sup)
        {
            sup.dt_sup = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    supDAO.Insert(sup);
                    return RedirectToAction("ListaSuportes");
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

        public ActionResult ListaSuportes()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            string cpf = (string)Session["CpfClienteLogado"];
            return View(supDAO.ListarPorCliente(cpf));
        }

        public ActionResult DetalhesSuporte(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            Suporte sup = supDAO.ListarPorCd(cd);

            ViewBag.Cd = cd;
            ViewBag.Status = sup.sup_status;

            return View(sup);
        }

        public ActionResult DeletarSuporte(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            supDAO.Delete(cd);

            return RedirectToAction("ListaSuportes");
        }



        // Teste De Game
        public ActionResult ListaDeTestes()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            int cd = (int)Session["CdClienteLogado"];

            return View(testeDAO.ListarPorCliente(cd));
        }

        public ActionResult EscolherGame()
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            return View(prodDAO.ListarPorCategoria("Games"));
        }

        public ActionResult CadastrarTeste(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            TesteDeGame teste = new TesteDeGame();
            teste.cd_produto = cd;
            teste.cd_cliente = (int)Session["CdClienteLogado"];

            return View(teste);
        }

        [HttpPost]
        public ActionResult CadastrarTeste(TesteDeGame teste)
        {
            if (ModelState.IsValid)
            {
                if (testeDAO.ChecaDisp(teste) && teste.dt_teste > DateTime.Now)
                {
                    try
                    {
                        testeDAO.Insert(teste);
                        return RedirectToAction("ListaDeTestes");
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

        public ActionResult CancelarTeste(int cd)
        {
            if (Session["FuncionarioLogado"] != null)
                return RedirectToAction("Index", "Funcionario", new { area = "Gerenciamento" });
            if (Session["ClienteLogado"] == null)
                return RedirectToAction("Index", "Home");

            testeDAO.Delete(cd);

            return RedirectToAction("ListaDeTestes");
        }
    }
}