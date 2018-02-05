using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public Nullable<DateTime> LastLoginTime { get; set; }
    }
}
