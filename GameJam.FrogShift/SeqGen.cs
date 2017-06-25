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
            int prev = 0;
            int temp = 0;
            int cnt = 0;
            int n = 0;
            Random rnd = new Random();
            for (int i = 0; i < Num + n; i++)
            {
                int num = rnd.Next(1, 5);
                temp = num;
                if ((Math.Abs(temp - prev)) <= 1) { cnt++; }
                else cnt = 0;
                if (cnt < 2)
                {
                    max += num;
                    intList.Add(max);
                }
                else n++;

            }
            return intList;
        }
        public List<int> GenerateArtIndexSequence(int Num, int maxVal)
        {
            List<int> indexList = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < Num; i++)
            {
                int num = rnd.Next(0, maxVal);
                indexList.Add(num);
            }
            return indexList;
        }
    }
}
