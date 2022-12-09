namespace Xiangqi.Command
{
    public class ExtendedMovementCommand : IBaseCommand
    {
        public string ExtendedDirection { get; set; }
        public long EndColumn { get; set; } = -1;
        public long NumberOfSteps { get; set; } = -1;
        public string StartChessType { get; set; }
        public long StartColumn { get; set; } = -1;
        public string StartExtendedRelativePosition { get; set; }
    }
}