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
    [Route("projects")]
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

        [HttpGet("{projectId}/channels")]
        public ActionResult GetAllChannels(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var channels = projectService.InputChannels.Find(x=>x.IsEnabled == true).ToList();
            return Ok(channels);
        }

        [HttpPost("{projectId}/channels")]
        public ActionResult CreateChannel(string projectId, [FromBody]CreateChannelModel createChannelModel)
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

        [HttpDelete("{projectId}/channels/{channelId}")]
        public ActionResult DeleteChannel(string projectId, string channelId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);

            var channel = projectService.InputChannels.Find(x => x.Id == channelId).FirstOrDefault();
            if (channel == null)
                throw new Exception();

            channel.SetValue("isenabled", false);

            projectService.InputChannels.Update(channel);

            return Ok(channel);
        }
    }
}
