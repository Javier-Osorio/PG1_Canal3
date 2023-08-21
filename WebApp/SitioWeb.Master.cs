using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    ltlMenu.Text = GenerarMenu();
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

        protected string GenerarMenu()
        {
            StringBuilder menu = new StringBuilder();
            List<Modulos> modulos = login.obtenerModulos(int.Parse(Session["rol"].ToString()));
            foreach (var modulo in modulos.Where(m => m.ID_modulo_padre1 == m.ID_modulo1))
            {
                //Response.Write("<li class='nav-item'>");
                menu.Append("<li class='nav-item'>");
                //Response.Write("<a href='#' class='nav-link'> " +
                //    "<i class='nav-icon fas fa-circle'></i>" +
                //    "<p> " + modulo.Nombre + "<i class='fas fa-angle-left right'></i> </p> </a>");
                menu.Append("<a href='#' class='nav-link'> " +
                    "<i class='nav-icon fas fa-circle'></i>" +
                    "<p> " + modulo.Nombre + "<i class='fas fa-angle-left right'></i> </p> </a>");
                //Response.Write("<ul class='nav nav-treeview'>"); 
                menu.Append("<ul class='nav nav-treeview'>");

                List < Modulos> submodulos = modulos.Where(m => m.ID_modulo_padre1 == modulo.ID_modulo1).ToList();
                foreach (var sub in submodulos)
                {
                    if (sub.ID_modulo1 != modulo.ID_modulo1)
                    {
                        //Response.Write($"<li class='nav-item'> <a href = '{ResolveUrl(sub.Url_path)}' class='nav-link'> <i class='far fa-circle nav-icon'></i><p>{sub.Nombre}</p></a></li>");
                        menu.Append($"<li class='nav-item'> <a href = '{ResolveUrl(sub.Url_path)}' class='nav-link'> <i class='far fa-circle nav-icon'></i><p>{sub.Nombre}</p></a></li>");
                    }                    
                }
                //Response.Write("</ul> </li>");
                menu.Append("</ul> </li>");
            }

            return menu.ToString();
        }          

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(ResolveUrl("~/WebLogIn.aspx"));
        }
    }
}