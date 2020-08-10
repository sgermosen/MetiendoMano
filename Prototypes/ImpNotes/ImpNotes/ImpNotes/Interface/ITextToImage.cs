using System;
using System.Collections.Generic;
using System.Text;

namespace ImpNotes.Interface
{
    public interface ITextToImage
    {
        Byte[] GetBytes(String text, string textColor, byte[] backGroundImageBytes, string imageName, float textSize = 15);
        //Task<ImageSource> GetImageFromText(String text, float textSize, Xamarin.Forms.Color textColor);
    }
}
