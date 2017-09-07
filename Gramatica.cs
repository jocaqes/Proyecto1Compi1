using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Proyecto1OLC1
{
    class Gramatica : Grammar
    {
        public Gramatica()
            : base(caseSensitive:true)
        {
            #region Expresiones_Regulares
            IdentifierTerminal id = new IdentifierTerminal("id", "_", "");
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+(([.][0-9]+)|)");
            RegexBasedTerminal signo = new RegexBasedTerminal("signo", "[+]|[-]");
            StringLiteral cadena = new StringLiteral("cadena", "\"");
            RegexBasedTerminal caracter = new RegexBasedTerminal("char", "'([a-zA-Z0-9]|[ ]|)'");
            CommentTerminal comentario_linea = new CommentTerminal("comentario_linea", "//", "\n", "\r\n");
            CommentTerminal comentario_bloque = new CommentTerminal("comentario_bloque", "/*", "*/");
            KeyTerm Lparen = new KeyTerm("(", "parentesis_abre");
            KeyTerm Rparen = new KeyTerm(")", "parentesis_cierra");
            KeyTerm Lkey = new KeyTerm("{", "llave_abre");
            KeyTerm Rkey = new KeyTerm("}", "llave_cierra");
            #endregion

            #region Simbolos_Terminales
            //Reservada
            var reservada_public = ToTerm("public");
            var reservada_private = ToTerm("private");
            var reservada_protected = ToTerm("protected");
            var reservada_class = ToTerm("class");
            var reservada_extends = ToTerm("extends");
            var reservada_if = ToTerm("if");
            var reservada_else = ToTerm("else");
            var reservada_override = ToTerm("@Override");
            var reservada_return = ToTerm("return");
            var reservada_null = ToTerm("null");
            var reservada_this = ToTerm("this");
            var reservada_void = ToTerm("void");
            var reservada_true = ToTerm("true");
            var reservada_false = ToTerm("false");
            var reservada_break = ToTerm("break");
            var reservada_continue = ToTerm("continue");
            var reservada_new = ToTerm("new");
            //Tipo de Dato
            var TD_int = ToTerm("int");
            var TD_String = ToTerm("String");
            var TD_boolean = ToTerm("boolean");
            var TD_double = ToTerm("double");
            var TD_char = ToTerm("char");
            //Relacional
            var relacional_menor = ToTerm("<");
            var relacional_mayor = ToTerm(">");
            var relacional_menor_igual = ToTerm("<=");
            var relacional_mayor_igual = ToTerm(">=");
            var relacional_igual = ToTerm("==");
            var relacional_distinto = ToTerm("!=");
            //Logico
            var logico_AND = ToTerm("&&");
            var logico_OR = ToTerm("||");
            //Aumentar Disminuir
            var mas_mas = ToTerm("++");
            var menos_menos = ToTerm("--");
            #endregion


            #region Simbolos_No_Terminales
            NonTerminal
                Atributo =                      new NonTerminal("<Atributo>"),
                ID =                            new NonTerminal("<ID>"),
                Asignacion =                    new NonTerminal("<Asignacion>"),
                TipoDato =                      new NonTerminal("<TipoDato>"),
                Parametro =                     new NonTerminal("<Parametro>"),
                Visibilidad =                   new NonTerminal("<Visibilidad>"),
                Parametro_metodo =              new NonTerminal("<Parametro_metodo>"),
                Expresion =                     new NonTerminal("<Expresion>"),
                Booleano =                      new NonTerminal("<Booleano>"),//nuevo
                T =                             new NonTerminal("<T>"),
                R =                             new NonTerminal("<R>"),
                S =                             new NonTerminal("<S>"),
                ID_acceso =                     new NonTerminal("<ID_acceso>"),
                Acceso =                        new NonTerminal("<Acceso>"),
                Llamada =                       new NonTerminal("<Llamada>"),
                If =                            new NonTerminal("<If>"),
                If_funcion =                    new NonTerminal("<If_funcion>"),
                Condicion =                     new NonTerminal("<Condicion>"),
                Relacional =                    new NonTerminal("<Relacional>"),//nuevo
                Else_if =                       new NonTerminal("<Else_if>"),
                Else_if_funcion =               new NonTerminal("<Else_if_funcion>"),
                ElseIf =                        new NonTerminal("<ElseIf>"),
                ElseIf_funcion =                new NonTerminal("<Elseif_funcion>"),
                Else =                          new NonTerminal("<Else>"),
                Else_funcion =                  new NonTerminal("<Else_funcion>"),
                Not =                           new NonTerminal("<Not>"),
                Comparacion =                   new NonTerminal("<Comparacion>"),
                Logico =                        new NonTerminal("<Logico>"),//nuevo
                Switch =                        new NonTerminal("<Switch>"),
                Switch_funcion =                new NonTerminal("<Switch_funcion>"),
                Pre_case =                      new NonTerminal("<Pre_case>"),
                Pre_case_funcion =              new NonTerminal("<Pre_case_funcion>"),
                Case =                          new NonTerminal("<Case>"),
                Case_funcion =                  new NonTerminal("<Case_funcion>"),
                Default =                       new NonTerminal("<Default>"),
                Default_funcion =               new NonTerminal("<Default_funcion>"),
                Valor =                         new NonTerminal("<Valor>"),
                While =                         new NonTerminal("<While>"),
                While_funcion =                 new NonTerminal("<While_funcion>"),
                Instruccion_while =             new NonTerminal("<Instruccion_while>"),
                Instruccion_while_funcion =     new NonTerminal("<Instruccion_while_funcion>"),
                Break_continue =                new NonTerminal("<Break_continue>"),
                DoWhile =                       new NonTerminal("<DoWhile>"),
                DoWhile_funcion =               new NonTerminal("<DoWhile_funcion>"),
                For =                           new NonTerminal("<For>"),
                For_funcion =                   new NonTerminal("<For_funcion>"),
                Declaracion_for =               new NonTerminal("<Declaracion_for>"),
                Paso =                          new NonTerminal("<Paso>"),
                Aumentar_disminuir =            new NonTerminal("<Aumentar_disminuir>"),
                Mas_menos =                     new NonTerminal("<Mas_menos>"),
                Llamada_metodo_asignacion =     new NonTerminal("<Llamada_metodo>"),
                Instruccion =                   new NonTerminal("<Instruccion>"),
                Instruccion_but_while =         new NonTerminal("<Instruccion_but_while>"),
                Instruccion_but_while_funcion = new NonTerminal("Instruccion_but_while_funcion"),
                Instruccion_funcion =           new NonTerminal("<Instruccion_funcion>"),
                Metodo =                        new NonTerminal("<Metodo>"),
                Funcion =                       new NonTerminal("<Funcion>"),
                Instr_fun =                     new NonTerminal("<Instr_fun>"),
                Return =                        new NonTerminal("<Return>"),
                Cuerpo_funcion =                new NonTerminal("<Cuerpo_funcion"),
                Cp_Sp =                         new NonTerminal("<ConParametro_SinParametro>"),
                Cuerpo_instruccion =            new NonTerminal("<Cuerpo_instruccion>"),
                Cuerpo_instruccion_funcion =    new NonTerminal("<Cuerpo_instruccion_funcion>"),
                Cuerpo_while =                  new NonTerminal("<Cuerpo_while>"),
                Cuerpo_while_funcion =          new NonTerminal("<Cuerpo_while_funcion>"),
                Variable_local =                new NonTerminal("<Variable_local>"),
                ID_local =                      new NonTerminal("<ID_local>"),
                Constructor =                   new NonTerminal("<Constructor>"),
                Clase =                         new NonTerminal("<Clase>"),
                Cuerpo_clase =                  new NonTerminal("<Cuerpo_clase>"),
                Extends =                       new NonTerminal("<Extends>"),
                Posible_instr_clase =           new NonTerminal("<Posible_instr_clase>"),
                Instruccion_clase =             new NonTerminal("<Instruccion_clase>"),
                Debug =                         new NonTerminal("<Debug>");
            #endregion


            #region Producciones
            //Atributo
            Atributo.Rule = ID + Asignacion | ID + ";";//Atributo.Rule = ID + Asignacion + ";";  este es el original
            ID.Rule = ID + "," + id | Visibilidad + TipoDato + id | TipoDato + id;
            TipoDato.Rule = TD_int | TD_String | TD_double | TD_boolean | TD_char | id;
            Asignacion.Rule = "=" + Expresion + ";";//el original es sin ; y con empty
            Visibilidad.Rule = reservada_public | reservada_protected | reservada_private;








            //Expresion
            Expresion.Rule = Expresion + signo + T | T;
            T.Rule = T + "*" + R | R;
            R.Rule = R + "/" + S | S;
            S.Rule = Lparen + Expresion + Rparen | numero | cadena | ID_acceso | reservada_this + "." + ID_acceso 
                | Llamada_metodo_asignacion | Booleano |reservada_new + id + Lparen + Rparen
                | reservada_new + id + Lparen + Parametro + Rparen|reservada_new+id+Lparen+Rparen+"."+ID_acceso 
                | reservada_new + id + Lparen + Parametro + Rparen + "."+ID_acceso 
                | reservada_null | caracter;//cambio de parentesis literal a keyword
            Booleano.Rule = reservada_true | reservada_false;










            //Parametro
            Parametro.Rule = Parametro + "," + Expresion | Expresion;
            ID_acceso.Rule = Acceso + Llamada | Acceso;
            Acceso.Rule = Acceso + "." + id | id;
            Llamada.Rule = Lparen + Parametro + Rparen | Lparen + Rparen;


            //if
            If.Rule = reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion + Else_if 
                | reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion;
            Else_if.Rule = ElseIf + Else | Else | ElseIf;
            ElseIf.Rule = ElseIf + reservada_else + reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion 
                | reservada_else + reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion;
            Else.Rule = reservada_else + Cuerpo_instruccion;
            Instruccion_but_while.Rule = Instruccion_but_while + Instruccion | Instruccion;
            //if para funcion
            If_funcion.Rule = reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion_funcion + Else_if_funcion 
                | reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion_funcion;
            Else_if_funcion.Rule = ElseIf_funcion + Else_funcion 
                | ElseIf_funcion 
                | Else_funcion;
            ElseIf_funcion.Rule = ElseIf_funcion + reservada_else + reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion_funcion 
                | reservada_else + reservada_if + Lparen + Condicion + Rparen + Cuerpo_instruccion_funcion;
            Else_funcion.Rule = reservada_else + Cuerpo_instruccion_funcion;
            Instruccion_but_while_funcion.Rule = Instruccion_but_while_funcion + Instruccion_funcion | Instruccion_funcion;

            //Switch
            Switch.Rule = ToTerm("Switch") + Lparen + Acceso + Rparen + Lkey + Pre_case + Default + Rkey;
            Pre_case.Rule = Pre_case + Case + ";" | Case + ";";
            Case.Rule = ToTerm("case") + Valor + ":" + Instruccion_but_while + ToTerm("break");
            Default.Rule = ToTerm("default") + ":" + Instruccion_but_while + ToTerm("break") + ";";
            Valor.Rule = numero | cadena | caracter;
            //Switch para funcion
            Switch_funcion.Rule = ToTerm("Switch") + Lparen + Acceso + Rparen + Lkey + Pre_case_funcion + Default_funcion + Rkey;
            Pre_case_funcion.Rule = Pre_case_funcion + Case_funcion + ";" | Case_funcion + ";";
            Case_funcion.Rule = ToTerm("case") + Valor + ":" + Instruccion_but_while_funcion + ToTerm("break");
            Default_funcion.Rule = ToTerm("default") + ":" + Instruccion_but_while_funcion + ToTerm("break") + ";";


            //While
            While.Rule = ToTerm("while") + Lparen + Condicion + Rparen + Cuerpo_while;
            Cuerpo_while.Rule = Lkey + Instruccion_while + Rkey | Lkey + Rkey;
            Instruccion_while.Rule = Instruccion_while + Instruccion + Break_continue + ";" 
                | Instruccion_while + Instruccion 
                | Instruccion + Break_continue + ";" 
                | Instruccion;
            //While para funcion
            While_funcion.Rule = ToTerm("while") + Lparen + Condicion + Rparen + Cuerpo_while_funcion;
            Cuerpo_while_funcion.Rule = Lkey + Instruccion_while_funcion + Rkey | Lkey + Rkey;
            Instruccion_while_funcion.Rule = Instruccion_while_funcion + Instruccion_funcion + Break_continue + ";" |
                Instruccion_while_funcion + Instruccion_funcion | Instruccion_funcion + Break_continue + ";" | Instruccion_funcion;
            Break_continue.Rule = reservada_break | reservada_continue;

            //Do while
            DoWhile.Rule = ToTerm("do") + Cuerpo_instruccion + ToTerm("while") + Lparen + Condicion + Rparen + ";";
            //Do while para funcion
            DoWhile_funcion.Rule = ToTerm("do") + Cuerpo_instruccion_funcion + ToTerm("while") + Lparen + Condicion + Rparen + ";";

            //For
            For.Rule = ToTerm("for") + Lparen + Declaracion_for + Acceso + Relacional + Expresion + ";" + Acceso + Paso + Rparen + Cuerpo_instruccion;//asignacion ya trae ;
            Declaracion_for.Rule = TD_int + Acceso + Asignacion | Acceso + Asignacion;
            Paso.Rule = Mas_menos | "+=" + Expresion | "-=" + Expresion | Asignacion;
            //For para funcion
            For_funcion.Rule = ToTerm("for") + Lparen + Declaracion_for + Acceso + Relacional + Expresion + ";" + Acceso + Paso + Rparen + Cuerpo_instruccion_funcion;//asignacion ya trae ;


            //Aumento o disminucion
            Aumentar_disminuir.Rule = Acceso + Mas_menos + ";";
            Mas_menos.Rule = mas_mas | menos_menos;

            //Llamada a un metodo
            Llamada_metodo_asignacion.Rule = reservada_this + "." + Acceso + Llamada + ";" | reservada_this + "." + Acceso + Asignacion;//asignacion ya incluye el ;



            //Condicion
            Condicion.Rule = Condicion + Logico + Not + Comparacion | Not + Comparacion;
            Comparacion.Rule = Not + Expresion + Relacional + Not + Expresion | Not + Expresion;
            Relacional.Rule = relacional_distinto | relacional_igual | relacional_mayor | relacional_mayor_igual | relacional_menor | relacional_menor_igual;//nuevo
            Not.Rule = ToTerm("!") | Empty;
            Logico.Rule = logico_AND | logico_OR;



            //Instruccion
            Instruccion.Rule = Llamada_metodo_asignacion | ID_acceso + ";" | ID_acceso + Asignacion | Aumentar_disminuir | If | For | While | Case | DoWhile | Variable_local;
            Variable_local.Rule = ID_local + Asignacion | ID_local + ";";
            ID_local.Rule = ID_local + "," + id | TipoDato + id;
            //Instruccion para funcion
            Instruccion_funcion.Rule = Llamada_metodo_asignacion | ID_acceso + ";" | ID_acceso + Asignacion | Aumentar_disminuir | If_funcion | For_funcion
                | While_funcion | Case_funcion | DoWhile_funcion | Variable_local | Return;



            //Metodo
            Metodo.Rule = reservada_override + Visibilidad + reservada_void + id + Cp_Sp + Cuerpo_instruccion | Visibilidad + reservada_void + id + Cp_Sp + Cuerpo_instruccion |
                reservada_override + reservada_void + id + Cp_Sp + Cuerpo_instruccion | reservada_void + id + Cp_Sp + Cuerpo_instruccion;
            Cp_Sp.Rule = Lparen + Rparen | Lparen + Parametro_metodo + Rparen;

            //Cuerpo instruccion
            Cuerpo_instruccion.Rule = Lkey + Instruccion_but_while + Rkey | Lkey + Rkey;//agregue el or
            //Cuerpo instruccion para funcion
            Cuerpo_instruccion_funcion.Rule = Lkey + Instruccion_but_while_funcion + Rkey | Lkey + Rkey;//agregue el or

            //Parametro_metodo
            Parametro_metodo.Rule = Parametro_metodo + "," + TipoDato + id | TipoDato + id;

            //Funcion
            Funcion.Rule = reservada_override + Visibilidad + TipoDato + id + Cp_Sp + Cuerpo_instruccion_funcion | Visibilidad + TipoDato + id + Cp_Sp + Cuerpo_instruccion_funcion |
                reservada_override + TipoDato + id + Cp_Sp + Cuerpo_instruccion_funcion | TipoDato + id + Cp_Sp + Cuerpo_instruccion_funcion;
            Return.Rule = reservada_return + Condicion + ";";

            //Constructor
            Constructor.Rule = Visibilidad + id + Cp_Sp + Cuerpo_instruccion | id + Cp_Sp + Cuerpo_instruccion;

            //clase
            Clase.Rule = Visibilidad + reservada_class + id + Extends + Cuerpo_clase | reservada_class + id + Extends + Cuerpo_clase | Visibilidad + reservada_class + id + Cuerpo_clase |
                reservada_class + id + Cuerpo_clase;
            Extends.Rule = reservada_extends + id;
            Cuerpo_clase.Rule = Lkey + Instruccion_clase + Rkey | Lkey + Rkey;
            Instruccion_clase.Rule = Instruccion_clase + Posible_instr_clase | Posible_instr_clase;
            Posible_instr_clase.Rule = Atributo | Funcion | Metodo | Constructor;
            #endregion

            #region Inicio
            this.Root = Clase;
            #endregion

            #region Preferencias
            NonGrammarTerminals.Add(comentario_linea);
            NonGrammarTerminals.Add(comentario_bloque);
            MarkPunctuation(Lparen, Rparen, Lkey, Rkey);
            #endregion


        }


    }
}
