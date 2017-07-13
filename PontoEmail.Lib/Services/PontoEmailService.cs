using System;
using System.Collections.Generic;
using PontoEmail.Lib.Domain;
using PontoEmail.Lib.Persistence;

namespace PontoEmail.Lib.Services
{
    internal class PontoEmailService
    {
        internal ServicerResult<IReadOnlyCollection<ItemDeEnvio>> GerarDatasEnvio(string dataInicio, string datafim)
        {
            try
            {
                var diasDeEnvio = new DiasDeEnvio(dataInicio, datafim);
                var diasComHorasDeEnvio = new DiasComHorasDeEnvio(diasDeEnvio);

                MemoryPersistence.DiasComHorasDeEnvio = diasComHorasDeEnvio;

                var result = new ServicerResult<IReadOnlyCollection<ItemDeEnvio>>(diasComHorasDeEnvio.GetDiasComHorasDeEnvio());

                return result;
            }
            catch (ArgumentException e)
            {
                return new ServicerResult<IReadOnlyCollection<ItemDeEnvio>>(e.Message);
            }
        }
    }
}