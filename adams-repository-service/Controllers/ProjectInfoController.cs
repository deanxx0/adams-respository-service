﻿using adams_repository_service.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Controllers
{
    [Route("projectInfo")]
    [ApiController]
    public class ProjectInfoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProjectInfoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public ActionResult GetAllProjectInfo()
        {
            var projectInfos = _appDbContext.ProjectInfos.AsQueryable().ToList();
            return Ok(projectInfos);
        }

        [HttpGet("{id}")]
        public ActionResult GetProjectInfo(string id)
        {
            var projectInfo = _appDbContext.ProjectInfos.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            return Ok(projectInfo);
        }
    }
}
