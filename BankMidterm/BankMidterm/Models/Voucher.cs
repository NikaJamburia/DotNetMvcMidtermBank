using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BankMidterm.Models
{
    public class Voucher
    {
      
        public Voucher() { }

        public Voucher(RelationToClient relationToClient, string identityNumber)
        {
            RelationToClient = relationToClient;
            IdentityNumber = identityNumber;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        [EnumDataType(typeof(RelationToClient))]
        public RelationToClient RelationToClient { get; set; }
        [DataMember]
        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber { get; set; }

        public Client client;
        

    }
}