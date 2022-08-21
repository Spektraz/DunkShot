using MVC.Service;

namespace Core.ServiceLayer
{
    public  class DeleteLineServiceLayer : ServiceLayer<bool,bool>
    {
        public override bool GetContext()
        {
            return dto;
        }
    }
}


