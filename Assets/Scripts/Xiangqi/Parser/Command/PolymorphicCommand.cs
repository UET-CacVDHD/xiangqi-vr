using System.Linq;
using System.Text;
using Xiangqi.Command;
using Xiangqi.Util;

namespace Xiangqi.Parser.Command
{
    public class PolymorphicCommand
    {
        public bool IsCapture { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCrossRiver { get; set; }
        public bool IsMeta { get; set; }
        public bool IsValid { get; set; }
        public string CheckType { get; set; }
        public string Direction { get; set; }
        public string EndChessTag { get; set; }
        public string EndChessType { get; set; }
        public long EndColumn { get; set; } = -1;
        public string EndExtendedRelativePosition { get; set; }
        public string EndVerticalRelativePosition { get; set; }
        public string ExtendedDirection { get; set; }
        public string MetaCommand { get; set; }
        public long NumberOfSteps { get; set; } = -1;
        public string StartChessTag { get; set; }
        public string StartChessType { get; set; }
        public long StartColumn { get; set; } = -1;
        public string StartExtendedRelativePosition { get; set; }
        public string StartVerticalRelativePosition { get; set; }

        public StandardCommand GetStandardCommand()
        {
            if (!IsValid || StartChessType == null || Direction == null || Utilities.IsUndefined(StartColumn))
                return null;

            return new StandardCommand
            {
                IsValid = true,
                StartChessType = StartChessType,
                StartVerticalRelativePosition = StartVerticalRelativePosition,
                StartColumn = StartColumn,
                Direction = Direction,
                NumberOfSteps = NumberOfSteps,
                EndColumn = EndColumn
            };
        }

        public ExtendedMovementCommand GetExtendedMovementCommand()
        {
            if (!IsValid || StartChessType == null || ExtendedDirection == null)
                return null;

            return new ExtendedMovementCommand
            {
                IsValid = true,
                StartChessType = StartChessType,
                StartExtendedRelativePosition = StartExtendedRelativePosition,
                StartColumn = StartColumn,
                ExtendedDirection = ExtendedDirection,
                NumberOfSteps = NumberOfSteps,
                EndColumn = EndColumn
            };
        }

        public override string ToString()
        {
            return GetType().GetProperties()
                .Select(info => (info.Name, Value: info.GetValue(this) ?? "(null)"))
                .Aggregate(
                    new StringBuilder(),
                    (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                    sb => sb.ToString());
        }
    }
}