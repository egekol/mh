// 20042023

using System.Collections.Generic;

namespace Utilities
{
    public static class Extensions
    {
        public static float Map(float value, float min1, float max1, float min2, float max2)
        {
            return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
        }
        public static T Mod<T>(this IList<T> list,int index)
        {
            return list[index%list.Count];
        }
        public static T ClampIndex<T>(this IList<T> list,int index)
        {
            if (index<0)
            {
                index = 0;
            }
            if (index>=list.Count)
            {
                return list[^1];
            }
            return list[index];
        }
    }
}