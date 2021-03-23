using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ATFA.FB_view
{
    public class View_Param : View_InOut
    {
        public View_Param(string label) : base(label)
        {
            Label img_param = new();
            img_param.FontSize = 16;
            img_param.Content = "_{ }";

            img_param.VerticalAlignment = VerticalAlignment.Center;
            img_param.HorizontalAlignment = HorizontalAlignment.Left;
            this.InOutLabel.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.SetColumn(img_param, 0);
            Grid.SetColumn(this.InOutLabel, 1);

            InGrid.Children.Add(img_param);

            this.entry_box = new ComboBox();
            ((ComboBox)this.entry_box).SelectionChanged += SetLink;
        }

        public void SetLink(object sender, SelectionChangedEventArgs e)
        {
            this.link_value = ((FB_class.FB_Param_Valve_2_Pos)((ComboBox)sender).SelectedValue).Label;
        }
    }
}
