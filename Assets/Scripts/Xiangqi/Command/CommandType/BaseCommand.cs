using Xiangqi.Game;

namespace Xiangqi.Command.CommandType
{
    public abstract class BaseCommand
    {
        protected GameSnapshot gss;
        protected string text;

        protected BaseCommand(string text, GameSnapshot gss)
        {
            this.text = text;
            this.gss = gss;
        }

        public void HandleCommand()
        {
            ConvertTextToCode();
            ExecuteCommand();
        }

        protected abstract void ConvertTextToCode();
        protected abstract void ExecuteCommand();
    }
}