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
    public class NewsController : ControllerBase
    {

        SqlConnection connection;
        DAL dal;
        private readonly IConfiguration _configuration;
        public NewsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddNews")]
        public Response AddNews(News news)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SNcon").ToString());
            dal = new DAL();
            response = dal.AddNews(news, connection);
            return response;

        }

        [HttpGet]
        [Route("NewsList")]
        public Response NewsList(News news)
        {
            Response response = new Response();
            connection=new SqlConnection(_configuration.GetConnectionString("SNcon").ToString());
            dal = new DAL();
            dal.Newslist(connection);
            return response;
        }
    }
}
