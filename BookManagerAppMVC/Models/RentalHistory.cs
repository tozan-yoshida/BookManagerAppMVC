using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagerAppMVC.Models
{
    public class RentalHistory
    {
        public int RentalHistoryId { get; set; }
        public DateTime RentDate {  get; set; }
        public string? RentUser {  get; set; }

        public int BookId {  get; set; }
        //public Book? Book { get; set; }
    }
}
