using MedidorModelo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModelo.DAL
{
    public interface ILecturaDAL
    {
        void IngresarLectura(Lectura lectura);

        List<Lectura> ObtenerLecturas();
    }
}

