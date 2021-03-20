using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATFA.FB_class
{
    public class FB_Param_Valve_2_Pos
    {

        const string pathname = "D:/Projets/wpf/ATFA/ATFA/data/FB_param/";

        enum ValvePos
        {
            In,         // Sensor should be high when the piston is full retracted
            Out,        // Sensor should be high when the piston is full out
            Middle,     // Sensor should be high when the piston is blocked in the middle of
                        // its way due to a mechanicle obstacle (should detect a presence)
            Undefined,  // Unknown position
            Max
        }

        [JsonProperty]
        public string Label { get; set; }
        [JsonProperty]
        private bool HasIn_Sensor { get; }
        [JsonProperty]
        private bool HasOut_Sensor { get; }
        [JsonProperty]
        private bool HasMiddle_Sensor { get; }
        [JsonProperty]
        private ValvePos GS_Pos { get; }
        
        public FB_Param_Valve_2_Pos(string label, FB_Param_Valve_2_Pos param)
        {
            this.Label = label;
            this.HasIn_Sensor = param.HasIn_Sensor;
            this.HasOut_Sensor = param.HasOut_Sensor;
            this.HasMiddle_Sensor = param.HasMiddle_Sensor;
            this.GS_Pos = param.GS_Pos;
        }
        public FB_Param_Valve_2_Pos(string label)
        {
            this.Label = label;
            this.HasIn_Sensor = true;
            this.HasOut_Sensor = true;
            this.HasMiddle_Sensor = false;
            this.GS_Pos = ValvePos.In;

            Manager.Tool.SaveJSON(pathname, this.Label, this);
        }

        public string ReadParamJson()
        {
            string ret = "";
            try
            {
                FileStream fs = System.IO.File.Open(pathname + this.Label + ".json", System.IO.FileMode.Open);
                Byte[] json_data = new byte[fs.Length];
                fs.Read(json_data, 0, json_data.Length);

                ret = Encoding.UTF8.GetString(json_data, 0, json_data.Length);

                fs.Close();
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return ret;
        }

        public void Delete()
        {
            string file_name = FB_Param_Valve_2_Pos.pathname + this.Label + ".json";
            System.IO.File.Delete(file_name);
        }

    }
}
