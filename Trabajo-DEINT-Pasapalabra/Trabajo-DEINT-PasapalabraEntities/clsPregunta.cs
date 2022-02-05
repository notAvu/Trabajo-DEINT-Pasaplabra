using System;
using System.Collections.Generic;
using System.Text;

namespace Trabajo_DEINT_PasapalabraEntities
{
    public class clsPregunta
    {
        #region propiedades
        public int Id { get; }
        public string Enunciado { get; }
        public string Respuesta { get; }
        public char Letra { get; }
        #endregion
        #region constructores
        public clsPregunta(int id, string enunciado, string respuesta, char letra)
        {
            Id = id;
            Enunciado = enunciado;
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
