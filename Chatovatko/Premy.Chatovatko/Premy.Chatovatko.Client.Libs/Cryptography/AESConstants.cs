using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Cryptography
{
    internal static class AESConstants
    {
        internal static readonly int SALT_LENGHT = 16;
        internal static readonly int PASSWORD_LENGHT = 32;

        internal static readonly int BLOCKSIZE = 128;
        internal static readonly int KEYSIZE = PASSWORD_LENGHT * 8;
    }
}
