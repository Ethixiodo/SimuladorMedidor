using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModelo.DTO
{
    public class Medidor
    {
        int nroMedidor;

        public Medidor(int nroMedidor)
        {
            NroMedidor = nroMedidor;

        }

        public int NroMedidor { get => nroMedidor; set => nroMedidor = value; }
    }
}
