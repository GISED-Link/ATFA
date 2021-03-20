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
    class View_Param : View_InOut
    {
        public View_Param(string label) : base(label)
        {
            Label img_param = new();
            img_param.FontSize = 16;
            img_param.Content = "_{ }";

            img_param.VerticalAlignment = VerticalAlignment.Center;
            img_param.HorizontalAlignment = HorizontalAlignment.Left;
            this.Text.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.SetColumn(img_param, 0);
            Grid.SetColumn(this.Text, 1);

            InGrid.Children.Add(img_param);
        }
    }
}
