using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;
using System.Collections.Generic;
namespace Reports.Core.Dao.Impl
{
    public class NoteTypeDao:DefaultDao<NoteType> , INoteTypeDao
    {
        public NoteTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }


        public IList<Dto.NoteTypeDto> GetAllNoteTypeDto()
        {
            List<Dto.NoteTypeDto> result = new List<Dto.NoteTypeDto>();
            var notetypes=LoadAll();
            foreach (var elem in notetypes)
            {
                var newelem = new Dto.NoteTypeDto() { Id = elem.Id, Name = elem.Name };
                result.Add(newelem);
            }
            return result;
        }

    }
}
