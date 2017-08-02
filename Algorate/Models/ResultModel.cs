using System.Collections.Generic;

namespace Algorate.Models
{
    public class ResultModel
    {
        public ResultModel(bool validated, float runTime, List<string> status)
        {
            Validated = validated;
            RunTime = runTime;
            Status = status;
        }
        public bool Validated { get; set; }
        public float RunTime { get; set; }
        public List<string> Status { get; set; }
    }
}