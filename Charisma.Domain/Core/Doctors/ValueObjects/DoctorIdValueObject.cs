﻿using System;
using System.Collections.Generic;
using Charisma.Domain.GenericCore.Abstractions;

namespace Charisma.Domain.Core.Doctors.ValueObjects
{
    public class DoctorIdValueObject : ValueObject
    {
        private readonly Guid _doctorId;
        public Guid Value => _doctorId;
        private DoctorIdValueObject(Guid doctorId)
        {
            _doctorId = doctorId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static DoctorIdValueObject New(Guid doctorId) => new(doctorId);
    }
}