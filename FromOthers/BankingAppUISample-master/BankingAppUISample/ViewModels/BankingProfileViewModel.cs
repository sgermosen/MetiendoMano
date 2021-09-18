using System;
using System.Collections.ObjectModel;
using BankingAppUISample.Models;

namespace BankingAppUISample.ViewModels
{
    public class BankingProfileViewModel
    {
        public ObservableCollection<Cards> cards { get; set; }
        public ObservableCollection<History> history { get; set; }

        public BankingProfileViewModel()
        {
            cards = new ObservableCollection<Cards>
            {
                new Cards
                {
                    CardImage="Card1",
                    CardBussinessCategory ="BUSINESS CARD",
                    CardNumber="4565",
                    CardType="Mastercard",
                    CardExpirationDate="05/20"
                },
                new Cards
                {
                    CardImage="Card2",
                    CardBussinessCategory ="PERSONAL CARD",
                    CardNumber="5664",
                    CardType="VISA",
                    CardExpirationDate="04/21"
                },
                new Cards
                {
                    CardImage="Card1",
                    CardBussinessCategory ="BUSINESS CARD",
                    CardNumber="4445",
                    CardType="Mastercard",
                    CardExpirationDate="05/22"
                }
            };

            history = new ObservableCollection<History>
            {

                new History
                {
                    Picture="Netflix",
                    Name="Netflix",
                    Price="$12.99",
                    Date="24/12/2019"
                },
                new History
                {
                    Picture="Dropbox",
                    Name="Dropbox",
                    Price="$12.99",
                    Date="24/12/2019"
                },
                new History
                {
                    Picture="XBoxLive",
                    Name="XBox Live",
                    Price="$12.99",
                    Date="24/12/2019"
                }
            };
        }
    }
}
