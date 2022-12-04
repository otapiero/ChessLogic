using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace ChessLogic
{
    public static class Cloning
    {
        static public T Clone<T>(this T original) where T : new()
        {

            T copyToObject = new T();

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);
            return copyToObject;
        }

        public static void Mover<T>(this T src, T dst)
        {
            foreach (var pi in typeof(T).GetProperties())
            {
                pi.SetValue(dst, pi.GetValue(src, null));
            }
    }
    }
}
