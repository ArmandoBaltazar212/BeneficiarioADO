using BeneficiarioAPI.Data;
using BeneficiarioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeneficiarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        // GET: api/<EmpleadoController>
        [HttpGet("Lista")]
        public IEnumerable<Empleado> GetEmpleados()
        {
            return EmpleadoStore.EmpleadoLista();
        }

        // GET api/<ClienteController>/5
        [HttpGet("GetEmpleado{id}")]
        public Empleado GetEmpleado(int id)
        {
            return EmpleadoStore.SeleccionaEmpleado(id);
            // Se puede realizar de la siguiente forma no recomendable ya que consumiria mas recursos
            // return EmpleadoStore.EmpleadoLista().FirstOrDefault(u => u.Id == id);
        }

        // POST api/<ClienteController>
        [HttpPost("AgregaEmpleado")]
        public bool AgregaEmpleado(Empleado reg)
        {
            return EmpleadoStore.EmpleadoAgregar(reg);
        }

        // POST api/<ClienteController>
        [HttpPost("ActualizaEmpleado")]
        public bool ActualizaEmpleado(Empleado reg)
        {
            return EmpleadoStore.EmpleadoActualiza(reg);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("PutEmpleado{id}")]
        public void PutEmpleado(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("BorrarEmpleado{id}")]
        public bool DeleteEmpleado(int id)
        {
            return EmpleadoStore.EmpleadoBorrar(id);
        }
    }

}
