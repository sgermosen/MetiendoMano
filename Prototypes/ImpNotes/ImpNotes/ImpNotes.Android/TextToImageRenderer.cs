using System;
using System.IO;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using ImpNotes.Droid;
using ImpNotes.Interface;
using Xamarin.Forms;
using ImpNotes.Extensions;

[assembly: Dependency(typeof(TextToImageRenderer))]
namespace ImpNotes.Droid
{
    public class TextToImageRenderer : ITextToImage
    {
        public Byte[] GetBytes(String text, string textColor, byte[] backGroundImageBytes, string imageName, float textSize = 15)
        {
            Bitmap backgroundImageBitmap = BitmapFactory.DecodeByteArray(backGroundImageBytes, 0, backGroundImageBytes.Length);

            var xamColor = GetXamarinColor(textColor);
            var textColorHEx = $"#{xamColor.ToHex()}";
            Android.Graphics.Color androidColor = Android.Graphics.Color.ParseColor(textColorHEx);

            var bitmapImage = textAsBitmap(text, textSize, androidColor, backgroundImageBitmap);
            if (bitmapImage == null)
                return null;

            var newBitMap = overlay(backgroundImageBitmap, bitmapImage);

            Byte[] bytes;
            using (var stream = new MemoryStream())
            {
                newBitMap.Compress(Bitmap.CompressFormat.Png, 0, stream); // You can change the compression asper your understanding
                bytes = stream.ToArray();
                SaveImage(imageName, bytes);
            }

            return bytes;
        }
        private Bitmap textAsBitmap(String text, float textSize, Android.Graphics.Color textColor, Bitmap backGroundBitMap)
        {
            try
            {
                var screenWidth = MainActivity.width;
                var screenHeight = MainActivity.height;

                Paint paint = new Paint(PaintFlags.AntiAlias);
                paint.Color = textColor;
                paint.TextAlign = Paint.Align.Left;

                int width = screenWidth - 20;
                int height = screenHeight - 20;

                Bitmap image = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);//Bitmap.Config.ARGB_8888
                Canvas canvas = new Canvas(image);
                return drawTextOnCanvas(canvas, text, paint, width, height, textSize, textColor, backGroundBitMap);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Bitmap drawTextOnCanvas(Canvas canvas, String text, Paint paint, int width, int height, float textSize, Android.Graphics.Color textColor, Bitmap backGroundBitMap)
        {


            canvas.DrawPaint(paint);

            //  canvas.DrawColor(Android.Graphics.Color.White);

            // Setup a textview like you normally would with your activity context
            TextView tv = new TextView(MainActivity.Instance);

            // setup text
            tv.SetText(text, TextView.BufferType.Normal);

            // maybe set textcolor
            tv.SetTextColor(textColor);

            tv.TextSize = textSize;

            // you have to enable setDrawingCacheEnabled, or the getDrawingCache will return null
            tv.DrawingCacheEnabled = true;

            // we need to setup how big the view should be..which is exactly as big as the canvas
            tv.Measure(Android.Views.View.MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly), Android.Views.View.MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly));

            // assign the layout values to the textview
            tv.Layout(0, 0, tv.MeasuredWidth, tv.MeasuredHeight);

            // draw the bitmap from the drawingcache to the canvas

            canvas.DrawBitmap(tv.DrawingCache, 0, 20, paint);

            return tv.DrawingCache;

            // disable drawing cache
            tv.DrawingCacheEnabled = (false);
        }

        private Xamarin.Forms.Color GetXamarinColor(string colorName)
        {

            switch (colorName)
            {
                case (""):
                    {
                        return Xamarin.Forms.Color.Default;
                    }
                case ("Accent"):
                    {
                        return Xamarin.Forms.Color.Accent;
                    }
                default:
                    {
                        var converter = new ColorTypeConverter();
                        var result = (Xamarin.Forms.Color)converter.ConvertFromInvariantString(colorName);
                        return result;
                    }
            }

        }

        private bool SaveImage(string filename, byte[] imageData)
        {
            try
            {
                CreateFolder();
                var dir = new Java.IO.File
                    (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Pictures/MobileApp");
                string filePath = System.IO.Path.Combine(dir.Path, filename);
                File.WriteAllBytes(filePath, imageData);
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        private void CreateFolder()
        {
            var dir = new Java.IO.File
                (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Pictures/MobileApp");
            if (!dir.Exists())
                dir.Mkdirs();
        }

        private Bitmap overlay(Bitmap bmp1, Bitmap bmp2)
        {
            int borderSize = 20;
            Bitmap bmp = Bitmap.CreateBitmap(bmp2.Width + borderSize * 2, bmp2.Height + borderSize * 2, bmp2.GetConfig());

            Bitmap bmOverlay = Bitmap.CreateBitmap(bmp2.Width, bmp2.Height, bmp1.GetConfig());
            float left = (bmp2.Width - (bmp1.Width * ((float)bmp2.Height / (float)bmp1.Height))) / (float)2.0;
            float bmp1newW = bmp1.Width * ((float)bmp2.Height / (float)bmp1.Height);
            Bitmap bmp1new = getResizedBitmap(bmp1, bmp2.Height, (int)bmp1newW);
            Canvas canvas = new Canvas(bmOverlay);
            canvas.DrawBitmap(bmp1new, left, 0, null);
            // Rect dstRectForRender = new Rect(X - halfWidth, Y - halfHeight, X + halfWidth, Y + halfHeight);
            canvas.DrawBitmap(bmp2, null, new Rect(30, 30, bmp2.Width - 30, bmp2.Height), null);
            return bmOverlay;
        }

        private Bitmap getResizedBitmap(Bitmap bm, int newHeight, int newWidth)
        {
            int width = bm.Width;
            int height = bm.Height;
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;
            Matrix matrix = new Matrix();
            matrix.PostScale(scaleWidth, scaleHeight);
            Bitmap resizedBitmap = Bitmap.CreateBitmap(bm, 0, 0, width, height, matrix, false);
            return resizedBitmap;
        }


        //public byte[] ConvertStreamToByteArray(Stream stream)
        //{
        //    try
        //    {
        //        byte[] imgBytes;
        //        using (var streamReader = new MemoryStream())
        //        {
        //            stream.CopyTo(streamReader);
        //            imgBytes = streamReader.ToArray();
        //        }
        //        return imgBytes;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        //private Bitmap CropBitmapTransparency(Bitmap sourceBitmap)
        //{
        //    int minX = sourceBitmap.Width;
        //    int minY = sourceBitmap.Height;
        //    int maxX = -1;
        //    int maxY = -1;
        //    for (int y = 0; y < sourceBitmap.Height; y++)
        //    {
        //        for (int x = 0; x < sourceBitmap.Width; x++)
        //        {
        //            int alpha = (sourceBitmap.GetPixel(x, y) >> 24) & 255;
        //            if (alpha > 0)   // pixel is not 100% transparent
        //            {
        //                if (x < minX)
        //                    minX = x;
        //                if (x > maxX)
        //                    maxX = x;
        //                if (y < minY)
        //                    minY = y;
        //                if (y > maxY)
        //                    maxY = y;
        //            }
        //        }
        //    }
        //    if ((maxX < minX) || (maxY < minY))
        //        return null; // Bitmap is entirely transparent

        //    return Bitmap.CreateBitmap(sourceBitmap, minX, minY, (maxX - minX) + 1, (maxY - minY) + 1);
        //}

    }
}