using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto1OLC1
{
    class Archivo
    {
        public Archivo()
        {

        }



        public String leerArchivo(String direccion)//codigo basico para leer un archivo
        {
            StreamReader archivo = new StreamReader(direccion);
            String entrada = "";
            String salida = "";
            try
            {
                while (entrada != null)
                {
                    entrada = archivo.ReadLine();
                    salida += entrada+"\n";
                }
            }catch(Exception e)
            {
                e.ToString();
            }
            finally
            {
                archivo.Close();
            }
            return salida;
        }

        public bool guardarArchivo(String texto, String direccion)//codigo basico para guardar un archivo
        {
            try
            {
                StreamWriter sw = new StreamWriter(direccion);
                sw.Write(texto);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool nuevoArchivo(String nombre)//semejante a guardar, pero no inserto ningun texto, nombre es en realidad una direccion
        {
            if (!File.Exists(nombre))
            {
                try
                {
                    StreamWriter sw = new StreamWriter(nombre);
                    sw.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public bool eliminarArchivo(String direccion)//codigo basico para eliminar un archivo
        {
            if (File.Exists(direccion))
            {
                try
                {
                    File.Delete(direccion);
                    return true;
                }catch (Exception e)
                {
                    return false;
                }
                
            }
            return false;
        }

        public bool nuevoProyecto(String direccion)//codigo basico para crear nueva carpeta
        {
            try
            {
                if (Directory.Exists(direccion))
                    return false;
                DirectoryInfo nuevo = Directory.CreateDirectory(direccion);
                return true;

            }catch(Exception e)
            {
                return false;
            }
        }

    }
}
