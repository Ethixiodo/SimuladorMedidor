using MedidorModelo.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mensajero.Comunicacion
{
    public class HebraServidor
    {
        private ILecturaDAL lecturaDAL =  LecturaDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);

            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {

                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    Console.WriteLine("Cliente conectado");

                    HebraCliente clientethread = new HebraCliente(cliente);
                    Thread t = new Thread(new ThreadStart(clientethread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                    
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} es en uso", puerto);
            }
        }




    }
}
