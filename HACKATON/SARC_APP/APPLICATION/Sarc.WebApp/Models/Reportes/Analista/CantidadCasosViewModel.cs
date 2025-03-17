namespace Sarc.WebApp.Models.Reportes.Analista
{
    public class CantidadCasosViewModel
    {
        public CantidadCasosViewModel(string nombre, int pendientes,int pendientesvencidos, int solucionados, int total)
        {
            Nombre = nombre;
            Pendientes = pendientes;
            Solucionados = solucionados;
            Total = total;
        }

        public string Nombre { get; set; }
        public int Pendientes { get; set; }
        public int PendientesVencidos { get; set; }
        public int Solucionados { get; set; }
        public int Total { get; set; }
    }
}
