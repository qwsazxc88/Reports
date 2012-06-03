using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using log4net;
using NHibernate.Util;
using NHibernate.Dialect;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using System.Globalization;
using Environment=System.Environment;
using StringHelper=NHibernate.Util.StringHelper;

namespace Reports.SchemaGen
{

    class Program
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
        static int Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(); 
                string scriptDir = Settings.Default.ScriptsDir;
                if (!Path.IsPathRooted (scriptDir))
                {
                    scriptDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scriptDir);
                }
                     
                Console.WriteLine("Generating script...");

                Configuration cfg = new Configuration();
                cfg.Configure();

                Dialect dialect = Dialect.GetDialect(cfg.Properties);
                string[] dropStatements = cfg.GenerateDropSchemaScript(dialect);
                string[] createStatements = cfg.GenerateSchemaCreationScript(dialect);

                using (StreamWriter writer = new StreamWriter(Path.Combine(scriptDir, Settings.Default.ScriptToGenerate), false, Encoding.Default))
                {
                    foreach (string s in dropStatements)
                    {
                        string f = Format(ParseDropConstraint(s));
                        writer.WriteLine(f);
                    }

                    writer.WriteLine();

                    foreach (string s in createStatements)
                    {
                        string f = Format(ParseCreateTable(s));
                        writer.WriteLine(f);
                    }

                    foreach (string scriptToAppend in Settings.Default.ScriptsToAppend)
                    {
                        writer.WriteLine();
                        writer.WriteLine(File.ReadAllText(Path.Combine(scriptDir, scriptToAppend),Encoding.Default));
                    }
                }
                Console.WriteLine("Done");
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                return 1;
            }

            return 0;
        }

        static string GetTableName(string statement, string prefix)
        {
            string token = statement.Substring(prefix.Length).TrimStart();
            int index = -1;
            if (token[0] == '[')
                index = token.IndexOf(']');
            else
                index = token.IndexOfAny(new char[] { ' ', '\t' });

            return token.Substring(0, index + 1);
        }
        static string ParseCreateTable(string statement)
        {
            string prefix = "create table";
            if (statement.StartsWith(prefix))
            {
                string tableName = GetTableName(statement, prefix).Trim('[', ']');
                statement = statement.Replace("primary key", "constraint " + "PK_" + tableName + " primary key");
                statement = statement.Replace("unique (", "constraint " + "UK_" + tableName + " unique (");
            }
            return statement;

        }
        static string ParseDropConstraint (string statement)
        {
            string prefix = "alter table";
            if (statement.StartsWith(prefix))
            {
                string tableName = GetTableName(statement, prefix);
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("if exists (select * from dbo.sysobjects where id = object_id(N'{0}') and OBJECTPROPERTY(id, N'IsUserTable') = 1)", tableName);
                builder.Append(" ");
                builder.Append(statement);
                return builder.ToString();
            }
            return statement;
        }

        private static string Format(string sql)
        {
            if (sql.IndexOf("\"") > 0 || sql.IndexOf("'") > 0)
            {
                return sql;
            }

            string formatted;

            if (sql.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("create table"))
            {
                StringBuilder result = new StringBuilder(60);
                StringTokenizer tokens = new StringTokenizer(sql, "(,)", true);

                int depth = 0;

                foreach (string tok in tokens)
                {
                    if (StringHelper.ClosedParen.Equals(tok))
                    {
                        depth--;
                        if (depth == 0)
                        {
                            result.AppendLine();
                        }
                    }
                    result.Append(tok);
                    if (StringHelper.Comma.Equals(tok) && depth == 1)
                    {
                        result.AppendLine().Append(' ');
                    }
                    if (StringHelper.OpenParen.Equals(tok))
                    {
                        depth++;
                        if (depth == 1)
                        {
                            result.AppendLine().Append(' ');
                        }
                    }
                }

                formatted = result.ToString();
            }
            else
            {
                formatted = sql;
            }

            return formatted;
        }

    }
}
