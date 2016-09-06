using System.Web.Http;
using Payments.Models;

namespace Payments.Controllers
{
    public class QueuePurchaseOrderController : ApiController
    {       
        [HttpPost]
        public IHttpActionResult MakePayment([FromBody] PurchaseOrder purchaseOrder)
        {            
            return Ok(purchaseOrder);
        }
    }
}
