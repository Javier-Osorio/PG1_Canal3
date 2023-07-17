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
    public partial class WebLogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Dao_Login dao = new Dao_Login();
        Sesion sesion = new Sesion();
        Usuarios usu = new Usuarios();

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            sesion.Correo = txtCorreo.Value;
            sesion.Contra = txtPass.Value;

            if (dao.ValidarUsuario(sesion))
            {
                usu = dao.ObtenerUsuario(sesion);
                if(usu.Usuario != "")
                {
                    Session["logueado"] = usu.Usuario;
                    Session["rol"] = usu.ID_rol1;
                }  
                Response.Redirect(ResolveUrl("~/WebForms/WebBienvenida.aspx"));
            }
            else
            {
                //string script = @"Swal.fire({
                //        showConfirmButton: false,
                //        timer: 3000
                //        title: 'Correo y/o contraseña incorrecto',
                //        icon: 'warning'                        
                //    });";
                //ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
                string mensaje_alerta = "swal({title: 'Correo y/o contraseña incorrecto',icon: 'error',buttons: false}); ";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alerta", mensaje_alerta, true);
            }

        }
    }
}