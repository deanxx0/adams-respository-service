using adams_repository_service.Data;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.RepositoryService;
using NAVIAIServices.RepositoryService.Entities;
using NAVIAIServices.SDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Controllers
{
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private readonly AppDbContext _appDbContext;

        public ProjectController(AppDbContext appDbContext, IRepositoryService repositoryService)
        {
            _appDbContext = appDbContext;
            _repositoryService = repositoryService;
        }

        [HttpGet("")]
        public ActionResult GetAllProject()
        {
            var projectInfos = _appDbContext.ProjectInfos.AsQueryable().ToList();
            var projects = projectInfos.Select(x => _repositoryService.GetProjectService(x.DbPath, DBType.LiteDB).Entity).ToList();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(string id)
        {
            var dbpath = _appDbContext.ProjectInfos.Where(x => x.EntityId == id)
                                                    .FirstOrDefault()
                                                    .DbPath;

            var project = _repositoryService.GetProjectService(dbpath, DBType.LiteDB);
            var entity =  project.Entity;
            return entity;
        }

        [HttpPost("")]
        //public ActionResult CreateProject(string name, string description, NAVIAITypes type)
        public ActionResult CreateProject([FromBody]CreateProjectModel createProjectModel)
        {
            //var entity = new Project(type, name, description);
            var entity = new Project(
                createProjectModel.AIType, 
                createProjectModel.Name, 
                createProjectModel.Description
                );

            var dbpath = CreateNewDbPath(entity.Id);
            var projectInfo = new ProjectInfo(entity.Id, dbpath);
            _appDbContext.ProjectInfos.Add(projectInfo);

            var project = _repositoryService.CreateProjectService(dbpath, DBType.LiteDB, entity);
            return Ok(project.Entity);
        }

        private string CreateNewDbPath(string id)
        {
            var baseDIr = @"D:\AdamsDBRoot";
            return System.IO.Path.Combine(baseDIr, id + ".db");
        }
    }
}
