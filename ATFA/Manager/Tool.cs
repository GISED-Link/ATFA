using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATFA.Manager
{
    class Tool
    {
        public static int SaveJSON(string path, string fileName, object obj)
        {
            string output = JsonConvert.SerializeObject(obj, Formatting.Indented);
            int ret = -1;

            try
            {
                FileStream fs = System.IO.File.Open(path + fileName + ".json", System.IO.FileMode.Create);

                Byte[] json_data = new UTF8Encoding(true).GetBytes(output);
                fs.Write(json_data, 0, json_data.Length);

                fs.Close();

                ret = 0;
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return ret;
        }
    }
}
