using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace karma
{
    public static class Logger
    {
        public static void WriteLog(string s)
        {
            string path = "E:/log.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"{DateTime.Now} : {s}");
            }
        }
    }
}
