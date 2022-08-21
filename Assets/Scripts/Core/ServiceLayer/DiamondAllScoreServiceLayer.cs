using BaseService.RegistryEntity.Service;
using MVC.Factory;
using MVC.Service;

namespace Core.ServiceLayer
{
    public class DiamondAllScoreServiceLayer  : ServiceLayer<int,int>
    {
        public override int GetContext()
        {
            ServiceFactory.GetService<DiamondAllScoreServiceLayer>().UpdateDto(RegistryService.GetDiamondMax());
            return dto;
        }
    }
}