namespace JWTApp.Model
{
    public class UserDto
    {
        public required string userName { get; set; }

        public required string passwordHashed { get; set; }
    }
}
