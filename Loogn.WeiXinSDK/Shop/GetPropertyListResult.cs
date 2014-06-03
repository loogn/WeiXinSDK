using System;
using System.Collections.Generic;
using System.Text;

namespace Loogn.WeiXinSDK.Shop
{
    public class GetPropertyListResult:ReturnCode
    {
        public List<Property_Table> properties { get; set; }

        public class Property_Table : ID_Name
        {
            public List<ID_Name> property_value { get; set; }
        }
    }
}
