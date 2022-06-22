using Shop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public class SmsService : ISmsService
    {
        public string apikey = "";
        public async  Task SendVirificationCode(string mobile, string activecode)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apikey);
            await api.VerifyLookup(mobile, activecode, "");
        }
    }
}
