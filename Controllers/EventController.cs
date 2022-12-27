using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialNetworkBE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        SqlConnection connection;
        DAL dal;
        private readonly IConfiguration _configuration;
        public EventController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddEvent")]
        public Response AddEvent(Events events)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SqCon").ToString());
            dal = new DAL();
            response=dal.AddEvents(events, connection);
            return response;
        }

        [HttpGet]
        [Route("EventList")]
        public Response EventList()
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SqCon").ToString());
            dal = new DAL();
            response= dal.EventsList(connection);
            return response;
        }

    }
}
