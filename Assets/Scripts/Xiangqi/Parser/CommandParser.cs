using Parlot.Fluent;
using Xiangqi.Parser.Chess;
using static Parlot.Fluent.Parsers;


namespace Xiangqi.Parser
{
    public class CommandParser
    {
        public static readonly Parser<(string, string, string?, long, string, long)> Parser;

        static CommandParser()
        {
            var expression = Deferred<(string, string, string, long, string, long)>();

            expression.Parser = CommandCategory.Parser
                .AndSkip(Misc.Separator)
                .And(ChessType.Parser)
                .And(ZeroOrOne(VerticalRelativePosition.Parser))
                .And(Terms.Integer())
                .And(Direction.Parser)
                .And(ZeroOrOne(Terms.Integer()));

            Parser = expression;
        }
    }
}