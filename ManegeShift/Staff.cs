using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManegeShift
{
    public  class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public Staff(int id,string name,int status)
        {
            Id = id;
            Name = name;
            Status = status;
            
        }

        public Staff(int id, string name)
        {
            Id = id;
            Name = name;
          

        }



    }
}
