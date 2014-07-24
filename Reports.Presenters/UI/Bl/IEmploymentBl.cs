﻿using System;
using Reports.Presenters.UI.ViewModel.Employment2;

namespace Reports.Presenters.UI.Bl
{
    public interface IEmploymentBl : IBaseBl
    {
        GeneralInfoModel GetGeneralInfoModel(int? userId = null);
        PassportModel GetPassportModel(int? userId = null);
        EducationModel GetEducationModel(int? userId = null);
        FamilyModel GetFamilyModel(int? userId = null);
        MilitaryServiceModel GetMilitaryServiceModel(int? userId = null);
        ExperienceModel GetExperienceModel(int? userId = null);
        ContactsModel GetContactsModel(int? userId = null);
        BackgroundCheckModel GetBackgroundCheckModel(int? userId = null);
        OnsiteTrainingModel GetOnsiteTrainingModel(int? userId = null);
        ManagersModel GetManagersModel(int? userId = null);
        PersonnelManagersModel GetPersonnelManagersModel(int? userId = null);
        RosterModel GetRosterModel();
        SignersModel GetSignersModel();

        void LoadDictionaries(GeneralInfoModel model);
        void LoadDictionaries(PassportModel model);
        void LoadDictionaries(EducationModel model);
        void LoadDictionaries(FamilyModel model);
        void LoadDictionaries(MilitaryServiceModel model);
        void LoadDictionaries(ExperienceModel model);
        void LoadDictionaries(ContactsModel model);
        void LoadDictionaries(BackgroundCheckModel model);
        void LoadDictionaries(OnsiteTrainingModel model);
        void LoadDictionaries(ManagersModel model);
        void LoadDictionaries(PersonnelManagersModel model);
        void LoadDictionaries(RosterModel model);
        void LoadDictionaries(SignersModel model);

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
