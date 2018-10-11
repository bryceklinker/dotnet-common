namespace Klinked.Cqrs.Logging.Common
{
    internal static class ArgumentsNameResolver
    {
        public static string GetName<TArgs>()
        {
            return typeof(TArgs).Name;
        }
    }
}