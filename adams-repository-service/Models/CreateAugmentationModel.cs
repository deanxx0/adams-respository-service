using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Models
{
    public class CreateAugmentationModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Mirror { get; set; }
        public bool Flip { get; set; }
        public bool Rotation90 { get; set; }

        public double Zoom { get; set; }
        public double Shift { get; set; }
        public double Tilt { get; set; }
        public double Rotation { get; set; }
        public double Contrast { get; set; }
        public double Brightness { get; set; }
        public double Shade { get; set; }
        public double Hue { get; set; }
        public double Saturation { get; set; }
        public double Noise { get; set; }
        public double Smoothing { get; set; }
        public double ColorNoise { get; set; }
        public double PartialFocus { get; set; }
        public double Probability { get; set; }
        public int RandomCount { get; set; }

        public CreateAugmentationModel()
        {

        }
    }
}
