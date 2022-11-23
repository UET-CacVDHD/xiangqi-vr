using System.Collections.Generic;
using Xiangqi.Enum;

namespace Xiangqi.Movement
{
    public class Path
    {
        public readonly List<Direction> directions;
        public readonly int maxSteps;

        public Path(List<Direction> directions, int maxSteps)
        {
            this.directions = directions;
            this.maxSteps = maxSteps;
        }
    }
}