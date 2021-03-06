using System.Threading.Tasks;
using ZwajApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ZwajApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _Context ;
        public AuthRepository(DataContext context)
        {
            _Context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(x=>x.UserName == username);
            if(user==null) return null;
            if(!VerifyPasswordHash(password,user.PasswordSalt,user.PasswordHash))
            return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if(ComputedHash[i]!=passwordHash[i]){
                        return false;
                    }
                }
                 return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte [] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash (string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _Context.Users.AnyAsync(x=>x.UserName == username))
            return true;
            return false;
        }
    }
}