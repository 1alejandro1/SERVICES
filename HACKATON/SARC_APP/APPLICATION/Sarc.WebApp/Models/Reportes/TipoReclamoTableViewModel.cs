using System.ComponentModel.DataAnnotations;

namespace Sarc.WebApp.Models.Reportes
{
    public class TipoReclamoTableViewModel
    {
        public TipoReclamoTableViewModel(string tipoCaso, int pendientes, int pendientesVencidos, int solucionadosTiempo, int solucionadosVencidos, int total)
        {
            TipoCaso = tipoCaso;
            Pendientes = pendientes;
            PendientesVencidos = pendientesVencidos;
            SolucionadosTiempo = solucionadosTiempo;
            SolucionadosVencidos = solucionadosVencidos;
            Total = total;
        }
        public string TipoCaso { get; set; }
        public int Pendientes { get; set; }
        public int PendientesVencidos { get; set; }
        public int SolucionadosTiempo { get; set; }
        public int SolucionadosVencidos { get; set; }
        public int Total { get; set; }

    }
}
