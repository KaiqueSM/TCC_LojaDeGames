using PythonGames.Classes.DAOs;
using PythonGames.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Areas.Gerenciamento.Controllers
{
    public class PedidoDeCompraController : Controller
    {
        PedidoDeCompraDAO pedDAO = new PedidoDeCompraDAO();
        FornecedorDAO fornDAO = new FornecedorDAO();
        ItensPedidoDAO ipDAO = new ItensPedidoDAO();
        ProdutoDAO prodDAO = new ProdutoDAO();
        AbastecimentoDAO absDAO = new AbastecimentoDAO();
        ItensAbsDAO iaDAO = new ItensAbsDAO();

        // GET: Gerenciamento/PedidoDeCompra
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login","Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index","Funcionario");

            Session["PedNmProd"] = null;
            Session["PedCdProd"] = null;
            Session["PedCdPed"] = null;

            return View(pedDAO.Listar());
        }

        [HttpPost]
        public ActionResult Index(string no_cnpj)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            if (no_cnpj == "")
                no_cnpj = "__.___.___/____-__";
            return View(pedDAO.ListarPorCnpj(no_cnpj));
        }



        public ActionResult ListaPendentes()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            Session["PedNmProd"] = null;
            Session["PedCdProd"] = null;
            Session["PedCdPed"] = null;

            return View(pedDAO.ListarPendentes());
        }

        [HttpPost]
        public ActionResult ListaPendentes(string no_cnpj)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            if (no_cnpj == "")
                no_cnpj = "__.___.___/____-__";
            return View(pedDAO.ListarPorCnpjPendentes(no_cnpj));
        }



        public ActionResult ListaConcluídos()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            Session["PedNmProd"] = null;
            Session["PedCdProd"] = null;
            Session["PedCdPed"] = null;

            return View(absDAO.Listar());
        }

        [HttpPost]
        public ActionResult ListaConcluídos(string no_cnpj)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            if (no_cnpj == "")
                no_cnpj = "__.___.___/____-__";
            return View(absDAO.ListarPorCnpj(no_cnpj));
        }



        public ActionResult Cadastrar()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            return View(fornDAO.Listar());
        }

        [HttpPost]
        public ActionResult Cadastrar(string no_cnpj)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            if (no_cnpj == "")
                no_cnpj = "__.___.___/____-__";
            return View(fornDAO.ListarPorCnpj(no_cnpj));
        }

        public ActionResult CadPedido(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            PedidoDeCompra ped = new PedidoDeCompra();
            ped.cd_fornecedor = cd;
            ped.cd_funcionario = (int)Session["CdFuncionarioLogado"];
            ped.dt_pedido = DateTime.Now;
            try
            {
                pedDAO.Insert(ped);
            }
            catch { }

            return RedirectToAction("Index");
        }



        public ActionResult Deletar(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            try
            {
                pedDAO.Delete(cd);
            }
            catch { }

            return RedirectToAction("Index");
        }



        public ActionResult ItensAbsPed(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            return View(ipDAO.Listar(cd));
        }



        public ActionResult ItensAbs(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            return View(iaDAO.Listar(cd));
        }



        public ActionResult ItensPedido(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            Session["PedNmProd"] = null;
            Session["PedCdProd"] = null;

            Session["PedCdPed"] = cd;

            return View(ipDAO.Listar(cd));
        }



        public ActionResult CadastrarItem()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            ItensPedido item = new ItensPedido();
            item.cd_pedido = (int) Session["PedCdPed"];
            if (Session["PedNmProd"] != null)
                item.nm_prod = (string) Session["PedNmProd"];

            if (Session["PedCdProd"] != null)
                item.cd_produto = (int) Session["PedCdProd"];

            PedidoDeCompra ped = pedDAO.ListarPorCd(item.cd_pedido);
            if (ped.ped_status == 0)
                return View(item);
            else
                return RedirectToAction("ItensPedido", new { cd = item.cd_pedido });
        }

        [HttpPost]
        public ActionResult CadastrarItem(ItensPedido item)
        {
            if (ModelState.IsValid)
            {
                ItensPedido ip = ipDAO.ListarPorCds(item.cd_pedido, item.cd_produto);
                if (ip == null)
                {
                    try
                    {
                        ipDAO.Insert(item);
            return RedirectToAction("ItensPedido", new { cd = item.cd_pedido });
                    }
                    catch
                    {
                        ViewBag.ErroMsg = "Algo deu errado :(";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErroMsg = "Este produto já está cadastrado nesse pedido";
                    return View();
                }
            }
            else
                return View(); 
        }



        public ActionResult EscolherProduto()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            return View(prodDAO.Listar());
        }

        [HttpPost]
        public ActionResult EscolherProduto(string busca)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            return View(prodDAO.ListarPorBusca(busca));
        }

        public ActionResult ProdutoEscolhido(int cd, string nm)
        {
            Session["PedNmProd"] = nm;
            Session["PedCdProd"] = cd;
            return RedirectToAction("CadastrarItem");
        }



        public ActionResult AlterarItem(int cd_pedido, int cd_produto)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            PedidoDeCompra ped = pedDAO.ListarPorCd(cd_pedido);
            if (ped.ped_status == 0)
                return View(ipDAO.ListarPorCds(cd_pedido, cd_produto));
            else
                return RedirectToAction("ItensPedido", new { cd = cd_pedido });
        }

        [HttpPost]
        public ActionResult AlterarItem(ItensPedido item)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            try
            {
                ipDAO.Update(item);
            }
            catch { }

            return RedirectToAction("ItensPedido", new { cd = item.cd_pedido });
        }



        public ActionResult DeletarItem(ItensPedido item)
        {
            PedidoDeCompra ped = pedDAO.ListarPorCd(item.cd_pedido);
            if (ped.ped_status == 0)
            {
                try
                {
                    ipDAO.Delete(item.cd_pedido, item.cd_produto);
                }
                catch { }
            }

            return RedirectToAction("ItensPedido", new { cd = item.cd_pedido });
        }



        public ActionResult ConfirmarEntrega(int cd)
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");
            if ((int)Session["AcFuncionarioLogado"] != 1)
                return RedirectToAction("Index", "Funcionario");

            PedidoDeCompra ped = pedDAO.ListarPorCd(cd);
            if (ped.ped_status == 1)
                return RedirectToAction("Index");

            Abastecimento abs = new Abastecimento()
            {
                cd_fornecedor = ped.cd_fornecedor,
                dt_abastecimento = DateTime.Now
            };

            int cd_abs = absDAO.Insert(abs);

            List<ItensPedido> itensPed = ipDAO.Listar(cd);

            foreach(ItensPedido ip in itensPed)
            {
                ItensAbs item = new ItensAbs()
                {
                    cd_abastecimento = cd_abs,
                    cd_produto = ip.cd_produto,
                    qt_prod = ip.qt_prod
                };
                iaDAO.Insert(item);
            }

            pedDAO.UpdateStatus(cd);

            return RedirectToAction("Index");
        }
    }
}