using System.ComponentModel.DataAnnotations;


namespace Reports.Presenters.UI.ViewModel
{
    public class PrintLoginFormModel
    {
        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Display(Name = "Имя пользователя (логин)")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
