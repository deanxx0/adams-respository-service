using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Models
{
    public class CreateConfigurationModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int BatchSize { get; set; }
        public string PretrainModelPath { get; set; }
        public int MaxIteration { get; set; }
        public int StepCount { get; set; }
        public double BaseLearningRate { get; set; }
        public double Gamma { get; set; }
        public bool UseCacheMemory { get; set; }
        public int GPUIndex { get; set; }
        public bool SaveBestPosition { get; set; }
        public double SavingPercentage { get; set; }

        public CreateConfigurationModel()
        {

        }

        //public CreateConfigurationModel(
        //    string name,
        //    string description,
        //    int width,
        //    int height,
        //    int batchSize,
        //    string pretrainPath,
        //    int maxIteration,
        //    int stepCount,
        //    double baseLearningRate,
        //    double gamma,
        //    bool UseCacheMemory,
        //    int gpuIndex,
        //    bool saveBestPosition,
        //    double savingPercentage
        //    )
        //{
        //    this.Name = name;
        //    this.Description = description;
        //    this.Width = width;
        //    this.Height = height;
        //    this.BatchSize = batchSize;
        //    this.PretrainModelPath = pretrainPath;
        //    this.MaxIteration = maxIteration;
        //    this.StepCount = stepCount;
        //    this.BaseLearningRate = baseLearningRate;
        //    this.Gamma = gamma;
        //    this.UseCacheMemory = UseCacheMemory;
        //    this.GPUIndex = gpuIndex;
        //    this.SaveBestPosition = saveBestPosition;
        //    this.SavingPercentage = savingPercentage;
        //}
    }
}
