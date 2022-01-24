using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo_DEINT_PasapalabraEntities
{
    public class clsPregunta
    {
        #region propiedades
        public int Id { get; set; }
        public string Enunciado { get; set; }
        public string Respuesta { get; set; }
        public char Letra { get; set; }
        #endregion
        #region constructores
        public clsPregunta(int id, string enenuciado, string respuesta, char letra)
        {
            Id = id;
            Enunciado = enenuciado;
            Respuesta = respuesta;
            Letra = letra;
        }
        public clsPregunta(string enunciado, string respuesta)
        {
            Enunciado = enunciado;
            Respuesta = respuesta;
        }
        public clsPregunta(int id, string enunciado, string respuesta)
        {
            Id = id;
            Enunciado = enunciado;
            Respuesta = respuesta;
        }

        public clsPregunta()
        {
            Enunciado = "";
            Respuesta = "";
            Letra = ' ';
        }

        #endregion

    }
}
