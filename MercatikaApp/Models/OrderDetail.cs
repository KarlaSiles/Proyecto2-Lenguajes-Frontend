using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MercatikaApp.Models
{
    public class OrderDetail : INotifyPropertyChanged
    {
        public int _orderId;
        public int _productDetailId;
        public int _amount;
        public decimal _linePrice;

        public int OrderId
        {
            get => _orderId;
            set { _orderId = value; OnPropertyChanged(); }
        }

        public int ProductDetailId
        {
            get => _productDetailId;
            set { _productDetailId = value; OnPropertyChanged(); }
        }

        public int Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(); }
        }

        public decimal LinePrice
        {
            get => _linePrice;
            set { _linePrice = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
