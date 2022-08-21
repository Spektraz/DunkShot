using MVC.Service;
using UnityEngine;

namespace Core.ServiceLayer
{
    public class TrajectoryServiceLayer : ServiceLayer<Vector2,Vector2>
    {
        public override Vector2 GetContext()
        {
            return dto;
        }
    }
}



