using MVC.Service;

namespace Core.ServiceLayer
{
    public class DiamondTakeServiceLayer   : ServiceLayer<int,int>
    {
        public override int GetContext()
        {
            return dto;
        }
    }
}



