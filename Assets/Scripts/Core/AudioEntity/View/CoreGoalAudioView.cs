using BaseService.AudioEntity.View;
using Core.ServiceLayer;
using Meta.AudioEntity.Meta.Model;
using MVC.Controller;

namespace Core.AudioEntity.View
{
    public class CoreGoalAudioView : BaseMetaAudioView
    {
        protected override IController CreateController() => new CoreGoalAudioController(this);
    }

    public class CoreGoalAudioController : AudioMetaController<CoreGoalAudioView, AudioModel, IsAddScoreServiceLayer> 
    {
        protected override bool PlayOnAwake => false;
        public CoreGoalAudioController(CoreGoalAudioView view) : base(view)
        {
        }

        protected override void HandleCustomServiceLayer()
        {
            Execute();
        }
    }
}