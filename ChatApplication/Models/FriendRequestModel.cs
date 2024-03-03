using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
    public class FriendRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sender Id is required")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "Receiver Id is required")]
        public int ReceiverId { get; set; }

        public bool IsAccepted { get; set; }
    }

}
