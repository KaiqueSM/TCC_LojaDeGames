using PythonGames.Classes.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PythonGames.Areas.Gerenciamento.Controllers
{
    public class EntregaController : Controller
    {
        EntregaDAO entregaDAO = new EntregaDAO();
        // GET: Gerenciamento/Entrega
        public ActionResult Index()
        {
            if (Session["FuncionarioLogado"] == null)
                return RedirectToAction("Login", "Funcionario");

            return View(entregaDAO.Listar());
        }
    }
}