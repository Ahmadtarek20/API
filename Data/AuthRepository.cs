using System;
using System.Threading.Tasks;
using AppApi.Models;
using Microsoft.EntityFrameworkCore;
namespace AppApi.Data {
    public class AuthRepository : IAuthRepository {
        private readonly ApiDbContext _apiDb;
        public AuthRepository (ApiDbContext apiDb) {
            _apiDb = apiDb;

        }
        public async Task<Users> Login (string username, string password) {

            var user = await _apiDb.Users.FirstOrDefaultAsync(x =>x.UserName==username);
            if(user == null)return null;
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PaswordSalt))
             return null;
             return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] paswordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(paswordSalt)){
              var ComputeHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              for (int i = 0; i < ComputeHash.Length; i++)
              {
                  if(ComputeHash[i]!=passwordHash[i]){
                      return false;
                  }
              }
                return true;
            }
        }

        public async Task<Users> Register (Users user, string password) {
            byte[] passwordHash, paswordSalt;
            CretePassHash(password,out passwordHash ,  out paswordSalt);
            user.PaswordSalt= paswordSalt;
            user.PasswordHash = passwordHash;
            await _apiDb.Users.AddAsync(user);
            await _apiDb.SaveChangesAsync();
            return user;
        }
        private void CretePassHash(string password, out byte[] passwordHash, out byte[] paswordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
              paswordSalt = hmac.Key;
              passwordHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExists (string username) {
            if(await _apiDb.Users.AnyAsync(x =>x.UserName==username)) return true;
            return false;
        }
    }
}