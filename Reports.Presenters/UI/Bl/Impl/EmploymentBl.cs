using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Reports.Presenters.UI.ViewModel.Employment2;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class EmploymentBl : BaseBl, IEmploymentBl
    {
        #region Get Model

        public GeneralInfoModel GetGeneralInfoModel()
        {
            // TODO: EMPL доработать реализацию
            GeneralInfoModel model = new GeneralInfoModel();
            LoadDictionaries(model);
            return model;
        }

        public PassportModel GetPassportModel()
        {
            // TODO: EMPL доработать реализацию
            PassportModel model = new PassportModel();
            LoadDictionaries(model);
            return model;
        }

        public EducationModel GetEducationModel()
        {
            // TODO: EMPL заменить реализацией
            return new EducationModel();
        }

        public FamilyModel GetFamilyModel()
        {
            // TODO: EMPL заменить реализацией
            return new FamilyModel();
        }

        public MilitaryServiceModel GetMilitaryServiceModel()
        {
            // TODO: EMPL доработать реализацию
            MilitaryServiceModel model = new MilitaryServiceModel();
            LoadDictionaries(model);
            return model;
        }

        public ExperienceModel GetExperienceModel()
        {
            // TODO: EMPL заменить реализацией
            return new ExperienceModel();
        }

        public ContactsModel GetContactsModel()
        {
            // TODO: EMPL заменить реализацией
            return new ContactsModel();
        }

        public BackgroundCheckModel GetBackgroundCheckModel()
        {
            // TODO: EMPL заменить реализацией
            return new BackgroundCheckModel();
        }

        public OnsiteTrainingModel GetOnsiteTrainingModel()
        {
            // TODO: EMPL заменить реализацией
            return new OnsiteTrainingModel();
        }

        public ManagersModel GetManagersModel()
        {
            // TODO: EMPL заменить реализацией
            return new ManagersModel();
        }

        public PersonnelManagersModel GetPersonnelManagersModel()
        {
            // TODO: EMPL заменить реализацией
            return new PersonnelManagersModel();
        }

        public RosterModel GetRosterModel()
        {
            // TODO: EMPL заменить реализацией
            return new RosterModel();
        }

        public SignersModel GetSignersModel()
        {
            // TODO: EMPL заменить реализацией
            return new SignersModel();
        }

        #endregion

        #region Set Model

        public void SetGeneralInfoModel(GeneralInfoModel model, bool hasError)
        {

        }
        
        public void SetPassportModel(PassportModel model, bool hasError)
        {

        }
        
        public void SetEducationModel(EducationModel model, bool hasError)
        {

        }
        
        public void SetFamilyModel(FamilyModel model, bool hasError)
        {

        }
        
        public void SetMilitaryServiceModel(MilitaryServiceModel model, bool hasError)
        {

        }
        
        public void SetExperienceModel(ExperienceModel model, bool hasError)
        {

        }
        
        public void SetContactsModel(ContactsModel model, bool hasError)
        {

        }
        
        public void SetBackgroundCheckModel(BackgroundCheckModel model, bool hasError)
        {

        }
        
        public void SetOnsiteTrainingModel(OnsiteTrainingModel model, bool hasError)
        {

        }
        
        public void SetManagersModel(ManagersModel model, bool hasError)
        {

        }
        
        public void SetPersonnelManagersModel(PersonnelManagersModel model, bool hasError)
        {

        }
        
        public void SetRosterModel(RosterModel model, bool hasError)
        {

        }
        
        public void SetSignersModel(SignersModel model, bool hasError)
        {

        }

        #endregion

        #region LoadDictionaries

        protected void LoadDictionaries(GeneralInfoModel model)
        {
            model.CitizenshipItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Россия", Value = "1"},
                    new SelectListItem {Text = "Украина", Value = "2"},
                    new SelectListItem {Text = "Китай", Value = "3"}
                },
                "Value", "Text"
            );

            model.InsuredPersonTypeItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Тип1", Value = "1"},
                    new SelectListItem {Text = "Тип2", Value = "2"},
                    new SelectListItem {Text = "Тип3", Value = "3"}
                },
                "Value", "Text"
            );

            model.StatusItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Резидент", Value = "1"},
                    new SelectListItem {Text = "Нерезидент", Value = "2"}
                },
                "Value", "Text"
            );
        }
        protected void LoadDictionaries(PassportModel model)
        {
            model.DocumentTypeItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Паспорт РФ", Value = "1"},
                    new SelectListItem {Text = "Военный билет", Value = "2"},
                    new SelectListItem {Text = "Водительское удостоверение", Value = "3"},
                    new SelectListItem {Text = "Студенческий билет", Value = "4"},
                    new SelectListItem {Text = "Справка", Value = "5"}
                },
                "Value", "Text"
            );
        }
        protected void LoadDictionaries(EducationModel model)
        {

        }
        protected void LoadDictionaries(FamilyModel model)
        {

        }
        protected void LoadDictionaries(MilitaryServiceModel model)
        {
            model.ReserveCategoryItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "1"},
                    new SelectListItem {Text = "2", Value = "2"}
                },
                "Value", "Text"
            );
            model.RankItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Рядовой", Value = "1"},
                    new SelectListItem {Text = "Матрос", Value = "2"},
                    new SelectListItem {Text = "Ефрейтор", Value = "3"},
                    new SelectListItem {Text = "Ст. матрос", Value = "4"},
                    new SelectListItem {Text = "Мл. сержант", Value = "5"},
                    new SelectListItem {Text = "Старшина 2-й статьи", Value = "6"},
                    new SelectListItem {Text = "Сержант", Value = "7"},
                    new SelectListItem {Text = "Старшина 1-й статьи", Value = "8"},
                    new SelectListItem {Text = "Ст. сержант", Value = "9"},
                    new SelectListItem {Text = "Гл. старшина", Value = "10"},
                    new SelectListItem {Text = "Старшина", Value = "11"},
                    new SelectListItem {Text = "Гл. корабельный старшина", Value = "12"},
                    new SelectListItem {Text = "Прапорщик", Value = "13"},
                    new SelectListItem {Text = "Мичман", Value = "14"},
                    new SelectListItem {Text = "Ст. прапорщик", Value = "15"},
                    new SelectListItem {Text = "Ст. мичман", Value = "16"},
                    new SelectListItem {Text = "Мл. лейтенант", Value = "17"},
                    new SelectListItem {Text = "Лейтенант", Value = "18"},
                    new SelectListItem {Text = "Ст. лейтенант", Value = "19"},
                    new SelectListItem {Text = "Капитан", Value = "20"},
                    new SelectListItem {Text = "Капитан-лейтенант", Value = "21"},
                    new SelectListItem {Text = "Майор", Value = "22"},
                    new SelectListItem {Text = "Капитан 3 ранга", Value = "23"},
                    new SelectListItem {Text = "Подполковник", Value = "24"},
                    new SelectListItem {Text = "Капитан 2 ранга", Value = "25"},
                    new SelectListItem {Text = "Полковник", Value = "26"},
                    new SelectListItem {Text = "Капитан 1 ранга", Value = "27"},
                    new SelectListItem {Text = "Генерал-майор", Value = "28"},
                    new SelectListItem {Text = "Контр-адмирал", Value = "29"},
                    new SelectListItem {Text = "Генерал-лейтенант", Value = "30"},
                    new SelectListItem {Text = "Вице-адмирал", Value = "31"},
                    new SelectListItem {Text = "Генерал-полковник", Value = "32"},
                    new SelectListItem {Text = "Адмирал", Value = "33"},
                    new SelectListItem {Text = "Генерал армии", Value = "34"},
                    new SelectListItem {Text = "Адмирал флота", Value = "35"},
                    new SelectListItem {Text = "Маршал РФ", Value = "36"}
                },
                "Value", "Text"
            );
            model.SpecialityCategoryItems = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text = "Солдаты и матросы", Value = "1"},
                    new SelectListItem {Text = "Сержанты и старшины", Value = "2"},
                    new SelectListItem {Text = "Прапорщики и мичманы", Value = "3"},
                    new SelectListItem {Text = "Младшие офицеры", Value = "4"},
                    new SelectListItem {Text = "Старшие офицеры", Value = "5"},
                    new SelectListItem {Text = "Высшие офицеры", Value = "6"}
                },
                "Value", "Text"
            );
        }
        protected void LoadDictionaries(ExperienceModel model)
        {

        }
        protected void LoadDictionaries(ContactsModel model)
        {

        }
        protected void LoadDictionaries(BackgroundCheckModel model)
        {

        }
        protected void LoadDictionaries(OnsiteTrainingModel model)
        {

        }
        protected void LoadDictionaries(ManagersModel model)
        {

        }
        protected void LoadDictionaries(PersonnelManagersModel model)
        {

        }
        protected void LoadDictionaries(RosterModel model)
        {

        }
        protected void LoadDictionaries(SignersModel model)
        {

        }

        #endregion
    }
}