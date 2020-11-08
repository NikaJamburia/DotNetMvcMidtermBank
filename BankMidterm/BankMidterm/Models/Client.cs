using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace BankMidterm.Models
{
    public class Client
    {

        public Client(){ }
        [DataMember]
        public int Id { get; set; }

        [DataMember][Required] [MinLength(2)] [MaxLength(50)]
        public string FirstName { get; set; }

        [DataMember] [Required] [MinLength(2)] [MaxLength(50)]
        public string LastName { get; set; }

        [DataMember] [Required] [EnumDataType(typeof(ClientGender))]
        public ClientGender gender { get; set; }

        [DataMember] [Required] [MaxLength(11)] [MinLength(11)]
        public string IdentityNumber { get; set; }

        [DataMember][Required] [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataMember] [MinLength(4)] [MaxLength(50)]
        public string telephone { get; set; }

        [DataMember] [EmailAddress]
        public string Email { get; set; }

        [DataMember]
        public Country Country { get; set; }
        [DataMember]
        public City City { get; set; }

        [DataMember]
        public Voucher Voucher { get; set; }


    }
}