using MedidorModelo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModelo.DAL
{
    public class MedidorDALObjeto : IMedidorDAL
    {
        private static List<Medidor> medidores = new List<Medidor>();

        private MedidorDALObjeto()
        {
            rellenarMedidores();

        }
        private static MedidorDALObjeto instancia;
        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALObjeto();
            }
            return instancia;
        }

        public void rellenarMedidores()
        {
            for(int i=1; i<=20; i++)
            {
                //Se rellena la lista estatica de medidores para utilizar de pruebas
                medidores.Add(new Medidor(i));
            }
        }


        public List<Medidor> ObtenerMedidores()
        {
            return medidores;
        }
    }
}
