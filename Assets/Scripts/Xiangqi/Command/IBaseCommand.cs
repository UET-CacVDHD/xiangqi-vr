using System.Linq;
using System.Text;

namespace Xiangqi.Command
{
    public abstract class IBaseCommand
    {
        public bool IsValid { get; set; }

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