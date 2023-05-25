using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace SecondPartialAccountApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // This is the route
    public class OperationController : Controller
    {
        /*private string ConnectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=1234;";*/
        private OperationService operationService;

        public OperationController(OperationService operationService)
        {
            this.operationService = operationService;
        }

        [HttpPut("{sourceAccountId}/Transferir/{targetAccountId}")]
        public ActionResult Transfer(int sourceAccountId, int targetAccountId, double amount)
        {
            try
            {
                var result = operationService.Transfer(sourceAccountId, targetAccountId, amount);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred" + ex);
            }
        }

        [HttpPut("Depositar/{targetAccountId}")]
        public ActionResult Deposit(double amount, int targetAccountId)
        {
            try
            {
                var result = operationService.Deposit(amount, targetAccountId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpPut("Devolución/{targetAccountId}")]
        public ActionResult Devolution(double amount, int targetAccountId)
        {
            try
            {
                var result = operationService.Devolution(amount, targetAccountId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpPut("Extracción/{targetAccountId}")]
        public ActionResult Extract(double amount, int targetAccountId)
        {
            try
            {
                var result = operationService.Extract(amount, targetAccountId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpDelete("Bloquear/{targetAccountId}")]
        public ActionResult Block(int targetAccountId)
        {
            try
            {
                var result = operationService.Block(targetAccountId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }



    }

}
