using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LineCounter
{
    public class Template
    {
        public string Name
        { get; set; }

        public List<string> Exts
        { get; set; } = new List<string>();


        public void WriteToStream(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("$ " + this.Name);

            foreach (string ext in this.Exts)
            {
                sw.WriteLine(ext);
            }
        }
    }
}
