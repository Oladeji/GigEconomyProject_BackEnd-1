using IgpDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public interface IClientManager{

    Task<Client> FindAsync(ClientRegisterDto user );
    Task<Client> CreateAsync(ClientRegisterDto user );
    Task<CustomReturnType> RegisterClient(ClientRegisterDto model);
}