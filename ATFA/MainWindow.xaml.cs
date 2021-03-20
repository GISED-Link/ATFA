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
        }

        private void addNewElement(object sender, RoutedEventArgs e)
        {
            var valve_class = new FB_view.View_Valve_1_out();
            valve_class.Save();
            valve_class.bp.Click += new RoutedEventHandler(setSelectedElement);
            i++;
            valveView.Children.Add(valve_class);
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
            FB_view.View_Valve_1_out fb_valve = GetValveById(((FB_view.View_Valve_1_out)((Grid)((Button)sender).Parent).Parent).GetId());
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

        private FB_view.View_Valve_1_out GetValveById(int id)
        {
            foreach(var valve in valveView.Children)
            {
                if (valve is FB_view.View_Valve_1_out)
                {
                    if (id == ((FB_view.View_Valve_1_out)valve).GetId())
                    {
                        return valve as FB_view.View_Valve_1_out;
                    }
                }
            }
            return null;
        }
    }
}
