using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Prueba_irony
{
    class Gramatica: Grammar
    {
        public Gramatica()
            : base(false)
        {


            // Expresiones regulares

            /*
            RegexBasedTerminal Id = new RegexBasedTerminal("Id", "[a-zA-Z]([0-9a-zA-Z])*");
            RegexBasedTerminal num_real = new RegexBasedTerminal("num_real", "[0-9]+(([.][0-9]+)|)");
            RegexBasedTerminal signo = new RegexBasedTerminal("signo", "[+]|[-]");*/

            RegexBasedTerminal id = new RegexBasedTerminal("id", "[a-zA-Z]([0-9a-zA-Z])*");
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+(([.][0-9]+)|)");
            RegexBasedTerminal visibilidad = new RegexBasedTerminal("visibilidad", "public|private|protected");
            RegexBasedTerminal tipo_dato = new RegexBasedTerminal("tipo_dato", "int|String|boolean|double|char");
            RegexBasedTerminal signo = new RegexBasedTerminal("signo", "[+]|[-]");
            StringLiteral cadena = new StringLiteral("cadena", "\"");
            RegexBasedTerminal caracter = new RegexBasedTerminal("char", "'[a-zA-Z]'");
            RegexBasedTerminal booleano = new RegexBasedTerminal("booleano", "true|false");
            CommentTerminal comentario_linea = new CommentTerminal("comentario_linea", "//", "\n", "\r\n");
            CommentTerminal comentario_bloque = new CommentTerminal("comentario_bloque", "/*", "*/");
            RegexBasedTerminal logico = new RegexBasedTerminal("logico", "&&|[|][|]");
            RegexBasedTerminal relacional = new RegexBasedTerminal("relacional", "<|>|>=|<=|==|!=");
            KeyTerm Lparen = new KeyTerm("(", "parentesis_abre");
            KeyTerm Rparen = new KeyTerm(")", "parentesis_cierra");


            NonGrammarTerminals.Add(comentario_linea);
            NonGrammarTerminals.Add(comentario_bloque);
            MarkPunctuation(Lparen, Rparen);



            //Declaracion de no terminales

            /*
            NonTerminal
                S = new NonTerminal("S"),
                T = new NonTerminal("T"),
                Sp = new NonTerminal("Sp"),
                R = new NonTerminal("R"),
                Tp = new NonTerminal("Tp");*/
            NonTerminal
                Atributo = new NonTerminal("<Atributo>"),
                Visibilidad = new NonTerminal("<Visibilidad>"),
                ID = new NonTerminal("<ID>"),
                Asignacion = new NonTerminal("<Asignacion>"),
                TipoDato = new NonTerminal("<TipoDato>"),
                Parametro = new NonTerminal("<Parametro>"),
                Expresion = new NonTerminal("<Expresion>"),
                T = new NonTerminal("<T>"),
                R = new NonTerminal("<R>"),
                S = new NonTerminal("<S>"),
                ID_acceso = new NonTerminal("<ID_acceso>"),
                Acceso = new NonTerminal("<Acceso>"),
                Llamada = new NonTerminal("<Llamada>"),
                If = new NonTerminal("<If>"),
                Condicion = new NonTerminal("<Condicion>"),
                ElseIf = new NonTerminal("<ElseIf>"),
                Else = new NonTerminal("<Else>"),
                Not = new NonTerminal("<Not>"),
                Comparacion = new NonTerminal("<Comparacion>"),
                Switch = new NonTerminal("<Switch>"),
                Pre_case = new NonTerminal("<Pre_case>"),
                Case = new NonTerminal("<Case>"),
                Default = new NonTerminal("<Default>"),
                Valor = new NonTerminal("<Valor>");//cuidado con valor






            //S.Rule para escribir el cuerpo de un no terminal con todas sus producciones.

            //Ejemplo
            /*
            S.Rule = T + Sp;
            Sp.Rule = signo + T + Sp | Empty;
            T.Rule = R + Tp;
            Tp.Rule = "*" + R + Tp | Empty;
            R.Rule = "(" + S + ")" | Id | num_real;*/

            //Atributo
            /*
            Atributo.Rule = ID + Asignacion + ";" | Empty;
            Visibilidad.Rule = visibilidad | Empty;
            ID.Rule = ID + "," + id | Visibilidad + TipoDato + id;
            TipoDato.Rule = tipo_dato | id;
            Asignacion.Rule = "=" + Expresion | Empty;*/

            //Expresion
                        Expresion.Rule = Expresion + signo + T | T;
            T.Rule = T + "*" + R | R;
            R.Rule = R + "/" + S | S;
            S.Rule = "(" + Expresion + ")" | numero | cadena | ID_acceso | booleano | ToTerm("new") + id + "(" + ")" | ToTerm("new") + id + "(" + Parametro + ")" | ToTerm("null");
            Parametro.Rule = Parametro + "," + Expresion | Expresion;
            ID_acceso.Rule = Acceso + Llamada;
            Acceso.Rule = Acceso + "." + id  | id  ;
            Llamada.Rule = "(" + Parametro + ")" | "(" + ")" |Empty;

            //if
            /*If.Rule = ToTerm("if") + "(" + Condicion + ")" + "{" + "}" + ElseIf + Else;
            ElseIf.Rule = ElseIf + ToTerm("else if") + "(" + Condicion + ")" + "{" + "}" | Empty;
            Else.Rule = ToTerm("else") + "{" + "}" | Empty;*/

            //Switch
            Switch.Rule = ToTerm("Switch") + Lparen + Acceso + Rparen + "{" + Pre_case + Default + "}";
            Pre_case.Rule = Pre_case + Case + ";" | Case + ";";
            Case.Rule = ToTerm("case") + Valor + ":" + ToTerm("break");//cuidado con empty
            Default.Rule = ToTerm("default") + ":" + ToTerm("break") + ";";
            Valor.Rule = numero | cadena | caracter;

            Condicion.Rule = Condicion + logico + Not + Comparacion | Not + Comparacion;
            Comparacion.Rule = Not + Expresion + relacional + Not + Expresion | Not + Expresion;
            Not.Rule = ToTerm("!") | Empty;


            //IDcont.Rule = id;




            //indicamos la produccion inicial con la siguiente linea

            //this.Root = S;
            //this.Root = Atributo;
            this.Root =Switch;

            //MarkPunctuation("-");
        }
    }
}
