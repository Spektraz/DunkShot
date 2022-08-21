using MVC.Service;

namespace Core.ServiceLayer
{
    public class HowManyScoreServiceLayer   : ServiceLayer<int,int>
    {
        public override int GetContext()
        {
            return dto;
        }
    }
}


