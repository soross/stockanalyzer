using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IntelliName.UI
{
    // 首先当成数字比较，如果不对，当作字符比较 
    class NumberComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            int xVal;
            int yVal;
            if (Int32.TryParse(x.ToString(), out xVal) && Int32.TryParse(y.ToString(), out yVal))
            {
                if (xVal > yVal)
                {
                    return 1;
                }
                else if (xVal < yVal)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                // 字符串比较
                return (new CaseInsensitiveComparer()).Compare(x, y);
            }
        }
    }
}
