using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpEditVersionModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата версии")]
        public string ReleaseDate { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public string Error { get; set; }
    }

    public class HelpSaveVersionModel
    {
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Comment { get; set; }
        public string Error { get; set; }
    }
    public class HelpSaveVersionModelFromView
    {
        public int Id { get; set; }
        public string ReleaseDate { get; set; }
        public string Comment { get; set; }
        public string Error { get; set; }
    }
    public class HelpEditFaqModel
    {
        public int Id { get; set; }
        [Display(Name = "Вопрос")]
        public string Question { get; set; }
        [Display(Name = "Ответ")]
        public string Answer { get; set; }
        public string Error { get; set; }
    }

    public class HelpSaveFaqModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Error { get; set; }
    }
}