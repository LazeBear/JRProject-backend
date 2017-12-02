using System;
using System.Threading.Tasks;

namespace LMS_Starter.Service
{
    public interface ISMSService
    {
       Task SendSMS(string number, string content);
    }
}
