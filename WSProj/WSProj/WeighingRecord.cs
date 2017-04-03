using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSProj
{
    public class WeighingRecord
    {

        public string SerialNumber;

        public float StartingWeight;

        public float EndingWeight;

        public WeighingRecord(string serial, float startingWeight, float endingWeight)
        {
            SerialNumber = serial;
            StartingWeight = startingWeight;
            EndingWeight = endingWeight;
        }
        
        //Requierd for XML serialization
        public WeighingRecord()
        {

        }



    }
}
