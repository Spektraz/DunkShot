using MVC.Service;

namespace Core.ServiceLayer
{
    public class BallSetPlaceServiceLayer  : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}



