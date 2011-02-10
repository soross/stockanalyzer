using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Portfolio
{
    interface Commentable
    {
        void setComment(String comment);
        String getComment();
    }
}
