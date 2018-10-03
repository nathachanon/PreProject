using System;
using System.Collections.Generic;

namespace MASdemo.Context
{
    public partial class Owner
    {
        public Owner()
        {
            Dorm = new HashSet<Dorm>();
        }

        public int Oid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tel { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<Dorm> Dorm { get; set; }
    }
}
