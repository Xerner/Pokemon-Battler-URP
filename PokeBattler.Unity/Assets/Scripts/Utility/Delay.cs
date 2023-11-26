using System;
using System.Threading.Tasks;

namespace PokeBattler.Core
{
    public class Delay
    {
        /// <summary>/// https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program</summary>
        public static async void DelayAction(int millisecond, Action action)
        {
           await Task.Delay(millisecond);
            action();
        }
    }
}
