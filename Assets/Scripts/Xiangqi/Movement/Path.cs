using System.Collections.Generic;
using Xiangqi.Enum;

namespace Xiangqi.Movement.Cell
{
    public class Path
    {
        public List<Direction> directions;
        public int maxSteps;

        public Path(List<Direction> directions, int maxSteps)
        {
            this.directions = directions;
            this.maxSteps = maxSteps;
        }
    }
}