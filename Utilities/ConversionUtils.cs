namespace Utilities
{
    public static class ConversionUtils
    {
        public static T Convert<T>(string value)
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public static T Convert<T>(object value)
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }
    }
}