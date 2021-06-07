using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModelo.DTO
{
    public class Lectura
    {
        int nroMedidor;
        DateTime fecha;
        double valorConsumo;

        public int NroMedidor { get => nroMedidor; set => nroMedidor = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public double ValorConsumo { get => valorConsumo; set => valorConsumo = value; }
    }
}
