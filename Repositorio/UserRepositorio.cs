using ClientesAPI.Data;
using ClientesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ClientesAPI.Repositorio {
    public class UserRepositorio : IUserRepositorio {

        private readonly ApplicationDbContext _context;

        public UserRepositorio(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<string> Login(string userName, string password) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName.ToLower()));
            if (user == null) {
                return "noUser";
            } else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                return "wrongPassword";
            } else {
                return "ok";
            }
        }

        public async Task<int> Register(User user, string password) {
            try {
                if (await UserExist(user.UserName)) {
                    return -1;
                }

                CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.Id;
            } catch (Exception) {
                return -500;
            }
        }

        public async Task<bool> UserExist(string userName) {
            if (await _context.Users.AnyAsync(user => user.UserName.ToLower().Equals(userName.ToLower()))) {
                return true;
            }
            return false;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
