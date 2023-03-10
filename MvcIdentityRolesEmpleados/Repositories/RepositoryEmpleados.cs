using MvcIdentityRolesEmpleados.Data;
using MvcIdentityRolesEmpleados.Models;

namespace MvcIdentityRolesEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public Empleado FindEmpleado(int id)
        {
            return this.context.Empleados.FirstOrDefault(z => z.IdEmpleado == id);
        }

        public List<Empleado> GetEmpleadosDepartamento (int iddepartamento)
        {
            return this.context.Empleados.Where(z=>z.Departamento == iddepartamento).ToList();
        }

        //Necesitamos un metodo para comprobar si existe un empleado por su usuario (apellido en este caso) y password(emp_no)
        public Empleado ExisteEmpleado(string apellido,int idempleado)
        {
            var consulta = from datos in this.context.Empleados where datos.Apellido == apellido && datos.IdEmpleado == idempleado select datos;
            return consulta.FirstOrDefault();
        }

    }
}
