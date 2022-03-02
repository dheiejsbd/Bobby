using UnityEngine;

namespace Bobby
{
    public class RandomPattern
    {
        int[] percent = new int[3] { 30, 30, 40 }; 
        int RandomPatternID()
        {
             int R=Random.Range(0, percent[0]+percent[1]+percent[2]);
            if (R<percent[0])
            {
                return 1;
            }
            if (R < percent[1])
            {
                return 2;
            }
            
                return 3;
        }

    }
}

  