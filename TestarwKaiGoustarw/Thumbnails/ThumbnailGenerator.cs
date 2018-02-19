using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TestarwKaiGoustarw.Thumbnails
{
    public class ThumbnailGenerator
    {
        private const string FILEPATH = @"C:\FileStorageSource\Thumbs\4k.jpg";
        private const string DEST_ASPECT = @"C:\FileStorageSource\Thumbs\thumbAspect.jpg";
        private const string DEST = @"C:\FileStorageSource\Thumbs\thumb.jpg";
        private static int BOXHEIGHT = 50;
        private static int BOXWIDTH = 50;

        public void CreateThumbnail()
        {
            Image image = Image.FromFile(FILEPATH);
            Image thumb = image.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
            thumb.Save(Path.ChangeExtension(DEST, "thumb"));
        }

        public void ResizeAndKeepAspect()
        {
            File.Delete(DEST_ASPECT);

            Bitmap original, resizedImage;

            try
            {
                using (var fs = new FileStream(FILEPATH, FileMode.Open))
                {
                    original = new Bitmap(fs);
                }

                int rectHeight = BOXHEIGHT;
                int rectWidth = BOXWIDTH;

                //if the image is squared set it's height and width to the smallest of the desired dimensions (our box). In the current example rectHeight<rectWidth
                if (original.Height == original.Width)
                {
                    resizedImage = new Bitmap(original, rectHeight, rectHeight);
                }
                else
                {
                    //calculate aspect ratio
                    float aspect = original.Width / (float)original.Height;

                    //calculate new dimensions based on aspect ratio
                    int newHeight = rectHeight;
                    var newWidth = (int)(newHeight * aspect);


                    resizedImage = new Bitmap(original, newWidth, newHeight);
                    resizedImage.Save(DEST_ASPECT);

                    //Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                    //thumb.Save(Path.ChangeExtension(fileName, "thumb"));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}