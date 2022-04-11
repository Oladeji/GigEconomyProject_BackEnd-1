using Newtonsoft.Json;

// [JsonConverter(typeof(ConcreteTypeConverter<LoginDto>))]
// public interface ILoginDtoOld{

//     public string Email { get; set; }
// 	public string Password { get; set; }
// }


public class LoginDto{

    public string Email { get; set; }
	public string Password { get; set; }
}