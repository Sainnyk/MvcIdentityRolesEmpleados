using Microsoft.AspNetCore.Mvc;
using MvcIdentityRolesEmpleados.Filters;
using MvcIdentityRolesEmpleados.Models;
using MvcIdentityRolesEmpleados.Repositories;

namespace MvcIdentityRolesEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;
        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }

        [AuthorizeEmpleados]
        public IActionResult PerfilEmpleado()
        {
            return View();
        }

        [AuthorizeEmpleados]
        public IActionResult CompisCurro()
        {
            //Debemos buscar el claim con el dato del departamento
            string dato = HttpContext.User.FindFirst("Departamento").Value; //Sin autorizacion no podemos recuperar este valor(hay que decorar el metodo)
            int iddepartamento = int.Parse(dato);
            List<Empleado> empleados = this.repo.GetEmpleadosDepartamento(iddepartamento);
            return View(empleados);
        }
    }
}
