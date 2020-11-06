using System;
using System.Collections.Generic;
using System.Text;

namespace FbiInquiry.DataModel
{
    public static class ObjectFactory
    {
        public static T GetInstance<T>(object ctx)
        {
            return (T)ctx;
        }
    }
}
