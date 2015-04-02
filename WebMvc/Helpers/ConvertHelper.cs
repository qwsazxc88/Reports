using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebMvc.Helpers
{
    public static class ConvertHelper
    {
        private static string[,] orderNames =
        {            
	        { "миллиард", "миллиарда", "миллиардов" },
	        { "миллион", "миллиона", "миллионов" },
	        { "тысяча", "тысячи", "тысяч" },
	        { "", "", "" },
        };

        public static MvcHtmlString ConvertNumberSumToText(this HtmlHelper html, decimal sum)
        {
            StringBuilder resultBuilder = new StringBuilder();

            long rubles = (long)sum;
            int copecks = (int)Math.Round((sum - rubles) * 100);

            int[] rublesParts = 
            {
	            (int)((rubles % 1000000000000 - rubles % 1000000000) / 1000000000), // миллиарды
	            (int)((rubles % 1000000000 - rubles % 1000000) / 1000000), // миллионы
	            (int)((rubles % 1000000 - rubles % 1000) / 1000), // тысячи
	            (int)(rubles % 1000), // 
            };

            int[] partDigits = new int[3];
            for (int i = 0; i < rublesParts.Length; i++)
            {
                if (rublesParts[i] != 0)
                {
                    partDigits[0] = (rublesParts[i] - rublesParts[i] % 100) / 100; // сотни
                    partDigits[1] = (rublesParts[i] % 100 - rublesParts[i] % 10) / 10; // десятки
                    partDigits[2] = rublesParts[i] % 10; // единицы

                    #region Названия сотен
                    switch (partDigits[0])
                    {
                        case 1: resultBuilder.Append(" сто"); break;
                        case 2: resultBuilder.Append(" двести"); break;
                        case 3: resultBuilder.Append(" триста"); break;
                        case 4: resultBuilder.Append(" четыреста"); break;
                        case 5: resultBuilder.Append(" пятьсот"); break;
                        case 6: resultBuilder.Append(" шестьсот"); break;
                        case 7: resultBuilder.Append(" семьсот"); break;
                        case 8: resultBuilder.Append(" восемьсот"); break;
                        case 9: resultBuilder.Append(" девятьсот"); break;
                    } 
                    #endregion

                    #region Названия десятков и чисел от 11 до 19
                    switch (partDigits[1])
                    {
                        case 1:
                            switch (partDigits[2])
                            {
                                case 0: resultBuilder.Append(" десять"); break;
                                case 1: resultBuilder.Append(" одиннадцать"); break;
                                case 2: resultBuilder.Append(" двенадцать"); break;
                                case 3: resultBuilder.Append(" тринадцать"); break;
                                case 4: resultBuilder.Append(" четырнадцать"); break;
                                case 5: resultBuilder.Append(" пятнадцать"); break;
                                case 6: resultBuilder.Append(" шестнадцать"); break;
                                case 7: resultBuilder.Append(" семнадцать"); break;
                                case 8: resultBuilder.Append(" восемнадцать"); break;
                                case 9: resultBuilder.Append(" девятнадцать"); break;
                            }
                            break;
                        case 2: resultBuilder.Append(" двадцать"); break;
                        case 3: resultBuilder.Append(" тридцать"); break;
                        case 4: resultBuilder.Append(" сорок"); break;
                        case 5: resultBuilder.Append(" пятьдесят"); break;
                        case 6: resultBuilder.Append(" шестьдесят"); break;
                        case 7: resultBuilder.Append(" семьдесят"); break;
                        case 8: resultBuilder.Append(" восемьдесят"); break;
                        case 9: resultBuilder.Append(" девяносто"); break;
                    } 
                    #endregion

                    #region Названия единиц
                    if (partDigits[1] != 1)
                    {
                        switch (partDigits[2])
                        {
                            case 1: resultBuilder.Append(i != 2 ? " один" : " одна"); break;
                            case 2: resultBuilder.Append(i != 2 ? " два" : " две"); break;
                            case 3: resultBuilder.Append(" три"); break;
                            case 4: resultBuilder.Append(" четыре"); break;
                            case 5: resultBuilder.Append(" пять"); break;
                            case 6: resultBuilder.Append(" шесть"); break;
                            case 7: resultBuilder.Append(" семь"); break;
                            case 8: resultBuilder.Append(" восемь"); break;
                            case 9: resultBuilder.Append(" девять"); break;
                        }
                    } 
                    #endregion

                    switch (partDigits[2])
                    {
                        case 1:
                            resultBuilder.Append(partDigits[1] != 1 ? " " + orderNames[i, 0] : " " + orderNames[i, 2]); break;
                        case 2:
                        case 3:
                        case 4: resultBuilder.Append(partDigits[1] != 1 ? " " + orderNames[i, 1] : " " + orderNames[i, 2]); break;
                        default: resultBuilder.AppendFormat(" {0}", orderNames[i, 2]); break;
                    }                                        
                }
            }

            if (rubles == 0)
                resultBuilder.AppendFormat(" {0}", "0");

            switch (partDigits[2])
            {
                case 1:
                    resultBuilder.Append(partDigits[1] != 1 ? " рубль" : " рублей"); break;
                case 2:
                case 3:
                case 4: resultBuilder.Append(" рубля"); break;
                default: resultBuilder.AppendFormat(" рублей"); break;
            }


            #region Добавление копеек
            switch (copecks % 10)
            {
                case 1:
                    resultBuilder.AppendFormat(copecks != 11 ? " {0} копейка" : " {0} копеек", copecks); break;
                case 2:
                case 3:
                case 4: resultBuilder.AppendFormat(" {0} копейки", copecks); break;
                default: resultBuilder.AppendFormat(" {0} копеек", copecks); break;
            } 
            #endregion

            if (resultBuilder[0] == ' ')
            {
                resultBuilder.Remove(0, 1);
            }

            return new MvcHtmlString(resultBuilder.ToString());
        }
    }
}