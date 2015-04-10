using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class GpdChargingTypeDao:DefaultDao<GpdChargingType>,IGpdChargingTypeDao
    {
        public GpdChargingTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<Dto.IdNameDto> GetAllTypes()
        {
            var list= LoadAll().ToList().ConvertAll(x => new IdNameDto { Id=x.Id, Name=x.Name });
            list.Add(new IdNameDto { Id = 0, Name = "Все" });
            return list;
        }
    }
}
