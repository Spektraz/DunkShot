using MVC.Service;

namespace Core.ServiceLayer
{
    public class IsAddScoreServiceLayer  : ServiceLayer<int,int>
    {
        public override int GetContext()
        {
            return dto;
        }
    }
}


