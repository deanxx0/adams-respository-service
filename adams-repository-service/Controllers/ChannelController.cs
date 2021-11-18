using adams_repository_service.Data;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly AppDbContext _appDbContext;

        public ChannelController(AppDbContext appDbContext, IRepositoryService repositoryService)
        {
            _appDbContext = appDbContext;
            _repositoryService = repositoryService;
        }

        [HttpGet("{projectId}")]
        public ActionResult GetAllChannels(string projectId)
        {
            var baseDIr = @"D:\AdamsDBRoot";
            var dbPath = System.IO.Path.Combine(baseDIr, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var channels = projectService.InputChannels.FindAll().ToList();
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
                createChannelModel.IsEnabled
                );
            var baseDIr = @"D:\AdamsDBRoot";
            var dbPath = System.IO.Path.Combine(baseDIr, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            projectService.InputChannels.Add(entity);
            return Ok(entity);
        }
    }
}
