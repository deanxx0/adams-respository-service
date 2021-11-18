using System;

namespace adams_repository_service.Models
{
    public class ProjectInfo
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string DbPath { get; set; }

        public ProjectInfo(string entityId, string dbpath)
        {
            this.Id = Guid.NewGuid().ToString();
            this.EntityId = entityId;
            this.DbPath = dbpath;
            
        }

        public ProjectInfo()
        {

        }
    }
}
