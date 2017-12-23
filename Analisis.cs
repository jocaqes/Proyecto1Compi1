using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Irony.Ast;

namespace Proyecto1OLC1
{
    class Analisis
    {
        private ParseTree arbol;
        private bool sin_error;
        private List<Error> lista_error;

        public Analisis()
        {
            sin_error = true;
        }

        public bool validar(string cadenaEntrada, Grammar gramatica)
        {
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser p = new Parser(lenguaje);
            arbol = p.Parse(cadenaEntrada);
            if (arbol.Root == null)
                sin_error = false;
            if (anyadirError(arbol))
                sin_error = false;

            return arbol.Root != null;
        }

        public String errMsj()//debug
        {
            Irony.LogMessageList a = arbol.ParserMessages;
            String salida = "";
            foreach (Irony.LogMessage item in a)
            {
                salida += item.Message + "  en:" + item.Location;
            }
            return salida;
        }


        private bool anyadirError(ParseTree arbol)
        {
            int limite = arbol.ParserMessages.Count;
            int i;
            for (i=0; i < limite; i++)
            {
                String mensaje = arbol.ParserMessages.ElementAt(i).Message;
                int fila = arbol.ParserMessages.ElementAt(i).Location.Line;
                int columna = arbol.ParserMessages.ElementAt(i).Location.Column;
                lista_error.Add(new Error(mensaje, fila, columna));
            }
            return limite > 0;
        }


        public ParseTree Arbol
        {
            get
            {
                return arbol;
            }

            set
            {
                arbol = value;
            }
        }

        public bool Sin_error
        {
            get
            {
                return sin_error;
            }

            set
            {
                sin_error = value;
            }
        }
    }
}
