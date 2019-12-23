using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;

namespace FBug.Plugin.FildderExtensions
{
    public class DecodingHelper
    {
        public static string DecodingContent(Session oSession)
        {
            string compressionType = oSession["Content-Encoding"];
            switch (compressionType)
            {
                case "gzip":
                    break;
                default:
                    break;
            }

            return oSession.GetResponseBodyAsString();
        }
    }
}
