using System.Collections.Generic;
using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Департамент/офис и т.п.
    /// </summary>
    public class Department : AbstractEntityWithVersion//AbstractUsedEntity
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Code1C { get; set; }
        public virtual int? ParentId { get; set; }
        //public virtual Department Parent { get; set; }
        public virtual string Path { get; set; }
        public virtual int? ItemLevel { get; set; }
        public virtual string CodeSKD { get; set; }
        public virtual IList<Department> Dep3 { get; set; }
        public virtual int? Priority { get; set; }
        public virtual bool IsUsed { get; set; }
        public virtual string FingradCode { get; set; }
        public virtual StaffDepartmentAccessory DepartmentAccessory { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual IList<DepartmentArchive> DepartmentArchives { get; set; }
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