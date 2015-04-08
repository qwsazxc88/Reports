using System;

namespace Reports.Core.Dto
{
    public class HelpServiceRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string ManagerName { get; set; }
        public string FiredUserName { get; set; }
        public string FiredUserSurname { get; set; }
        public string FiredUserPatronymic { get; set; }
        public DateTime UserBirthDate { get; set; }
        public string Dep7Name { get; set; }
        public int RequestNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        public DateTime? ConfirmWorkDate { get; set; }
        public string RequestType { get; set; }
        public string RequestTransferType { get; set; }
        public int StatusNumber { get; set; }
        public string Status { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string Dep3Name { get; set; }
        public string ProdTimeName { get; set; }
        public int Note { get; set; }
        public string NoteName { get; set; }
        public bool IsOriginalReceived { get; set; }
        public bool IsForGEMoney { get; set; }
        public int DocumentsCount { get; set; }
    }
      //AddScalar("Id", NHibernateUtil.Int32).
      //          AddScalar("UserId", NHibernateUtil.Int32).
      //          AddScalar("UserName", NHibernateUtil.String).
      //          AddScalar("Position", NHibernateUtil.String).
      //          AddScalar("ManagerName", NHibernateUtil.String).
      //          AddScalar("Dep7Name", NHibernateUtil.String).
      //          AddScalar("RequestNumber", NHibernateUtil.Int32).
      //          AddScalar("CreateDate", NHibernateUtil.DateTime).
      //          AddScalar("ConfirmWorkDate", NHibernateUtil.DateTime).
      //          AddScalar("RequestType", NHibernateUtil.String).
      //          AddScalar("RequestTransferType", NHibernateUtil.String).
      //          AddScalar("StatusNumber", NHibernateUtil.Int32).
      //          AddScalar("Status", NHibernateUtil.String);  
}