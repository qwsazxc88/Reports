using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник Примечаний 
    /// </summary>
    public class NoteType : AbstractEntity
    {
        #region Properties
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual int Id { get; protected set; }
        /// <summary>
        /// Название
        /// </summary>
        public virtual string Name { get; set; }
        #endregion
    }
}
