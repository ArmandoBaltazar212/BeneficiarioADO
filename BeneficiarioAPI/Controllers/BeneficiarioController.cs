using BeneficiarioAPI.Data;
using BeneficiarioAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace BeneficiarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : ControllerBase
    {

        [HttpGet("ListaBeneficiarios{idEmpleado}")]
        public IEnumerable<Beneficiario> GetBeneficiarios(int idEmpleado)
        {
            return BeneficiarioStore.BeneficiarioLista(idEmpleado);
        }


        [HttpGet("SelecionaBeneficiario{id}")]
        public Beneficiario GetBeneficiario(int id)
        {
            return BeneficiarioStore.SeleccionaBeneficiario(id);
        }

        [HttpGet("TotalPorcentajes{idEmpleado}")]
        public int GetTotalPorcentaje(int idEmpleado)
        {
            int Res;
            Res = BeneficiarioStore.TotalPorcentaje(idEmpleado);
            return Res;
        }

        [HttpGet("TotalPorcentajesMenosBeneficiario{idEmpleado}/{idBeneficiario}")]
        public int GetTotalPorcentajeMenosBeneficiario(int idEmpleado, int idBeneficiario)
        {
            int Res;
            Res = BeneficiarioStore.TotalPorcentajeMenosBeneficiario(idEmpleado, idBeneficiario);
            return Res;
        }

        // POST api/<BeneficiarioController>
        [HttpPost("AgregaBeneficiario")]
        public bool AgregaBeneficiario(Beneficiario reg)
        {
            return BeneficiarioStore.BeneficiarioAgregar(reg);
        }

        // POST api/<ClienteController>
        [HttpPost("ActualizaBeneficiario")]
        public bool ActualizaBeneficiario(Beneficiario reg)
        {
            return BeneficiarioStore.BeneficiarioActualiza(reg);
        }

        // DELETE api/<BeneficiarioController>/5
        [HttpDelete("BorraBeneficiario{id}")]
        public bool DeleteBeneficiario(int id)
        {
            return BeneficiarioStore.BeneficiarioBorrar(id);
        }


    }

}
