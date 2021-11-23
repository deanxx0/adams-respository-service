using adams_repository_service.Auth;
using adams_repository_service.Data;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVIAIServices.RepositoryService;
using NAVIAIServices.RepositoryService.Entities;
using System;
using System.Linq;

namespace adams_repository_service.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize(Policy = PolicyNames.MemberOrAdmin)]
    public class ChannelController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private string _DbRoot;

        public ChannelController(IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _repositoryService = repositoryService;
        }

        [HttpGet("projects/{projectId}/channels")]
        public ActionResult GetAllChannels(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var channels = projectService.InputChannels.Find(x=>x.IsEnabled == true).ToList();
            return Ok(channels);
        }

        [HttpPost("projects/{projectId}/channels")]
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

        [HttpDelete("projects/{projectId}/channels/{channelId}")]
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
