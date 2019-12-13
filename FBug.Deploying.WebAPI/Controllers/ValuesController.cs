using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FBug.Deploying.WebAPI.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        // GET api/values
        [HttpGet]
        [Route("api/based")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2, hehe." };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Route("api/based")]
        public ActionResult<string> Get(int id)
        {
            if (!this.IsCreated(id))
            {
                // 1分钟内有发送
                return "exists";
            }

            return "created!";
        }

        [HttpGet()]
        [Route("api/values/Repeated")]
        public ActionResult<string> Repeated(int count)
        {
            int counted = 0;
            for (int i = 0; i < count; i++)
            {
                if ( this.IsCreated(10))
                {
                    counted++;
                }
            }

            return $"created count: {counted}";
        }

        private bool IsCreated(int key)
        {
            string keyCode = $"testkey:{key};";
            bool cacheCreated = false;
            cache.GetOrCreate(keyCode, entry =>
            {
                entry.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1));
                cacheCreated = true;
                return keyCode;
            });

            return cacheCreated;
        }
    }
}
