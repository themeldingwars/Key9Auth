using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using System.IO;

namespace ShiteLauncher
{
    public class Key9Interface
    {
        const int TOKEN_LENGTH    = 512;
        const int USERNAME_LENGTH = 256;

        public struct MemMapedData
        {
            public string Token;
            public string Username;
        }

        public MemoryMappedFile MFile;

        public void Init()
        {
            MFile = MemoryMappedFile.CreateNew("TMW_LAUNCHER_LOGIN_DATA", 1000);
        }

        public void CleanUp()
        {
            MFile.Dispose();
        }

        public void SetData(MemMapedData data)
        {
            using (MemoryMappedViewStream stream = MFile.CreateViewStream())
            {
                using (BinaryWriter w = new BinaryWriter(stream))
                {
                    w.Write(data.Token.Length);
                    w.Write(data.Token.PadRight(TOKEN_LENGTH, (char)0).ToCharArray());
                    w.Write(data.Username.Length);
                    w.Write(data.Username.PadRight(USERNAME_LENGTH, (char)0).ToCharArray());
                }
            }
        }
    }
}
