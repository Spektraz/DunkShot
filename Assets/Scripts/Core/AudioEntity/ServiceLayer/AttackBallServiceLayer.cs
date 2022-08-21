using MVC.Service;

namespace Core.AudioEntity.ServiceLayer
{
    public class AttackBallServiceLayer : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}

