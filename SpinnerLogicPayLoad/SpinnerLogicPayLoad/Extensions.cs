using System;
using System.Collections.Generic;
using System.Linq;

namespace SpinnerLogicPayLoad
{
    public static class Extensions
    {        
        //http://csharp.2000things.com/2010/11/07/143-an-example-of-implementing-icloneable-for-deep-copies/
        
        //http://stackoverflow.com/a/222640/139698
        //http://stackoverflow.com/a/6632401/139698


        public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }
    } 
}
