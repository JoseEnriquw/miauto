namespace miauto.identity.api.Models.Response
{
    public class UserResponse
    {
        public string email { get; set; }
        public string token { get; set; }

        public UserResponse(string email, string token)
        {
            this.email = email;
            this.token = token;
        }
    }
}
