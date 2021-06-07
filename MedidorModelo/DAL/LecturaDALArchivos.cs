using MedidorModelo.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModelo.DAL
{
    public class LecturaDALArchivos : ILecturaDAL
    {

        private LecturaDALArchivos()
        {

        }
        private static LecturaDALArchivos instancia;
        public static ILecturaDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturaDALArchivos();
            }
            return instancia;
        }
        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";
        public void IngresarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(lectura.NroMedidor + "|" + lectura.Fecha + "|" + lectura.ValorConsumo);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lista = new List<Lectura>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            string[] palabrasFecha = arr[1].Split('-');     
                            int nroMedidor = Convert.ToInt32(arr[0]);
                            DateTime fecha = Convert.ToDateTime(arr[1]);
                            double valorConsumo = Convert.ToDouble(arr[2]);
                            Lectura lectura = new Lectura()
                            {
                                NroMedidor = nroMedidor,
                                Fecha = fecha,
                                ValorConsumo = valorConsumo
                            };
                            lista.Add(lectura);
                        }

                    } while (texto != null);
                }

            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
    }


