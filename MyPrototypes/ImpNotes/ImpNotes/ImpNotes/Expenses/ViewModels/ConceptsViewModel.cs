using ImpNotes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ImpNotes.Models;
using ImpNotes.ViewModels;

namespace ImpNotes.Expenses.ViewModels
{
    public class ConceptsViewModel : BaseViewModel
    {

        private ObservableCollection<Concept> _concepts;
        public ObservableCollection<Concept> Concepts
        {
            get { return _concepts; }
            set { SetProperty(ref _concepts, value); }
        }

        private Concept _concept;
        public Concept Concept
        {
            get { return _concept; }
            set { SetProperty(ref _concept, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public void Initilize()
        {
            try
            {
                var list = new DataService().GetList<Concept>(false);

                Concepts = new ObservableCollection<Concept>(list);
            }
            catch (Exception ex)
            {
                Concepts = new ObservableCollection<Concept>();
            }

        }

        public async void SaveAdd()
        {
            if (String.IsNullOrEmpty(Name))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Name field cannot be empty !", "ok");
                return;
            }

            DataService dataService = new DataService();

            dataService.Add(new Concept
            {
                Name = Name,
                Description = Description,
                AmountWasted = 0,
                AmountGain = 0
            });

        }


    }
}
