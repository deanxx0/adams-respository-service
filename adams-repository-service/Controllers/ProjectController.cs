using adams_repository_service.Data;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVIAIServices.RepositoryService;
using NAVIAIServices.RepositoryService.Entities;
using System.Linq;

namespace adams_repository_service.Controllers
{
    [Route("")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private readonly AppDbContext _appDbContext;
        private string _DbRoot;

        public ProjectController(AppDbContext appDbContext, IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _appDbContext = appDbContext;
            _repositoryService = repositoryService;
        }

        [HttpGet("projects")]
        public ActionResult GetAllProject()
        {
            var projectInfos = _appDbContext.ProjectInfos.AsQueryable().ToList();
            var projects = projectInfos.Select(x => _repositoryService.GetProjectService(x.DbPath, DBType.LiteDB).Entity).Where(x=>x != null).ToList();
            return Ok(projects);
        }

        [HttpGet("projects/{id}")]
        public ActionResult<Project> GetProject(string id)
        {
            var dbpath = _appDbContext.ProjectInfos.Where(x => x.EntityId == id)
                                                    .FirstOrDefault()
                                                    .DbPath;

            var project = _repositoryService.GetProjectService(dbpath, DBType.LiteDB);
            var entity =  project.Entity;
            return entity;
        }

        [HttpPost("projects")]
        public ActionResult CreateProject([FromBody]CreateProjectModel createProjectModel)
        {
            var entity = new Project(
                createProjectModel.AIType, 
                createProjectModel.Name, 
                createProjectModel.Description
                );

            var dbpath = System.IO.Path.Combine(_DbRoot, entity.Id + ".db");
            var projectInfo = new ProjectInfo(entity.Id, dbpath);
            _appDbContext.ProjectInfos.Add(projectInfo);
            _appDbContext.SaveChanges();

            var project = _repositoryService.CreateProjectService(dbpath, DBType.LiteDB, entity);
            return Ok(project.Entity);
        }
    }
}
