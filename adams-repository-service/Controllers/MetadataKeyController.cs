using adams_repository_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVIAIServices.RepositoryService;
using NAVIAIServices.RepositoryService.Entities;
using NAVIAIServices.RepositoryService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Controllers
{
    [Route("")]
    [ApiController]
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
