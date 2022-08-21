using BaseService.AudioEntity.View;
using Core.AudioEntity.ServiceLayer;
using Meta.AudioEntity.Meta.Model;
using MVC.Controller;

namespace Core.AudioEntity.View
{
    public class CoreAttackBallAudioView  : BaseMetaAudioView
    {
        protected override IController CreateController() => new CoreAttackBallAudioController(this);
    }

    public class CoreAttackBallAudioController : AudioMetaController<CoreAttackBallAudioView, AudioModel, AttackBallServiceLayer> 
    {
        protected override bool PlayOnAwake => false;
        public CoreAttackBallAudioController(CoreAttackBallAudioView view) : base(view)
        {
        }
        protected override void HandleCustomServiceLayer()
        {
            Execute();
        }
    }
}