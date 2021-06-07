using MedidorModelo.DAL;
using MedidorModelo.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensajero.Comunicacion
{
    class HebraCliente
    {

        private ClienteCom cliente;
        private ILecturaDAL lecturaDAL = LecturaDALArchivos.GetInstancia();
        private IMedidorDAL medidorDAL = MedidorDALObjeto.GetInstancia();

        public HebraCliente(ClienteCom cliente)
        {
            this.cliente = cliente;
        }

        public HebraCliente()
        {

        }

        public void Ejecutar()
        {
            try
            {
                cliente.Escribir("Bienvenido cliente, ingrese los datos de la lectura del medidor: ");
                string texto = cliente.Leer();
                string[] arr = texto.Trim().Split('|');
                string[] palabrasFecha = arr[1].Split('-');
                string fechaFormateada = palabrasFecha[2] + "/" + palabrasFecha[1] + "/" + palabrasFecha[0] + " " + palabrasFecha[3] + ":" + palabrasFecha[4] + ":" + palabrasFecha[5];
                arr[2] = arr[2].Replace(".", ",");
                int nroMedidor = Convert.ToInt32(arr[0]);
                if (validarMedidor(nroMedidor))
                {
                    DateTime fecha = Convert.ToDateTime(fechaFormateada);
                    double valorConsumo = Convert.ToDouble(arr[2]);
                    Lectura lectura = new Lectura()
                    {
                        NroMedidor = nroMedidor,
                        Fecha = fecha,
                        ValorConsumo = valorConsumo,
                    };
                    lock (lecturaDAL)
                    {
                        lecturaDAL.IngresarLectura(lectura);
                    }
                    Console.WriteLine("Lectura del medidor almacenada correctamente");
                    cliente.Escribir("OK");
                    cliente.Desconectar();
                }
                else
                {
                    cliente.Escribir("ERROR");
                    cliente.Desconectar();
                }

            }
            catch (Exception)
            {

                cliente.Escribir("FORMATO INVALIDO");
                Ejecutar();
            }

        }

        public Boolean validarMedidor(int nroMedidor)
        {
            List<Medidor> medidores = new List<Medidor>();
            medidores = medidorDAL.ObtenerMedidores();
            if( medidores.Any(i => i.NroMedidor == nroMedidor))
            {
                return true;
            } else
            {
                return false;
            }
        }

      
    }
}
