using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser
{
    public class TestParser
    {
        public static readonly Parser<string> Expression;

        static TestParser()
        {
            var expression = Deferred<string>();

            expression.Parser = CommandCategory.Parser.Then(x => { return x; });


            Expression = expression;
        }
    }
}