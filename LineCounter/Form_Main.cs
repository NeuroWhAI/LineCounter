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

        /// <summary>
        /// * 템플릿 목록
        /// </summary>
        protected List<Template> m_templateList = new List<Template>();

        //#################################################################################################

        private void Form_Main_Load(object sender, EventArgs e)
        {
            try
            {
                // 템플릿 정의 파일을 엽니다.
                using (StreamReader sr = new StreamReader(new FileStream("Template.txt", FileMode.Open)))
                {
                    /// 현재 해석중인 템플릿
                    Template currentTemplate = null;

                    // 템플릿 파일을 다 읽을때까지 반복
                    while (!sr.EndOfStream)
                    {
                        // 현재 해석중인 템플릿이 존재하면
                        if (currentTemplate != null)
                        {
                            // 다음 패턴을 읽어옴
                            string ext = sr.ReadLine().TrimEnd();

                            // 읽은 패턴이 유효하면
                            if (ext != null && ext.Length > 0)
                            {
                                // 패턴 목록에 추가
                                currentTemplate.Exts.Add(ext);
                            }
                            else
                            {
                                // 유효하지 않으면

                                // 현재 템플릿의 해석을 중단
                                currentTemplate = null;
                            }
                        }
                        else
                        {
                            // 해석중인 템플릿이 존재하지 않으면

                            // 다음 문자를 읽음
                            var ch = sr.Read();

                            // 읽은 문자가 그룹토큰이면
                            if (ch == '$')
                            {
                                // 이어질 그룹 이름을 읽고
                                string name = sr.ReadLine().Trim();

                                // 해석할 템플릿을 새로 만들어 초기화 함
                                currentTemplate = new Template()
                                {
                                    Name = name,
                                };

                                // 템플릿 목록에도 추가
                                m_templateList.Add(currentTemplate);

                                // 선택 목록에 추가
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

        protected decimal CountLine(string folder, string pattern, bool deeper, HashSet<string> checkedList)
        {
            decimal line = 0;


            // 하위 폴더까지 검색할지 옵션
            SearchOption searchOption = ((deeper) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);


            // folder내에 존재하는 파일들에 대해서 반복
            foreach (string file in Directory.EnumerateFiles(folder, pattern, searchOption))
            {
                try
                {
                    // 이미 확인한 파일이 아니라면
                    if (!checkedList.Contains(file))
                    {
                        // 파일 열기
                        using (StreamReader sr = new StreamReader(new FileStream(file, FileMode.Open)))
                        {
                            // 확인목록에 추가
                            checkedList.Add(file);


                            // 마지막 줄은 개행문자가 없으므로 미리 카운트
                            ++line;

                            // 파일 끝까지 반복
                            while (!sr.EndOfStream)
                            {
                                // 개행 문자를 만나면
                                if (sr.Read() == '\n')
                                {
                                    // 카운트 증가
                                    ++line;
                                }
                            }
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
            }


            return line;
        }

        /// <summary>
        /// 패턴 목록을 나타내는 텍스트박스를 한줄씩 읽어주는 반복자 반환.
        /// </summary>
        /// <returns>패턴 반복자</returns>
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
            // 패턴 목록이 존재하면
            if (this.textBox_extList.TextLength > 0)
            {
                // 폴더를 선택하였으면
                if (this.folderBrowserDialog_find.ShowDialog() == DialogResult.OK)
                {
                    // 해당 폴더 경로을 얻고
                    string path = this.folderBrowserDialog_find.SelectedPath;


                    HashSet<string> checkedList = new HashSet<string>();
                    decimal totalLine = 0;

                    // 탐색 시작
                    foreach (string ext in GetExtBoxEnumerable())
                    {
                        totalLine += CountLine(path, ext, this.checkBox_deep.Checked, checkedList);
                    }


                    // 결과 표시
                    MessageBox.Show(totalLine.ToString(), "Line count", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("패턴 목록을 작성해주세요.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.textBox_extList.Focus();
            }
        }

        private void linkLabel_maker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel_maker.LinkVisited = true;

            System.Diagnostics.Process.Start("http://blog.naver.com/neurowhai/220697843695");
        }

        private void comboBox_extTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 템플릿 목록에서 선택한 번호 얻기
            int selectedIndex = this.comboBox_extTemplate.SelectedIndex;

            // 범위안의 유효한 번호라면
            if (selectedIndex >= 0 && selectedIndex < m_templateList.Count)
            {
                // 패턴 목록 입력상자를 비우고
                this.textBox_extList.Clear();

                // 선택한 번호의 템플릿이 가지는 패턴 목록을
                StringBuilder extText = new StringBuilder();

                foreach (var templt in m_templateList[selectedIndex].Exts)
                {
                    extText.AppendLine(templt);
                }

                // 입력상자에 설정
                this.textBox_extList.Text = extText.ToString();
            }
        }

        private void ToolStripMenuItem_saveExt_Click(object sender, EventArgs e)
        {
            // 입력한 템플릿 이름 얻기
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
                // 새로운 템플릿 생성후 초기화
                Template newTemplate = new Template()
                {
                    Name = extName,
                };


                // 템플릿 파일을 이어쓰기 모드로 염
                using (StreamWriter sw = new StreamWriter(new FileStream("Template.txt", FileMode.Append)))
                {
                    // 새로운 템플릿 정보를 파일에 씀과 동시에
                    sw.WriteLine();
                    sw.WriteLine("$ " + extName);

                    foreach (string ext in GetExtBoxEnumerable())
                    {
                        string trimmedExt = ext.TrimEnd();

                        sw.WriteLine(trimmedExt);

                        // 새로 만든 템플릿 인스턴스도 초기화
                        newTemplate.Exts.Add(trimmedExt);
                    }
                }


                // 템플릿 목록에 추가
                m_templateList.Add(newTemplate);

                // 선택 목록에 추가
                this.comboBox_extTemplate.Items.Add(extName);


                MessageBox.Show("추가되었습니다!", "Success!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToolStripMenuItem_editExt_Click(object sender, EventArgs e)
        {
            // 선택한 템플릿 이름 얻기
            string extName = this.comboBox_extTemplate.Text.Trim();


            Template editTemplt = null;


            // 선택한 템플릿 이름이 존재하면 얻음
            if (m_templateList.Any(delegate (Template templt) { return (editTemplt = templt).Name == extName; }))
            {
                // 기존 패턴 목록 리셋
                editTemplt.Exts.Clear();

                // 패턴 목록 입력상자로부터 패턴 목록을 다시 생성
                foreach (string ext in GetExtBoxEnumerable())
                {
                    editTemplt.Exts.Add(ext);
                }


                // 모든 패턴 목록을 다시 저장
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
