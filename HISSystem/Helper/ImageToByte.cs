using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Helper
{
    public class ImageToByte
    {
        public byte[] GetFileBits(IFormFile file)
        {
            //byte[] bytes = File.ReadAllBytes(file);
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                //string s = Convert.ToBase64String(fileBytes);
                return fileBytes;
            }
            
        }
    }
}
