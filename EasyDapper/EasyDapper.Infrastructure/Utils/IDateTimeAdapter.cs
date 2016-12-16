using System;
namespace EasyDapper.Infrastructure.Utils
{

    public interface IDateTimeAdapter
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
