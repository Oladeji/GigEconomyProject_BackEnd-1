using IgpDAL;

public interface IServiceProviderManager{

    Task<IgpDAL.ServiceProvider> FindAsync(ProviderRegisterDto user );
    Task<IgpDAL.ServiceProvider> CreateAsync(ProviderRegisterDto user );
    Task<CustomReturnType> RegisterProvider(ProviderRegisterDto model);
}