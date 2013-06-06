using System;

namespace Reports.Core.Domain
{
    public class SicklistComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Sicklist Sicklist { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}