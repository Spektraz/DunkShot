using BaseService.RegistryEntity.Service;
using Meta.Model;
using MVC.Factory;
using MVC.Service;

namespace Meta.ServiceLayer
{
    public class SaveBallsServiceLayer  : ServiceLayer<BallsName,BallsName>
    {
        public override BallsName GetContext()
        {
            ServiceFactory.GetService<SaveBallsServiceLayer>().UpdateDto(RegistryService.GetBallsMax());
            return dto;
        }
    }
}
