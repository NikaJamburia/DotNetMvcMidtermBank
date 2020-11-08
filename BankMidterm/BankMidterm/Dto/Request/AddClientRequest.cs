﻿using BankMidterm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BankMidterm.Dto.Request
{
    public class AddClientRequest
    {
        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        [EnumDataType(typeof(ClientGender))]
        public ClientGender gender { get; set; }

        [DataMember]
        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataMember]
        [MinLength(4)]
        [MaxLength(50)]
        public string telephone { get; set; }

        [DataMember]
        [EmailAddress]
        public string Email { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public string VoucherId { get; set; }

        [DataMember]
        public RelationToClient VoucherRelationToClient { get; set; }
    }
}