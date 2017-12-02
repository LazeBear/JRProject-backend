using System;
namespace LMS_Starter.Model
{
    public class Address
    {
        public int Id { get; set; }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }

        public int? StudentId { get; set; }
        public int? LecturerId { get; set; }
        public Student Student { get; set; }
        public Lecturer Lecturer { get; set; }

        public Address()
        {
        }
        public static Address AddressMapping(Address address)
        {
            Address newAddress = new Address
            {
                Line1=address.Line1,
                Line2=address.Line2,
                City=address.City,
                State=address.State,
                PostCode=address.PostCode,
                Country=address.Country
            };
            return newAddress;
        }
    }
}
