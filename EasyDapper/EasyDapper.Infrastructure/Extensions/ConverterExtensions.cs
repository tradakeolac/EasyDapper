namespace EasyDapper.Infrastructure.Extensions
{
    using System;

    public static class ConverterExtensions
    {
        public static int ConvertToInteger(this string source)
        {
            int result;

            int.TryParse(source, out result);

            return result;
        }

        public static float ConvertToFloat(this string source)
        {
            float result;

            float.TryParse(source, out result);

            return result;
        }

        public static bool ConvertToBool(this string source)
        {
            bool result;

            bool.TryParse(source, out result);

            return result;
        }

        public static T To<T>(this object src)
        {
            T value;
            try
            {
                value = (T)Convert.ChangeType(src, typeof(T));
            }
            catch
            {
                value = default(T);
            }

            return value;
        }
    }
}
