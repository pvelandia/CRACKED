using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Dtos
{
    public class UserListDto
    {
        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public int Response { get; set; }
        public string Message { get; set; }

        
    }
}