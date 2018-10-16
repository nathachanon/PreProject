using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASdemo.Models
{
    public class MyOwner
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tel { get; set; }
    }

    public class MyRoomType
    {
        public int getTid { get; set; }
        public string getType { get; set; }
        public int getPrice { get; set; }
    }

    public class MyDid
    {
        public int did { get; set; }
    }
}
