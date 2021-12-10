using BingoCard.Models;
using BingoCard.Outputs;
using BingoCard.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BingoCard.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private CardOutput cardOutput;
        private ObservableCollection<BingoNumber> bNumbers = new ObservableCollection<BingoNumber>();
        private ObservableCollection<BingoNumber> iNumbers = new ObservableCollection<BingoNumber>();
        private ObservableCollection<BingoNumber> nNumbers = new ObservableCollection<BingoNumber>();
        private ObservableCollection<BingoNumber> gNumbers = new ObservableCollection<BingoNumber>();
        private ObservableCollection<BingoNumber> oNumbers = new ObservableCollection<BingoNumber>();
        public Command OnClickBingoNumberCommand { get; set; }
        public Command OnClickSubmitCommand { get; set; }
        private string message = "";

        public ObservableCollection<BingoNumber> BNumbers
        {
            get => bNumbers;
            set => SetProperty(ref bNumbers, value);
        }

        public ObservableCollection<BingoNumber> INumbers
        {
            get => iNumbers;
            set => SetProperty(ref iNumbers, value);
        }

        public ObservableCollection<BingoNumber> NNumbers
        {
            get => nNumbers;
            set => SetProperty(ref nNumbers, value);
        }

        public ObservableCollection<BingoNumber> GNumbers
        {
            get => gNumbers;
            set => SetProperty(ref gNumbers, value);
        }

        public ObservableCollection<BingoNumber> ONumbers
        {
            get => oNumbers;
            set => SetProperty(ref oNumbers, value);
        }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public async void OnInitialize()
        {
            cardOutput = await RESTServices.GetCard();
            // B
            for (int i = 0; i < cardOutput.card.B.Count; i++)
                BNumbers.Add(new BingoNumber(cardOutput.card.B[i]));
            // I
            for (int i = 0; i < cardOutput.card.B.Count; i++)
                INumbers.Add(new BingoNumber(cardOutput.card.I[i]));
            // N
            for (int i = 0; i < cardOutput.card.B.Count; i++)
                NNumbers.Add(new BingoNumber(cardOutput.card.N[i]));
            // G
            for (int i = 0; i < cardOutput.card.B.Count; i++)
                GNumbers.Add(new BingoNumber(cardOutput.card.G[i]));
            // O
            for (int i = 0; i < cardOutput.card.B.Count; i++)
                ONumbers.Add(new BingoNumber(cardOutput.card.O[i]));
            CallOnPropertiesChanged();
        }

        private void OnClickBingoNumber(string indexes)
        {
            string[] location = indexes.Split(',');
            switch(location[0])
            {
                case "0":
                    BNumbers[int.Parse(location[1])].IsNotChosen = false;
                    break;
                case "1":
                    INumbers[int.Parse(location[1])].IsNotChosen = false;
                    break;
                case "2":
                    NNumbers[int.Parse(location[1])].IsNotChosen = false;
                    break;
                case "3":
                    GNumbers[int.Parse(location[1])].IsNotChosen = false;
                    break;
                case "4":
                    ONumbers[int.Parse(location[1])].IsNotChosen = false;
                    break;
                default:
                    break;
            }
            CallOnPropertiesChanged();
        }

        private void CallOnPropertiesChanged()
        {
            // https://stackoverflow.com/questions/46751563/bind-an-element-of-array-to-a-xaml-property
            // since dili man ka notify ang single index na change
            // so mao e explicit call ang kaning mga properties para maupdate ang change
            OnPropertyChanged("BNumbers");
            OnPropertyChanged("INumbers");
            OnPropertyChanged("NNumbers");
            OnPropertyChanged("GNumbers");
            OnPropertyChanged("ONumbers");
        }

        private async void OnClickSubmit()
        {
            bool isWinningCard = await RESTServices.IsWinningCard(cardOutput.playcard_token);
            Message = isWinningCard ? "You won." : "Not a winning card";
        }

        public MainViewModel()
        {
            OnInitialize();
            OnClickBingoNumberCommand = new Command<string>(OnClickBingoNumber);
            OnClickSubmitCommand = new Command(OnClickSubmit);
        }
    }
}
