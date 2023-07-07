using System;
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
                    // Deshabilitando el almacenamiento en caché de la página
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();

                }
            }
            
        }

        Modulos modulos = new Modulos();
        Dao_Login login = new Dao_Login();

        private void LlenarTreeView()
        {
            // Obtener los módulos desde la base de datos
            List<Modulos> modulos = login.obtenerModulos(int.Parse(Session["rol"].ToString()));

        //    // Llamar al método recursivo para construir el árbol de nodos
        //    foreach (Modulo modulo in modulos)
        //    {
        //        if (modulo.ID_MODULO_PADRE == 0)
        //        {
        //            TreeNode nodoPadre = new TreeNode(modulo.NOMBRE, modulo.ID_MODULO.ToString());
        //            treeView.Nodes.Add(nodoPadre);
        //            ConstruirArbol(modulo.ID_MODULO, nodoPadre, modulos);
        //        }
        //    }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(ResolveUrl("~/WebLogIn.aspx"));
        }
    }
}