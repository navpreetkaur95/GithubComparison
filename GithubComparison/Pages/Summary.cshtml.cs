using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace GithubComparison.Pages
{
    public class SummaryModel : PageModel
    {
        public string Message { get; set; }
        public string JSONresponse;

        public string FirstUsername;
        public string SecondUsername;

        public string First_Name;
        public string First_Followers;
        public string First_Bio;
        public string First_Location;
        public string First_PublicRepos;
        public string First_AvatarURL;

        public string Second_Name;
        public string Second_Followers;
        public string Second_Bio;
        public string Second_Location;
        public string Second_PublicRepos;
        public string Second_AvatarURL;

        public void OnGet()
        {
            string userEntry = Request.QueryString.Value;
            FirstUsername = userEntry.Split("=")[1].Split("&")[0];
            SecondUsername = userEntry.Split("=")[2];
            
            string firstUserDetails = FetchDetails(FirstUsername);
            string secondUserDetails= FetchDetails(SecondUsername);

            First_Name = firstUserDetails.Split(",")[0];
            First_Followers = firstUserDetails.Split(",")[1];
            First_Bio = firstUserDetails.Split(",")[2];
            First_Location = firstUserDetails.Split(",")[3];
            First_PublicRepos = firstUserDetails.Split(",")[4];
            First_AvatarURL = firstUserDetails.Split(",")[5];

            Second_Name = secondUserDetails.Split(",")[0];
            Second_Followers = secondUserDetails.Split(",")[1];
            Second_Bio = secondUserDetails.Split(",")[2];
            Second_Location = secondUserDetails.Split(",")[3];
            Second_PublicRepos = secondUserDetails.Split(",")[4];
            Second_AvatarURL = secondUserDetails.Split(",")[5];


        }

        public string FetchDetails(string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/users/"+ username);
            try
            {
                request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1521.3 Safari/537.36";
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    JSONresponse = reader.ReadToEnd();

                    dynamic parsing = JObject.Parse(JSONresponse);
                    try
                    {
                        string Name = parsing.name;
                        string Followers = parsing.followers;
                        string Bio = parsing.bio;
                        string Location = parsing.location;
                        string PublicRepos = parsing.public_repos;
                        string AvatarURL = parsing.avatar_url;

                        return Name + "," + Followers + "," + Bio + "," + Location + "," + PublicRepos + "," + AvatarURL;
                    }
                    catch (Exception e)
                    {
                        Message = "Sorry no movie with this name found. Please try again.";
                        return null;
                    }
                }
            }
            catch (WebException ex)
            {
                Message = ex.ToString();
                return null;
            }
        }
    }
}
