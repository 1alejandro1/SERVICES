﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Sarc
{
    public class TiempoCaso:TipoCaso
    {
        public string IdServicio { get; set; }
        public int TiempoResolucion { get; set; }
    }
}
