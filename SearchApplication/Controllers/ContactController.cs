using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchApplication.Data;
using SearchApplication.Data.Classes;
using SearchApplication.Services;
using SearchApplication.ViewModel;

namespace SearchApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(ILogger<ContactController> logger, IContactService contactService, IMapper mapper)
        {
            _logger = logger;
            _contactService = contactService;
            _mapper = mapper;
        }

        // GET: api/Contact
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("GET /api/contact called");
                var contacts = _contactService.GetAllContacts();
                return Ok(_mapper.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(contacts));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all contact: {ex}");
                return BadRequest("Failed to get all contact");
            }
        }

        // GET: api/Contact/{name}
        [HttpGet("{name}", Name = "Get")]
        public IActionResult Get(string name)
        {
            try
            {
                _logger.LogInformation("GET /api/contact/{name} called");
                var contacts = _contactService.GetContact(name);
                if (contacts != null) return Ok(_mapper.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(contacts));
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to search contact: {ex}");
                return BadRequest("Failed to search contact");
            }
        }

        // POST: api/Contact
        [HttpPost]
        public IActionResult Post([FromBody] ContactViewModel model)
        {
            try
            {
                _logger.LogInformation("POST /api/contact called");

                if (ModelState.IsValid)
                {
                    var newContact = _mapper.Map<ContactViewModel, Contact>(model);
                    _contactService.AddContact(newContact);
                    return Created("/api/contact", true);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add contact: {ex}");
                return BadRequest($"Failed to add contact: ");
            }
        }
    }
}
