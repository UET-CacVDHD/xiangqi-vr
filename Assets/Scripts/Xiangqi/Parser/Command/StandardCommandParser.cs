using Parlot.Fluent;
using Xiangqi.Parser.Chess;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Command
{
    public static class StandardCommandParser
    {
        public static readonly Parser<PolymorphicCommand> Parser =
            CommandCategoryParser.Standard.Then(_ => new PolymorphicCommand())
                .AndSkip(MiscParser.Separator)
                .And(ChessTypeParser.Parser).Then((_, combined) =>
                {
                    var (command, chessType) = combined;
                    command.StartChessType = chessType;
                    return command;
                })
                .And(ZeroOrOne(VerticalRelativePositionParser.Parser)).Then((_, combined) =>
                {
                    var (command, vrp) = combined;
                    command.StartVerticalRelativePosition = vrp;
                    return command;
                })
                .And(Literals.Integer()).When(combined =>
                {
                    var (_, startColumn) = combined;
                    return startColumn is >= 1 and <= 9;
                }).Then((_, combined) =>
                {
                    var (command, startColumn) = combined;
                    if (startColumn <= 0) return command;

                    command.StartColumn = startColumn;
                    return command;
                })
                .And(DirectionParser.Parser).Then((_, combined) =>
                {
                    var (command, direction) = combined;
                    command.Direction = direction;
                    return command;
                }).And(ZeroOrOne(Literals.Integer())).Then((_, combined) =>
                {
                    var (command, numOfStepOrEndColumn) = combined;
                    if (numOfStepOrEndColumn <= 0) return command;

                    if (OneOf(ChessTypeParser.Soldier, ChessTypeParser.Rook, ChessTypeParser.Cannon,
                                ChessTypeParser.General)
                            .TryParse(command.StartChessType, out var _) &&
                        OneOf(DirectionParser.Forward, DirectionParser.Backward).TryParse(command.Direction, out var _))
                        command.NumberOfSteps = numOfStepOrEndColumn;
                    else
                        command.EndColumn = numOfStepOrEndColumn;

                    command.IsValid = true;
                    return command;
                });
    }
}