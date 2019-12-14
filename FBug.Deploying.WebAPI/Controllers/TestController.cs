﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBug.Deploying.WebAPI.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("api/Test/ParseXml")]
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
    }
}