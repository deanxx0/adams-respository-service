
using NAVIAIServices.RepositoryService.Enums;

namespace adams_repository_service.Models
{
    public class CreateProjectModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public NAVIAITypes AIType { get; set; }

        public CreateProjectModel(string name, string description, NAVIAITypes aitype)
        {
            this.Name = name;
            this.Description = description;
            this.AIType = aitype;
        }

        public CreateProjectModel()
        {

        }
    }
}
