using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBug.Deploying.WebAPI.Demo
{
    public class MessageImplementation : IMessage
    {
        public void Echo(string message) => Console.WriteLine($"Echo参数：{message}");

        public string Method(string info)
        {
            Console.WriteLine($"Method参数：{info}");
            return info;
        }
    }
}
