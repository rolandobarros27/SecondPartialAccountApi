using Infraestructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace SecondPartialAccountApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // This is the route
    public class AccountController : Controller
    {
        /*private string ConnectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=1234;";*/
        private AccountService accountService;
        private IConfiguration configuration;

        /*public SubjectController()*/
        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.accountService = new AccountService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpGet("ListarCuenta")]
        // Get all the subjects
        public ActionResult<List<AccountModel>> read()
        {
            var resultado = accountService.read();
            return Ok(resultado);
        }

        [HttpGet("ConsultarCuenta/{id}")]
        /*public ActionResult<AccountModel> getById(int id, string documento)*/
        public ActionResult<AccountModel> getById(int id)
        {
            var result = this.accountService.getByid(id);
            return Ok(result);
        }

        [HttpPost("InsertarCuenta")]
        public ActionResult<string> insert(AccountModel models)
        {
            /*return Ok("Ok");*/
            var result = this.accountService.insert(new Infraestructure.Models.AccountModel
            {
                id_account = models.id_account,
                name = models.name,
                number = models.number,
                balance = models.balance,
                limit_balance = models.limit_balance,
                limit_transfer = models.limit_transfer,
                status = models.status,
                
            });
            return Ok(result);
        }
        // endpoint for subject modify
        [HttpPut("ModificarCuenta/{id}")]
        public ActionResult<string> modify(AccountModel models, int id)
        {
            var result = this.accountService.modify(new Infraestructure.Models.AccountModel
            {
                id_account = models.id_account,
                name = models.name,
                number = models.number,
                balance = models.balance,
                limit_balance = models.limit_balance,
                limit_transfer = models.limit_transfer,
                status = models.status,
            }, id);
            return Ok(result);
        }

        // endpoint for subject delete
        [HttpDelete("EliminarCuenta/{id}")]
        public ActionResult<string> delete(int id)
        {
            var result = this.accountService.delete(id);
            return Ok(result);
        }
    }
}
