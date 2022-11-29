using System.Collections.Generic;
using System.Linq;
using Xiangqi.Command.CommandType;
using Xiangqi.Game;

namespace Xiangqi.Command
{
    public static class CommandParser
    {
        private static readonly List<string> MovementCmdPhrases = new()
        {
            "xe",
            "mã",
            "tượng",
            "sĩ",
            "tướng",
            "pháo",
            "tốt"
        };

        private static readonly List<string> MetaCmdPhrases = new()
        {
            "thua",
            "hoà"
        };

        // TODO: AI - team 
        public static BaseCommand CreateCommand(string text, GameSnapshot gss)
        {
            if (MovementCmdPhrases.Any(text.Contains)) return new MovementCommand(text, gss);
            if (MetaCmdPhrases.Any(text.Contains)) return new MetaCommand(text, gss);

            return null;
        }
    }
}