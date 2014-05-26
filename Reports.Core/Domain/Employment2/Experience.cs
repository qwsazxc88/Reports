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
        public virtual string WorkBookSeries { get; set; } //ok
        public virtual string WorkBookNumber { get; set; } //ok
        public virtual DateTime? WorkBookDateOfIssue { get; set; } //ok
        public virtual string WorkBookSupplementSeries { get; set; } //ok
        public virtual string WorkBookSupplementNumber { get; set; } //ok
        public virtual DateTime? WorkBookSupplementDateOfIssue { get; set; } //ok
        // Скан трудовой книжки
        // Скан вкладыша в трудовую книжку
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