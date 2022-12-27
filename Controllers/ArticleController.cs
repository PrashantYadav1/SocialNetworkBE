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
    public class ArticleController : ControllerBase
    {
        SqlConnection connection;
        DAL dal;
        private readonly IConfiguration _configuration;
        public ArticleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddArticle")]
        public Response AddArticle(Article article)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SNcon").ToString());
            dal = new DAL();
            response = dal.AddArticle(article, connection);
            return response;
        }

        [HttpPost]
        [Route("ArticleList")]
        public Response NewsList(Article article)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SNcon").ToString());
            dal = new DAL();
            dal.Artcilelist(article, connection);
            return response;
        }
        [HttpPost]
        [Route("ArticleApproval")]
        public Response ArticleApproval(Article article)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            dal = new DAL();
            dal.ArticleApproval(article, connection);
            return response;
        }

    }
}
