using MongoDB.Bson.Serialization;

namespace EAD_TravelManagement.Models
{
    public class CustomIdGenerator : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);
            var id = randomNumber.ToString();
            return id;
        }

        public bool IsEmpty(object id)
        {
            return id == null || string.IsNullOrWhiteSpace(id.ToString());
        }
    }
}