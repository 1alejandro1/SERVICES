namespace Sarc.WebApp.Models.Reportes.Analista
{
    public class EspecialidadViewModel
    {
        public EspecialidadViewModel(string nombre)
        {
            Nombre = nombre;
            ATM = 0;
            Certificaciones = 0;
            OtrasSol = 0;
            Complejo = 0;
            Reclamos = 0;
        }

        public string Nombre { get; set; }
        public int ATM { get; set; }
        public int Certificaciones { get; set; }
        public int OtrasSol { get; set; }
        public int Complejo { get; set; }
        public int Reclamos { get; set; }
    }
}
