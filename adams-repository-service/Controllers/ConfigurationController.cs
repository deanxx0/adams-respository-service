using adams_repository_service.Auth;
using adams_repository_service.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = PolicyNames.MemberOrAdmin)]
    public class ConfigurationController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private string _DbRoot;

        public ConfigurationController(IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _repositoryService = repositoryService;
        }

        [HttpGet("projects/{projectId}/configurations")]
        public ActionResult GetAllConfiguration(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var configuration = projectService.TrainConfigurations.Find(x => x.IsEnabled == true).ToList();
            return Ok(configuration);
        }

        [HttpPost("projects/{projectId}/configurations")]
        public ActionResult CreateConfiguration(string projectId, [FromBody] CreateConfigurationModel createConfigurationModel)
        {
            var entity = new TrainConfiguration(
                createConfigurationModel.Name,
                createConfigurationModel.Description,
                createConfigurationModel.Width,
                createConfigurationModel.Height,
                createConfigurationModel.BatchSize,
                createConfigurationModel.PretrainModelPath,
                createConfigurationModel.MaxIteration,
                createConfigurationModel.StepCount,
                createConfigurationModel.BaseLearningRate,
                createConfigurationModel.Gamma,
                createConfigurationModel.UseCacheMemory,
                createConfigurationModel.GPUIndex,
                createConfigurationModel.SaveBestPosition,
                createConfigurationModel.SavingPercentage
                );
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            projectService.TrainConfigurations.Add(entity);
            return Ok(entity);
        }

        [HttpDelete("projects/{projectId}/configurations/{configurationId}")]
        public ActionResult DeleteConfiguration(string projectId, string configurationId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);

            var configuration = projectService.TrainConfigurations.Find(x => x.Id == configurationId).FirstOrDefault();
            if (configuration == null)
                throw new Exception();

            configuration.SetValue("isenabled", false);

            projectService.TrainConfigurations.Update(configuration);

            return Ok(configuration);
        }
    }
}
