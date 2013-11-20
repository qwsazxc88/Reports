namespace Reports.Presenters.UI.ViewModel
{
    public class MissionOrderEditModel : UserInfoModel, IContainId
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }


        public RequestCommentsModel CommentsModel { get; set; }
        #endregion
    }
}