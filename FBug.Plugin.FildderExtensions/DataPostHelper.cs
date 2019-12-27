using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace FBug.Plugin.FildderExtensions
{
    public class DataPostHelper : IDisposable
    {
        private HttpClient m_HttpClient;
        private IList<string> m_ServiceUrls;


        public DataPostHelper()
        {
            m_HttpClient = new HttpClient();

            m_HttpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            m_HttpClient.Timeout = TimeSpan.FromSeconds(15);

            m_ServiceUrls = DataPostHelper.ParseStringList(ConfigurationManager.AppSettings["ServiceUrl"]);
            this.AppendHttpHeader(m_HttpClient, ConfigurationManager.AppSettings["PostHeads"]);
        }


        public PostResult Post(string content)
        {
            bool hasOk = false;
            bool hasEr = false;

            if (m_ServiceUrls.Count > 0)
            {
                StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                foreach (string forwardUrl in m_ServiceUrls)
                {
                    var result = m_HttpClient.PostAsync(forwardUrl, stringContent).GetAwaiter().GetResult();
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        hasOk = true;
                    }
                    else
                    {
                        hasEr = true;
                    }

                    // if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    // {
                    //     result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    // }
                }
            }

            if (hasOk && !hasEr)
            {
                return PostResult.Ok;
            }
            else if (hasEr && !hasOk)
            {
                return PostResult.Error;
            }
            else if (hasOk && hasEr)
            {
                return PostResult.Partial;
            }

            return PostResult.Unknow;
        }


        public void Dispose()
        {
            if (m_HttpClient != null)
            {
                m_HttpClient.Dispose();
                m_HttpClient = null;
            }
        }


        private void AppendHttpHeader(HttpClient httpClient, string content)
        {
            foreach (JObject jo in DataPostHelper.ParseJTokenArray(content))
            {
                if (jo.HasValues)
                {
                    JProperty jp = (JProperty)jo.First;
                    httpClient.DefaultRequestHeaders.Add(jp.Name, Convert.ToString(jo[jp.Name]));
                }
            }
        }


        internal static JObject[] ParseJTokenArray(string content)
        {
            string[] stringList = DataPostHelper.ParseStringList(content);
            return Array.ConvertAll(
                DataPostHelper.ParseStringList(content),
                (s => (JObject)JsonConvert.DeserializeObject(s))
            );
        }

        internal static string[] ParseStringList(string content)
        {
            content = content.Trim();
            if (!string.IsNullOrWhiteSpace(content))
            {
                if (content[0] == '[' && content[content.Length - 1] == ']')
                {
                    return content.Substring(1, content.Length - 2)
                        .Split('|', ',');
                }

                return new string[] { content };
            }

            return new string[] { };
        }
    }
}
