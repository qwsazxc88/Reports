﻿namespace Reports.Core.Domain
{
    /// <summary>
    /// Отношения "начальник-подчиненный"?
    /// </summary>
    public class ChiefToUser : AbstractEntity 
    {
        public virtual User Chief { get; set; }
        public virtual User User { get; set; }
    }
}
