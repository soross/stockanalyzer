using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class Code
    {
        private Code(String code)
        {
            this.code = code;
        }

        public static Code newInstance(String code)
        {
            if (code == null)
            {
                throw new ArgumentException("code cannot be null");
            }

            return new Code(code);
        }

        public String toString()
        {
            return code;
        }

        private String code;
    }
}
