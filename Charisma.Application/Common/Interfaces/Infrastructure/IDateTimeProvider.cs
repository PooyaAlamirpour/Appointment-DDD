using System;

namespace Charisma.Application.Common.Interfaces.Infrastructure
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateOnly Today { get; }
    }
}