using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
namespace NoteDocumentCreator
{
    public class NoteCreator
    {
        public static byte[] CreateNote(string WebPath,string To, string From, string Theme, DateTime Date, string Reason, string Departments, string Position, int PositionCount, decimal Salary, decimal Premium)
        {
            StreamReader reader = new StreamReader(Path.Combine(WebPath,@"Template\word\document.xml"));
            string data = reader.ReadToEnd();
            reader.Close();
            data = data.Replace("{{FROM}}", From);
            data = data.Replace("{{THEME}}", Theme);
            data = data.Replace("{{DATE}}", Date.ToShortDateString());
            data = data.Replace("{{REASON}}", Reason);
            data = data.Replace("{{DEPARTMENTS}}", Departments);
            data = data.Replace("{{POSITION}}", Position);
            data = data.Replace("{{POSITIONCOUNT}}", PositionCount.ToString());
            data = data.Replace("{{SALARY}}", Salary.ToString());
            data = data.Replace("{{PREMIUM}}", Premium.ToString());
            data = data.Replace("{{TOTALSUM}}", (Salary + Premium).ToString());
            StreamWriter writer = new StreamWriter(Path.Combine(WebPath,@"doc\word\document.xml"));
            writer.WriteLine(data);
            writer.Flush(); writer.Close();
            string filename = Path.Combine(WebPath,Guid.NewGuid().ToString()+".docx");
            FileInfo file = new FileInfo(filename);
            if (file.Exists) file.Delete();
            CreateZipFromFolder(filename, Path.Combine(WebPath,@"doc"), CompressionOption.Normal);
            StreamReader r = new StreamReader(filename);
            var b = ReadFull(r.BaseStream);
            r.Close();
            File.Delete(filename);
            return b;
            
        }
        private const long BUFFER_SIZE = 4096;

        public static void CreateZipFromFolder(string result,string folder,CompressionOption compressionOption)
        {
            if (File.Exists(result)) File.Delete(result);
            var zip = Package.Open(result,FileMode.CreateNew);
            DirectoryInfo dir = new DirectoryInfo(folder);
            foreach (var el in GetDirFiles(folder))
            {
                string filename = el;
                
                AddFile(zip, dir.FullName,@"/"+ filename.Replace("\\","/"),compressionOption);
            }
            zip.Flush();
            zip.Close();
            
        }
        private static List<string> GetDirFiles(string path,string parent="")
        {
            if (!string.IsNullOrWhiteSpace(parent)) parent += "/";
            DirectoryInfo dir = new DirectoryInfo(path);
            var result = dir.EnumerateFiles().Where(x=>!x.Name.Contains("[Content_Types].xml")).Select(x=>parent+x.Name).ToList();
            foreach(var d in dir.EnumerateDirectories())
            {
                result.AddRange(GetDirFiles(d.FullName,parent+ d.Name));
            }
            return result.ToList();
            
        }
        public static void AddFile(Package result,string baseFolder,string relativePath,CompressionOption compressionOption)
        {
            FileInfo f = new FileInfo(baseFolder+relativePath.Replace("/","\\"));
            //f.Extension;
            PackagePart data = result.CreatePart(new Uri(relativePath, UriKind.Relative), GetContentType(f.Name), compressionOption);
            StreamReader reader = new StreamReader(baseFolder+relativePath.Replace("/","\\"));
            WriteAll(data.GetStream(), reader.BaseStream);
            reader.Close();
        }
        private static string GetContentType(string filename)
        {
            if (filename.Contains(".rels")) return "application/vnd.openxmlformats-package.relationships+xml";
            if (filename.Contains(".jpeg")) return "image/jpeg";
            if (filename.Contains(".jpg")) return "image/jpeg";
            switch (filename)
            {
               case "document.xml": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml";
               case "styles.xml": return "application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml";
               case "app.xml": return "application/vnd.openxmlformats-officedocument.extended-properties+xml";
               case "settings.xml": return "application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml";
               case "theme1.xml": return "application/vnd.openxmlformats-officedocument.theme+xml";
               case "fontTable.xml": return "application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml";
               case "webSettings.xml": return "application/vnd.openxmlformats-officedocument.wordprocessingml.webSettings+xml";
               case "core.xml": return "application/vnd.openxmlformats-package.core-properties+xml";
            }
            if (filename.Contains(".xml")) return "application/xml";
            
            return "";
        }
        public static void WriteAll(Stream target, Stream source)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }
        public static byte[] ReadFull(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }
    }
}
