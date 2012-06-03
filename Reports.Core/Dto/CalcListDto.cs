using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
	public class CalcListDto
	{
		public const string DateFormat = "dd/MM/yyyy";

		private int id;
		private DateTime dateCreated;
		private DateTime beginDate;
		private DateTime endDate;
		private string number;
		private string comment;

		public CalcListDto()
		{
		
		}
		public CalcListDto(Document doc)
		{
			id = doc.Id;
			dateCreated = doc.DateCreated;
			beginDate = doc.BeginDate;
			endDate = doc.EndDate;
			if(doc.ReportType == ReportType.CalList)
				number = "За " + CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.MonthNames[doc.BeginDate.Month - 1].ToLower();
			else
				number = "Cправка от "+ DateCreated.ToString(DateFormat);
			this.comment = string.Empty;
		}
		public CalcListDto(int id,DateTime dateCreated,DateTime beginDate,DateTime endDate, string number, string comment)
		{
			this.id = id;
			this.dateCreated = dateCreated;
			this.beginDate = beginDate;
			this.endDate = endDate;
			this.number = number;
			this.comment = comment;
		}
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		public DateTime DateCreated
		{
			get { return dateCreated; }
			set { dateCreated = value; }
		}
		public DateTime BeginDate
		{
			get { return beginDate; }
			set { beginDate = value; }
		}
		public DateTime EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}
		public string Number
		{
			get { return number; }
			set { number = value; }
		}
		public string Comment
		{
			get { return comment; }
			set { comment = value; }
		}
	}
}
