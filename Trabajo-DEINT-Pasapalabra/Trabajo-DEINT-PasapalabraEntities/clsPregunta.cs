using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo_DEINT_PasapalabraEntities
{
    public class clsPregunta
    {
        #region propiedades
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public char Letra { get; set; }
        #endregion
        #region constructores
        public clsPregunta(int id, string pregunta, string respuesta, char letra)
        {
            Id = id;
            Pregunta = pregunta;
            Respuesta = respuesta;
            Letra = letra;
        }
        #endregion

    }
}
