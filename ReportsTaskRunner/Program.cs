using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReportsTaskRunner.DAL;
using ReportsTaskRunner.Helpers;

namespace ReportsTaskRunner
{
    class Program
    {
        private static IDictionary<string, Action> tasks = new Dictionary<string, Action>
        {
            { "SendCCLNotifications", SendCCLNotifications },
            { "SendFixedTermContractNotifications", SendFixedTermContractNotifications}
        };

        private static void PrintHelp()
        {
            Console.WriteLine("Запуск: ReportsTaskRunner [-help] [имя_задачи_1 [, имя_задачи_2] [,...]]");
            Console.WriteLine(Environment.NewLine + "Доступные задачи:");
            Console.WriteLine("SendCCLNotifications: отправить оповещения по обходным листам");
        }

        static void Main(string[] args)
        {
            Action task = null;
            foreach (var arg in args)
            {
                if (arg == "-help")
                {
                    PrintHelp();
                }
                else if (tasks.TryGetValue(arg, out task))
                {
                    task();
                }
            }

            if (args.Count() == 0)
            {
                PrintHelp();
            }
        }

        // Отправить всем согласующим ОЛ оповещения об имеющихся ОЛ с увольнением сегодня
        static void SendCCLNotifications()
        {
            // Список ОЛ с увольнением сегодня
            IList<Dismissal> ccls = ClearanceChecklistDAL.GetClearanceChecklistsByDismissalDate(DateTime.Today);
            // Список согласующих
            IList<User> authorities = ClearanceChecklistDAL.GetClearanceChecklistApprovingAuthorities()
                // Только с адресами e-mail
                .Where(x => !string.IsNullOrWhiteSpace(x.Email)).ToList();
            IList<string> toList = new List<string>();

            // Если есть ОЛ с увольнением сегодня, то делаем отправку
            if (ccls.Count > 0)
            {
                string eAddress = string.Empty;
                foreach (var authority in authorities)
                {
                    toList.Add(authority.Email);
                }

                const string subject = @"Новые обходные листы";

                var cclNotificationListBuilder = new StringBuilder();
                cclNotificationListBuilder.Append(
                    @"<table>
                    <tr style=""border: 1px solid black;"">
                        <th>№</th>
                        <th>Дата создания обх. листа</th>
                        <th>Дата увольнения</th>
                        <th>Сотрудник</th>
                        <th>Подразделение</th>
                    </tr>"
                );
                foreach (var ccl in ccls)
                {
                    cclNotificationListBuilder.AppendFormat(
                        @"<tr style=""border: 1px solid black;"">
                        <td style=""border: 1px solid black;"">{0}</td>
                        <td style=""border: 1px solid black;"">{1}</td>
                        <td style=""border: 1px solid black;"">{2}</td>
                        <td style=""border: 1px solid black;"">{3}</td>
                        <td style=""border: 1px solid black;"">{4}</td>
                    </tr>",
                        ccl.Number, ccl.CreateDate.ToShortDateString(), ccl.EndDate.ToShortDateString(), ccl.User.Name, ccl.User.Department.Name);
                }
                cclNotificationListBuilder.Append("</table>");
                string body = string.Format(@"Сегодня истекает срок согласования следующих обходных листов:
                <p style=""margin: 20px;"">{0}</p>
                <a href=""https://ruscount.com:8002"">Кадровый портал</a>",
                    cclNotificationListBuilder.ToString());

                Console.WriteLine();
                Console.WriteLine(DateTime.Now);
                Console.WriteLine();
                Console.WriteLine(subject);
                Console.WriteLine();
                foreach (var to in toList)
                {
                    Console.Write(to + "; ");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(body);


                Mailer.SendNotification(toList, subject, body);
            }            
        }

        static void SendFixedTermContractNotifications()
        {
            // Список сотрудников с ТД с истекающим сроком
            IList<EmploymentCandidate> employees = FixedTermContractDAL.GetEmployeesWithExpiringContracts();
            IList<string> toList = new List<string>();
            const string subject = @"Срочные ТД";
            string body = @"Уведомляем Вас о том, что срочный трудовой договор №{0} от {1} г., 
                заключенный с сотрудником [{2}],
                будет прекращен по основанию, предусмотренному пунктом 2 части первой статьи 77 Трудового кодекса Российской Федерации
                (истечение срока трудового договора).
                В случае, если сотрудник [{2}] будет переведен на бессрочный договор,
                просим непосредственнго руководителя зайти в программу «Прием»
                и в реестре в колонке  «Сотрудник переведен на бессрочный ТД» нажать соответствующую галку.
                В случае, если непосредственный руководитель не нажал галку в реестре в программе «Прием»,
                трудовые отношения с сотрудником [{2}] будут прекращены.<br /><br />
                <a href='https://ruscount.com:8002'>Кадровый портал</a>";
            

            foreach (var e in employees)
            {
                body = string.Format(body,
                    !string.IsNullOrEmpty(e.PersonnelManagers.ContractNumber) ? e.PersonnelManagers.ContractNumber : "?",
                    e.PersonnelManagers.ContractDate.HasValue ? e.PersonnelManagers.ContractDate.Value.ToShortDateString() : "?",
                    e.CandidateUser.Name);
                
                toList.Add("RogozinaIE@sovcombank.ru");
                toList.Add(e.CandidateUser.Email ?? string.Empty);
                toList.Add(MainDAL.db.User.Where(u => u.Id == e.AppointmentCreator.Id).Single().Email ?? string.Empty);

                Mailer.SendNotification(toList, subject, body);                
            }
        }
    }
}
