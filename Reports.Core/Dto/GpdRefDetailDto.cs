using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Список типов реквизитов.
    /// </summary>
    public class GpdRefDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Справочник реквизитов.
    /// </summary>
    public class GpdRefDetailFullDto : GpdRefDetailDto
    {
        public virtual int DTID { get; set; }
        public virtual string INN { get; set; }
        public virtual string KPP { get; set; }
        public virtual string Account { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankBIK { get; set; }
        public virtual string CorrAccount { get; set; }
        public virtual int CreatorID { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string CreatorName { get; set; }
        public virtual string CreatePositionName { get; set; }
        public virtual string CrDep7Level { get; set; }
        public virtual string CrDep3Level { get; set; }
        public virtual int EditorID { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual string EditorName { get; set; }
        public virtual string EditPositionName { get; set; }
        public virtual string EDep7Level { get; set; }
        public virtual string EDep3Level { get; set; }
    }
    /// <summary>
    /// Для прав доступа к операциям 
    /// </summary>
    public class GpdPermissionDto
    {
        public virtual bool IsCreate { get; set; }
        public virtual bool IsDraft { get; set; }
        public virtual bool IsWrite { get; set; }
        public virtual bool IsCancel { get; set; }
        public virtual bool IsComment { get; set; }
        public virtual bool IsCreateAct { get; set; }
    }
    /// <summary>
    /// Для отображения списка наборов реквизитов
    /// </summary>
    public class GpdDetailSetsListDto : GpdRefDetailDto
    {
        public virtual string Surname { get; set; }
        public virtual string PayerName { get; set; }
        public virtual string PayeeName { get; set; }
        public virtual string Account { get; set; }
        public virtual int CreatorID { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual string CreatorName { get; set; }
        public virtual int EditorID { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual string EditorName { get; set; }
        public virtual int PersonID { get; set; }
        public virtual int PayerID { get; set; }
        public virtual int PayeeID { get; set; }
        public virtual bool AllowEdit { get; set; }
    }
    /// <summary>
    /// Для отображения списка наборов реквизитов
    /// </summary>
    public class GpdDetailDto : GpdRefDetailDto
    {
        public virtual int DTID { get; set; }
        public virtual string INN { get; set; }
        public virtual string KPP { get; set; }
        public virtual string Account { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankBIK { get; set; }
        public virtual string CorrAccount { get; set; }
    }
}
