using System;
using Xiangqi.Game;

namespace Xiangqi.Command.CommandType
{
    public class MetaCommand : BaseCommand
    {
        public MetaCommand(string text, GameSnapshot gss) : base(text, gss)
        {
        }

        // TODO: AI team - update this func
        protected override void ConvertTextToCode()
        {
        }

        protected override void ExecuteCommand()
        {
            throw new NotImplementedException();
        }
    }
}