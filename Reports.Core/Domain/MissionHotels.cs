using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Гостиницы
    /// </summary>
    public class MissionHotels : AbstractEntityWithVersion
    {
        #region Fields
        public virtual string Name { get; set; }
        public virtual string Account { get; set; }
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}
