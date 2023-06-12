using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IXmlLoaderService<Cards> xmlLoaderService;
        private readonly IOpenFileDialogService openFileDialogService;
        private readonly DBAccess dBAccess;

        public MainViewModel(IXmlLoaderService<Cards> xmlLoaderService, IOpenFileDialogService openFileDialogService, DBAccess dBAccess)
        {
            this.xmlLoaderService = xmlLoaderService;
            this.openFileDialogService = openFileDialogService;
            this.dBAccess = dBAccess;

            ExitCommand = new RelayCommand((parameter) => true, Exit);
            LoadCommand = new RelayCommand((parameter) => IsConnected, Load);
            SaveCommand = new RelayCommand((parameter) => IsConnected, Save);
            AddConnectionCommand = new RelayCommand((parameter) => true, AddConnection);
        }

        public ObservableCollection<Card> Cards { get; private set; }

        public bool IsConnected { get; private set; }

        public ICommand ExitCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddConnectionCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Exit(object parameter)
        {
            dBAccess.SaveChanges();
            dBAccess.Dispose();

            Environment.Exit(0);
        }

        private void Load(object parameter)
        {
            var fileName = openFileDialogService.Show();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.Error.WriteLine("File name is null or empty.");
                return;
            }

            var data = xmlLoaderService.LoadXml(fileName);

            foreach (var item in data)
            {
                Card card = dBAccess.Find<Card>(item.CardCode);

                if (card != null)
                {
                    card.AltAddress = item.AltAddress;
                    card.Apartment = item.Apartment;
                    card.Birthday = item.Birthday;
                    card.CardCode = item.CardCode;
                    card.Cardper = item.Cardper;
                    card.CardType = item.CardType;
                    card.City = item.City;
                    card.Email = item.Email;
                    card.FinishDate = item.FinishDate;
                    card.FirstName = item.FirstName;
                    card.FullName = item.FullName;
                    card.GenderId = item.GenderId;
                    card.House = item.House;
                    card.LastName = item.LastName;
                    card.OwnerGuid = item.OwnerGuid;
                    card.PhoneHome = item.PhoneHome;
                    card.PhoneMobil = item.PhoneMobil;
                    card.StartDate = item.StartDate;
                    card.Street = item.Street;
                    card.SurName = item.SurName;
                    card.Turnover = item.Turnover;
                }
                else
                {
                    dBAccess.Add(item);
                }
            }

            dBAccess.SaveChanges();
        }

        private void Save(object parameter)
        {
            dBAccess.SaveChanges();
        }

        private void AddConnection(object parameter)
        {
            if (parameter != null)
            {
                dBAccess.Database.SetConnectionString(parameter.ToString());

                if (dBAccess.Database.CanConnect())
                {
                    dBAccess.Database.Migrate();
                    dBAccess.Cards.Load();
                    Cards = dBAccess.Cards.Local.ToObservableCollection();

                    IsConnected = true;
                    OnPropertyChanged(nameof(Cards));
                }
            }
        }
    }
}