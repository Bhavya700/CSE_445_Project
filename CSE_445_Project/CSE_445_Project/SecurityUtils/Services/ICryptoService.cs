using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this was built by Bhavya PateL as a part of DDL class library Module

namespace SecurityUtils.Services
{
    public interface ICryptoService
    {
        string Hash(string plainText);
        bool Verify(string plainText, string hashedValue);
    }
}
