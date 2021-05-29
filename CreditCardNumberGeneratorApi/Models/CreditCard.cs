using System.ComponentModel.DataAnnotations;

namespace CreditCardNumberGeneratorApi.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public int AccountId { get; set; }
    }
}