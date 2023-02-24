using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcIdentityRolesEmpleados.Filters
{
    public class AuthorizeEmpleadosAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if(user.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRoute("Manager", "Login");
            }
            else
            {
                //PARA GUARDAR EL ACTION Y EL CONTROLLER:
                //string controller = context.RouteData.Values["controller"].ToString();
                //string action = context.RouteData.Values["action"].ToString();
                //Hacemos un if y si es empleado y compiscurro podemos mostrar x info, sino z..

                //Validamos el rol con su oficio si está validado
                if (user.IsInRole("DIRECTOR") == false && user.IsInRole("PRESIDENTE") == false)
                {
                    context.Result = this.GetRoute("Manager", "ErrorAcceso");
                }
            }
        }



        //Creamos un metodo de ayuda por si redireccionamos a mas sitios ademas del login
        private RedirectToRouteResult GetRoute(string controlador,string vista)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(new
                                            {
                                                controller = controlador,
                                                action=vista
                                            }) ;
            return new RedirectToRouteResult(ruta);
        }

    }
}
