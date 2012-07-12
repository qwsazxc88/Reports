namespace Reports.Presenters.UI.ViewModel
{
    public class SaveCommentModel
    {
        public int DocumentId { get; set; }
        public int TypeId { get; set; }
        public string Comment { get; set; }
        public string Error { get; set; }
    }
}