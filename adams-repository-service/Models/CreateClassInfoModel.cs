using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Models
{
    public class CreateClassInfoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public bool IsEnabled { get; set; }

        public CreateClassInfoModel(string name, string Description, byte R, byte G, byte B, bool isEnabled = true)
        {
            this.Name = name;
            this.IsEnabled = isEnabled;
            this.Description = Description;
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public CreateClassInfoModel()
        {

        }
    }
}
