using MedidorModelo.DAL;
using MedidorModelo.DTO;
using Mensajero.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimuladorMedidor
{
    public class Program
    {
        private static ILecturaDAL lecturaDAL = LecturaDALArchivos.GetInstancia();
        private static IMedidorDAL medidorDAL = MedidorDALObjeto.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("\nBienvenido al Mensajero");
            Console.WriteLine(" 1. Ingresar Lecturas \n 2. Mostrar Lecturas \n 3. Revisar medidores existentes \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    IngresarLectura();
                    break;
                case "2":
                    MostrarLectura();
                    break;
                case "3":
                    MostrarMedidores();
                    break;
                case "0":
                    continuar = false;
                    break;

                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;

        }
        static void Main(string[] args)
        {
            

            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            while (Menu()) ;

     
        }
        static void IngresarLectura()
        {
            try
            {
                string txtnroMedidor;
                string txtFecha;
                string txtValorConsumo;
                HebraCliente test = new HebraCliente();
                do
                {
                    Console.WriteLine("Ingrese el número del medidor");
                    txtnroMedidor = Console.ReadLine().Trim();
                    if (test.validarMedidor(Convert.ToInt32(txtnroMedidor)))
                    {
                        do
                        {

                            Console.WriteLine("Ingrese la fecha");
                            txtFecha = Console.ReadLine().Trim();
                        } while ((txtFecha == string.Empty));

                        do
                        {

                            Console.WriteLine("Ingrese el valor de consumo");
                            txtValorConsumo = Console.ReadLine().Trim();
                            txtValorConsumo = txtValorConsumo.Replace(".", ",");
                        } while ((txtValorConsumo == string.Empty));
                        int nroMedidor = Convert.ToInt32(txtnroMedidor);
                        DateTime fecha = Convert.ToDateTime(txtFecha);
                        double valorConsumo = Convert.ToDouble(txtValorConsumo);
                        Lectura lectura = new Lectura()
                        {
                            NroMedidor = nroMedidor,
                            Fecha = fecha,
                            ValorConsumo = valorConsumo
                        };
                        lock (lecturaDAL)
                        {
                            lecturaDAL.IngresarLectura(lectura);
                        }


                    }
                    else
                    {
                        Console.WriteLine("ERROR");
                    }
                } while (txtnroMedidor == string.Empty);


            }
            catch (Exception)
            {

                Console.WriteLine("\nFORMATO INVALIDO\n");
            }

        }

        static void MostrarLectura()
        {

            List<Lectura> lecturas = null;
            lock (lecturaDAL)
            {
                lecturas = lecturaDAL.ObtenerLecturas();
                
            };
            for (int i = 0; i < lecturas.Count(); ++i)
            {
                Lectura actual = lecturas[i];
                Console.WriteLine("{0}:\nNumero medidor:{1}\nFecha:{2}\nValor consumo:{3}\n--------o-------\n",
                    i + 1, actual.NroMedidor, actual.Fecha, actual.ValorConsumo);
            }
        }

        static void MostrarMedidores()
        {
            List<Medidor> medidores = null;
            lock (medidorDAL)
            {
                medidores = medidorDAL.ObtenerMedidores();

            };
            Console.WriteLine("Medidores existentes:");
            for (int i = 0; i < medidores.Count(); ++i)
            {
                Medidor actual = medidores[i];
                Console.WriteLine("\nNumero medidor:{1}",
                    i + 1, actual.NroMedidor);
            }
        }
    }
}
