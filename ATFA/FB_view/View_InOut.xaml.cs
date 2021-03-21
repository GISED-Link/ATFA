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

namespace ATFA.FB_view
{
    /// <summary>
    /// Logique d'interaction pour View_InOut.xaml
    /// </summary>
    public partial class View_InOut : UserControl
    {
        public enum Dir
        {
            In,
            Param,
            Out,
            Max
        }

        public UIElement entry_box;

        public View_InOut(string label)
        {
            InitializeComponent();
            Text.Content = label;
        }
    }
}
