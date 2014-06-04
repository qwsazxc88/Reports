using System;
using Reports.Presenters.UI.ViewModel.Employment2;

namespace Reports.Presenters.UI.Bl
{
    public interface IEmploymentBl : IBaseBl
    {
        GeneralInfoModel GetGeneralInfoModel(int? userId);
        PassportModel GetPassportModel(int? userId);
        EducationModel GetEducationModel(int? userId);
        FamilyModel GetFamilyModel(int? userId);
        MilitaryServiceModel GetMilitaryServiceModel(int? userId);
        ExperienceModel GetExperienceModel(int? userId);
        ContactsModel GetContactsModel(int? userId);
        BackgroundCheckModel GetBackgroundCheckModel(int? userId);
        OnsiteTrainingModel GetOnsiteTrainingModel(int? userId);
        ManagersModel GetManagersModel(int? userId);
        PersonnelManagersModel GetPersonnelManagersModel(int? userId);
        RosterModel GetRosterModel();
        SignersModel GetSignersModel();

        void SetGeneralInfoModel(GeneralInfoModel model, bool hasError);
        void SetPassportModel(PassportModel model, bool hasError);        
        void SetEducationModel(EducationModel model, bool hasError);        
        void SetFamilyModel(FamilyModel model, bool hasError);        
        void SetMilitaryServiceModel(MilitaryServiceModel model, bool hasError);        
        void SetExperienceModel(ExperienceModel model, bool hasError);        
        void SetContactsModel(ContactsModel model, bool hasError);        
        void SetBackgroundCheckModel(BackgroundCheckModel model, bool hasError);        
        void SetOnsiteTrainingModel(OnsiteTrainingModel model, bool hasError);        
        void SetManagersModel(ManagersModel model, bool hasError);        
        void SetPersonnelManagersModel(PersonnelManagersModel model, bool hasError);        
        void SetRosterModel(RosterModel model, bool hasError);        
        void SetSignersModel(SignersModel model, bool hasError);

        bool SaveModel<TVM, TE>(TVM model, out string error) where TVM : AbstractEmploymentModel where TE : new();
    }
}
