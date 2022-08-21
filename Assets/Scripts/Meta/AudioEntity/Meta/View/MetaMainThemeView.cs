using BaseService.AudioEntity.View;
using Meta.AudioEntity.Meta.Model;
using MVC.Controller;

namespace Meta.AudioEntity.Meta.View
{
    public class MetaMainThemeView  : BaseMetaAudioView
    {
        protected override IController CreateController() => new MetaMainThemeController(this);
    }

    public class MetaMainThemeController : AudioMetaController<MetaMainThemeView, AudioModel> 
    {
        protected override bool PlayOnAwake => true;
        public MetaMainThemeController(MetaMainThemeView view) : base(view)
        {
        }
    }
}