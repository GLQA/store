using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Store.Helpers
{
    public class MD5Checker
    {
        public string CheckMD5(string pathToFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(pathToFile))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }
    }
}
