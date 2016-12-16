namespace EasyDapper.Infrastructure
{
    public class Guard
    {
        public static void EnsureArgementNotNull(object argument, string message = null)
        {
            if(argument == null)
            {
                message = message ?? nameof(argument);
                throw new System.ArgumentNullException(message);
            }
        }
    }
}
