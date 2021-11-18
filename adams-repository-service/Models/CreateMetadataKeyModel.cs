using NAVIAIServices.RepositoryService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Models
{
    public class CreateMetadataKeyModel
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }

        public CreateMetadataKeyModel(string key, string description, string type)
        {
            this.Key = key;
            this.Type = type;
            this.Description = description;
        }

        public CreateMetadataKeyModel()
        {

        }
    }
}
