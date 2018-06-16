using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Base;
using WebApplication1.Dtos;
using WebApplication1.Helpers;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    
    public class HomeController : BaseController
    {
        readonly IPeopleService _peopleService;


        public HomeController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public IActionResult Index()
        {
            RabbitMq.SendToQueue(Constants.QueueName, "Home - Index " + DateTime.Now);

            return View();
        }

        public IActionResult GetPeoplePartial() => View("~/Pages/Home/GetPeoplePartial.cshtml",_peopleService.GetAll().Data);


        public IActionResult GetLogPartial() => View("~/Pages/Home/GetLogPartial.cshtml", RabbitMq.ReceiveFromQueue(Constants.QueueName));

        [HttpPost]
        public JsonResult SaveNewPeople(PeopleDto people) => 
            ToJsonResult(_peopleService.Insert(people));
        
        
    }
}
