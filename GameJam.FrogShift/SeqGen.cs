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

        public List<int> GenerateSequence(int Num)
        {
            intList = new List<int>();
            int max = 0;
            Random rnd = new Random();
            for (int i = 0; i < Num;i++) {
                int num = rnd.Next(1, 5);
                max += num;
                intList.Add(max);
            }
            return intList;
        }
    }
}
