using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCommonTools
{
    public static class ListHelper
    {
        public static void Swap<T>(List<T> list, int indexA, int indexB)
        {
            // 确保索引在范围内
            if (indexA >= 0 && indexB >= 0 && indexA < list.Count && indexB < list.Count)
            {
                T temp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = temp;
            }
        }
    }
}
