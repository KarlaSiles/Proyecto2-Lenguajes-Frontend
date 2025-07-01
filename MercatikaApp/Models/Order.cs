using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MercatikaApp.Models
{
    public class Order : INotifyPropertyChanged
    {
        public int _orderId;
        public int _clientId;
        public int _employeeId;
        public DateTime _orderDate;
        public string _addressTrip;
        public string _provinceTrip;
        public string _countryTrip;
        public int _phoneTrip;
        public DateTime _dateTrip;
        public ObservableCollection<OrderDetail> _details = new();
        public string? Estado { get; set; } = "";


        public int OrderId
        {
            get => _orderId;
            set { _orderId = value; OnPropertyChanged(); }
        }

        public int ClientId
        {
            get => _clientId;
            set { _clientId = value; OnPropertyChanged(); }
        }

        public int EmployeeId
        {
            get => _employeeId;
            set { _employeeId = value; OnPropertyChanged(); }
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set { _orderDate = value; OnPropertyChanged(); }
        }

        public string AddressTrip
        {
            get => _addressTrip;
            set { _addressTrip = value; OnPropertyChanged(); }
        }

        public string ProvinceTrip
        {
            get => _provinceTrip;
            set { _provinceTrip = value; OnPropertyChanged(); }
        }

        public string CountryTrip
        {
            get => _countryTrip;
            set { _countryTrip = value; OnPropertyChanged(); }
        }

        public int PhoneTrip
        {
            get => _phoneTrip;
            set { _phoneTrip = value; OnPropertyChanged(); }
        }

        public DateTime DateTrip
        {
            get => _dateTrip;
            set { _dateTrip = value; OnPropertyChanged(); }
        }

        public ObservableCollection<OrderDetail> Details
        {
            get => _details;
            set { _details = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
