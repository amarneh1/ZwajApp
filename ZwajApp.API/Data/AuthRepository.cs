using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ZwajApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext _context { get; }
        public AuthRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);
            if (!VerifyPasswordHash(password,user.PasswordSalt,user.PasswordHash)) 
            return null;
            return user;
        }
         private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                
                var ComputedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
           byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt );
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;   
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x=>x.Username==username)) return true;
            return false;
        }
    }
}