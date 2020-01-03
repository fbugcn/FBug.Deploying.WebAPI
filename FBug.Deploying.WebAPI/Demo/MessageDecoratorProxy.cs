using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FBug.Deploying.WebAPI.Demo
{
    public class MessageDecoratorProxy : DispatchProxy
    {
        public object Wrapped { get; set; }
        public Action<MethodInfo, object[]> Start { get; set; }
        public Action<MethodInfo, object[], object> End { get; set; }


        public static T CreateProxy<T>()
        {
            return DispatchProxy.Create<T, MessageDecoratorProxy>();
        }


        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Start(targetMethod, args);
            object result = targetMethod.Invoke(Wrapped, args);
            End(targetMethod, args, result);
            return result;
        }
    }
}
