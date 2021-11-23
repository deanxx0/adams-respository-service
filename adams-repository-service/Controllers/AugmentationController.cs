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
    public class AugmentationController : ControllerBase
    {
        IRepositoryService _repositoryService;
        private string _DbRoot;

        public AugmentationController(IRepositoryService repositoryService, IConfiguration configuration)
        {
            _DbRoot = configuration.GetValue<string>("DbRoot");
            _repositoryService = repositoryService;
        }

        [HttpGet("projects/{projectId}/augmentations")]
        public ActionResult GetAllAugmentation(string projectId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            var augmentation = projectService.Augmentations.Find(x => x.IsEnabled == true).ToList();
            return Ok(augmentation);
        }

        [HttpPost("projects/{projectId}/augmentations")]
        public ActionResult CreateAugmentation(string projectId, [FromBody] CreateAugmentationModel createAugmentationModel)
        {
            var entity = new Augmentation(
                createAugmentationModel.Name,
                createAugmentationModel.Description,
                createAugmentationModel.Mirror,
                createAugmentationModel.Flip,
                createAugmentationModel.Rotation90,
                createAugmentationModel.Zoom,
                createAugmentationModel.Shift,
                createAugmentationModel.Tilt,
                createAugmentationModel.Rotation,
                createAugmentationModel.BorderMode,
                createAugmentationModel.Contrast,
                createAugmentationModel.Brightness,
                createAugmentationModel.Shade,
                createAugmentationModel.Hue,
                createAugmentationModel.Saturation,
                createAugmentationModel.Noise,
                createAugmentationModel.Smoothing,
                createAugmentationModel.ColorNoise,
                createAugmentationModel.PartialFocus,
                createAugmentationModel.Probability,
                createAugmentationModel.RandomCount
                );
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);
            projectService.Augmentations.Add(entity);
            return Ok(entity);
        }

        [HttpDelete("projects/{projectId}/augmentations/{augmentationId}")]
        public ActionResult DeleteAugmentation(string projectId, string augmentationId)
        {
            var dbPath = System.IO.Path.Combine(_DbRoot, projectId + ".db");
            var projectService = _repositoryService.GetProjectService(dbPath, DBType.LiteDB);

            var augmentation = projectService.Augmentations.Find(x => x.Id == augmentationId).FirstOrDefault();
            if (augmentation == null)
                throw new Exception();

            augmentation.SetValue("isenabled", false);

            projectService.Augmentations.Update(augmentation);

            return Ok(augmentation);
        }
    }
}
