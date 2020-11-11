using System;
using BirthdayGreetings.Core;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayGreetings.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BirthdayController : ControllerBase
    {
        readonly IBirthdayService service;

        public BirthdayController(IBirthdayService service)
        {
            this.service = service;
        }

        public IActionResult Get(string id)
        {
            service.SendGreetings(DateTime.Today);
            return Ok("called with: " + id);
        }
    }
}