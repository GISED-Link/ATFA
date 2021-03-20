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
            this.param = new FB_class.FB_Param_Valve_2_Pos(this.ParamFileName);
            this.Label = "FB_Valve_1_out";
            this.InputD.Add("xIn");
            this.InputD.Add("xSensorGS");
            this.InputD.Add("xSensorAS");
            this.OutputD.Add("xOut");
            this.OutputD.Add("xBusy");
            this.OutputD.Add("xError");

            // Must be called at the end of the configuration
            base.BuildView();
        }
    }
}
