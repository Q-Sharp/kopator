using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kopator
{
    public static class Extenstions
    {
        public static bool IsImage(this string file, out Image thumbnail)
        {
            Image imgInput = null;
            Graphics gInput = null;
            var isImage = false;

            try
            {
                imgInput = Image.FromFile(file);
                gInput = Graphics.FromImage(imgInput);
                
                thumbnail = imgInput.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                isImage = true;
            }
            finally
            {
                imgInput?.Dispose(); 
                gInput?.Dispose(); 
            }

            return isImage;
        }
    }
}
