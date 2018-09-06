using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BDVendaDireta.Models;

namespace BDVendaDireta.Controllers
{
    public class UsuariosController : Controller
    {
        private BDVendaDiretaContect db = new BDVendaDiretaContect();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Senha")] Usuario usuario)
        {
            if (Session["UsuarioId"] != null)
            {
                return RedirectToAction("Index", "Produtos");
            }
                Usuario usuarioEncontrado = db.Usuario.Where(u => u.Email == usuario.Email).FirstOrDefault();
            if (usuarioEncontrado != null && usuarioEncontrado.Senha.Equals(usuario.Senha))
            {
                ViewBag.Error = "";
                Session["UsuarioId"] = usuarioEncontrado.UsuarioId;
                Session["UsuarioNome"] = usuarioEncontrado.Nome.ToString();
                Session["UsuarioReceita"] = usuarioEncontrado.Receita.ToString();
                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                ViewBag.Error = "Não foi possível realizar o login. Tente novamente.";
            }
            return View(usuario);
        }

        public ActionResult Logout()
        {
            Session["UsuarioId"] = null;
            Session["UsuarioNome"] = null;
            Session["UsuarioReceita"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            if (Session["UsuarioId"] != null)
            {
                return RedirectToAction("Index", "Produtos");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,Nome,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Receita = 0;
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
