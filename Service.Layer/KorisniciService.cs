using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainModels.Layer;
using Repository.Layer;
using ViewModels.Layer;


namespace Service.Layer
{
    public interface IKorisniciService
    {
        int InsertUser(RegisterViewModel rvm);
        void UpdateUserDetails(EditKorisnikDetailsViewModel ekdvm);
        void UpdateUserPassword(EditKorisnikPasswordViewModel ekpvm);
        void DeleteUser(int uid);
        List<KorisnikViewModel> GetUsers();
        KorisnikViewModel GetUsersByEmailAndPassword(string Email, string Password);
        KorisnikViewModel GetUsersByEmail(string Email);
        KorisnikViewModel GetUsersByUserId(int UserId);
    }
    public class KorisniciService : IKorisniciService
    {
        readonly IKorisniciRepository kr;

        public KorisniciService()
        {
            kr = new KorisniciRepository();
        }

        public int InsertUser(RegisterViewModel rvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, Korisnik>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Korisnik k = mapper.Map<RegisterViewModel, Korisnik>(rvm);
            k.PasswordHash = SHA256HashGenerator.GenerateHash(rvm.Password);
            kr.InsertUser(k);
            int uid = kr.GetLatestUserId();
            return uid;
        }

        public void UpdateUserDetails (EditKorisnikDetailsViewModel ekdvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditKorisnikDetailsViewModel, Korisnik>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Korisnik k = mapper.Map<EditKorisnikDetailsViewModel, Korisnik>(ekdvm);
            kr.UpdateUserDetails(k);

        }

        public void UpdateUserPassword(EditKorisnikPasswordViewModel ekpvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditKorisnikPasswordViewModel, Korisnik>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Korisnik k = mapper.Map<EditKorisnikPasswordViewModel, Korisnik>(ekpvm);
            k.PasswordHash = SHA256HashGenerator.GenerateHash(ekpvm.Password);
            kr.UpdateUserPassword(k);
        }

        public void DeleteUser(int uid)
        {
            kr.DeleteUser(uid);
        }

        public List<KorisnikViewModel> GetUsers()
        {
            List<Korisnik> k = kr.GetUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Korisnik, KorisnikViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<KorisnikViewModel> kvm = mapper.Map<List<Korisnik>, List<KorisnikViewModel>>(k);
            return kvm;
        }

        public KorisnikViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            Korisnik k = kr.GetUsersByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            KorisnikViewModel kvm = null;
            if (k != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Korisnik, KorisnikViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                kvm = mapper.Map<Korisnik, KorisnikViewModel>(k);
            }
            return kvm;
        }

        public KorisnikViewModel GetUsersByEmail(string Email)
        {
            Korisnik k = kr.GetUsersByEmail(Email).FirstOrDefault();
            KorisnikViewModel kvm = null;
            if (k != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Korisnik, KorisnikViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                kvm = mapper.Map<Korisnik, KorisnikViewModel>(k);
            }
            return kvm;
        }

        public KorisnikViewModel GetUsersByUserId(int UserId)
        {
            Korisnik k = kr.GetUsersByUserId(UserId).FirstOrDefault();
            KorisnikViewModel kvm = null;
            if (k != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Korisnik, KorisnikViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                kvm = mapper.Map<Korisnik, KorisnikViewModel>(k);
            }
            return kvm;
        }
    }
}
