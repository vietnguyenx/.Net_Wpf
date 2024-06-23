using BusinessObject;
using DataAccess.Models;
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
using System.Windows.Shapes;

namespace NguyenXuanVietWPF
{
    /// <summary>
    /// Interaction logic for WindowRoom.xaml
    /// </summary>
    public partial class WindowRoom : Window
    {
        private readonly RoomObject roomObject;

        public WindowRoom()
        {
            InitializeComponent();
            roomObject = new RoomObject();
            Loaded += RoomsLoad;
        }

        private void RoomsLoad(object sender, RoutedEventArgs e)
        {
            LoadRoom();
        }

        private void LoadRoom()
        {
            dgRoom.ItemsSource = null;
            dgRoom.ItemsSource = roomObject.GetRoom();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RoomPopUp roomPopUp = new RoomPopUp();
            roomPopUp.WindowClosed += RoomsLoad;
            roomPopUp.Show();
        }

        private void DeleteBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (dgRoom.SelectedItem is RoomInformation selectedRoom)
            {
                if (MessageBox.Show($"Are you sure you want to delete room {selectedRoom.RoomNumber}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    roomObject.DeleteRoom(selectedRoom.RoomId); 
                    LoadRoom();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.");
            }
        }

        private void UpdateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (dgRoom.SelectedItem is RoomInformation selectedRoom)
            {
                RoomPopUp roomPopUp = new RoomPopUp(selectedRoom);
                roomPopUp.WindowClosed += RoomsLoad;
                roomPopUp.Show();
            }
            else
            {
                MessageBox.Show("Please select a room to update.");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            dgRoom.ItemsSource = roomObject.SearchRoom(keyword);
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void btnLoadAll_Click(object sender, RoutedEventArgs e)
        {
            LoadRoom();
        }
    }
}
