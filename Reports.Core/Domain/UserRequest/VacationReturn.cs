using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class VacationReturn: StandartRequestEntity
    {
    
    public virtual DateTime? ReturnDate {get;set;}
    public virtual DateTime? ContinueDate {get;set;}
    public virtual string ReturnReason {get;set;}
    public virtual DateTime? ManagerDateAccept {get;set;}
    public virtual DateTime? PersonnelManagerDateAccept {get;set;}
    public virtual DateTime? ChiefDateAccept {get;set;}
    public virtual DateTime? VacationStartDate {get;set;}
    public virtual DateTime? VacationEndDate {get;set;}
    public virtual int DaysNotUsedCount {get;set;}
    
    public virtual AbstractReferencyBookEntity ReturnType {get;set;}
    public virtual Vacation Vacation {get;set;}
    
    public virtual User Manager {get;set;}
    public virtual User Chief {get;set;}
    public virtual User PersonnelManager { get; set; }
    }
}
