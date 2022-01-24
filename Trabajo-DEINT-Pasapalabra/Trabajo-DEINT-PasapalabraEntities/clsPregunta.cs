using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo_DEINT_PasapalabraEntities
{
    public class clsPregunta
    {
        #region propiedades
        public int Id { get; set; }
        public string Enuciado { get; set; }
        public string Respuesta { get; set; }
        public char Letra { get; set; }
        #endregion
        #region constructores
        public clsPregunta(int id, string enenuciado, string respuesta, char letra)
        {
            Id = id;
            Enuciado = enenuciado;
            Respuesta = respuesta;
            Letra = letra;
        }
        public clsPregunta(string enunciado, string respuesta)
        {
            Enuciado = enunciado;
            Respuesta = respuesta;
        }

        public clsPregunta()
        {
            Enuciado = "";
            Respuesta = "";
            Letra = ' ';
        }

        #endregion

    }
}
