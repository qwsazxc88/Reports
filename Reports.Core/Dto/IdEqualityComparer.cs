using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class IdEqualityComparer: IEqualityComparer<IdNameDto>
    {
        public bool Equals(IdNameDto x, IdNameDto y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IdNameDto obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
