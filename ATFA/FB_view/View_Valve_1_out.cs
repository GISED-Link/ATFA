using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATFA.FB_view
{
    class View_Valve_1_out : View_Valve_2_Pos
    {
        public View_Valve_1_out() : base()
        {
            // TODO should get default param 
            this.ParamFileName = "param_1";
            this.param_type = typeof(FB_class.FB_Param_Valve_2_Pos);
            this.FB_Name = "FB_Valve_1_out";
            this.InputD.Add("xGoGS");
            this.InputD.Add("xGoAS");
            this.InputD.Add("xSensorGS");
            this.InputD.Add("xSensorAS");
            this.OutputD.Add("xOutGS");
            this.OutputD.Add("xOutAS");
            this.OutputD.Add("xBusy");
            this.OutputD.Add("xError");

            // Must be called at the end of the configuration
            base.BuildView();
        }
    }
}
