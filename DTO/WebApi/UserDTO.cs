using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.WebApi
{
    public class User
    {
        public int id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string phone { get; set; }
        public bool? isActive { get; set; }
    }

    public class UserAll
    {
        public int id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public bool? isActive { get; set; }
    }

    public class UserGet
    {
        public int id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
    }

    public class UserGetRequest
    {
        [Required]
        public int id { get; set; }

        public string email { get; set; }
    }

    public class GetPagedListRequest : PageListRequest
    {
        public string lastName { get; set; }
    }

    public class GetPagedList : PageList<IList<UserAll>>
    {
    }
}
