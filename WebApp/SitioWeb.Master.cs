﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Dao;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp
{
    public partial class SitioWeb : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logueado"] == null)
            {
                Response.Redirect(ResolveUrl("~/WebLogIn.aspx"));
            }
            else
            {
                lblPrueba.Text = Session["logueado"].ToString();
                int rol = int.Parse(Session["rol"].ToString());

                if (!IsPostBack)
                {
                    //LlenarTreeView();
                    // Deshabilitando el almacenamiento en caché de la página
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();

                }
                //else
                //{
                //    Session.Timeout = 30;
                //}
            }
            
        }

        Modulos modulos = new Modulos();
        Dao_Login login = new Dao_Login();

        private void LlenarTreeView()
        {
            // Obtener los módulos desde la base de datos
            List<Modulos> modulos = login.obtenerModulos(int.Parse(Session["rol"].ToString()));

            // Llamar al método recursivo para construir el árbol de nodos
            foreach (Modulos modulo in modulos)
            {
                if (modulo.ID_modulo1 == modulo.ID_modulo_padre1)
                {
                    TreeNode nodoPadre = new TreeNode(modulo.Nombre, modulo.ID_modulo1.ToString());
                    //treeListMenu.Nodes.Add(nodoPadre);
                    ConstruirArbol(modulo.ID_modulo1, nodoPadre, modulos);
                }
            }
        }

        private void ConstruirArbol(int idModuloPadre, TreeNode NodoPadre, List<Modulos> modulos)
        {
            foreach (Modulos modulo in modulos)
            {
                if (modulo.ID_modulo_padre1 == idModuloPadre && modulo.ID_modulo1 != idModuloPadre)
                {
                    TreeNode nodoHijo = new TreeNode(modulo.Nombre, modulo.ID_modulo1.ToString(),"",modulo.Url_path,"_self");
                    NodoPadre.ChildNodes.Add(nodoHijo);                    
                }
            }
        }

        protected void GenerarMenu()
        {
            List<Modulos> modulos = login.obtenerModulos(int.Parse(Session["rol"].ToString()));
            foreach (var modulo in modulos.Where(m => m.ID_modulo_padre1 == m.ID_modulo1))
            {
                Response.Write("<li class='nav - item'>");
                Response.Write("<a href='#' class='nav-link'> " +
                    "" +
                    "<p> " + modulo.Nombre + "<i class='fas fa-angle-left right'></i> </p> </a>");
                Response.Write("<ul class='nav nav - treeview'>");
                var id_moduloPadre = modulo.ID_modulo1;
                foreach (var moduloHijo in modulos.Where(m => m.ID_modulo_padre1 == id_moduloPadre && modulo.ID_modulo1 != id_moduloPadre))
                {
                    Response.Write("<li class='nav - item'>" +
                        "<a href = '"+ ResolveUrl(moduloHijo.Url_path) +"' class='nav-link'> " +
                        "" +
                        "<p>"+ moduloHijo.Nombre +"</p> " +
                        "</a> </li>");
                }
                Response.Write("</ul> </li>");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(ResolveUrl("~/WebLogIn.aspx"));
        }
    }
}