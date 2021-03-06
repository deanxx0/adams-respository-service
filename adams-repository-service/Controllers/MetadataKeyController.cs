using adams_repository_service.Auth;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVIAIServices.RepositoryService;
using NAVIAIServices.RepositoryService.Entities;
using NAVIAIServices.RepositoryService.Enums;
using System;
using System.Linq;

namespace adams_repository_service.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize(Policy = PolicyNames.MemberOrAdmin)]
    public class MetadataKeyController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private string _DbRoot;

        public MetadataKeyController(IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _repositoryService = repositoryService;
        }

        [HttpGet("projects/{projectId}/metadatakeys")]
        public ActionResult GetAllMetadataKey(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var metadataKeys = projectService.MetadataKeys.Find(x=>x.IsEnabled == true).ToList();
            return Ok(metadataKeys);
        }

        [HttpPost("projects/{projectId}/metadatakeys")]
        public ActionResult CreateMetadataKey(string projectId, [FromBody] CreateMetadataKeyModel createMetadataKeyModel)
        {

            MetadataTypes type = MetadataTypes.Boolean;
            try
            {
                type = convert(createMetadataKeyModel.Type);
            }
            catch (Exception)
            {
                return BadRequest($"invalid type {createMetadataKeyModel.Type}");
            }
           
            var entity = new MetadataKey(
                createMetadataKeyModel.Key,
                createMetadataKeyModel.Description,
                type,
                true);
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            projectService.MetadataKeys.Add(entity);
            return Ok(entity);
        }

        [HttpDelete("projects/{projectId}/metadatakeys/{metadatakeyId}")]
        public ActionResult DeleteMetadataKey(string projectId, string metadatakeyId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);

            var metadatakey = projectService.MetadataKeys.Find(x => x.Id == metadatakeyId).FirstOrDefault();
            if (metadatakey == null)
                throw new Exception();

            metadatakey.SetValue("isenabled", false);

            projectService.MetadataKeys.Update(metadatakey);

            return Ok(metadatakey);
        }

        private MetadataTypes convert(string typeStr)
        {
            foreach(MetadataTypes type in Enum.GetValues(typeof(MetadataTypes)))
            {
                if (type.ToString().ToLower() == typeStr.ToLower())
                    return type;
            }

            throw new Exception("MetadataType convert fail");

        }
    }
}
