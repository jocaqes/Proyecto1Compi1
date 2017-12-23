using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1OLC1
{
    class Error
    {
        private String mensaje;
        private int fila, columna;

        public Error() { }

        public Error(String mensaje, int fila, int columna)
        {
            this.mensaje = mensaje;
            this.fila = fila;
            this.columna = columna;
        }
    }
}
