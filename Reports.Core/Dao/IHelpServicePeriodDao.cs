﻿using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IHelpServicePeriodDao : IDaoSorted<HelpServicePeriod>
    {
        List<HelpServicePeriod> LoadForPeriodSortedByOrder(int typeId);
    }
}