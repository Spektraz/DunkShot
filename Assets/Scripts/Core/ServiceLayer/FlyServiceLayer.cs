using MVC.Service;

namespace Core.ServiceLayer
{
    public class FlyServiceLayer : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}

