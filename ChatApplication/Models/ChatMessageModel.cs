using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
    public class ChatMessageModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sender Id is required")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "Receiver Id is required")]
        public int ReceiverId { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsDeleted { get; set; }
    }

}

