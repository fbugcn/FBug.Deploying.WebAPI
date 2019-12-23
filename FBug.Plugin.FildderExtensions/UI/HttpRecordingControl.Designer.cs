namespace FBug.Plugin.FildderExtensions.UI
{
    partial class HttpRecordingControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtReguraText = new System.Windows.Forms.TextBox();
            this.lblReguraText = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbRequested = new System.Windows.Forms.ListBox();
            this.cmsListItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csmiClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStop = new System.Windows.Forms.Button();
            this.cbIgnoreCase = new System.Windows.Forms.CheckBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.cmsListItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReguraText
            // 
            this.txtReguraText.Location = new System.Drawing.Point(56, 13);
            this.txtReguraText.Name = "txtReguraText";
            this.txtReguraText.Size = new System.Drawing.Size(309, 21);
            this.txtReguraText.TabIndex = 0;
            this.txtReguraText.TextChanged += new System.EventHandler(this.TxtReguraText_TextChanged);
            // 
            // lblReguraText
            // 
            this.lblReguraText.AutoSize = true;
            this.lblReguraText.Location = new System.Drawing.Point(9, 16);
            this.lblReguraText.Name = "lblReguraText";
            this.lblReguraText.Size = new System.Drawing.Size(41, 12);
            this.lblReguraText.TabIndex = 1;
            this.lblReguraText.Text = "正则：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(56, 40);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(60, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // lbRequested
            // 
            this.lbRequested.ContextMenuStrip = this.cmsListItem;
            this.lbRequested.DisplayMember = "Text";
            this.lbRequested.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbRequested.FormattingEnabled = true;
            this.lbRequested.ItemHeight = 12;
            this.lbRequested.Location = new System.Drawing.Point(507, 0);
            this.lbRequested.Name = "lbRequested";
            this.lbRequested.Size = new System.Drawing.Size(193, 500);
            this.lbRequested.TabIndex = 3;
            this.lbRequested.ValueMember = "Value";
            this.lbRequested.SelectedIndexChanged += new System.EventHandler(this.LbRequested_SelectedIndexChanged);
            // 
            // cmsListItem
            // 
            this.cmsListItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csmiClearAll});
            this.cmsListItem.Name = "cmsListItem";
            this.cmsListItem.Size = new System.Drawing.Size(117, 26);
            // 
            // csmiClearAll
            // 
            this.csmiClearAll.Name = "csmiClearAll";
            this.csmiClearAll.Size = new System.Drawing.Size(116, 22);
            this.csmiClearAll.Text = "清理(&C)";
            this.csmiClearAll.Click += new System.EventHandler(this.CsmiClearAll_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(122, 40);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // cbIgnoreCase
            // 
            this.cbIgnoreCase.AutoSize = true;
            this.cbIgnoreCase.Checked = true;
            this.cbIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreCase.Location = new System.Drawing.Point(281, 44);
            this.cbIgnoreCase.Name = "cbIgnoreCase";
            this.cbIgnoreCase.Size = new System.Drawing.Size(84, 16);
            this.cbIgnoreCase.TabIndex = 4;
            this.cbIgnoreCase.Text = "IgnoreCase";
            this.cbIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(11, 87);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ReadOnly = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(354, 389);
            this.txtContent.TabIndex = 5;
            // 
            // HttpRecordingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.cbIgnoreCase);
            this.Controls.Add(this.lbRequested);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblReguraText);
            this.Controls.Add(this.txtReguraText);
            this.Name = "HttpRecordingControl";
            this.Size = new System.Drawing.Size(700, 500);
            this.Load += new System.EventHandler(this.HttpRecordingControl_Load);
            this.Resize += new System.EventHandler(this.HttpRecordingControl_Resize);
            this.cmsListItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReguraText;
        private System.Windows.Forms.Label lblReguraText;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lbRequested;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox cbIgnoreCase;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.ContextMenuStrip cmsListItem;
        private System.Windows.Forms.ToolStripMenuItem csmiClearAll;
    }
}
