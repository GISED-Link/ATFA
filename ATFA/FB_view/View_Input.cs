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
    public class View_Input : View_InOut
    {
        public View_Input(string label) : base(label)
        {
            Rectangle rect = new();
            rect.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            rect.Height = 5;
            rect.Width = 25;

            rect.VerticalAlignment = VerticalAlignment.Center;
            rect.HorizontalAlignment = HorizontalAlignment.Left;
            this.Text.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.SetColumn(rect, 0);
            Grid.SetColumn(this.Text, 1);

            InGrid.Children.Add(rect);
        }
    }
}
