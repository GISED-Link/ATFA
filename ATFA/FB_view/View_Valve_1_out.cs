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
            this.InputD.Add(new View_Input("xGoGS"));
            this.InputD.Add(new View_Input("xGoAS"));
            this.InputD.Add(new View_Input("xSensorGS"));
            this.InputD.Add(new View_Input("xSensorAS"));
            this.OutputD.Add(new View_Output("xOutGS"));
            this.OutputD.Add(new View_Output("xOutAS"));
            this.OutputD.Add(new View_Output("xBusy"));
            this.OutputD.Add(new View_Output("xError"));

            // Must be called at the end of the configuration
            base.BuildView();
        }
    }
}
