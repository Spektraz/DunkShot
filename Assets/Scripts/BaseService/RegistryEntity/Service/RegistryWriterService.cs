using MVC.Service;

namespace BaseService.RegistryEntity.Service
{
    // public class RegistryWriterService : ServiceLayer<ICredential, CredentialDto>
    // {
    //     public override CredentialDto GetContext()
    //     {
    //         return new CredentialDto(dto);
    //     }
    // }
 

    public enum RegistryType
    {
        Unset = 0,
        AudioActiveState = 1,
        ScoreMax = 2,
        BuyBalls = 3,
        ScoreDiamond = 4,
    }
}
