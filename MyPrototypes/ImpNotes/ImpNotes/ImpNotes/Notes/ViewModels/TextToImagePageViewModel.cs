using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ImpNotes.Interface;
using ImpNotes.Models;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace ImpNotes.Notes.ViewModels
{
    public class TextToImagePageViewModel : NotesBaseViewModel
    {
        public static byte[] BackgroundImageBytes;
        //  public static ImageSource StaticBackgroundImg;

        private ImageSource _backgroundImg;
        public ImageSource BackgroundImg
        {
            get => _backgroundImg;
            set {
                _backgroundImg = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _textImage;
        public ImageSource TextImage
        {
            get => _textImage;
            set {
                _textImage = value;
                RaisePropertyChanged();
            }
        }


        private bool _isRunning = false;
        public bool IsRunning
        {
            get => _isRunning;
            set {
                _isRunning = value; RaisePropertyChanged();
            }
        }

        private bool _isViewEnable = true;
        public bool IsViewEnable
        {
            get => _isViewEnable;
            set {
                _isViewEnable = value; RaisePropertyChanged();
            }
        }


        private string _fontSize = "15";
        public string FontSize
        {
            get => _fontSize;
            set {
                _fontSize = value; RaisePropertyChanged();
            }
        }

        private string _wordsCount = "100";
        public string WordsCount
        {
            get => _wordsCount;
            set {
                _wordsCount = value; RaisePropertyChanged();
            }
        }

        private ObservableCollection<ImagesModel> _notesList;
        public ObservableCollection<ImagesModel> NotesList
        {
            get => _notesList;
            set {
                _notesList = value;
                RaisePropertyChanged();
            }
        }




        public RelayCommand SetImageCommand => new RelayCommand(SetImageClick);

        private async void SetImageClick()
        {
            try
            {
                var result = await new Services.MediaService().SelectSource();

                if (result == null) return;

                BackgroundImageBytes = result.Item3;
                BackgroundImg = ImageSource.FromStream(() => new MemoryStream(result.Item3));
                //  StaticBackgroundImg = result.Item1;
            }
            catch (Exception ex)
            {

            }

        }

        public RelayCommand ProcessCommand => new RelayCommand(ProcessClick);

        private async void ProcessClick()
        {
            NotesList = new ObservableCollection<ImagesModel>();

            try
            {



                if (BackgroundImageBytes == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please set background image to proceed", "okay");
                    return;
                }

                if (String.IsNullOrEmpty(WordsCount) || WordsCount == "0")
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Words Count cannot be 0", "okay");

                    return;

                }


                var testArr = Text.Split(' ').ToList();
                var testArrList = testArr.SplitList(Convert.ToInt32(WordsCount));

                bool isSuccess = true;

                IsRunning = true;
                IsViewEnable = false;
                await Task.Run(() =>
                {
                    int i = 1;
                    foreach (var item in testArrList)
                    {
                        var sentence = string.Join(" ", item);
                        var imageName = $"MobileApp_{i}_{Guid.NewGuid()}.jpg";
                        var byteArray = DependencyService.Get<ITextToImage>().GetBytes(sentence, SelectedTextColor, BackgroundImageBytes, imageName, Convert.ToInt32(FontSize));

                        i++;

                        if (byteArray == null)
                        {
                            isSuccess = false;
                            break;
                        };

                    }

                });

                IsRunning = false;
                IsViewEnable = true;

                if (isSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("Meesage", "Please check your images in MobileApp Folder in Pictures", "Ok");

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Sorry something went wrong", "Ok");

                }
            }
            catch (Exception ex)
            {

            }
            // foreach



        }

        internal void Initilize(NotesModel notes)
        {
            Text = notes.Text;
            SelectedTextColor = notes.TextColor;

            //if (StaticBackgroundImg != null)
            //{
            //    BackgroundImg = StaticBackgroundImg;
            //}

        }


       
        
    }

   
}
