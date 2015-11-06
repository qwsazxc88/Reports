using System;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник разделов приложения.
    /// </summary>
    public class PartialTypes : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
    }
}
