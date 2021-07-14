using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Error
{
    internal class ErrorMessage
    {
        private Dictionary<string, string> messages = new();

        public ErrorMessage()
        {

        }

        public ErrorMessage(params (string key, string value)[] messages)
        {
            AddMessage(messages);
        }

        public void AddMessage(params(string key, string value)[] messages)
        {
            foreach (var message in messages)
            {
                this.messages.Add(message.key, message.value);
            }
        }

        public string GetMessage(string key)
        {
            return messages[key];
        }
    }
}
