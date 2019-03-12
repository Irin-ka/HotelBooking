

namespace HotelBooking.ViewModels {
    public class BookingResultViewModel {

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Message { get; set; }

        public BookingResultViewModel(int roomId, string roomName, string message) {
            RoomId = roomId;
            RoomName = roomName;
            Message = message;
        }
    }
}
