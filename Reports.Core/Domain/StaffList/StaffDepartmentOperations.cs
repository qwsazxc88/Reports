﻿using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник операций подразделений.
    /// </summary>
    public class StaffDepartmentOperations : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}