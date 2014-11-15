namespace Reports.Presenters.UI.ViewModel
{
    public class HelpQuestionEditModel : IContainId
    {
        public int Version { get; set; }

        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion
    }
}