using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MercatikaApp.Models
{
    public class OrderDetail : INotifyPropertyChanged
    {
        public int _orderId;
        public int _productDetailId;
        public int _amount;
        public decimal _linePrice;
        [JsonIgnore] // Esto evita que se envíe al backend
        public string ProductName { get; set; }
        public Product? Product { get; set; } // Esta propiedad permite recibir el objeto completo anidado

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
