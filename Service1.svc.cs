using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StreamingUpload
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
      
        public void Upload(RemoteFileInfo objRemoteFile)
        {

            string directoryPath = @"C:\NewFile\Practic101\In\";
            if(!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, objRemoteFile.FileName);
            List<string> str = new List<string>();
            
            using (Stream stream = objRemoteFile.Data) // data is the Stream value.
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Append))
                    {
                        using (BinaryWriter writer = new BinaryWriter(fs))
                        {
                            byte[] buffer;
                            do
                            {
                                buffer = reader.ReadBytes(4096);
                                writer.Write(buffer);
                            } while (buffer.Length > 0);
                        }
                    }
                }
            }
            
        }
    }
}
