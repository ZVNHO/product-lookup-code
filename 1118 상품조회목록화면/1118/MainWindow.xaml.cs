using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1118
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> Products = new List<Product>();
        public MainWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = Products; // 초기 상품 목록 바인딩
        }

        // 등록 버튼 클릭 이벤트
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // 입력 값 가져오기
            string name = ProductNameInput.Text;
            string priceText = ProductPriceInput.Text;
            string category = (ProductCategoryInput.SelectedItem as ComboBoxItem)?.Content.ToString();
            string description = ProductDescriptionInput.Text;

            // 유효성 검사
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("상품명, 가격, 카테고리를 입력하세요.", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(priceText, out int price))
            {
                MessageBox.Show("가격은 숫자로 입력해야 합니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 상품 추가
            Product newProduct = new Product
            {
                Name = name,
                Price = price,
                Category = category,
                Description = description
            };
            Products.Add(newProduct);

            // 상품 목록 갱신
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = Products;

            // 입력 필드 초기화
            ProductNameInput.Text = "";
            ProductPriceInput.Text = "";
            ProductCategoryInput.SelectedIndex = -1;
            ProductDescriptionInput.Text = "";

            MessageBox.Show("상품이 등록되었습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 리스트뷰에서 항목 선택 이벤트
        private void ProductListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                DetailName.Text = $"상품명: {selectedProduct.Name}";
                DetailPrice.Text = $"가격: {selectedProduct.Price}원";
                DetailCategory.Text = $"카테고리: {selectedProduct.Category}";
                DetailDescription.Text = $"설명: {selectedProduct.Description}";
            }
        }
    }

    // 상품 클래스 정의
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
   