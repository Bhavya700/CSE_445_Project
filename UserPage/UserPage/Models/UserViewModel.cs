using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserPage.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CoinDataJson { get; set; }
        
        //maybe add stuff for emails if you wanted... etc. 
    }
}