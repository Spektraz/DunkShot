using BaseService.RegistryEntity.Service;
using MVC.Factory;
using MVC.Service;

namespace Meta.ServiceLayer
{
    public class BestScoreSaveServiceLayer : ServiceLayer<int,int>
    {
        public override int GetContext()
        {
            ServiceFactory.GetService<BestScoreSaveServiceLayer>().UpdateDto(RegistryService.GetStoreMax());
            return dto;
        }
    }
}



