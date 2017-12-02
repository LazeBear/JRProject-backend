using System;
using System.Threading.Tasks;

namespace LMS_Starter.Service
{
    public interface IMailService
    {
        Task Send(string address, string name, string subject,string content);
    }
}
