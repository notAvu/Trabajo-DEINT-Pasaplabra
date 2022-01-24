using System;

namespace Trabajo_DEINT_PasapalabraEntities
{
    public class clsPartida
    {
        #region propiedades autoimplementadas
        public int Id { get; set; }
        public string Nick { get; set; }
        public int TotalAcertadas { get; set; }
        public int TotalFalladas { get; set; }
        public DateTime Tiempo { get; set; }
        #endregion
        #region constructores
        public clsPartida(int id, string nick, int totalAcertadas, int totalFalladas, DateTime tiempo)
        {
            Id = id;
            Nick = nick;
            TotalAcertadas = totalAcertadas;
            TotalFalladas = totalFalladas;
            Tiempo = tiempo;
        }

        #endregion          
    }

}
