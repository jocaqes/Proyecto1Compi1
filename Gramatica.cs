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
            : base(true)
        {


            // Expresiones regulares

            /*
            RegexBasedTerminal Id = new RegexBasedTerminal("Id", "[a-zA-Z]([0-9a-zA-Z])*");
            RegexBasedTerminal num_real = new RegexBasedTerminal("num_real", "[0-9]+(([.][0-9]+)|)");
            RegexBasedTerminal signo = new RegexBasedTerminal("signo", "[+]|[-]");*/

            RegexBasedTerminal id = new RegexBasedTerminal("id", "[a-zA-Z]([0-9a-zA-Z]|[_])*");
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+(([.][0-9]+)|)");
            //RegexBasedTerminal visibilidad = new RegexBasedTerminal("visibilidad", "public|private|protected");
            //RegexBasedTerminal tipo_dato = new RegexBasedTerminal("tipo_dato", "int|String|boolean|double|char");
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
            KeyTerm Lkey = new KeyTerm("{", "llave_abre");
            KeyTerm Rkey = new KeyTerm("}", "llave_cierra");
            RegexBasedTerminal break_continue = new RegexBasedTerminal("break_continue", "break|continue");
            RegexBasedTerminal mas_menos = new RegexBasedTerminal("mas_menos", "[+][+]|[-][-]");
            RegexBasedTerminal reservada_this = new RegexBasedTerminal("this", "this");
            RegexBasedTerminal reservada_override = new RegexBasedTerminal("override", "@override");
            //RegexBasedTerminal reservada_void = new RegexBasedTerminal("void", "void");
            RegexBasedTerminal reservada_new = new RegexBasedTerminal("new", "new");
            RegexBasedTerminal reservada_return = new RegexBasedTerminal("return", "return");


            #region Terminales
            var reservada_public = ToTerm("public");
            var reservada_private = ToTerm("private");
            var reservada_protected = ToTerm("protected");
            var reservada_void = ToTerm("void");
            var TD_int = ToTerm("int");
            var TD_String = ToTerm("String");
            var TD_boolean = ToTerm("boolean");
            var TD_double = ToTerm("double");
            var TD_char = ToTerm("char");
            
            #endregion







            MarkReservedWords("int", "String", "boolean", "double", "char", "public",
                "private", "protected", "break", "continue", "this", "true", "false",
                "if", "while", "do", "for", "Switch", "case", "@override", "void", "new", "return");


            NonGrammarTerminals.Add(comentario_linea);
            NonGrammarTerminals.Add(comentario_bloque);
            MarkPunctuation(Lparen, Rparen, Lkey, Rkey);



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
                ID = new NonTerminal("<ID>"),
                Asignacion = new NonTerminal("<Asignacion>"),
                TipoDato = new NonTerminal("<TipoDato>"),
                Parametro = new NonTerminal("<Parametro>"),
                Visibilidad = new NonTerminal("<Visibilidad>"),
                Parametro_metodo = new NonTerminal("<Parametro_metodo"),
                Expresion = new NonTerminal("<Expresion>"),
                T = new NonTerminal("<T>"),
                R = new NonTerminal("<R>"),
                S = new NonTerminal("<S>"),
                ID_acceso = new NonTerminal("<ID_acceso>"),
                Acceso = new NonTerminal("<Acceso>"),
                Llamada = new NonTerminal("<Llamada>"),
                If = new NonTerminal("<If>"),
                Condicion = new NonTerminal("<Condicion>"),
                Else_if = new NonTerminal("<Else_if>"),
                ElseIf = new NonTerminal("<ElseIf>"),
                Else = new NonTerminal("<Else>"),
                Not = new NonTerminal("<Not>"),
                Comparacion = new NonTerminal("<Comparacion>"),
                Switch = new NonTerminal("<Switch>"),
                Pre_case = new NonTerminal("<Pre_case>"),
                Case = new NonTerminal("<Case>"),
                Default = new NonTerminal("<Default>"),
                Valor = new NonTerminal("<Valor>"),
                While = new NonTerminal("<While>"),
                Instruccion_while = new NonTerminal("<Instruccion_while>"),
                Break_continue = new NonTerminal("<Break_continue>"),
                DoWhile = new NonTerminal("<DoWhile>"),
                For = new NonTerminal("<For>"),
                Declaracion_for = new NonTerminal("<Declaracion_for>"),
                Paso = new NonTerminal("<Paso>"),
                Aumentar_disminuir = new NonTerminal("<Aumentar_disminuir>"),
                Llamada_metodo_asignacion = new NonTerminal("<Llamada_metodo>"),
                Instruccion = new NonTerminal("<Instruccion>"),
                Instruccion_but_while = new NonTerminal("<Instruccion_but_while>"),
                Metodo = new NonTerminal("<Metodo>"),
                Funcion = new NonTerminal("<Funcion>"),
                Instr_fun = new NonTerminal("<Instr_fun>"),
                Return = new NonTerminal("<Return>"),
                Cuerpo_funcion = new NonTerminal("<Cuerpo_funcion"),
                Cp_Sp = new NonTerminal("<ConParametro_SinParametro>"),
                Cuerpo_instruccion = new NonTerminal("<Cuerpo_instruccion>"),
                Cuerpo_while = new NonTerminal("<Cuerpo_while>"),
                Variable_local = new NonTerminal("<Variable_local>"),
                ID_local = new NonTerminal("<ID_local>"),
                Constructor = new NonTerminal("<Constructor>"),
                Debug = new NonTerminal("<Debug>");






            //S.Rule para escribir el cuerpo de un no terminal con todas sus producciones.

            //Ejemplo
            /*
            S.Rule = T + Sp;
            Sp.Rule = signo + T + Sp | Empty;
            T.Rule = R + Tp;
            Tp.Rule = "*" + R + Tp | Empty;
            R.Rule = "(" + S + ")" | Id | num_real;*/

            //Atributo
            Atributo.Rule = ID + Asignacion | ID + ";";//Atributo.Rule = ID + Asignacion + ";";  este es el original
            ID.Rule = ID + "," + id | Visibilidad + TipoDato + id|TipoDato+id;
            TipoDato.Rule = TD_int|TD_String|TD_double|TD_boolean|TD_char | id;
            Asignacion.Rule = "=" + Expresion + ";";//el original es sin ; y con empty
            Visibilidad.Rule = reservada_public | reservada_protected | reservada_private;

            //Expresion
            Expresion.Rule = Expresion + signo + T | T;
            T.Rule = T + "*" + R | R;
            R.Rule = R + "/" + S | S;
            S.Rule = Lparen + Expresion + Rparen | numero | cadena | ID_acceso | booleano | "new" + id + Lparen + Rparen | "new" + id + Lparen + Parametro + Rparen | "null";//cambio de parentesis literal a keyword

            //Parametro
            Parametro.Rule = Parametro + "," + Expresion | Expresion;
            ID_acceso.Rule = Acceso + Llamada | Acceso;
            //ID_acceso.Rule = Acceso +Lparen+ Llamada+Rparen | Acceso; //este es el original
            Acceso.Rule = Acceso + "." + id | id;
            Llamada.Rule = Lparen + Parametro + Rparen | Lparen + Rparen;
            //Llamada.Rule = Parametro |Empty; //este es el original

            //if
            /*
            If.Rule = ToTerm("if") + Lparen + Condicion + Rparen + "{" + Instruccion_but_while + "}" + ElseIf + Else;
            ElseIf.Rule = ElseIf + ToTerm("else if") + Lparen + Condicion +Rparen + "{" + Instruccion_but_while + "}" | Empty;
            Else.Rule = ToTerm("else") + "{" + Instruccion_but_while + "}" | Empty;
            Instruccion_but_while.Rule = Instruccion_but_while + Instruccion | Empty;*///original
            If.Rule = ToTerm("if") + Lparen + Condicion + Rparen + Cuerpo_instruccion + Else_if | ToTerm("if") + Lparen + Condicion + Rparen + Cuerpo_instruccion;
            Else_if.Rule = Else_if + Else | Else;
            ElseIf.Rule = ElseIf + ToTerm("else if") + Lparen + Condicion + Rparen + Cuerpo_instruccion | ToTerm("else if") + Lparen + Condicion + Rparen + Cuerpo_instruccion;
            Else.Rule = ToTerm("else") + Lkey + Instruccion_but_while;
            Instruccion_but_while.Rule = Instruccion_but_while + Instruccion | Instruccion;

            //Switch

            Switch.Rule = ToTerm("Switch") + Lparen + Acceso + Rparen + Lkey + Pre_case + Default + Rkey;
            Pre_case.Rule = Pre_case + Case + ";" | Case + ";";
            Case.Rule = ToTerm("case") + Valor + ":" + Instruccion_but_while + ToTerm("break");
            Default.Rule = ToTerm("default") + ":" + Instruccion_but_while + ToTerm("break") + ";";
            Valor.Rule = numero | cadena | caracter;

            //While


            While.Rule = ToTerm("while") + Lparen + Condicion + Rparen + Cuerpo_while;
            Cuerpo_while.Rule = Lkey + Instruccion_while + Rkey | Lkey + Rkey;
            Instruccion_while.Rule = Instruccion_while + Instruccion + break_continue + ";" | Instruccion_while + Instruccion | Instruccion + break_continue + ";" | Instruccion;

            //Do while

            DoWhile.Rule = ToTerm("do") + Cuerpo_instruccion + ToTerm("while") + Lparen + Condicion + Rparen + ";";

            //For
            For.Rule = ToTerm("for") + Lparen + Declaracion_for + Acceso + relacional + Expresion + ";" + Acceso + Paso + Rparen + Cuerpo_instruccion;//asignacion ya trae ;
            Declaracion_for.Rule = ToTerm("int") + Acceso + Asignacion | Acceso + Asignacion;
            Paso.Rule = mas_menos | "+=" + Expresion | "-=" + Expresion | Asignacion;

            //Aumento o disminucion
            Aumentar_disminuir.Rule = Acceso + mas_menos + ";";

            //Llamada a un metodo
            Llamada_metodo_asignacion.Rule = reservada_this + "." + Acceso + Llamada + ";" | Acceso + Llamada + ";" | Acceso + Asignacion;//asignacion ya incluye el ;

            //Condicion
            Condicion.Rule = Condicion + logico + Not + Comparacion | Not + Comparacion;
            Comparacion.Rule = Not + Expresion + relacional + Not + Expresion | Not + Expresion;
            Not.Rule = ToTerm("!") | Empty;

            //Instruccion
            Instruccion.Rule = Llamada_metodo_asignacion | Aumentar_disminuir | If | For | While | Case | DoWhile | Variable_local;
            Variable_local.Rule = ID_local + Asignacion | ID_local + ";";
            ID_local.Rule = ID_local + "," + id | TipoDato + id;



            //Metodo
            Metodo.Rule = reservada_override + Visibilidad + reservada_void + id + Cp_Sp + Cuerpo_instruccion | Visibilidad + reservada_void + id + Cp_Sp + Cuerpo_instruccion|
                reservada_override + reservada_void + id + Cp_Sp + Cuerpo_instruccion | reservada_void + id + Cp_Sp + Cuerpo_instruccion;
            Cp_Sp.Rule = Lparen + Rparen | Lparen + Parametro_metodo + Rparen;
            Cuerpo_instruccion.Rule = Lkey + Instruccion_but_while + Rkey | Lkey + Rkey;//agregue el or

            //Parametro_metodo
            Parametro_metodo.Rule = Parametro_metodo + "," + TipoDato + id | TipoDato + id;

            //Funcion
            Funcion.Rule = reservada_override + Visibilidad + TipoDato + id + Cp_Sp + Cuerpo_funcion |Visibilidad + TipoDato + id + Cp_Sp + Cuerpo_funcion|
                reservada_override + TipoDato + id + Cp_Sp + Cuerpo_funcion | TipoDato + id + Cp_Sp + Cuerpo_funcion;
            Cuerpo_funcion.Rule = Lkey + Instr_fun + Rkey | Lkey + Rkey;
            Instr_fun.Rule = Instr_fun + Instruccion + Return | Instr_fun + Instruccion | Instruccion + Return | Instruccion;
            Return.Rule = reservada_return + Expresion + ";" |reservada_return + Condicion + ";";

            //Constructor
            Constructor.Rule = Visibilidad + id + Cp_Sp + Cuerpo_instruccion | id + Cp_Sp + Cuerpo_instruccion;


            //Debug
            Debug.Rule = id + "=" +id;

            //IDcont.Rule = id;
            //indicamos la produccion inicial con la siguiente linea
            //this.Root = S;
            //this.Root = Atributo;
            this.Root =Debug;

            //MarkPunctuation("-");
        }
    }
}
