using System;

namespace Reports.Core.Dto
{
    public class HelpServiceQuestionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        //public string ManagerName { get; set; }
        public string DepName { get; set; }
        public int RequestNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        public string QuestionType { get; set; }
        public string QuestionSubtype { get; set; }
        public int QuestionsCount { get; set; }
        public bool  IsManagerQuestion { get; set; }
        public string RedirectRole { get; set; }
        public int StatusNumber { get; set; }
        public string Status { get; set; }
        public int Number { get; set; }
        public string Dep3Name { get; set; }
        public bool Base { get; set; }
    }
}