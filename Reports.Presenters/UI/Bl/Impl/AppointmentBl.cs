using System.Collections.Generic;
using System.Linq;
using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class AppointmentBl : BaseBl, IAppointmentBl
    {
        public AppointmentListModel GetAppointmentListModel()
        {
            //User user = UserDao.Load(AuthenticationService.CurrentUser.Id);
            //IdNameReadonlyDto dep = GetDepartmentDto(user);
            AppointmentListModel model = new AppointmentListModel
            {
                UserId = AuthenticationService.CurrentUser.Id,
                DepartmentName = string.Empty,
                DepartmentId = 0,
                DepartmentReadOnly = false,
            };
            SetInitialDates(model);
            SetDictionariesToModel(model);
            //SetInitialStatus(model);
            //SetIsAvailable(model);
            return model;
        }
        public void SetDictionariesToModel(AppointmentListModel model)
        {
            model.Statuses = GetAppRequestStatuses();
        }
        protected List<IdNameDto> GetAppRequestStatuses()
        {
            //var requestStatusesList = RequestStatusDao.LoadAllSorted().ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
            List<IdNameDto> moStatusesList = new List<IdNameDto>
                                                       {
                                                           new IdNameDto(1, "Одобрен сотрудником"),
                                                           new IdNameDto(2, "Не одобрен сотрудником"),
                                                           new IdNameDto(3, "Одобрен руководителем"),
                                                           new IdNameDto(4, "Не одобрен руководителем"),
                                                           new IdNameDto(5, "Одобрен бухгалтером"),
                                                           new IdNameDto(6, "Не одобрен бухгалтером"),
                                                           new IdNameDto(7, "Требует одобрения руководителем"),
                                                           new IdNameDto(8, "Требует одобрения бухгалтером"),
                                                           new IdNameDto(10, "Отклонен"),
                                                       }.OrderBy(x => x.Name).ToList();
            moStatusesList.Insert(0, new IdNameDto(0, SelectAll));
            return moStatusesList;
        }
    }
}