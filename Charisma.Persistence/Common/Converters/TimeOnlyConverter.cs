﻿using System;
using Charisma.Persistence.Common.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Charisma.Persistence.Common.Converters
{
    internal sealed class TimeOnlyConverter : ValueConverter<TimeOnly, DateTime>
    {
        public TimeOnlyConverter() : base(timeOnly => timeOnly.ToDateTime(),
            dateTime => TimeOnly.FromDateTime(dateTime))
        {
        }
    }
}