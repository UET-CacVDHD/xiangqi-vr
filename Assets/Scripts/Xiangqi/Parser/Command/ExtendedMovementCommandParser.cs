using Parlot.Fluent;
using Xiangqi.Parser.Chess;
using Xiangqi.Util;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Command
{
    public static class ExtendedMovementCommandParser
    {
        public static readonly Parser<PolymorphicCommand> Parser =
            CommandCategoryParser.Extended.Then(_ => new PolymorphicCommand())
                .AndSkip(MiscParser.Separator)
                .And(ChessTypeParser.Parser).Then((_, combined) =>
                {
                    var (command, chessType) = combined;
                    command.StartChessType = chessType;
                    return command;
                })
                .And(ZeroOrOne(ExtendedRelativePositionParser.Parser)).Then((_, combined) =>
                {
                    var (command, xrp) = combined;
                    command.StartExtendedRelativePosition = xrp;
                    return command;
                })
                .And(ZeroOrOne(Literals.Integer())).Then((_, combined) =>
                {
                    var (command, startColumn) = combined;
                    if (startColumn is <= 0 or > 9) return command;

                    command.StartColumn = startColumn;
                    return command;
                })
                .And(ExtendedDirectionParser.Parser).Then((_, combined) =>
                {
                    var (command, direction) = combined;
                    command.ExtendedDirection = direction;
                    return command;
                }).And(ZeroOrOne(Literals.Integer())).Then((_, combined) =>
                {
                    var (command, numOfStepOrEndColumn) = combined;
                    command.IsValid = true;

                    if (Utilities.IsUndefined(numOfStepOrEndColumn)) return command;

                    if (OneOf(
                            ChessTypeParser.Soldier, ChessTypeParser.Rook, ChessTypeParser.Cannon,
                            ChessTypeParser.General).TryParse(command.StartChessType, out var _)
                       )
                    {
                        if (OneOf(ExtendedDirectionParser.Forward, ExtendedDirectionParser.Backward,
                                ExtendedDirectionParser.Left, ExtendedDirectionParser.Right)
                            .TryParse(command.ExtendedDirection, out var _))
                            command.NumberOfSteps = numOfStepOrEndColumn;
                        else if (ExtendedDirectionParser.Sideways.TryParse(command.ExtendedDirection, out var _))
                            command.EndColumn = numOfStepOrEndColumn;
                    }
                    else
                    {
                        // Advisor, Horse, Elephant
                        if (OneOf(ExtendedDirectionParser.ForwardLeft, ExtendedDirectionParser.ForwardRight,
                                ExtendedDirectionParser.BackwardLeft, ExtendedDirectionParser.BackwardRight)
                            .TryParse(command.ExtendedDirection, out var _))
                            command.NumberOfSteps = numOfStepOrEndColumn;
                        else if (OneOf(ExtendedDirectionParser.Forward, ExtendedDirectionParser.Backward)
                                 .TryParse(command.ExtendedDirection, out var _))
                            command.EndColumn = numOfStepOrEndColumn;
                    }

                    return command;
                });
    }
}