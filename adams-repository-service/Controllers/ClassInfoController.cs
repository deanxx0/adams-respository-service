using adams_repository_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVIAIServices.RepositoryService;
using NAVIAIServices.RepositoryService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Controllers
{
    [Route("")]
    [ApiController]
    public class ClassInfoController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private string _DbRoot;

        public ClassInfoController(IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _repositoryService = repositoryService;
        }

        [HttpGet("projects/{projectId}/classinfos")]
        public ActionResult GetAllClassInfo(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var classInfo = projectService.ClassInfos.FindAll().ToList();
            return Ok(classInfo);
        }

        [HttpPost("projects/{projectId}/classinfos")]
        public ActionResult CreateClassInfo(string projectId, [FromBody] CreateClassInfoModel createClassInfoModel)
        {
            var entity = new ClassInfo(
                createClassInfoModel.Name,
                createClassInfoModel.Description,
                createClassInfoModel.R,
                createClassInfoModel.G,
                createClassInfoModel.B,
                true
                );
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            projectService.ClassInfos.Add(entity);
            return Ok(entity);
        }
    }
}
