using System;
using System.Collections.Generic;
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

namespace ATFA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int i = 0;
        int lastElement_id = -1;

        SolidColorBrush COLOR_GREY_UNSEL = new BrushConverter().ConvertFromString("#CCCCCC") as SolidColorBrush;
        SolidColorBrush COLOR_GREY_SEL = new BrushConverter().ConvertFromString("#AAAAAA") as SolidColorBrush;

        public MainWindow()
        {
            InitializeComponent();

            var param_tv = new TreeViewItem();
            param_tv.Header = "Param files";
            Manager.Project_Explorer.LoadParamFiles(param_tv);
            ParamTv.Items.Add(param_tv);

            foreach(var fb in Manager.Project_Explorer.GetFB_Files())
            {
                AddNewFB(fb);
            }
        }

        private void AddNewFB(FB_view.View_Valve_2_Pos fb)
        {
            fb.bp.Click += new RoutedEventHandler(setSelectedElement);
            i++;
            valveView.Children.Add(fb);
        }

        private void addNewElement(object sender, RoutedEventArgs e)
        {
            var fb = new FB_view.View_Valve_1_out();
            AddNewFB(fb);
        }

        private void removeElement(object sender, RoutedEventArgs e)
        {
            var last_valve = GetValveById(lastElement_id);
            if (null != last_valve)
            {
                valveView.Children.Remove(last_valve);
                lastElement_id = -1;
            }
        }

        private void setSelectedElement(object sender, RoutedEventArgs e)
        {
            FB_view.View_Valve_2_Pos fb_valve = GetValveById(((FB_view.View_Valve_2_Pos)((Grid)((Button)sender).Parent).Parent).GetId());
            if (null != fb_valve)
            {
                if (fb_valve.GetId() == lastElement_id)
                {
                    fb_valve.rect.Fill = COLOR_GREY_UNSEL;
                    lastElement_id = -1;
                }
                else
                {
                    var last_valve = GetValveById(lastElement_id);
                    if (null != last_valve)
                    {
                        last_valve.rect.Fill = COLOR_GREY_UNSEL;
                    }
                    fb_valve.rect.Fill = COLOR_GREY_SEL;
                    lastElement_id = fb_valve.GetId();

                    fb_json.Text = fb_valve.ReadFBJson();
                    param_json.Text = fb_valve.ReadParamJson();
                }
            }
        }

        private FB_view.View_Valve_2_Pos GetValveById(int id)
        {
            foreach(var valve in valveView.Children)
            {
                if (valve is FB_view.View_Valve_2_Pos)
                {
                    if (id == ((FB_view.View_Valve_2_Pos)valve).GetId())
                    {
                        return valve as FB_view.View_Valve_2_Pos;
                    }
                }
            }
            return null;
        }
    }
}
