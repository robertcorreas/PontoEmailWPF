using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PontoEmail.Lib.Domain;

namespace PontoEmail.Lib.Services
{
    public class PontoEmailService
    {
        public ServicerResult<IReadOnlyCollection<string>> GerarDatasEnvio(string dataInicio, string datafim)
        {
            try
            {
                var diasDeEnvio = new DiasDeEnvio(dataInicio, datafim);
                var diasComHorasDeEnvio = new DiasComHorasDeEnvio(diasDeEnvio);

                var result = new ServicerResult<IReadOnlyCollection<string>>(diasComHorasDeEnvio.GetDiasComHorasDeEnvio());

                return result;
            }
            catch (ArgumentException e)
            {
                return new ServicerResult<IReadOnlyCollection<string>>(e.Message);
            }
            
        }
    }

    public class ServicerResult<T> where T : class
    {
        public string Message { get; }
        public bool IsOk { get; }
        public T Data { get; }

        public ServicerResult(T data)
        {
            IsOk = true;
            Data = data;
            Message = null;
        }

        public ServicerResult(string message)
        {
            Message = message;
            IsOk = false;
            Data = null;
        }


    }
}
