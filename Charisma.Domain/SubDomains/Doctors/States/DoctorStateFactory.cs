﻿using System;
using Charisma.Domain.SubDomains.Doctors.States.Abstracts;

namespace Charisma.Domain.SubDomains.Doctors.States
{
    public class DoctorStateFactory
    {
        public static IDoctorState Make(DoctorSpeciality speciality) => speciality switch
        {
            DoctorSpeciality.General => new GeneralDoctorState(),
            DoctorSpeciality.Specialist => new SpecialistDoctorState(),
            DoctorSpeciality.SubSpecialty => new SubSpecialtyDoctorState(),
            _ => throw new ArgumentOutOfRangeException(nameof(speciality), speciality, null)
        };
    }
}