using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TemplateEngine;
using TemplateEngine.Docx;
using TemplateEngine.Docx.Processors;

namespace NoteDocumentCreator
{
    public class StaffMovementsDocsCreator
    {
        public static byte[] CreateAgreementDoc(string WebPath, string AgreementNumber, string AgreementDate, string UserName, string SignerName, string TargetPosition, string TargetDepartment, string SignerShortName, string SignerPositionWithDepartment, string UserShortName)
        {
            FileInfo file = new FileInfo(Path.Combine(WebPath, @"StaffMovements\Dogovor.docx"));
            string newfilename = Path.Combine(WebPath,Guid.NewGuid().ToString()+".docx");
            var newfile = file.CopyTo(newfilename,true);
            using (var outputDocument = new TemplateProcessor(newfilename)
                .SetRemoveContentControls(true))
            {     
                var documentenc = Encoding.GetEncoding(outputDocument.Document.Declaration.Encoding);
                var valuesToFill = new Content(
                    new FieldContent("AgreementNumber", AgreementNumber),
                    new FieldContent("AgreementDate", AgreementDate),
                    new FieldContent("UserName", UserName),
                    new FieldContent("Signer", SignerName),
                    new FieldContent("TargetPosition", TargetPosition),
                    new FieldContent("TargetDepartment", TargetDepartment),
                    new FieldContent("SignerShortName", SignerShortName),
                    new FieldContent("SignerPositionWithDepartment", SignerPositionWithDepartment),
                    new FieldContent("UserShortName", UserShortName)
                );
                outputDocument.FillContent(valuesToFill);                            
                outputDocument.SaveChanges();                
            }
            StreamReader reader = new StreamReader(newfilename);
            var result =  NoteCreator.ReadFull(reader.BaseStream);
            //newfile.Delete();
            return result;
        }
        public static string ConvertEncoding(string source,Encoding target)
        {
            return target.GetString(Encoding.Convert(Encoding.Default, target, Encoding.Default.GetBytes(source)));
        }
    }
}
