using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Domain
{
    public class AbstractUsedEntity : AbstractEntity, IAbstractUsedEntity
    {
        #region Fields
        private bool isUsed;
        #endregion
        #region Properties
        public virtual bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }
        #endregion
    }
}
