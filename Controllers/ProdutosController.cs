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
    public class ProdutosController : Controller
    {
        private BDVendaDiretaContect db = new BDVendaDiretaContect();

        public ActionResult Index()
        {
            if (Session["UsuarioId"] != null)
            {
                IQueryable<Produto> produto = db.Produto.Where(p => p.Vendido == false);
                return View(produto.ToList());
            }
            return RedirectToAction("Login", "Usuarios");
        }

        public ActionResult Details(int? id)
        {
            if (Session["UsuarioId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Produto produto = db.Produto.Find(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                return View(produto);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        public ActionResult Create()
        {
            if (Session["UsuarioId"] != null)
            {
                ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome");
                return View();
            }
            return RedirectToAction("Login", "Usuarios");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoId,UsuarioId,Nome,Preco,Vendido")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.Vendido = false;
                produto.UsuarioId = Convert.ToInt32(Session["UsuarioId"]);
                db.Produto.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", produto.UsuarioId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "ProdutoId,UsuarioId,Nome,Preco,Vendido")] Produto produto)
        { 
                Produto vendido = db.Produto.Find(produto.ProdutoId);
                vendido.Vendido = true;
                Usuario usuario = db.Usuario.Find(vendido.UsuarioId);
                usuario.Receita = usuario.Receita + vendido.Preco;
                db.SaveChanges();
                Session["UsuarioReceita"] = usuario.Receita;
                return RedirectToAction("Index");
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
;