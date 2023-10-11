/*
 * File: CustomIdGenerator.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the custom id generation.
 */


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