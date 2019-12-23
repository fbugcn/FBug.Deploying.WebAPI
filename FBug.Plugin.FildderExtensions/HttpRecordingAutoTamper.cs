using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fiddler;
using FBug.Plugin.FildderExtensions.UI;

namespace FBug.Plugin.FildderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpRecordingAutoTamper : IAutoTamper
    {
        /// <summary>
        /// HttpRecording控件
        /// </summary>
        private HttpRecordingControl m_RecordingControl;


        public void AutoTamperRequestBefore(Session oSession)
        {
        }

        public void AutoTamperRequestAfter(Session oSession)
        {
        }


        public void AutoTamperResponseBefore(Session oSession)
        {
        }

        public void AutoTamperResponseAfter(Session oSession)
        {
            if (m_RecordingControl.IsMatch(oSession.url))
            {
                m_RecordingControl.RecordingResponseContent(
                    oSession.url, DecodingHelper.DecodingContent(oSession)
                );
            }
        }


        public void OnBeforeReturningError(Session oSession)
        {
        }


        public void OnLoad()
        {
            TabPage recordingTab = new TabPage("HttpRecording");

            // 添加到选项卡
            recordingTab.Controls.Add(m_RecordingControl = new HttpRecordingControl());

            // 添加icon图标
            recordingTab.ImageIndex = (int)Fiddler.SessionIcons.Downloading;

            TabControl tabs = FiddlerApplication.UI.tabsViews;
            m_RecordingControl.Width = tabs.Width;
            m_RecordingControl.Height = tabs.Height;
            tabs.Resize += Tabs_Resize;
            Form main = tabs.FindForm();
            if (main != null)
            {

            }

            tabs.TabPages.Add(recordingTab);
        }

        public void OnBeforeUnload()
        {

        }


        private void Tabs_Resize(object sender, EventArgs e)
        {
            m_RecordingControl.Width = FiddlerApplication.UI.tabsViews.Width;
            m_RecordingControl.Height = FiddlerApplication.UI.tabsViews.Height;
        }
    }
}
