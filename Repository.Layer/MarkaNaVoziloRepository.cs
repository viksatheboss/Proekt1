using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Layer;

namespace Repository.Layer
{
    public interface IMarkaNaVoziloRepository
    {
        void InsertModel(MarkaNaVozilo mv);
        void UpdateModel(MarkaNaVozilo mv);
        void DeleteModel(int mid);
        List<MarkaNaVozilo> GetModels();
        List<MarkaNaVozilo> GetModelsByModelId(int ModelId);

    }
     public class MarkaNaVoziloRepository : IMarkaNaVoziloRepository
    {
         readonly StoredVehiclesDatabaseDbContext dbcontext;

        public MarkaNaVoziloRepository()
        {
            dbcontext = new StoredVehiclesDatabaseDbContext();
        }

        public void InsertModel (MarkaNaVozilo mv)
        {
            dbcontext.MarkaNaVozilo.Add(mv);
            dbcontext.SaveChanges();
        }

        public void UpdateModel (MarkaNaVozilo mv)
        {
            MarkaNaVozilo mvo = dbcontext.MarkaNaVozilo.Where(m => m.ModelId == m.ModelId).FirstOrDefault();
            if(mvo != null)
            {
                mvo.ModelName = mv.ModelName;
                dbcontext.SaveChanges();
            }
        }

        public void DeleteModel (int dim)
        {
            MarkaNaVozilo mv = dbcontext.MarkaNaVozilo.Where(m => m.ModelId == dim).FirstOrDefault();
            if (mv != null)
            {
                dbcontext.MarkaNaVozilo.Remove(mv);
                dbcontext.SaveChanges();
            }
        }

        public List<MarkaNaVozilo> GetModels()
        {
            List<MarkaNaVozilo> mv = dbcontext.MarkaNaVozilo.ToList();
            return mv;

        }

        public List<MarkaNaVozilo> GetModelsByModelId(int ModelId)
        {
            List<MarkaNaVozilo> mv = dbcontext.MarkaNaVozilo.Where(m => m.ModelId == ModelId).ToList();
            return mv;
        }



    }
}
