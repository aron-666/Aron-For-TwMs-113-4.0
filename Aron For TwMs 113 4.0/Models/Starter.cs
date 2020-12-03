using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aron_For_TwMs_113_4.Models
{

    public class ApiResualt<T>
    {
        public string result { get; set; }
        public T data { get; set; }
    }

    public class Data
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Site { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    

}
