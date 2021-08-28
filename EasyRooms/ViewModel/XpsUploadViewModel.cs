using System.Collections.ObjectModel;

namespace EasyRooms.ViewModel
{
    public class XpsUploadViewModel
    {
        public ObservableCollection<RoomInfo> RoomNames { get; set; }

        public XpsUploadViewModel()
        {
            RoomNames = new ObservableCollection<RoomInfo>(new[] {
                new RoomInfo { Name = "one" },
                new RoomInfo { Name = "two"}});
        }
    }
}
