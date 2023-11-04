namespace HavaDurumu.Models
{
    public class ApiUsersDto
    {
        //Veri tabanıymiş gibi kullanacagım
        public static List<ApiUser> ApiUsers = new()
        {
            new ApiUser { Id = 1, UserName = "Olya", Password = "123456", Role="Admin"},
            new ApiUser { Id = 2, UserName= "Oguljemal", Password = "123456", Role="Süper Admin"}
        };
    }
}
