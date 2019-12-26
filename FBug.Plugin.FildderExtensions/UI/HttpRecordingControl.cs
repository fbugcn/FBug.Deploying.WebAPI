using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FBug.Plugin.FildderExtensions.UI
{
    public partial class HttpRecordingControl : UserControl
    {
        private HttpClient m_HttpClient;
        private Dictionary<int, string> m_DictContents;
        private int m_Index;
        private IList<string> m_ServiceUrls;

        /// <summary>
        /// 是否在运行中
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private Regex m_RexpUrl;


        public HttpRecordingControl()
        {
            InitializeComponent();
            this.Disposed += HttpRecordingControl_Disposed;
        }

        public bool IsMatch(string url)
        {
            if (!this.IsRunning || m_RexpUrl == null)
            {
                return false;
            }

            return m_RexpUrl.IsMatch(url);
        }

        public void RecordingResponseContent(string rawUrl, string content)
        {
            if (!this.IsRunning) return;

            bool hasOk = false;
            bool hasEr = false;
            if (m_ServiceUrls.Count > 0)
            {
                StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                foreach (string forwardUrl in m_ServiceUrls)
                {
                    var result = m_HttpClient.PostAsync(forwardUrl, stringContent).GetAwaiter().GetResult();
                    if ( result.StatusCode == System.Net.HttpStatusCode.OK)
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

            string prefix = string.Empty;
            if (hasOk && !hasEr)
            {
                prefix = "[OK]";
            }
            else if (hasEr && !hasOk)
            {
                prefix = "[ER]";
            }
            else if (hasOk && hasEr)
            {
                prefix = "[OE]";
            }

            int index = m_Index++;
            lbRequested.Items.Add(new TextValueModel() { Text = string.Concat(prefix, rawUrl), Value = index });
            txtContent.Text = content;

            m_DictContents.Add(index, content);
        }


        private void HttpRecordingControl_Load(object sender, EventArgs e)
        {
            m_Index = 0;
            m_HttpClient = new HttpClient();
            m_DictContents = new Dictionary<int, string>();

            m_HttpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            m_HttpClient.Timeout = TimeSpan.FromSeconds(15);

            this.LoadConfig();
            this.LayoutControls();
        }

        private void HttpRecordingControl_Resize(object sender, EventArgs e)
        {
            this.LayoutControls();
        }

        private void TxtReguraText_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReguraText.Text))
            {
                this.m_RexpUrl = null;
                return;
            }

            try
            {
                this.m_RexpUrl = cbIgnoreCase.Checked
                    ? new Regex(txtReguraText.Text, RegexOptions.IgnoreCase)
                    : new Regex(txtReguraText.Text);
            }
            catch
            {
                this.m_RexpUrl = null;
                MessageBox.Show("正则表达式无效");
            }
        }

        private void LbRequested_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbRequested.SelectedIndex < 0)
            {
                return;
            }

            TextValueModel item = lbRequested.Items[this.lbRequested.SelectedIndex] as TextValueModel;
            if (item != null
                && m_DictContents.TryGetValue(item.Value, out string content))
            {
                txtContent.Text = content;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            try
            {


                this.IsRunning = true;
                btnStop.Enabled = true;
            }
            catch
            {
                btnStart.Enabled = true;
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;

            try
            {
                this.IsRunning = false;


                btnStart.Enabled = true;
            }
            catch
            {
                btnStop.Enabled = true;
            }
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            lbRequested.Items.Clear();
            m_DictContents.Clear();
        }

        private void HttpRecordingControl_Disposed(object sender, EventArgs e)
        {
            m_HttpClient.Dispose();
            m_HttpClient = null;
        }

        private void CsmiCopy_Click(object sender, EventArgs e)
        {
            if (lbRequested.SelectedIndex >= 0)
            {
                TextValueModel item = lbRequested.Items[this.lbRequested.SelectedIndex] as TextValueModel;
                if (item != null)
                {
                    Clipboard.SetText(item.Text);
                }
            }
        }

        private void CsmiClearAll_Click(object sender, EventArgs e)
        {
            lbRequested.Items.Clear();
            m_DictContents.Clear();
        }


        private void LayoutControls()
        {
            int splited = 5;
            lbRequested.Width = (int)(this.Width * 0.25M);
            txtReguraText.Width = this.Width - lbRequested.Width - splited - txtReguraText.Left;
            cbIgnoreCase.Left = this.Width - lbRequested.Width - splited - cbIgnoreCase.Width;
            txtContent.Width = this.Width - lbRequested.Width - splited - txtContent.Left;
            txtContent.Height = this.Height - txtContent.Top - 25;
        }

        private void LoadConfig()
        {
            txtReguraText.Text = ConfigurationManager.AppSettings["ReguraText"];
            m_ServiceUrls = this.ParseStringList(ConfigurationManager.AppSettings["ServiceUrl"]);
            if (bool.TryParse(ConfigurationManager.AppSettings["AutoStart"], out bool autoStart)
                && autoStart)
            {
                btnStart.PerformClick();
            }
        }

        private IList<string> ParseStringList(string content)
        {
            content = content.Trim();
            if (!string.IsNullOrWhiteSpace(content))
            {
                if (content[0] == '[' && content[content.Length - 1] == ']')
                {
                    return content.Substring(1, content.Length - 2).Split('|', ',');
                }

                return new List<string>() { content };
            }

            return new List<string>();
        }

    }
}
