using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Core.Dto
{
	public class NameDto
	{
		private string name;

		public NameDto()
		{
			
		}
		public NameDto(string name)
		{
			this.name = name;
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}
}
