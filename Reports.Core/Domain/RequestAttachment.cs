using System;
using System.IO;
using System.Reflection;
using ICSharpCode.SharpZipLib.GZip;
using log4net;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Attachment
    /// </summary>
    public class RequestAttachment : AbstractEntityWithVersion //AbstractEntity
    {
        #region Fields

        #endregion

        #region Properties

        public virtual string FileName { get; set; }
        public virtual string ContextType { get; set; }
        protected virtual byte[] Context { get; set; }
        public virtual byte[] UncompressContext
        {
            get { return ContextCompressor.Decompress(Context); }
            set
            {
               Context = ContextCompressor.Compress(value);
            }

        }
        public virtual int RequestId { get; set; }
        public virtual int RequestType { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string Description { get; set; }
        //public virtual Document Document { get; set; }

        #endregion

        #region Constructors

        #endregion
    }
    static class ContextCompressor
    {
        static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static byte[] Compress(byte[] buffer)
        {
            Log.DebugFormat("Initial buffer size {0}",buffer.Length);
            MemoryStream ms = new MemoryStream();
            //GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            GZipOutputStream zip = new GZipOutputStream(ms) { IsStreamOwner = false };
            zip.SetLevel(9);
            //BZip2OutputStream zip = new BZip2OutputStream(ms) { IsStreamOwner = false };
            zip.Write(buffer, 0, buffer.Length);
            zip.Close();
            ms.Position = 0;

            //MemoryStream outStream = new MemoryStream();
           
            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);
            Log.DebugFormat("Compressed buffer size {0}", compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return gzBuffer;
        }

        public static byte[] Decompress(byte[] gzBuffer)
        {
            MemoryStream ms = new MemoryStream();
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

            byte[] buffer = new byte[msgLength];

            ms.Position = 0;
            //GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            GZipInputStream zip = new GZipInputStream(ms);
            //BZip2InputStream zip = new BZip2InputStream(ms);
            zip.Read(buffer, 0, buffer.Length);

            return buffer;
        }
    }

}