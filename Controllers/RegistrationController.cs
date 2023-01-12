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
    public class RegistrationController : ControllerBase
    {
        SqlConnection connection;
        DAL dal;
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //readonly can be changed in contructor
        //const used for absolute constant value it is intialized while declaring bcz it can't change
        [HttpPost]
        [Route("Reg")]
        public Response Registration(Registration registration)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SqCon").ToString());
            dal = new DAL();
            response = dal.Registration(registration, connection);
            return response;
            //As token Controller already declared hence method route get concatineated ...localhost/api/Controller/Registration
        }
        [HttpPost]
        [Route("Login")]
        public Response Login(Registration registration)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SqCon"));
            dal = new DAL();
            response=dal.Login(registration, connection);

            return response;
        }

        [HttpPost]
        [Route("UserApproval")]
        public Response UserApproval(Registration registration)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SqlCon").ToString());
            dal = new DAL();
            dal.UserApproval(registration, connection);
            return response;
        }

        [HttpPost]
        [Route("StaffRegistration")]
        public Response StaffRegistration(Staff staff)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            dal = new DAL();
            dal.StaffRegistration(staff, connection);
            return response;
        }

        [HttpPost]
        [Route("DeleteStaff")]
        public Response DeleteStaff(Staff staff)
        {
            Response response = new Response();
            connection = new SqlConnection(_configuration.GetConnectionString("SNCon").ToString());
            dal = new DAL();
            dal.DeleteStaff(staff, connection);
            return response;
        }

       
    }
}
