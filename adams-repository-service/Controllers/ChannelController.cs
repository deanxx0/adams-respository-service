using adams_repository_service.Data;
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
    [Route("channels")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private string _DbRoot;

        public ChannelController(IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _repositoryService = repositoryService;
        }

        [HttpGet("{projectId}")]
        public ActionResult GetAllChannels(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var channels = projectService.InputChannels.Find(x=>x.IsEnabled == true).ToList();
            return Ok(channels);
        }

        [HttpPost("{projectId}")]
        public ActionResult CreateChannels(string projectId, [FromBody]CreateChannelModel createChannelModel)
        {
            var entity = new InputChannel(
                createChannelModel.Name,
                createChannelModel.IsColor,
                createChannelModel.Description,
                createChannelModel.NamingRegex,
                true
                );
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            projectService.InputChannels.Add(entity);
            return Ok(entity);
        }
    }
}
