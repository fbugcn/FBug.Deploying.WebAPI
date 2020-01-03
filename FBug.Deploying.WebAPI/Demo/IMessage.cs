using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBug.Deploying.WebAPI.Demo
{
    public interface IMessage
    {
        void Echo(string message);
        string Method(string info);
    }
}
