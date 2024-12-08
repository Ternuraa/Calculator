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

        // GET: /numbers/{id}
        [HttpGet("{id}")]
        public IActionResult GetNumberById(int id)
        {
            var number = _context.Numbers.Find(id);
            if (number == null) return NotFound();
            return Ok(number);
        }

        // POST: /numbers/add
        [HttpPost("add")]
        public IActionResult Add([FromBody] OperationRequest request)
        {
            var result = request.LeftOperand + request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        // POST: /numbers/sub
        [HttpPost("sub")]
        public IActionResult Subtract([FromBody] OperationRequest request)
        {
            var result = request.LeftOperand - request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        // POST: /numbers/mul
        [HttpPost("mul")]
        public IActionResult Multiply([FromBody] OperationRequest request)
        {
            var result = request.LeftOperand * request.RightOperand;
            var record = new NumberRecord { Number = result };
            _context.Numbers.Add(record);
            _context.SaveChanges();
            return Ok(new { id = record.Id, result });
        }

        // POST: /numbers/div
        [HttpPost("div")]
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
