using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Lexico
{
    public class ParametroLexicoRequest
    {
        public string Lexico { get; set; }
        public string Valor { get; set; }

        public ParametroLexicoRequest()
        {
            Lexico = string.Empty;
            Valor = string.Empty;
        }
    }
}
