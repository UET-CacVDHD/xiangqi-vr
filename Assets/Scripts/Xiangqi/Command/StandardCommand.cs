namespace Xiangqi.Command
{
    public class StandardCommand : IBaseCommand
    {
        public string Direction { get; set; }
        public long EndColumn { get; set; } = -1;
        public long NumberOfSteps { get; set; } = -1;
        public string StartChessType { get; set; }
        public long StartColumn { get; set; } = -1;
        public string StartVerticalRelativePosition { get; set; }
    }
}