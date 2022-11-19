using System.Collections;

namespace Xiangqi.Movement
{
    public class Path
    {
        public Path(ArrayList directions, int maxSteps)
        {
            Directions = directions;
            MaxSteps = maxSteps;
        }

        public ArrayList Directions { get; }

        public int MaxSteps { get; }
    }
}