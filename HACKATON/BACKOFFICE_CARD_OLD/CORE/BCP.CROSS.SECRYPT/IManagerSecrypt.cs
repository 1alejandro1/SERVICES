using System;

namespace BCP.CROSS.SECRYPT
{
    public interface IManagerSecrypt
    {
        string Encriptar(string value);

        string Desencriptar(string value);

        Tuple<bool, string> Check();
    }
}