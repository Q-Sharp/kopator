using System;
using System.Drawing;

namespace kopator
{
    public static class Extension
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
