using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class dtoUserLogin
    {
        
        public int UserId { get; set; }
        public string Email { get; set; }
        public string? Token { get; set; } = "";
        public bool? IsLogin { get; set; } = false;
        public TimeSpan SessionDuration { get; set; }
    }
}
