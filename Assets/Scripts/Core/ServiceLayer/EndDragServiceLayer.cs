using MVC.Service;

namespace Core.ServiceLayer
{
    public class EndDragServiceLayer  : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}

