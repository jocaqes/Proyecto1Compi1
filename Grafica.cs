using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Irony.Parsing;

namespace Proyecto1OLC1
{
    class Grafica
    {
        private String codigo_salida;
        private String codigo_extra;
        private String class_name;


        public Grafica()
        {
            codigoInicial();
            class_name = "";
        }


        public bool generarGrafica(String comando)
        {
            try
            {

                ProcessStartInfo cmd = new ProcessStartInfo("cmd", "/c " + comando);
                cmd.RedirectStandardOutput = true;
                cmd.UseShellExecute = false;
                cmd.CreateNoWindow = false;
                Process proceso = new Process();
                proceso.StartInfo = cmd;
                proceso.Start();
                return true;
                //String resultado = proceso.StandardOutput.ReadToEnd();
                //Console.WriteLine(resultado);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void codigoInicial()
        {
            codigo_salida = "digraph G{\n";
            codigo_salida += "rankdir = LR;\n";
        }


        #region Generacion del Codigo
        public String crearCodigoClase(ParseTreeNode raiz)
        {
            String nombre_clase = nombreClase(raiz);
            class_name = nombre_clase;//nuevo
            codigo_salida += "n" + nombre_clase + "[label=\"<f0> " + nombre_clase + "|";
            crearCodigoAtributos(raiz);//ingreso atributos
            codigo_salida += "|";//pongo otro separador
            crearCodigoFunciones(raiz);//ingreso funciones
            codigo_salida += "\" shape = \"record\"];\n";
            return codigo_salida;
        }

        private String nombreClase(ParseTreeNode raiz)
        {
            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                String[] objeto = hijo.ToString().Split(' ');
                int longitud = objeto.Length;
                if (longitud > 1)
                {
                    if (objeto[1].Equals("(id)"))
                        return objeto[0];
                }
            }
            return "Error";
        }


        #region Atributos
        public void crearCodigoAtributos(ParseTreeNode raiz)//genera el codigo para los atributos, esta separado para poder poner la division entre los atributos y los metodos
        {
            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                switch (hijo.ToString())
                {
                    case "<Cuerpo_clase>"://simplemente me adentro en el arbol
                        crearCodigoAtributos(hijo);
                        break;
                    case "<Instruccion_clase>"://simplemente me adentro en el arbol
                        crearCodigoAtributos(hijo);
                        break;
                    case "<Posible_instr_clase>"://simplemente me adentro en el arbol
                        crearCodigoAtributos(hijo);
                        break;
                    case "<Atributo>"://Esta es una de las 3 cosas necesarias para la clase: atributos
                        crearCodigoAtributos(hijo);//me adentro en el arbol, pero el siguiente case manejara los ids de esta opcion
                        break;
                    case "<ID>"://aqui tomo los atributos
                        switch (hijo.ChildNodes.Count)
                        {
                            case 2://cuando viene sin visibilidad
                                codigo_salida += "+";//agrego visibilidad public
                                codigo_salida += hijo.ChildNodes[0].ChildNodes[0].ToString().Split(' ')[0] + " ";//tipo de dato
                                //hijo: llamo al nodo; .ChildNodes[0]: llamo al primer hijo que es <TipoDato>; .ChildNodes[0]:llamo al nieto que ya es el tipo de dato;.Split(' ')[0]: retiro el (tipodato) que incluye y tomo solo el 
                                codigo_salida += hijo.ChildNodes[1].ToString().Split(' ')[0] + "\\n";//\\n es para salto de linea en graphiz; // este es el id

                                String[] aux = hijo.ChildNodes[0].ChildNodes[0].ToString().Split(' ');//siempre tiene 2 elementos
                                if (aux[1].Equals("(id)"))//reviso si el tipo de dato es un objeto
                                {
                                    codigo_extra += "n" + class_name + "-> n" + aux[0] + "[arrowhead=ovee,arrowtail=odiamond,dir=both];\n";
                                }
                                break;
                            case 3://cuando viene con visibilidad
                                codigo_salida += retornarVisibilidad(hijo.ChildNodes[0].ChildNodes[0].ToString()); //visibilidad estaria en nieto 0 hijo 0
                                codigo_salida += hijo.ChildNodes[1].ChildNodes[0].ToString().Split(' ')[0] + " ";//ahora el tipo de dato esta en el childnode[1]
                                codigo_salida += hijo.ChildNodes[2].ToString().Split(' ')[0] + "\\n";//este es el id
                                String[] aux2 = hijo.ChildNodes[1].ChildNodes[0].ToString().Split(' ');//siempre tiene 2 elementos
                                if (aux2[1].Equals("(id)"))//reviso si el tipo de dato es un objeto
                                {
                                    codigo_extra += "n" + class_name + "-> n" + aux2[0] + "[arrowhead=ovee,arrowtail=odiamond,dir=both];\n";
                                }
                                break;
                        }
                        break;
                    case "<Extends>"://para poner flechitas, lo hago aqui solo porque si
                        codigo_extra += "n" + class_name + "-> n" + hijo.ChildNodes[1].ToString().Split(' ')[0]+"[arrowhead=onormal];\n";
                        //codigo que agrego para las flechas, en extends: clase analizada apunta a clase que extiende, <Extends> produce extends+id, id esta en childnode[1] y es el primer elemento del split
                        break;
                }
            }
        }


        #endregion

        #region Metodos,Funciones y Constructores
        public void crearCodigoFunciones(ParseTreeNode raiz)
        {
            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                switch (hijo.ToString())
                {
                    case "<Cuerpo_clase>"://simplemente me adentro en el arbol
                        crearCodigoFunciones(hijo);
                        break;
                    case "<Instruccion_clase>"://simplemente me adentro en el arbol
                        crearCodigoFunciones(hijo);
                        break;
                    case "<Posible_instr_clase>"://simplemente me adentro en el arbol
                        crearCodigoFunciones(hijo);
                        break;
                    case "<Constructor>"://el constructor, que normalmente no se deberia incluir en un diagrama de clases pero ni modo
                        switch (hijo.ChildNodes.Count)
                        {
                            case 4://cuando viene visibilidad,id,parametro y el resto
                                codigo_salida += retornarVisibilidad(hijo.ChildNodes[0].ChildNodes[0].ToString());//visibilidad estaria en nieto 0 hijo 0<Visibilidad>
                                codigo_salida += hijo.ChildNodes[1].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                            case 3://cuando no viene visibilidad
                                codigo_salida += "+";//es publico
                                codigo_salida += hijo.ChildNodes[0].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                        }
                        break;
                    case "<Funcion>"://Esta es una de las 3 cosas necesarias para la clase: funciones
                        switch (hijo.ChildNodes.Count)
                        {
                            case 6://cuando viene override,visibilidad,tipodato,id,parametro y el resto
                                codigo_salida += retornarVisibilidad(hijo.ChildNodes[1].ChildNodes[0].ToString());//visibilidad estaria en nieto 0 hijo 1<Visibilidad>
                                codigo_salida += hijo.ChildNodes[2].ChildNodes[0].ToString().Split(' ')[0] + " ";//tipo de dato
                                codigo_salida += hijo.ChildNodes[3].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                            case 5://cuando viene override o visibilidad, pero no ambos
                                if (!retornarVisibilidad(hijo.ChildNodes[0].ChildNodes[0].ToString()).Contains("error"))
                                    codigo_salida += retornarVisibilidad(hijo.ChildNodes[0].ChildNodes[0].ToString());//visibilidad estaria en nieto 0 hijo 0<Visibilidad>
                                else
                                    codigo_salida += "+";//significa que viene override y no visibilidad, asi que es publico
                                codigo_salida += hijo.ChildNodes[1].ChildNodes[0].ToString().Split(' ')[0] + " ";//tipo de dato
                                codigo_salida += hijo.ChildNodes[2].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                            case 4://cuando no viene ni override ni visibilidad
                                codigo_salida += "+";//es publico
                                codigo_salida += hijo.ChildNodes[1].ChildNodes[0].ToString().Split(' ')[0] + " ";//tipo de dato
                                codigo_salida += hijo.ChildNodes[2].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                        }
                        break;
                    case "<Metodo>"://Una de las 3 cosas necesarias para la clase: metodos
                        switch (hijo.ChildNodes.Count)
                        {
                            case 6://cuando viene override,visibilidad,void,id,parametro y el resto
                                codigo_salida += retornarVisibilidad(hijo.ChildNodes[1].ChildNodes[0].ToString());//visibilidad estaria en nieto 0 hijo 1<Visibilidad>
                                codigo_salida += hijo.ChildNodes[3].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                            case 5://cuando viene override o visibilidad, pero no ambos
                                if (!retornarVisibilidad(hijo.ChildNodes[0].ChildNodes[0].ToString()).Contains("error"))
                                    codigo_salida += retornarVisibilidad(hijo.ChildNodes[0].ChildNodes[0].ToString());//visibilidad estaria en nieto 0 hijo 0<Visibilidad>
                                else
                                    codigo_salida += "+";//significa que viene override y no visibilidad, asi que es publico
                                codigo_salida += hijo.ChildNodes[2].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                            case 4://cuando no viene ni override ni visibilidad
                                codigo_salida += "+";//es publico
                                codigo_salida += hijo.ChildNodes[2].ToString().Split(' ')[0];//este es el id
                                codigo_salida += "(";
                                crearCodigoFunciones(hijo);//llamada recursiva para recuperar los parametros
                                codigo_salida += ")\\n";
                                break;
                        }
                        break;
                    case "<ConParametro_SinParametro>"://parte de una funcion o metodo
                        crearCodigoFunciones(hijo);//solo me adentro en el arbol
                        break;
                    case "<Parametro_metodo>"://parte de una funcion o metodo
                        crearCodigoFunciones(hijo);//me meto primero al mas profundo por recursividad
                        switch (hijo.ChildNodes.Count)//luego de llegar al final de los parametros, hay 2 casos
                        {
                            case 4://cuando viene <parametro_metodo> (,) tipoDato id Y otro 
                                codigo_salida += ",";
                                codigo_salida += hijo.ChildNodes[2].ChildNodes[0].ToString().Split(' ')[0] + " ";//tipo de dato
                                codigo_salida += hijo.ChildNodes[3].ToString().Split(' ')[0] + " ";//este es el id
                                break;
                            case 2://cuando es el viene solo tipoDato id
                                codigo_salida += hijo.ChildNodes[0].ChildNodes[0].ToString().Split(' ')[0] + " ";//tipo de dato
                                codigo_salida += hijo.ChildNodes[1].ToString().Split(' ')[0] + " ";//este es el id
                                break;
                        }
                        break;
                    default:
                        break;

                }
            }
        }

        #endregion

        #endregion

        #region Funciones auxiliares

        private String retornarVisibilidad(String visibilidad)
        {
            String entrada = visibilidad.Split(' ')[0];//aun si visibilidad no tiene dos partes, no hay problema en hacer este codigo
            switch (entrada)
            {
                case "public":
                    return "+";
                case "private":
                    return "-";
                case "protected":
                    return "#";
                default:
                    return "error";

            }
        }


        public string Codigo_extra//este codigo extra es las flechitas, se agrega hasta que todas las clases fueron analizadas porque hay referencia de una a otra clase
        {
            get
            {
                return codigo_extra;
            }

            set
            {
                codigo_extra = value;
            }
        }


        #endregion

    }
}
