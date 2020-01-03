using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using FBug.Deploying.WebAPI.Demo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBug.Deploying.WebAPI.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("api/Test/ParseXml")]
        [Consumes(MediaTypeNames.Application.Xml)]
        public ActionResult<string> ParseXml(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                key = "out_trade_no";
            }

            string xmlBody = @"<xml>
    <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
    <attach><![CDATA[支付测试]]></attach>
    <bank_type><![CDATA[CFT]]></bank_type>
    <fee_type><![CDATA[CNY]]></fee_type>
    <is_subscribe><![CDATA[Y]]></is_subscribe>
    <mch_id><![CDATA[10000100]]></mch_id>
    <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
    <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
    <out_trade_no><![CDATA[1409811653]]></out_trade_no>
    <result_code><![CDATA[SUCCESS]]></result_code>
    <return_code><![CDATA[SUCCESS]]></return_code>
    <sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign>
    <time_end><![CDATA[20140903131540]]></time_end>
    <total_fee>1</total_fee>
    <coupon_fee><![CDATA[10]]></coupon_fee>
    <coupon_count><![CDATA[1]]></coupon_count>
    <coupon_type><![CDATA[CASH]]></coupon_type>
    <coupon_id><![CDATA[10000]]></coupon_id>
    <coupon_fee><![CDATA[100]]></coupon_fee>
    <trade_type><![CDATA[JSAPI]]></trade_type>
    <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
</xml>";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlBody);
                string json = JsonConvert.SerializeXmlNode(doc);

                XmlNode node = doc.DocumentElement.SelectSingleNode(string.Concat("//",key));
                if (node == null)
                {
                    return new ActionResult<string>($"【{key}】没找到");
                }

                return new ActionResult<string>(node.InnerText);
            }
            catch (Exception ex)
            {
                return new ActionResult<string>(ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Test/Call")]
        [Consumes(MediaTypeNames.Application.Xml)]
        public ActionResult<string> Call()
        {
            var toWrap = new MessageImplementation();
            IMessage decorator = MessageDecoratorProxy.CreateProxy<IMessage>();
            ((MessageDecoratorProxy)decorator).Wrapped = toWrap;
            ((MessageDecoratorProxy)decorator).Start = (tm, a) => Console.WriteLine($"{tm.Name}({string.Join(',', a)})方法开始调用");
            ((MessageDecoratorProxy)decorator).End = (tm, a, r) => Console.WriteLine($"{tm.Name}({string.Join(',', a)})方法结束调用，返回结果{r}");
            decorator.Echo("Echo");
            return decorator.Method("Method");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Test/GetString")]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<string> GetString(string c)
        {
            Regex rexp = new Regex(@"\\u[\da-f]{4}");
            return rexp.Replace(c, (match => {
                return ((char)Convert.ToUInt16(match.Value.Substring(2), 0x10)).ToString();
            }));
        }


        [HttpGet]
        [Route("api/Test/GetDefaultValue")]
        public ActionResult<string> GetDefaultValue()
        {
            string content = " mtopjsonp6({\"api\":\"mtop.alimama.union.xt.en.api.entry\",\"v\":\"2.0\"})";
            string json = "{\"webapi\":\"D4917D03-6630-4FA4-978A-5415FAE29B69\"}";
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            if (jo.HasValues)
            {
                JProperty jp = (JProperty)jo.First;

                using (HttpClient client = new HttpClient())
                {
                    StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add(jp.Name, Convert.ToString(jo[jp.Name]));
                    var result = client.PostAsync("https://h5.taohuikeji.net/api/ShopNew/ReceiveContent", stringContent)
                        .GetAwaiter().GetResult();
                    return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }

            return "hehe";
        }
    }
}
