using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Запись в истории вопроса/ответа
    /// </summary>
    public class HelpQuestionHistoryEntity : AbstractEntityWithVersion
    {
        #region Fields
        public virtual DateTime CreateDate { get; set; } // дата создания записи в истории
        public virtual DateTime? SendDate { get; set; } // дата отправки вопрос (для вопроса)
        public virtual DateTime? BeginWorkDate { get; set; } // дата приема вопроса (для вопроса)
        public virtual DateTime? EndWorkDate { get; set; } // дата ответа (для вопроса)
       
        
        public virtual int Type { get; set; } // вопрос (1) или редирект (2)
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