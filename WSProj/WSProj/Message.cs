using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProj
{
    public class Message
    {

        public string Address
        { get { return _address; } }
        string _address;

        public CommandType CommandType
        { get { return _commandTye; } }
        CommandType _commandTye;

        public RegisterType RegisterType
        { get { return _registerType; } }
        RegisterType _registerType;

        public string Parameters
        { get { return _parameters; } }
        string _parameters;

        public Message(string address, CommandType commandType, RegisterType registerType, string parameters)
        {
            _address = address;
            _commandTye = commandType;
            _registerType = registerType;
            _parameters = parameters;
        }

        public Message(string rawMessage)
        {
            _address = rawMessage.Substring(0, 2);
            _commandTye = GetCommandType(rawMessage.Substring(2, 2));
            _registerType = GetRegisterType(rawMessage.Substring(4, 4));
            _parameters = rawMessage.Substring(9);
        }

        public override string ToString()
        {
            string message = "";

            message += _address;
            message += GetCommandValue(_commandTye);
            message += GetRegisterValue(_registerType);
            message += ":";
            message += _parameters;

            return message;
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

        CommandType GetCommandType(string text)
        {
            if (text == "05")
                return CommandType.ReadLiteralValue;
            else if (text == "11")
                return CommandType.ReadFinalValue;
            else
                return CommandType.WriteFinalValue;
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

        RegisterType GetRegisterType(string text)
        {
            if (text == "0026")
                return RegisterType.GrossWeight;
            else if (text == "0027")
                return RegisterType.NetWeight;
            else if (text == "0170")
                return RegisterType.SetpointType;
            else if (text == "0171")
                return RegisterType.SetpointSource;
            else if (text == "0172")
                return RegisterType.SetpointTarget;
            else if (text == "0008")
                return RegisterType.KeyPress;
            else if (text == "0021")
                return RegisterType.SystemStatus;
            else
                return RegisterType.SystemError;
        }

    }
}
