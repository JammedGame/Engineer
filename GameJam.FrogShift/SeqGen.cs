using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.FrogShift
{
    class SeqGen
    {
        private List<int> intList;        
        private int diff;

        public SeqGen()
        {
            diff = 0;
            intList = null;
        }

        public List<int> GenerateSequence()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100;i++) {
                intList.Add(rnd.Next(1,5));
            }
            return intList;
        }
    }
}
