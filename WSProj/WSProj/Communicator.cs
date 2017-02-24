using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace WSProj
{
    public class Communicator
    {
        SerialPort _serialPort;

        public Communicator(SerialPort serialPort)
        {
            _serialPort = serialPort;

            Debug.Log("Communicator initialized");
        }
        
        public void SendData(string address, CommandType commandType, RegisterType registerType, string optionalParameters)
        {
            string command = address + GetCommandValue(commandType) + GetRegisterValue(registerType) + ":" + optionalParameters;

            Debug.Log("Sending command: " + command);

            _serialPort.WriteLine(command);
        }

        string GetCommandValue(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.ReadLiteralValue:
                    return "05";
                case CommandType.ReadFinalValue:
                    return "11";
                case CommandType.WriteFinalValue:
                    return "12";
            }

            return "";
        }

        string GetRegisterValue(RegisterType registerType)
        {
            switch (registerType)
            {
                case RegisterType.GrossWeight:
                    return "0026";
                case RegisterType.NetWeight:
                    return "0027";
                case RegisterType.SetpointType:
                    return "0170";
                case RegisterType.SetpointSource:
                    return "0171";
                case RegisterType.SetpointTarget:
                    return "0172";
                case RegisterType.KeyPress:
                    return "0008";
                case RegisterType.SystemStatus:
                    return "0021";
                case RegisterType.SystemError:
                    return "0022";
            }

            return "";
        }
    }
}
