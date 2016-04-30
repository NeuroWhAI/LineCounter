using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LineCounter
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        //#################################################################################################

        protected List<Template> m_templateList = new List<Template>();

        //#################################################################################################

        private void Form_Main_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream("Template.txt", FileMode.Open)))
                {
                    Template currentTemplate = null;

                    while (!sr.EndOfStream)
                    {
                        if (currentTemplate != null)
                        {
                            string ext = sr.ReadLine().TrimEnd();

                            if (ext != null && ext.Length > 0)
                            {
                                currentTemplate.Exts.Add(ext);
                            }
                            else
                            {
                                currentTemplate = null;
                            }
                        }
                        else
                        {
                            var ch = sr.Read();

                            if (ch == '$')
                            {
                                string name = sr.ReadLine().Trim();

                                currentTemplate = new Template()
                                {
                                    Name = name,
                                };

                                m_templateList.Add(currentTemplate);

                                this.comboBox_extTemplate.Items.Add(name);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                this.textBox_extList.Focus();
            }
            catch (IOException)
            {
                MessageBox.Show("템플릿을 불러오는데 실패하였습니다.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected decimal CountLine(string folder, string pattern, bool deeper)
        {
            decimal line = 0;


            foreach (string file in Directory.EnumerateFiles(folder, pattern))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(new FileStream(file, FileMode.Open)))
                    {
                        while (!sr.EndOfStream)
                        {
                            if (sr.Read() == '\n')
                            {
                                ++line;
                            }
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
            }


            if (deeper)
            {
                foreach (string innerFolder in Directory.EnumerateDirectories(folder))
                {
                    line += CountLine(innerFolder, pattern, deeper);
                }
            }


            return line;
        }

        protected IEnumerable<string> GetExtBoxEnumerable()
        {
            using (StringReader sr = new StringReader(this.textBox_extList.Text))
            {
                string ext = string.Empty;
                do
                {
                    ext = sr.ReadLine();

                    if (ext != null)
                    {
                        yield return ext;
                    }
                    else
                    {
                        break;
                    }
                } while (ext != string.Empty && ext != null);
            }
        }

        private void button_count_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog_find.ShowDialog() == DialogResult.OK)
            {
                string path = this.folderBrowserDialog_find.SelectedPath;


                decimal totalLine = 0;

                foreach (string ext in GetExtBoxEnumerable())
                {
                    totalLine += CountLine(path, ext, this.checkBox_deep.Checked);
                }


                MessageBox.Show(totalLine.ToString(), "Line count", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel_maker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel_maker.LinkVisited = true;

            System.Diagnostics.Process.Start(this.linkLabel_maker.Text);
        }

        private void comboBox_extTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.comboBox_extTemplate.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < m_templateList.Count)
            {
                this.textBox_extList.Clear();

                StringBuilder extText = new StringBuilder();

                foreach (var templt in m_templateList[selectedIndex].Exts)
                {
                    extText.AppendLine(templt);
                }

                this.textBox_extList.Text = extText.ToString();
            }
        }

        private void ToolStripMenuItem_saveExt_Click(object sender, EventArgs e)
        {
            string extName = this.comboBox_extTemplate.Text.Trim();


            if (extName.Length <= 0)
            {
                MessageBox.Show("이름이 너무 짧습니다.",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.comboBox_extTemplate.Focus();
                this.comboBox_extTemplate.SelectAll();
            }
            else if (m_templateList.Any(delegate (Template templt) { return templt.Name == extName; }))
            {
                MessageBox.Show("템플릿 이름이 중복됩니다.\n템플릿 선택 상자에 새로운 이름을 넣어주세요.",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.comboBox_extTemplate.Focus();
                this.comboBox_extTemplate.SelectAll();
            }
            else
            {
                Template newTemplate = new Template()
                {
                    Name = extName,
                };


                using (StreamWriter sw = new StreamWriter(new FileStream("Template.txt", FileMode.Append)))
                {
                    sw.WriteLine();
                    sw.WriteLine("$ " + extName);

                    foreach (string ext in GetExtBoxEnumerable())
                    {
                        string trimmedExt = ext.TrimEnd();

                        sw.WriteLine(trimmedExt);

                        newTemplate.Exts.Add(trimmedExt);
                    }
                }


                m_templateList.Add(newTemplate);

                this.comboBox_extTemplate.Items.Add(extName);


                MessageBox.Show("추가되었습니다!", "Success!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToolStripMenuItem_editExt_Click(object sender, EventArgs e)
        {
            string extName = this.comboBox_extTemplate.Text.Trim();


            Template editTemplt = null;


            if (m_templateList.Any(delegate (Template templt) { return (editTemplt = templt).Name == extName; }))
            {
                editTemplt.Exts.Clear();

                foreach (string ext in GetExtBoxEnumerable())
                {
                    editTemplt.Exts.Add(ext);
                }


                using (StreamWriter sw = new StreamWriter(new FileStream("Template.txt", FileMode.Create)))
                {
                    foreach (var templt in m_templateList)
                    {
                        templt.WriteToStream(sw);
                    }
                }


                MessageBox.Show("수정되었습니다!", "Success!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("해당 이름을 가진 템플릿이 없습니다.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox_extList_DoubleClick(object sender, EventArgs e)
        {
            this.textBox_extList.SelectAll();
        }
    }
}
