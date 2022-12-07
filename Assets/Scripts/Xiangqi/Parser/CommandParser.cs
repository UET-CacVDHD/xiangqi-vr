using Parlot.Fluent;
using Xiangqi.Parser.Chess;
using Xiangqi.Parser.Command;
using static Parlot.Fluent.Parsers;


namespace Xiangqi.Parser
{
    public class CommandParser
    {
        public static readonly Parser<PolymorphicCommand> Parser;

        static CommandParser()
        {
            var expression = Deferred<PolymorphicCommand>();

            expression.Parser = OneOf(StandardCommandParser.Parser);

            Parser = expression;
        }
    }
}