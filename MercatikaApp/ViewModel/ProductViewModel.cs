using MercatikaApp.Models;
using MercatikaApp.Services;
using MercatikaApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace MercatikaApp.ViewModel
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        private Product _selectedProduct = new Product
        {
            CategoryCode = new Category()
        };

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductViewModel()
        {
            _productService = new ProductService();

            LoadCommand = new RelayCommand(async () => await LoadProductsAsync());
            AddCommand = new RelayCommand(async () => await AddProductAsync(), CanExecuteProductAction);
            UpdateCommand = new RelayCommand(async () => await UpdateProductAsync(), CanExecuteProductAction);
            DeleteCommand = new RelayCommand(async () => await DeleteProductAsync(), CanExecuteProductAction);
            SearchCommand = new RelayCommand(async () => await SearchProductsAsync());
        }
        private int _selectedQuantity = 1;
        public int SelectedQuantity
        {
            get => _selectedQuantity;
            set
            {
                _selectedQuantity = value;
                OnPropertyChanged(); // Si implementas INotifyPropertyChanged
            }
        }

        public async Task LoadProductsAsync()
        {
            var products = await _productService.SearchProductsAsync(""); 
            Products.Clear();
            foreach (var product in products)
                Products.Add(product);

            await LoadCategoriesAsync(); // NUEVO: Cargar categorías al cargar productos
        }

        private async Task AddProductAsync()
        {
            bool creado = await _productService.CreateProductAsync(SelectedProduct);
            if (creado)
                MessageBox.Show("Product inserted correctly");
            else
                MessageBox.Show("Error inserting product");

            await LoadProductsAsync();


            SelectedProduct = new Product
            {
                CategoryCode = new Category()
            };
        }

        private async Task UpdateProductAsync()
        {
            bool actualizado = await _productService.UpdateProductAsync(SelectedProduct);
            if (actualizado)
                MessageBox.Show("Successfully updated product");
            else
                MessageBox.Show("Error updating product");

            await LoadProductsAsync();
        }

        private async Task DeleteProductAsync()
        {
            if (SelectedProduct?.ProductId > 0)
            {
                bool eliminado = await _productService.DeleteProductAsync(SelectedProduct.ProductId);
                if (eliminado)
                    MessageBox.Show("Product disposed correctly");
                else
                    MessageBox.Show("Product deletion error");

                await LoadProductsAsync();
                SelectedProduct = new Product
                {
                    CategoryCode = new Category()
                };
            }
        }

        private async Task SearchProductsAsync()
        {
            var results = await _productService.SearchProductsAsync(SearchTerm ?? "");
            Products.Clear();
            foreach (var p in results)
                Products.Add(p);
        }

        private bool CanExecuteProductAction()
        {
            return SelectedProduct != null
                && !string.IsNullOrWhiteSpace(SelectedProduct.ProductName)
                && SelectedProduct.Price > 0
                && SelectedProduct.CategoryCode?.CategoryCode > 0;
        }


        public async Task LoadCategoriesAsync()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            Categories.Clear();
            foreach (var category in categories)
                Categories.Add(category);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


