using IgpDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public interface IClientManager{

    Task<Client> Find(ClientRegisterDto user );
    Task<Client> Create(ClientRegisterDto user );
    Task<CustomReturnType> RegisterClient(ClientRegisterDto model);
}