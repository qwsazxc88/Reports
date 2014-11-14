using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// ������ � ������� �������/������
    /// </summary>
    public class HelpQuestionHistoryEntity : AbstractEntityWithVersion
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; } // ���� �������� ������ � �������
        public virtual DateTime? SendDate { get; set; } // ���� �������� ������ (��� �������)
        public virtual DateTime? BeginWorkDate { get; set; } // ���� ������ ������� (��� �������)
        public virtual DateTime? EndWorkDate { get; set; } // ���� ������ (��� �������)
       
        
        public virtual int Type { get; set; } // ������ (1) ��� �������� (2)
        public virtual HelpQuestionRequest Request { get; set; }

       
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
       

       
        public virtual User Creator { get; set; }
        public virtual User Consultant { get; set; }
        public virtual int CreatorRoleId { get; set; } 
        public virtual int RecipientRoleId { get; set; }


        //public virtual IList<HelpServiceRequestComment> Comments { get; set; }

      
        #endregion

        #region Properties
        #endregion
        #region Constructors
        #endregion
    }
}