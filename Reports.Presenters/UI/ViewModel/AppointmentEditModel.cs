namespace Reports.Presenters.UI.ViewModel
{
    public class AppointmentEditModel : ManagerInfoModel, IContainId
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion

        public RequestCommentsModel CommentsModel { get; set; }
    }
}