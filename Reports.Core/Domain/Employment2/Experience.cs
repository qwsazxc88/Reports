using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class Experience : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual IList<ExperienceItem> ExperienceItems { get; set; }
        // public virtual int ExperienceYears { get; set; } | Canceled
        // public virtual int ExperienceMonths { get; set; } | Canceled
        public virtual string WorkBookSeries { get; set; }
        public virtual string WorkBookNumber { get; set; }
        public virtual DateTime? WorkBookDateOfIssue { get; set; }
        public virtual string WorkBookSupplementSeries { get; set; }
        public virtual string WorkBookSupplementNumber { get; set; }
        public virtual DateTime? WorkBookSupplementDateOfIssue { get; set; }
        // Скан трудовой книжки
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}