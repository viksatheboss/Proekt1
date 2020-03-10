using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Layer;

namespace Repository.Layer
{
     public interface IKorisniciRepository
    {
        void InsertUser(Korisnik u);
        void UpdateUserDetails(Korisnik u);
        void UpdateUserPassword(Korisnik u);
        void DeleteUser(int uid);
        List<Korisnik> GetUsers();
        List<Korisnik> GetUsersByEmailAndPassword(string Email, string Password);
        List<Korisnik> GetUsersByEmail(string Email);
        List<Korisnik> GetUsersByUserId(int UserId);
        int GetLatestUserId();

    }
     public class KorisniciRepository : IKorisniciRepository
    {
         readonly StoredVehiclesDatabaseDbContext dbcontext;

        public KorisniciRepository()
        {
            dbcontext = new StoredVehiclesDatabaseDbContext();
        }

        public void InsertUser(Korisnik u)
        {
            dbcontext.Korisnici.Add(u);
            dbcontext.SaveChanges();
        }

        public void UpdateUserDetails(Korisnik u)
        {
            Korisnik us = dbcontext.Korisnici.Where(k => k.UserId == k.UserId).FirstOrDefault();
            if (us != null)
            {
                us.Name = u.Name;
                us.Mobile = u.Mobile;
                dbcontext.SaveChanges();
            }
        }

        public void UpdateUserPassword(Korisnik u)
        {
            Korisnik us = dbcontext.Korisnici.Where(k => k.UserId == k.UserId).FirstOrDefault();
            if(us != null)
            {
                us.PasswordHash = u.PasswordHash;
                dbcontext.SaveChanges();
            }
        }

        public void DeleteUser(int uid)
        {
            Korisnik us = dbcontext.Korisnici.Where(k => k.UserId == uid).FirstOrDefault();
            if (us != null)
            {
                dbcontext.Korisnici.Remove(us);
                dbcontext.SaveChanges();
            }
        }

        public List<Korisnik> GetUsers()
        {
            List<Korisnik> us = dbcontext.Korisnici.Where(k => k.IsAdmin == false).OrderBy(k => k.Name).ToList();
            return us;
        }

        public List<Korisnik> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<Korisnik> us = dbcontext.Korisnici.Where(k => k.Email == Email && k.PasswordHash == PasswordHash).ToList();
            return us;
        }

        public List<Korisnik> GetUsersByEmail(string Email)
        {
            List<Korisnik> us = dbcontext.Korisnici.Where(k => k.Email == Email).ToList();
            return us;
        }

        public List<Korisnik> GetUsersByUserId (int UserId)
        {
            List<Korisnik> us = dbcontext.Korisnici.Where(k => k.UserId == UserId).ToList();
            return us;
        }
        public int GetLatestUserId()
        {
            int uid = dbcontext.Korisnici.Select(k => k.UserId).Max();
            return uid;
        }

    }
}
