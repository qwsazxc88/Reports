using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core.Domain;

namespace Reports.Core.Dto
{
    public class QuestionDto
    {
        private const int textLength = 32;

        private int id;
        private string fullName;
        private DateTime date;
        private int userId;
        private string text;
        private DateTime? answerDate;
        private string answerFullName;
        private string answerText;

        public QuestionDto(Question item)
		{
            id = item.Id;
			fullName = item.User.FullName;
            userId = item.User.Id;
            date = item.Date;
            text = item.Text.Length > textLength? item.Text.Substring(0,textLength) : item.Text;
            answerDate = item.AnswerDate;
            answerFullName = item.AnswerUser == null ? string.Empty : item.AnswerUser.FullName;
            answerText = item.AnswerText == null?string.Empty : item.AnswerText.Length > textLength ? item.AnswerText.Substring(0, textLength) : item.AnswerText;
		}
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public string AnswerFullName
        {
            get { return answerFullName; }
            set { answerFullName = value; }
        }
        public string AnswerText
        {
            get { return answerText; }
            set { answerText = value; }
        }
        public DateTime? AnswerDate
        {
            get { return answerDate; }
            set { answerDate = value; }
        }
    }
}
