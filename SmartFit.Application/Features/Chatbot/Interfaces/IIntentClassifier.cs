using SmartFit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Application.Features.Chatbot.Interfaces
{
    public interface IIntentClassifier
    {
        ChatIntent Predict(string message);
    }
}
