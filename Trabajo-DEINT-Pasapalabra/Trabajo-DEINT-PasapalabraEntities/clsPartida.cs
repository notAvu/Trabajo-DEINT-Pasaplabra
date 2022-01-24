using System;

namespace Trabajo_DEINT_PasapalabraEntities
{
    public class clsPartida
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public int TotalAcertadas { get; set; }
        public int TotalFalladas { get; set; }
        public DateTime tiempo { get; set; }
        public int PuntuacionTotal() {
            //TODO metodo para calcular puntuacionTotal
            int puntuacionTotal=0;
            return puntuacionTotal;
        }
          
    }
    
}
