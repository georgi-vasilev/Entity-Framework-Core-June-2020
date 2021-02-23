namespace ProductShop.DTO.Users
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UserWithSoldProductsDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public List<UsersSoldProductDTO> SoldProducts { get; set; }
    }
}
