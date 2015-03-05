using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface INoteTypeDao : IDao<NoteType>
    {
        IList<NoteTypeDto> GetAllNoteTypeDto();
    }
}
