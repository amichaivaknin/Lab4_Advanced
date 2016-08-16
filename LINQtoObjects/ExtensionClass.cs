using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQtoObjects
{
    internal static class ExtensionClass
    {

        public static void CopyTo(this object from1, object to)
        {
            (from obj in from1.GetType().GetProperties()
             where obj.CanRead
                from obj2 in to.GetType().GetProperties()
                where obj.Name==obj2.Name && obj2.CanWrite
             select new
             {
                 fromObj=obj,
                 toObj=obj2
             }).ToList().ForEach(x=>x.toObj.SetValue(to, x.fromObj.GetValue(from1)));
                
                
        }
    }
}
