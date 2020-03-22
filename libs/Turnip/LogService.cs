using System;
using System.Threading.Tasks;
using Discord;

namespace Turnip
{
    public class LogService
    {
        public Task Log(LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}