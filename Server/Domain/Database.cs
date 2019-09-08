using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server.Domain
{
    class Database
    {
        public List<Users> users { get; set; }

    }

    class Users
    {
        public string name { get; set; }

    }
}
