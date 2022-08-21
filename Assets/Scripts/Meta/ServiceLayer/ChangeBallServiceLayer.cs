using Meta.Model;
using MVC.Service;

namespace Meta.ServiceLayer
{
    public class ChangeBallServiceLayer : ServiceLayer<BallsName,BallsName>
    {
        public override BallsName GetContext()
        {
            return dto;
        }
    }
}

