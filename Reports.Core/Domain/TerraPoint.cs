using System;

namespace Reports.Core.Domain
{
    public class TerraPoint : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Code1C { get; set; }
        public virtual string ParentId { get; set; }
        public virtual string Path { get; set; }
        public virtual int? ItemLevel { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual bool IsHoliday { get; set; }
        public virtual TerraPointToUser TerraPointToUser { get; set; }
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