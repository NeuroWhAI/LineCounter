namespace LineCounter
{
    partial class Form_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_extList = new System.Windows.Forms.TextBox();
            this.contextMenuStrip_extList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_saveExt = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_editExt = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox_deep = new System.Windows.Forms.CheckBox();
            this.button_count = new System.Windows.Forms.Button();
            this.folderBrowserDialog_find = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBox_extTemplate = new System.Windows.Forms.ComboBox();
            this.linkLabel_maker = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip_extList.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ext";
            // 
            // textBox_extList
            // 
            this.textBox_extList.ContextMenuStrip = this.contextMenuStrip_extList;
            this.textBox_extList.Location = new System.Drawing.Point(12, 35);
            this.textBox_extList.Multiline = true;
            this.textBox_extList.Name = "textBox_extList";
            this.textBox_extList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_extList.Size = new System.Drawing.Size(209, 106);
            this.textBox_extList.TabIndex = 2;
            this.textBox_extList.Text = "*.*";
            this.textBox_extList.DoubleClick += new System.EventHandler(this.textBox_extList_DoubleClick);
            // 
            // contextMenuStrip_extList
            // 
            this.contextMenuStrip_extList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_extList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_saveExt,
            this.ToolStripMenuItem_editExt});
            this.contextMenuStrip_extList.Name = "contextMenuStrip_extList";
            this.contextMenuStrip_extList.Size = new System.Drawing.Size(215, 56);
            // 
            // ToolStripMenuItem_saveExt
            // 
            this.ToolStripMenuItem_saveExt.Name = "ToolStripMenuItem_saveExt";
            this.ToolStripMenuItem_saveExt.Size = new System.Drawing.Size(214, 26);
            this.ToolStripMenuItem_saveExt.Text = "템플릿 목록에 추가";
            this.ToolStripMenuItem_saveExt.Click += new System.EventHandler(this.ToolStripMenuItem_saveExt_Click);
            // 
            // ToolStripMenuItem_editExt
            // 
            this.ToolStripMenuItem_editExt.Name = "ToolStripMenuItem_editExt";
            this.ToolStripMenuItem_editExt.Size = new System.Drawing.Size(214, 26);
            this.ToolStripMenuItem_editExt.Text = "현재 템플릿 수정";
            this.ToolStripMenuItem_editExt.Click += new System.EventHandler(this.ToolStripMenuItem_editExt_Click);
            // 
            // checkBox_deep
            // 
            this.checkBox_deep.AutoSize = true;
            this.checkBox_deep.Location = new System.Drawing.Point(12, 147);
            this.checkBox_deep.Name = "checkBox_deep";
            this.checkBox_deep.Size = new System.Drawing.Size(124, 19);
            this.checkBox_deep.TabIndex = 3;
            this.checkBox_deep.Text = "하위 폴더까지";
            this.checkBox_deep.UseVisualStyleBackColor = true;
            // 
            // button_count
            // 
            this.button_count.Location = new System.Drawing.Point(12, 172);
            this.button_count.Name = "button_count";
            this.button_count.Size = new System.Drawing.Size(209, 40);
            this.button_count.TabIndex = 4;
            this.button_count.Text = "탐색";
            this.button_count.UseVisualStyleBackColor = true;
            this.button_count.Click += new System.EventHandler(this.button_count_Click);
            // 
            // folderBrowserDialog_find
            // 
            this.folderBrowserDialog_find.Description = "탐색을 시작할 폴더를 선택하세요.";
            this.folderBrowserDialog_find.ShowNewFolderButton = false;
            // 
            // comboBox_extTemplate
            // 
            this.comboBox_extTemplate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox_extTemplate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_extTemplate.FormattingEnabled = true;
            this.comboBox_extTemplate.Location = new System.Drawing.Point(47, 9);
            this.comboBox_extTemplate.Name = "comboBox_extTemplate";
            this.comboBox_extTemplate.Size = new System.Drawing.Size(174, 23);
            this.comboBox_extTemplate.TabIndex = 5;
            this.comboBox_extTemplate.Text = "All files";
            this.comboBox_extTemplate.SelectedIndexChanged += new System.EventHandler(this.comboBox_extTemplate_SelectedIndexChanged);
            // 
            // linkLabel_maker
            // 
            this.linkLabel_maker.AutoSize = true;
            this.linkLabel_maker.LinkArea = new System.Windows.Forms.LinkArea(0, 31);
            this.linkLabel_maker.Location = new System.Drawing.Point(6, 223);
            this.linkLabel_maker.Name = "linkLabel_maker";
            this.linkLabel_maker.Size = new System.Drawing.Size(222, 15);
            this.linkLabel_maker.TabIndex = 6;
            this.linkLabel_maker.TabStop = true;
            this.linkLabel_maker.Text = "http://blog.naver.com/neurowhai";
            this.linkLabel_maker.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_maker_LinkClicked);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 247);
            this.Controls.Add(this.linkLabel_maker);
            this.Controls.Add(this.comboBox_extTemplate);
            this.Controls.Add(this.button_count);
            this.Controls.Add(this.checkBox_deep);
            this.Controls.Add(this.textBox_extList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Line Counter";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.contextMenuStrip_extList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_extList;
        private System.Windows.Forms.CheckBox checkBox_deep;
        private System.Windows.Forms.Button button_count;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_find;
        private System.Windows.Forms.ComboBox comboBox_extTemplate;
        private System.Windows.Forms.LinkLabel linkLabel_maker;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_extList;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_saveExt;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_editExt;
    }
}

