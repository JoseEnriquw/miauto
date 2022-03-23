using Dapper;
using miauto.identity.api.Config;
using miauto.identity.api.Models.Common;
using miauto.identity.api.Models.Entity;
using miauto.identity.api.Models.Querys;
using miauto.identity.api.Models.Request;
using miauto.identity.api.Models.Response;
using miauto.identity.api.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace miauto.identity.api.Services.Repository
{
    public class UserRepositories : IUserRepositories
    {

        private PostgreSQLConfiguration _connectionString;
        private readonly AppSettings _appSettings;

        public UserRepositories(PostgreSQLConfiguration connectionString, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        //DELETE
        public async Task<bool> DeleteUser(int id)
        {
            var db = dbConnection();

            var result = await db.ExecuteAsync(QUser.DELETE, new { Id = id });

            return result > 0;
        }

        //GETA
        public async Task<IEnumerable<User>> GetAllUser()
        {

            var db = dbConnection();


            return await db.QueryAsync<User>(QUser.GETALL, new { });


        }

        //GET BY ID
        public async Task<User> GetUser(int id)
        {
            var db = dbConnection();

            return await db.QueryFirstOrDefaultAsync<User>(QUser.GET, new { Id = id });
        }

        //INSERT 
        public async Task<bool> InsertDefaultUser(User user)
        {
            var db = dbConnection();

            user.password=Encrypt.GetSHA256(user.password);

            var result = await db.ExecuteAsync(QUser.INSERT, new { user.email, user.password });

            return result > 0;
        }

        //UPDATE
        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();

            var result = await db.ExecuteAsync(QUser.UPDATE, new { Email = user.email, Password = Encrypt.GetSHA256(user.password) });

            return result > 0;
        }

        public async Task<UserResponse> Auth(AuthRequest authRequest)
        {
            var db = dbConnection();
            User result =await db.QueryFirstOrDefaultAsync<User>(QUser.GET_BY_EMAIL_AND_PASSWORD, new { Email = authRequest.email, Password = Encrypt.GetSHA256(authRequest.password) });
            if (result == null) return null;
            return new UserResponse(result.email,GetToken(result));
                
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                   new Claim[]
                   {
                       new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),
                       new Claim(ClaimTypes.Email,user.email)

                   }
                   ),
                   Expires = DateTime.UtcNow.AddDays(60),
                   SigningCredentials= new SigningCredentials( new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256 )
            };

            var token= tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
