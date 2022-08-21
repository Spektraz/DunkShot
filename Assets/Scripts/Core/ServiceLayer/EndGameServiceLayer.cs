using MVC.Service;

namespace Core.ServiceLayer
{
    public class EndGameServiceLayer : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}

