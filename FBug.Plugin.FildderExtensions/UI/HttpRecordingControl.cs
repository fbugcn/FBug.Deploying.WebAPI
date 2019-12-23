using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace FBug.Plugin.FildderExtensions.UI
{
    public partial class HttpRecordingControl : UserControl
    {
        private Dictionary<int, string> m_DictContents;
        private int m_Index;

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
        }


        public bool IsMatch(string url)
        {
            if (!this.IsRunning || m_RexpUrl == null)
            {
                return false;
            }

            return m_RexpUrl.IsMatch(url);
        }

        public void RecordingResponseContent(string url, string content)
        {
            if (!this.IsRunning) return;

            int index = m_Index++;
            lbRequested.Items.Add(new TextValueModel() { Text = url, Value = index });
            txtContent.Text = content;

            m_DictContents.Add(index, content);
        }


        private void HttpRecordingControl_Load(object sender, EventArgs e)
        {
            this.LayoutControls();

            m_Index = 0;
            m_DictContents = new Dictionary<int, string>();
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
                Thread.Sleep(1000);

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
                Thread.Sleep(1000);

                btnStart.Enabled = true;
            }
            catch
            {
                btnStop.Enabled = true;
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
    }
}
