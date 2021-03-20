using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATFA.FB_view
{
    /// <summary>
    /// Logique d'interaction pour View_Valve_2_Pos.xaml
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class View_Valve_2_Pos : UserControl
    {
        const string pathname = "D:/Projets/wpf/ATFA/ATFA/data/FB_def/";

        [JsonProperty]
        public string Label { get; set; }

        [JsonIgnore]
        private static int id_count = 0;
        [JsonIgnore] 
        public int id = id_count++;

        /// <value> input direct list </value>
        [JsonProperty]
        public List<string> InputD = new();
        /// <value> output direct list </value>
        [JsonProperty]
        public List<string> OutputD = new();

        [JsonProperty]
        public string ParamFileName = "";

        /// <value> Parameter of a FB_Valve_2_Pos</value>
        [JsonIgnore] 
        public FB_class.FB_Param_Valve_2_Pos param;

        public View_Valve_2_Pos()
        {
            InitializeComponent();
        }

        public void BuildView()
        {
            int row = 1;
            foreach (string input in this.InputD)
            {
                AddInOutView(new View_Input(input), row, 1, View_InOut.Dir.In);
                row++;
            }

            // we re-use row here to place the config at the last line of the input side
            AddInOutView(new View_Param(this.param.Label), row, 1, View_InOut.Dir.Param);

            row = 1;
            foreach (string output in this.OutputD)
            {
                AddInOutView(new View_Output(output), row, 2, View_InOut.Dir.Out);
                row++;
            }
        }

        /// <summary>
        /// Automated add View_InOut element in FB_Valve
        /// </summary>
        /// <param name="uc">the newly created user control</param>
        /// <param name="row">row of the element in the FB</param>
        /// <param name="col">column of the uc element</param>
        /// <param name="dir">inform if we are at the input our output side. The goal 
        ///                   is to invert the combobox and the View_InOut element</param>
        private void AddInOutView(View_InOut uc, int row, int col, View_InOut.Dir dir)
        {
            var view_enum = new ComboBox();

            fb_bloc.Children.Add(uc);
            Grid.SetRow(uc, row);
            Grid.SetColumn(uc, col);

            fb_bloc.Children.Add(view_enum);
            Grid.SetRow(view_enum, row);
            if (View_InOut.Dir.Out == dir)
            {
                Grid.SetColumn(view_enum, col + 1);
            }
            else
            {
                Grid.SetColumn(view_enum, col - 1);
            }
        }

        /// <summary>
        /// Save in json format the class
        /// </summary>
        /// <returns>0 when success, -1 when failed</returns>
        public int Save()
        {
            return Manager.Tool.SaveJSON(pathname, this.Label, this);
        }

        public View_Valve_2_Pos Open(string name)
        {
            View_Valve_2_Pos ret = null;

            try
            {
                FileStream fs = File.Open(pathname + name + ".json", System.IO.FileMode.Open);

                Byte[] json_data = new byte[fs.Length];
                fs.Read(json_data, 0, json_data.Length);

                string input = Encoding.UTF8.GetString(json_data, 0, json_data.Length);

                ret = JsonConvert.DeserializeObject<View_Valve_2_Pos>(input);

                fs.Close();
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return ret;
        }

        public string ReadParamJson()
        {
            return this.param.ReadParamJson();
        }

        public string ReadFBJson()
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

        public View_Valve_2_Pos Reload()
        {
            return this.Open(this.Label);
        }

        public int GetId()
        {
            return this.id;
        }
    }
}
