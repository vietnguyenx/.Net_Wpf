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
    /// Interaction logic for RoomPopUp.xaml
    /// </summary>
    public partial class RoomPopUp : Window
    {
        private RoomObject roomObject;
        public Action<object, RoutedEventArgs> WindowClosed;
        private readonly RoomInformation existRoom;

        public RoomPopUp() => InitProp();

        public RoomPopUp(RoomInformation r)
        {
            InitProp();
            this.existRoom = r;
        }

        public void InitProp()
        {
            InitializeComponent();
            roomObject = new RoomObject();
            Loaded += LoadedUpdateForm;
        }

        private void LoadedUpdateForm(object sender, RoutedEventArgs e)
        {
            cbRoomType.ItemsSource = roomObject.GetRoomTypes();
            if(existRoom != null)
            {
                tbRoomNumber.Text = existRoom.RoomNumber;
                tbRoomDetailDescription.Text = existRoom.RoomDetailDescription;
                tbRoomMaxCapacity.Text = existRoom.RoomMaxCapacity.ToString();
                cbRoomType.SelectedValue = existRoom.RoomTypeId;
                tbRoomStatus.Text = existRoom.RoomStatus.ToString();
                tbRoomPricePerDay.Text = existRoom.RoomPricePerDay.ToString();
                btnAdd.Content = "Update";

            }
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!TakeRoom(out RoomInformation? r))
            {
                return;
            }

            if(existRoom != null)
            {
                r!.RoomId = existRoom.RoomId;
                roomObject.UpdateRoom(r);
            }
            else
            {
                roomObject.AddRoom(r!);
            }

            WindowClosed?.Invoke(sender, e);
            Close();
        }

        private bool TakeRoom(out RoomInformation? validRoom)
        {
            StringBuilder errorMessage = new StringBuilder();
            if (string.IsNullOrEmpty(tbRoomNumber.Text))
            {
                errorMessage.Append("Room number is required.\n");
            }

            if (string.IsNullOrEmpty(tbRoomDetailDescription.Text))
            {
                errorMessage.Append("Room detail description is required.\n");
            }

            if (!int.TryParse(tbRoomMaxCapacity.Text, out int roomMaxCapacity))
            {
                errorMessage.Append("Room max capacity is required.\n");
            }

            if (!byte.TryParse(tbRoomStatus.Text, out byte roomStatus))
            {
                errorMessage.Append("Room status must be a byte value.\n");
            }

            if (!decimal.TryParse(tbRoomPricePerDay.Text, out decimal roomPricePerDay))
            {
                errorMessage.Append("Room price per day must be a decimal value.\n");
            }

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                validRoom = null;
                return false;
            }

            validRoom = new RoomInformation
            {
                RoomNumber = tbRoomNumber.Text,
                RoomDetailDescription = tbRoomDetailDescription.Text,
                RoomMaxCapacity = roomMaxCapacity,
                RoomStatus = roomStatus,
                RoomPricePerDay = roomPricePerDay,
                RoomTypeId = (int)cbRoomType.SelectedValue 
            };
            return true;
        }

        private void CancelBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you want to cancel?", "Room",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }

        }
    }
}
