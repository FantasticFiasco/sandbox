using System.Web.Http;
using Payments.Models;

namespace Payments.Controllers
{
    public class QueueCardPaymentController : ApiController
    {       
        [HttpPost]
        public IHttpActionResult MakePayment([FromBody] CardPayment payment)
        {
            return Ok(payment);
        }
    }
}

