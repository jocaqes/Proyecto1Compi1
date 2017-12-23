using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1OLC1
{
    public partial class Form1 : Form
    {
        private Archivo manejo_archivos;
        private Grafica diagrama_clases;
        private Analisis analizar_clase;
        private Gramatica gramatica_java;

        public Form1()
        {
            InitializeComponent();
            generarTreeView();
            setPropiedades();
            manejo_archivos = new Archivo();
            analizar_clase = new Analisis();
            gramatica_java = new Gramatica();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        #region Prueba_Tree_View
        private void generarTreeView()//codigo que provee microsoft para hacer un treeview
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(@"D:\Sistemas\2017\2do_Semestre\Compi\proyecto1");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                getDirectorios(info.GetDirectories(), rootNode);
                arbol_carpetas.Nodes.Clear();//vacio el arbol, sirve para llamar otra vez esta funcion pero con nuevos items
                arbol_carpetas.Nodes.Add(rootNode);
            }
        }

        private void getDirectorios(DirectoryInfo[] subDirs,
        TreeNode nodeToAddTo)//codigo que provee microsoft para hacer los sub folders del treeview
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                //aNode.ImageKey = "folder"; //quite imagen por defecto mucho lio encotrar un dibujito 
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    getDirectorios(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }


        private void arbol_carpetasClick(object sender, TreeNodeMouseClickEventArgs e)//parte del codigo lo provee microsoft junto con el como hacer treeview
        {
            TreeNode seleccionado = e.Node;
            lista_java_files.Items.Clear();
            DirectoryInfo directorio = (DirectoryInfo)seleccionado.Tag;
            ListViewItem item = null;
            foreach(var file in directorio.GetFiles("*.java")){//esto lo agregue para que liste solo archivos que terminen en .java
                item = new ListViewItem(file.Name);
                lista_java_files.Items.Add(item);
            }
            text_directorio.Text = directorio.FullName;
        }


        private void lista_java_files_SelectedIndexChanged(object sender, EventArgs e)//es para añadir el texto del archivo .java a un rich text box
        {
            if (!String.IsNullOrEmpty(text_directorio.Text))//reviso que haya una carpeta seleccionada
            {
                //text_edicion.Text = new Archivo().leerArchivo(@lista_java_files.SelectedItems.ToString());
                String direccion = text_directorio.Text+"\\";
                String archivo = "";
                foreach(ListViewItem item in lista_java_files.SelectedItems)//asi recupero el item que se selecciono en el list view, como desabilite multiple selection se que solo hay 1
                    archivo = item.Text;
                if (!String.IsNullOrEmpty(archivo))//en caso de que si encontro un archivo
                {
                    direccion += archivo;
                    text_edicion.Text = new Archivo().leerArchivo(@direccion);//leo automaticamente cualquier archivo .java del listview, para facilitar la edicion
                }
                else
                    text_edicion.Text = "";
                
            }
        }

        #endregion

        #region Propiedades
        private void setPropiedades()
        {
            lista_java_files.GridLines = true;
            lista_java_files.View = View.List;
            
        }

        #endregion

        #region Funciones de los botones
        private void boton_modificar_Click(object sender, EventArgs e)
        {
          
            String archivo = "";
            String direccion = text_directorio.Text + "\\";
            foreach (ListViewItem item in lista_java_files.SelectedItems)//tomo el nombre del archivo .java elegido
                archivo = item.Text;
            if (!String.IsNullOrEmpty(archivo))
            {
                direccion += archivo;
                if (manejo_archivos.guardarArchivo(text_edicion.Text, @direccion))
                    addMessageToLog("Archivo Modificaco:" + archivo);
                //MessageBox.Show("Archivo Modificado");
                else
                    addMessageToLog("Error al modificar:" + archivo);
                //MessageBox.Show("Error al guardar el Archivo");
            }
            else
                addMessageToLog("Seleccione un archivo primero");//de la listview 
                //MessageBox.Show("Seleccione un archivo primero");
              
        }

        private void boton_proyecto_Click(object sender, EventArgs e)//para crear un nuevo proyecto (en realidad es una carpeta nueva)
        {
            if (!String.IsNullOrEmpty(text_directorio.Text))
            {
                String nombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre del Proyecto", "Nuevo Proyecto");
                String direccion = text_directorio.Text + "\\" + nombre;
                if (manejo_archivos.nuevoProyecto(@direccion))
                {
                    generarTreeView();
                    text_directorio.Text = direccion;
                    addMessageToLog("Proyecto Creado:"+nombre);
                }
                else
                    addMessageToLog("Proyecto ya existe");
            }
            else
            {
                MessageBox.Show("Seleccione una carpeta primero");
                addMessageToLog("Seleccione una carpeta primero");
            }
        }



        private void boton_clase_Click(object sender, EventArgs e)//para crear una nueva clase
        {
            if (!String.IsNullOrEmpty(text_directorio.Text))
            {
                String nombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese nombre de la clase", "Nueva Clase");
                if (!nombre.EndsWith(".java"))//por si acaso
                    nombre += ".java";
                String directorio = text_directorio.Text + "\\" + nombre;
                if (manejo_archivos.nuevoArchivo(@directorio))
                    addMessageToLog("Clase creada:" + nombre);
                else
                    addMessageToLog("La clase ya existe:" + nombre);
                
                
            }
        }

        private void boton_eliminar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(text_directorio.Text))//reviso que haya un directorio elegido
            {
                String direccion = text_directorio.Text + "\\";
                String archivo = "";
                foreach (ListViewItem item in lista_java_files.SelectedItems)//reviso que clase esta seleccionada en el listview               
                    archivo = item.Text;
                if (!String.IsNullOrEmpty(archivo))//de haber una clase seleccionada, la intento eliminar, de lo contrario no
                {
                    direccion += archivo;
                    if (manejo_archivos.eliminarArchivo(@direccion))
                    {
                        addMessageToLog("Clase eliminada:" + archivo);
                        text_edicion.Text = "";
                        lista_java_files.FocusedItem.Remove();
                    }
                    else
                    {
                        addMessageToLog("Error al tratar de eliminar la clase:" + archivo);
                    }
                }
                else
                    addMessageToLog("Seleccione una clase primero");
            }
            else
                addMessageToLog("Seleccione una carpeta primero");
        }

        private void boton_diagrama_Click(object sender, EventArgs e)
        {
            generarDiagrama();
        }

        #endregion






        #region Funciones auxiliares

        private void addMessageToLog(String mensaje)
        {
            DateTime hora = DateTime.Now;
            text_log.Text += mensaje + "   at:" + hora.Hour + ":" + hora.Minute + "\n";
        }

        private void generarDiagrama()
        {
            diagrama_clases = new Grafica();
            String archivo_con_error="";
            if (!String.IsNullOrEmpty(text_directorio.Text) && lista_java_files.Items.Count > 0)//reviso que se haya seleccionado un directorio y que tenga clases .java
            {
                String codigo_salida = analizarListaClases();
                if (!analizar_clase.Sin_error)
                {
                    addMessageToLog("Error en clase:" + archivo_con_error);

                }else
                {
                    codigo_salida += diagrama_clases.Codigo_extra;//agrego las flechas
                    codigo_salida += "}";//ciero el codigo, hasta aqui es valido hacerlo
                    int index_nombre = text_directorio.Text.Split('\\').Length - 1;//adquiero la posicion del nombre del proyecto
                    String nombre_proyecto = text_directorio.Text.Split('\\')[index_nombre];//adquiero el nombre del proyecto
                    String comando_cmd = generarComandoCMD(codigo_salida);
                    if (diagrama_clases.generarGrafica(comando_cmd))
                    {
                        addMessageToLog("Diagrama generado correctamente:" + nombre_proyecto);
                    }
                    else
                    {
                        addMessageToLog("Error al generar diagrama de clases:" + nombre_proyecto);
                    }
                }                
            }else
            {
                addMessageToLog("Seleccione un directorio adecuado");
            }


        }

        private String analizarListaClases()
        {
            String codigo_salida = "";
            analizar_clase.Sin_error = true;//inicia sin errores
            foreach (ListViewItem clase in lista_java_files.Items)
            {

                String entrada_path = text_directorio.Text + "\\" + clase.Text;//el path completo del archivo a analizar
                String codigo_clase = manejo_archivos.leerArchivo(@entrada_path);//tomo el codigo dentro del archivo
                if (analizar_clase.validar(codigo_clase, gramatica_java))
                {
                    codigo_salida = diagrama_clases.crearCodigoClase(analizar_clase.Arbol.Root);//agrego a el codigo de salida, codigo para la clase actual
                }

            }
            return codigo_salida;
        }

        private String generarComandoCMD(String codigo_salida)//codigo para el cmd, separe los string solo para verlos mejor aunque no ayuda mucho en el codigo
        {
            String comando_cmd = "";
            int index_nombre = text_directorio.Text.Split('\\').Length - 1;//adquiero la posicion del nombre del proyecto
            String nombre_proyecto = text_directorio.Text.Split('\\')[index_nombre];//adquiero el nombre del proyecto
            String path_imagen = text_directorio.Text + "\\" + "diagrama_Clase_" + nombre_proyecto + ".png";//creo el path para el archivo con el codigo, que sea .png es un truco que descubri para no usar un .dot
            manejo_archivos.guardarArchivo(codigo_salida, path_imagen);//creo el archivo con el codigo de graphviz
            String dot_path = "D:\\Sistemas\\Instaladores\\graphviz-2.38\\release\\bin\\dot.exe";//aqui tengo el dot.exe de graphviz
            String tparam = "-Tpng";//instruccion para cmd
            String fileinput = path_imagen;//archivo de entrada
            String oparam = "-o";//instruccion para cmd
            String fileoutput = path_imagen;//archivo de salida
            comando_cmd = dot_path + " " + tparam + " " + fileinput + " " + oparam + " " + fileoutput;//el comando que quiero
            return comando_cmd;        
        }


        #endregion


    }
}
