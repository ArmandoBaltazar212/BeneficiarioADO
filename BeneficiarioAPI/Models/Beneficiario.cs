namespace BeneficiarioAPI.Models
{
    public class Beneficiario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public string Telefono { get; set; }
        public string Nacionalidad { get; set; }
        public decimal Porcentaje { get; set; }
        public int IdEmpleado { get; set; }
    }
}
