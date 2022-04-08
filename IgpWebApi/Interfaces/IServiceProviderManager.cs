using IgpDAL;

public interface IServiceProviderManager{

    Task<Client> Find(ProviderRegisterDto user );
    Task<Client> Create(ProviderRegisterDto user );
    Task<CustomReturnType> RegisterClient(ProviderRegisterDto model);
}