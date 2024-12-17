using Calculator.Data;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace NumbersAPI.Controllers
{
    [ApiController]
    [Route("numbers")]
    public class NumbersController : ControllerBase
    {
        private readonly NumbersDbContext _context;

        public NumbersController(NumbersDbContext context)
        {
            _context = context;
        }

        // GET: /numbers
        [HttpGet]
        public IActionResult GetAllNumbers()
        {
            return Ok(_context.Numbers.ToList());
        }

        // GET: /numbers/id /api/products/123
        [HttpGet("{id}")] //{} From
        public IActionResult GetNumberById([FromRoute] string id) //получает значение из строки запроса
        {
            var number = _context.Numbers.Find(id);
            if (number == null) return NotFound();
            return Ok(number);
        }

        // POST: /numbers/addition
        [HttpPost("addition")]
        public IActionResult Add([FromBody] OperationRequest request)
        //без FromBody метод Add не смог бы связать параметр request с данными в запроса
        {
            var result = request.LeftOperand + request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        // POST: /numbers/subtraction
        [HttpPost("subtraction")]
        public IActionResult Subtract([FromBody] OperationRequest request)
        {
            var result = request.LeftOperand - request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        // POST: /numbers/multiplication
        [HttpPost("multiplication")]
        public IActionResult Multiply([FromBody] OperationRequest request)
        {
            var result = request.LeftOperand * request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        // POST: /numbers/division
        [HttpPost("division")]
        public IActionResult Divide([FromBody] OperationRequest request)
        {
            if (request.RightOperand == 0) return BadRequest("Division by zero is not allowed.");
            var result = request.LeftOperand / request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        public class OperationRequest
        {
            public double LeftOperand { get; set; }
            public double RightOperand { get; set; }
        }
    }
}
