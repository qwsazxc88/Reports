using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class GeneralInfo : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Patronymic { get; set; }
        public virtual IList<NameChange> NameChanges { get; set; }
        public virtual bool IsMale { get; set; }
        public virtual string Citizenship { get; set; }
        public virtual string InsuredPersonType { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string RegionOfBirth { get; set; }
        public virtual string DistrictOfBirth { get; set; }
        public virtual string CityOfBirth { get; set; }
        public virtual IList<ForeignLanguage> ForeignLanguages { get; set; }
        public virtual string INN { get; set; }
        public virtual string SNILS { get; set; }
        public virtual IList<Disability> Disabilities { get; set; }
        public virtual bool IsResident { get; set; }
        public virtual bool AgreedToPersonalDataProcessing { get; set; }
    }
}
