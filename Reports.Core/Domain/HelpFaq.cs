using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Помощь - вопрос/ответ
    /// </summary>
    public class HelpFaq : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
    }
}