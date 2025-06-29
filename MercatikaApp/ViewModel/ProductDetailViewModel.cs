using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Helpers;
using MercatikaApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;


namespace MercatikaApp.ViewModel
{

        public class ProductDetailViewModel : INotifyPropertyChanged
        {
            private readonly ProductDetailService _productService;

            public ObservableCollection<Product> Products { get; set; } = new();
            private Product? _selectedProduct;

            public Product? SelectedProduct
            {
                get => _selectedProduct;
                set
                {
                    _selectedProduct = value;
                    if (value != null && value.ProductDetails.Count > 0)
                    {
                        var detail = value.ProductDetails[0];
                        ProductDetail = new ProductDetail
                        {
                            ProductDetailId = detail.ProductDetailId,
                            StockAmount = detail.StockAmount,
                            UniqueProductCode = detail.UniqueProductCode,
                            Size = detail.Size
                        };
                    }
                    else
                    {
                        ProductDetail = new ProductDetail();
                    }
                    OnPropertyChanged();
                }
            }

            private string _searchTerm = "";
            public string SearchTerm
            {
                get => _searchTerm;
                set
                {
                    _searchTerm = value;
                    OnPropertyChanged();
                }
            }

            private ProductDetail _productDetail = new();
            public ProductDetail ProductDetail
            {
                get => _productDetail;
                set
                {
                    _productDetail = value;
                    OnPropertyChanged();
                }
            }

            public ICommand SearchCommand { get; }
            public ICommand InsertCommand { get; }
            public ICommand UpdateCommand { get; }

            public ProductDetailViewModel()
            {
                _productService = new ProductDetailService();
                SearchCommand = new RelayCommand(async () => await SearchAsync());
                InsertCommand = new RelayCommand(async () => await InsertDetailAsync());
                UpdateCommand = new RelayCommand(async () => await UpdateDetailAsync());
            }

            private async Task SearchAsync()
            {
                Products.Clear();
                var result = await _productService.SearchProductsAsync(SearchTerm);
                foreach (var product in result)
                    Products.Add(product);
            }

            private async Task InsertDetailAsync()
            {
                if (SelectedProduct == null || SelectedProduct.ProductId <= 0)
                {
                    MessageBox.Show("Seleccione un producto primero.");
                    return;
                }

                var success = await _productService.CreateProductDetailAsync(SelectedProduct.ProductId, ProductDetail);
                MessageBox.Show(success ? "Detalle insertado correctamente" : "Error al insertar el detalle");
            }

            private async Task UpdateDetailAsync()
            {
                if (SelectedProduct == null || SelectedProduct.ProductId <= 0)
                {
                    MessageBox.Show("Seleccione un producto primero.");
                    return;
                }

                var success = await _productService.UpdateProductDetailAsync(SelectedProduct.ProductId, ProductDetail);
                MessageBox.Show(success ? "Detalle actualizado correctamente" : "Error al actualizar el detalle");
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string? name = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

