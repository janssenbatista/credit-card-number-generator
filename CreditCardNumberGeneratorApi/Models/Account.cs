using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardNumberGeneratorApi.Models
{
    public class Account
    {
        [Key] public int Id { get; set; }
        [EmailAddress] public string Email { get; set; }
    }
}