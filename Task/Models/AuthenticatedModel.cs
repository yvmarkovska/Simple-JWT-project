namespace Assignment.Models
{
    public class AuthenticatedModel
    {
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticatedModel(LoginModel user, string token)
        {
            Username = user.Username;
            Token = token;
        }
    }
}
