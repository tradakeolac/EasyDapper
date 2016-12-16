using System;

namespace EasyDapper.Infrastructure.Utils
{

    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}