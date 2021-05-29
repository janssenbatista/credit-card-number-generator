using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCardNumberGeneratorApi.Data;
using CreditCardNumberGeneratorApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCardNumberGeneratorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccounts([FromServices] DataContext context)
        {
            return Ok(await context.Accounts.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromServices] DataContext context,
            [FromBody] Account account)
        {
            if (ModelState.IsValid)
            {
                context.Accounts.Add(account);
                await context.SaveChangesAsync();
                return Created("", account);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{accountId}/cards")]
        public async Task<ActionResult<List<CreditCard>>> GetCreditCardsByAccountId(
            [FromServices] DataContext context,
            int accountId)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(acc => acc.Id == accountId);
            if (account != null)
            {
                List<CreditCard> cards =
                    await context.CreditCards.Where(card => card.AccountId == accountId).ToListAsync();
                return Ok(cards);
            }

            return BadRequest("This account does not exists");
        }

        [HttpPost("{accountId}/cards")]
        public async Task<ActionResult<CreditCard>> CreateCreditCard([FromServices] DataContext context,
            int accountId)
        {
            if (ModelState.IsValid)
            {
                var creditCard = new CreditCard();
                creditCard.AccountId = accountId;
                creditCard.Number = GenerateCreditCardNumber();
                context.CreditCards.Add(creditCard);
                await context.SaveChangesAsync();
                return Created("", creditCard);
            }
            else
            {
                return BadRequest();
            }
        }

        private string GenerateCreditCardNumber()
        {
            var rand = new Random();
            var creditCardNumber = new StringBuilder("");
            for (int i = 0; i < 16; i++)
            {
                creditCardNumber.Append(rand.Next(10));
            }

            return creditCardNumber.ToString();
        }
    }
}