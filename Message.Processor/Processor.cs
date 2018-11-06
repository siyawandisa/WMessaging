using System;
using System.Linq;

namespace Message.Processor
{
    public class Processor
    {
        public static string GetName(string message){
            var name = "NONAME";
            var strings = message.Split(',');
            if (strings.Count() > 1){
                var str = strings[1].Trim();
                name = string.IsNullOrEmpty(str) ? name : str;
            }
            return name;
        }
    }
}
