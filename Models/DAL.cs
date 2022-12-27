using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBE.Models
{
    public class DAL
    {
        public Response Registration(Registration registration, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Registration (Name,Email,Password,PhoneNo,Isactive,IsApproved) " +
                "Values('" + registration.Name + "','" + registration.Email + "','" + registration.Password + "','"
                + registration.PhoneNo
                + "','" + registration.IsActive + "','" + registration.IsApproved + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Registration has been done Succesfully!!";


            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Registration Failed!!";
            }
            return response;
        }
        public Response Login(Registration registration,SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Registration where Email='" + registration.Email + "'And Password='" + registration.Password + "'", connection);
            //SQlDataAdapter act as bridge between Datasource(Sql server) and DataTable or DataSet(which contain mulitple table)
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                #region Success
                response.StatusCode = 200;
                response.StatusMessage = "Login SuccessFull";
                Registration reg = new Registration();
                reg.ID = Convert.ToInt32(dt.Rows[0]["ID"]);//because size of id is Int (4byte ) i.e.,32 bit
                reg.Name = dt.Rows[0]["Name"].ToString();
                reg.Email = dt.Rows[0]["Email"].ToString();
                reg.PhoneNo = dt.Rows[0]["PhoneNo"].ToString();
                response.Registration = reg;
                #endregion
            }
            else
            {
                #region Failure
                response.StatusCode = 100;
                response.StatusMessage = "Login Failed";
                response.Registration = null;
                #endregion
            }

            return response;
        }

        public Response UserApproval(Registration registration, SqlConnection connection)
        {
            Response response = new Response();
         SqlCommand cmd=new SqlCommand("Update Registration set IsApproved=1 where ID='"+registration.ID+"' and IsActive=1",connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User has been approved";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User approval failed";
            }


            return response;
        }
      
        public Response AddNews(News news, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into News (Title,Content,Email,Isactive,CreatedOn) " +
                "Values('" + news.Title + "','" + news.Content + "','" + news.Email + "','1','GETDATE())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "News has been created!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "News upload Failed!!";
            }
            return response;
        }

        public Response Newslist(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from News where IsActive=1", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<News> lsNews = new List<News>();
            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    News news = new News();
                    news.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    news.Title = dt.Rows[i]["Title"].ToString();
                    news.Content = dt.Rows[i]["Content"].ToString();
                    news.Email = dt.Rows[i]["Email"].ToString();
                    news.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    news.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    lsNews.Add(news);
                }
                if (lsNews.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "NewsData found";
                    response.listNews = lsNews;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No Data found";
                    response.listNews = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
                response.listNews = null;

            }
            return response;
        }

        public Response AddArticle(Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Article (Title,Content,Email,Image,Isactive,CreatedOn) " +
                "Values('" + article.Title + "','" + article.Content + "','" + article.Email+ "','" + article.Image
                + "','1','GETDATE())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article has been created!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "News upload Failed!!";
            }
            return response;
        }

        public Response Artcilelist(Article article,SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = null;
            if (article.type == "User")
            {
                new SqlDataAdapter("Select * from Article where Email='"+article.type+"' and IsActive=1", connection);
            }
            if (article.type == "Page")
            {
                new SqlDataAdapter("Select * from Article where IsActive=1", connection);
            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Article> lsArticle = new List<Article>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Article art = new Article();
                    art.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    art.Title = dt.Rows[i]["Title"].ToString();
                    art.Content = dt.Rows[i]["Content"].ToString();
                    art.Email = dt.Rows[i]["Email"].ToString();
                    art.Image = dt.Rows[0]["Image"].ToString();
                    art.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    art.Image = Convert.ToString(dt.Rows[i]["Image"]);
                    lsArticle.Add(art);
                }
                if (lsArticle.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Article Data found";
                    response.listArticle = lsArticle;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No Data found";
                    response.listArticle = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
                response.listArticle = null;

            }
            return response;
        }
        public Response ArticleApproval(Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Article set IsApproved=1 where ID='" + article.ID + "' and IsActive=1", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article approved";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Article approval failed";
            }


            return response;
        }
        public Response StaffRegistration(Staff staff, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Staff (Name,Email,Password,PhoneNo,Isactive) " +
                "Values('" + staff.Name + "','" + staff.Email + "','" + staff.Password 
                + "','" + staff.IsActive + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "staff Registration has been done Succesfully!!";


            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage =  " staff Registration Failed!!";
            }
            return response;
        }

        public Response DeleteStaff(Staff staff, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Staff where ID='"+staff.ID+"' and IsActive=1", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "staff deleted successfully!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = " staff deletion Failed!!";
            }
            return response;
        }

        public Response AddEvents(Events events, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Events (Title,Content,Isactive,CreatedOn) " +
                "Values('" + events.Title + "','" + events.Content + "','1','GETDATE())", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Events has been created!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Events upload Failed!!";
            }
            return response;
        }

        public Response EventsList(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Events where IsActive=1", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Events> lsEvents = new List<Events>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Events events = new Events();
                    events.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    events.Title = dt.Rows[i]["Title"].ToString();
                    events.Content = dt.Rows[i]["Content"].ToString();
                    events.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    events.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    lsEvents.Add(events);
                }
                if (lsEvents.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "event Data found";
                    response.listEvents = lsEvents;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No Data found";
                    response.listEvents = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
                response.listEvents = null;

            }
            return response;
        }
    }
}
