using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ProjectWpfApp1
{
    public partial class MainWindow : Window
    {
        public class Item
        {
            public string Title { get; set; }
            public int Price { get; set; }
        }

        public ObservableCollection<Item> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<Item>
            {
                new Item { Title = "Зелье здоровья", Price = 120 },
                new Item { Title = "Меч", Price = 110 },
                new Item { Title = "Щит", Price = 100 }
            };

            DataContext = this;
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Return) return;
            e.Handled = true;

            string command = InputTextBox.Text.Trim().ToLower();
            InputTextBox.Clear();

            switch (command)
            {
                case "пойти вверх":
                case "пойти вниз":
                case "пойти вправо":
                case "пойти влево":
                    PathCheck(command);
                    break;

                case "просмотр инвентаря":
                case "инвентарь":
                    SetInventoryVisibility(true);
                    Print("Инвентарь открыт");
                    break;

                case "закрыть":
                    SetInventoryVisibility(false);
                    Print("Инвентарь закрыт");
                    break;

                default:
                    Print("Неизвестная команда");
                    break;
            }
        }

        private void TasksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksListBox.SelectedItem is ListBoxItem item)
            {
                InputTextBox.Text = item.Content?.ToString() ?? "";
                InputTextBox.Focus();
                TasksListBox.SelectedItem = null;
            }
        }

        public void PathCheck(string path)
        {
            if (path == "пойти вверх")
            {
                var uri = new Uri("pack://application:,,,/Images/room_1.jpg", UriKind.Absolute);
                MyImage.Source = new BitmapImage(uri);
                SetInventoryVisibility(false); 
                Print("Конец");
            }
            else
            {
                Print("Туда пройти нельзя");
            }
        }

        public void Print(string text)
        {
            StatusTextBlock.Text = text;
        }

        private void SetInventoryVisibility(bool showInventory)
        {
            MyListBox.Visibility = showInventory ? Visibility.Visible : Visibility.Collapsed;
            MyImage.Visibility = showInventory ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
