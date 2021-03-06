using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Domain
{
    public interface IAbstractUsedEntity
    {
        bool IsUsed { get; set;}
    }
    public interface ISortOrder
    {
        int SortOrder { get; set; }
    }
}
