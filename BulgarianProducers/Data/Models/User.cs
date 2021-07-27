using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BulgarianProducers.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        [Display(Name ="Името ви в сайта:")]
        public string Nickname { get; set; }
        [Required]
        [Display(Name ="Дата на раждане")]
        public DateTime DateOfBirth { get; set; }
        [Url]
        [Display(Name ="Ваша профилна снимка")]
        public string PhotoUrl { get; set; }
        [Display(Name = "Опишете себе си, допълнителна информация(тел.номер, адрес т.н.)")]
        [MaxLength(300)]
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
        public IEnumerable<Service> Services { get; set; } = new HashSet<Service>();
    }
}
