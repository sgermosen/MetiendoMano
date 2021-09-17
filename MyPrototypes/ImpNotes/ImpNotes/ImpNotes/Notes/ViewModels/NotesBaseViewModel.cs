using System;
using System.Collections.Generic;
using System.Text;
using ImpNotes.ViewModels;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace ImpNotes.Notes.ViewModels
{
    public class NotesBaseViewModel: ViewModelBase
    {
       // public INavigation Navigation { get; }

        private bool isRunning;
        private bool isEnabled;

        public bool IsRunning
        {
            get => this.isRunning;
           set {
                this.isRunning = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set {
                this.isEnabled = value;
                RaisePropertyChanged();
            } 
        }



        private string _selectedTextColor;
        public string SelectedTextColor
        {
            get => _selectedTextColor;
            set {
                _selectedTextColor = value;
                RaisePropertyChanged();
            }
        }


        private string _text;

        

        public string Text
        {
            get => _text;
            set {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private string _title;



        public string Title
        {
            get => _title;
            set {
                _title = value;
                RaisePropertyChanged();
            }
        }
    }
}
