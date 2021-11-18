using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Models
{
    public class CreateChannelModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsColor { get; set; }
        public string NamingRegex { get; set; }
        public bool IsEnabled { get; set; }

        public CreateChannelModel(string name, bool isColor, string description, string namingRegex)
        {
            this.Name = name;
            this.IsColor = isColor;
            this.Description = description;
            this.NamingRegex = namingRegex;
        }
        public CreateChannelModel()
        {

        }
    }
}
