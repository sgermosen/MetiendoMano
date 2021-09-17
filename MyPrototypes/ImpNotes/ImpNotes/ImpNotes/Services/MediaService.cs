using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImpNotes.Interface;
using Xamarin.Forms;

namespace ImpNotes.Services
{
    public class MediaService
    {

        #region SelectSource
        public async Task<Tuple<ImageSource, string, Byte[], Stream>> SelectSource()
        {
            try
            {
                var selectedItem = await App.Current.MainPage.DisplayActionSheet("Choose Media", "Cancel",
                    null, "Take Photo", "Browse From Album");

                Tuple<ImageSource, string, Byte[], Stream> src = null;
                switch (selectedItem)
                {
                    case "Take Photo":
                        src = await TakePhoto();
                        var imgName = $"BackImage{Guid.NewGuid()}.jpg";
                        DependencyService.Get<IManageFiles>().SaveImage(imgName, src.Item3);
                        var bytes = DependencyService.Get<IManageFiles>().GetImage(imgName);
                        return new Tuple<ImageSource, string, Byte[], Stream>(null, imgName, bytes, null);

                        break;
                    case "Browse From Album":
                        src = await PickPhoto();
                        break;
                }

                return src;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region PickPhoto
        public async Task<Tuple<ImageSource, string, Byte[], Stream>> PickPhoto()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {

                    await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to take photo", "OK");

                    return new Tuple<ImageSource, string, Byte[], Stream>(null, ":( Permission not granted to take photo", null, null);
                }

                // string imageName = "Ld" + new Guid();
                MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    CustomPhotoSize = 100,
                    PhotoSize = PhotoSize.Medium,
                });

                var words = file.Path.Split('/');
                var path = words[words.Length - 1];
                return new Tuple<ImageSource, string, Byte[], Stream>(ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                }), path, ConvertStreamToByteArray(file.GetStream()), file.GetStream());


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region TakePhoto
        public async Task<Tuple<ImageSource, string, Byte[], Stream>> TakePhoto()
        {
            try
            {
                if (!CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Message", "Camera not Available!", "Okay");

                    return new Tuple<ImageSource, string, Byte[], Stream>(null, ":( Camera not Available!", null, null);
                }
                string imageName = "ErpTestImage_" + Guid.NewGuid().ToString() + ".jpg";
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    Directory = "ERPWeb",
                    SaveToAlbum = true,
                    Name = imageName,
                    CompressionQuality = 92,

                    CustomPhotoSize = 15,

                });

                //  var path = file.Path;
                return new Tuple<ImageSource, string, Byte[], Stream>(ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                }), imageName, ConvertStreamToByteArray(file.GetStream()), file.GetStream());

                //var promptConfig = new PromptConfig();
                //promptConfig.InputType = InputType.Name;
                //promptConfig.IsCancellable = true;
                //promptConfig.Message = "Enter Image Name";
                //var result = await UserDialogs.Instance.PromptAsync(promptConfig);
                //if (result.Ok)
                //{
                //    string imageName = result.Text;
                //    MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                //    {
                //        Directory = "Ezblast",
                //        SaveToAlbum = true,
                //        Name = imageName
                //    });

                //    //  var path = file.Path;
                //    return new Tuple<ImageSource, string, Byte[], Stream>(ImageSource.FromStream(() =>
                //    {
                //        var stream = file.GetStream();
                //        file.Dispose();
                //        return stream;
                //    }), imageName, ConvertStreamToByteArray(file.GetStream()), file.GetStream());
                //}

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region PickVideo
        public async Task<Tuple<ImageSource, string, byte[], Stream>> PickVideo()
        {
            try
            {
                if (!CrossMedia.Current.IsPickVideoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                    return null;
                }
                var file = await CrossMedia.Current.PickVideoAsync();
                if (file == null)
                    return null;
                var words = file.Path.Split('/');
                var path = words[words.Length - 1];

                return new Tuple<ImageSource, string, byte[], Stream>(null, path, ConvertStreamToByteArray(file.GetStream()), file.GetStream());
                var filePath = file.Path;
                await App.Current.MainPage.DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
                file.Dispose();

                // return filePath;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region TakeVideo
        public async Task<string> TakeVideo()
        {
            if (!CrossMedia.Current.IsTakeVideoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Message", "Camera not Available !", "Okay");
                return null;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Name = "video.mp4",
                Directory = "DefaultVideos",
            });

            if (file == null)
                return null;

            var filePath = file.Path;


            await App.Current.MainPage.DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

            file.Dispose();

            return filePath;
        }
        #endregion

        #region ConvertStreamToByteArray
        public byte[] ConvertStreamToByteArray(Stream stream)
        {
            try
            {
                byte[] imgBytes;
                using (var streamReader = new MemoryStream())
                {
                    stream.CopyTo(streamReader);
                    imgBytes = streamReader.ToArray();
                }
                return imgBytes;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion
    }
}
