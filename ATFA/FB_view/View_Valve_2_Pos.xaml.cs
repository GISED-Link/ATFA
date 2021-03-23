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

        [JsonProperty]
        public string FB_Name { get; set; }

        [JsonIgnore]
        private static int id_count = 0;
        [JsonIgnore]
        public int id = id_count++;

        /// <value> input direct list </value>
        [JsonProperty]
        public List<View_Input> InputD = new();
        /// <value> output direct list </value>
        [JsonProperty]
        public List<View_Output> OutputD = new();

        [JsonProperty]
        public string ParamFileName = "";

        /// <value> Parameter of a FB_Valve_2_Pos</value>
        [JsonIgnore] 
        public Type param_type;

        public View_Valve_2_Pos()
        {
            InitializeComponent();
        }

        public void BuildView()
        {
            FbName.Text = FB_Name;

            int row = 1;
            foreach (View_Input input in this.InputD)
            {
                AddInOutView(input, row, 1, View_InOut.Dir.In);
                ((TextBox)input.entry_box).Text = input.link_value;
                row++;
            }
            // we re-use row here to place the config at the last line of the input side
            View_Param viewParam = new ("Parameter file");
            AddInOutView(viewParam, row, 1, View_InOut.Dir.Param);
            ((ComboBox)viewParam.entry_box).ItemsSource = Manager.Project_Explorer.GetParamList();
            ((ComboBox)viewParam.entry_box).SelectedItem = Manager.Project_Explorer.GetParamByName(ParamFileName);

            row = 1;
            foreach (View_Output output in this.OutputD)
            {
                AddInOutView(output, row, 2, View_InOut.Dir.Out);
                ((TextBox)output.entry_box).Text = output.link_value;
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
            if (View_InOut.Dir.Out == dir)
            {
                Grid.SetColumn(uc.entry_box, col + 1);
            }
            else
            {
                Grid.SetColumn(uc.entry_box, col - 1);
            }

            fb_bloc.Children.Add(uc);
            Grid.SetRow(uc, row);
            Grid.SetColumn(uc, col);

            fb_bloc.Children.Add(uc.entry_box);
            Grid.SetRow(uc.entry_box, row);
        }

        /// <summary>
        /// Save in json format the class
        /// </summary>
        /// <returns>0 when success, -1 when failed</returns>
        public int Save(string path_name)
        {
            return Manager.Tool.SaveJSON(path_name, this.FB_Name, this);
        }

        public static View_Valve_2_Pos Open(string path_name, string name)
        {
            View_Valve_2_Pos ret = null;

            try
            {
                FileStream fs = File.Open(path_name + name, System.IO.FileMode.Open);

                Byte[] json_data = new byte[fs.Length];
                fs.Read(json_data, 0, json_data.Length);

                string input = Encoding.UTF8.GetString(json_data, 0, json_data.Length);

                ret = JsonConvert.DeserializeObject<View_Valve_2_Pos>(input);

                if (ret != null)
                { 
                    ret.BuildView();
                }

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
            // return this.param.ReadParamJson();
            return "";
        }

        public string ReadFBJson(string path_name)
        {
            string ret = "";
            try
            {
                FileStream fs = System.IO.File.Open(path_name + this.FB_Name + ".json", System.IO.FileMode.Open);
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

        public int GetId()
        {
            return this.id;
        }

        private void SaveNewName(object sender, TextChangedEventArgs e)
        {
            FB_Name = ((TextBox)sender).Text;
        }
    }
}
