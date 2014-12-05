using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Помощь - версия
    /// </summary>
    public class HelpVersion : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime ReleaseDate { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}