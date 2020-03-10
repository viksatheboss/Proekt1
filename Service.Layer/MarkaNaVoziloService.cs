using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Layer;
using ViewModels.Layer;
using Repository.Layer;
using AutoMapper;

namespace Service.Layer
{
    public interface IMarkaNaVoziloService
    {
        void InsertModel(MarkaNaVoziloViewModel mvm);
        void UpdateModel(MarkaNaVoziloViewModel mvm);
        void DeleteModel(int mid);
        List<MarkaNaVoziloViewModel> GetModels();
        MarkaNaVoziloViewModel GetModelsByModelId(int ModelId);
    }
    public class MarkaNaVoziloService : IMarkaNaVoziloService
    {
        readonly IMarkaNaVoziloRepository mr;

        public MarkaNaVoziloService()
        {
            mr = new MarkaNaVoziloRepository();
        }

        public void InsertModel(MarkaNaVoziloViewModel mvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<MarkaNaVoziloViewModel, MarkaNaVozilo>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            MarkaNaVozilo mv = mapper.Map<MarkaNaVoziloViewModel, MarkaNaVozilo>(mvm);
            mr.InsertModel(mv);
        }

        public void UpdateModel(MarkaNaVoziloViewModel mvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<MarkaNaVoziloViewModel, MarkaNaVozilo>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            MarkaNaVozilo mv = mapper.Map<MarkaNaVoziloViewModel, MarkaNaVozilo>(mvm);
            mr.UpdateModel(mv);
        }

        public void DeleteModel(int mid)
        {
            mr.DeleteModel(mid);
        }

        public List<MarkaNaVoziloViewModel> GetModels()
        {
            List<MarkaNaVozilo> mv = mr.GetModels();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<MarkaNaVozilo, MarkaNaVoziloViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<MarkaNaVoziloViewModel> mvm = mapper.Map<List<MarkaNaVozilo>, List<MarkaNaVoziloViewModel>>(mv);
            return mvm;
        }

        public MarkaNaVoziloViewModel GetModelsByModelId(int ModelId)
        {
            MarkaNaVozilo mv = mr.GetModelsByModelId(ModelId).FirstOrDefault();
            MarkaNaVoziloViewModel mvm = null;
            if (mv != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<MarkaNaVozilo, MarkaNaVoziloViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                mvm = mapper.Map<MarkaNaVozilo, MarkaNaVoziloViewModel>(mv);
            }
            return mvm;
        }
    }
}
