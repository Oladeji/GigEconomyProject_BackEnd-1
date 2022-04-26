using IgpDAL;
using NetTopologySuite.Geometries;

public interface IServiceProviderManager{

    Task<IgpDAL.ServiceProvider> FindAsync(ProviderRegisterDto user );
    Task<IgpDAL.ServiceProvider> CreateAsync(ProviderRegisterDto user );
    Task<CustomReturnType> RegisterProvider(ProviderRegisterDto model);
    Task<List<IgpDAL.ServiceProvider>>   ViewallProvidersthatShowedIntension4aCustomer(string clientId);
    Task<List<IgpDAL.ServiceProvider>> ViewIntentionsForAJob(int  JobId);
    object ChangeToProviderRegisterDto2(Point location, List<IgpDAL.ServiceProvider> result);
}