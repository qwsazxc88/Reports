namespace Reports.Core.Domain
{
    public class ForeignLanguage : AbstractEntityWithVersion
    {
        public virtual string LanguageName { get; set; }
        public virtual string Level { get; set; }
    }
}
