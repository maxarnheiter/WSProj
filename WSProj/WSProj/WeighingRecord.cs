using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProj
{
    public class WeighingRecord
    {

        public string SerialNumber
        {
            get { return _serialNumber; }
        }
        string _serialNumber;

        public float StartingWeight
        {
            get { return _startingWeight; }
        }
        float _startingWeight;

        public float EndingWeight
        {
            get { return _endingWeight; }
        }
        float _endingWeight;

        public WeighingRecord(string serial, float startingWeight, float endingWeight)
        {
            _serialNumber = serial;
            _startingWeight = startingWeight;
            _endingWeight = endingWeight;
        }
        



    }
}
