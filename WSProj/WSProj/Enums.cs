using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProj
{
    public enum CommandType
    {
        ReadLiteralValue,
        ReadFinalValue,
        WriteFinalValue
    }

    public enum RegisterType
    {
        GrossWeight,
        NetWeight,
        SetpointType,
        SetpointSource,
        SetpointTarget,
        KeyPress,
        SystemStatus,
        SystemError
    }
}
