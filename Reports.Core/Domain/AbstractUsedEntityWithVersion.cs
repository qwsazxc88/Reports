namespace Reports.Core.Domain
{
    //public class AbstractUsedEntityWithVersion:AbstractUsedEntity
    //{
    //    #region Fields
    //    private int version;
    //    #endregion
    //    #region Properties
    //    public virtual int Version
    //    {
    //        get { return version; }
    //        set { version = value; }
    //    }
    //    #endregion
    //}
    public class AbstractEntityWithVersion : AbstractEntity
    {
        #region Fields

        #endregion

        #region Properties

        public virtual int Version { get; set; }

        #endregion
    }
}