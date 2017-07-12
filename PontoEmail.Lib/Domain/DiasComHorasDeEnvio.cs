using System;
using System.Collections.Generic;

namespace PontoEmail.Lib.Domain
{
    internal class DiasComHorasDeEnvio
    {
        private readonly DiasDeEnvio _diasDeEnvio;

        public DiasComHorasDeEnvio(DiasDeEnvio diasDeEnvio)
        {
            _diasDeEnvio = diasDeEnvio;
        }

        public IReadOnlyCollection<string> GetDiasComHorasDeEnvio()
        {
            var r = new Random();
            var listaDiasComHorarioEnvio = new List<string>();
            foreach (var diasEnvio in _diasDeEnvio.GetListaDeEnvio())
            {
                var dateTimeEntrada = DateTime.Parse(diasEnvio);
                dateTimeEntrada = dateTimeEntrada.AddHours(7);
                dateTimeEntrada = dateTimeEntrada.AddMinutes(55);

                var entrada = dateTimeEntrada.AddMinutes(r.Next(0, 10));

                var dateTimeSaida = DateTime.Parse(diasEnvio);
                dateTimeSaida = dateTimeSaida.AddHours(17);
                dateTimeSaida = dateTimeSaida.AddMinutes(25);

                var saida = dateTimeSaida.AddMinutes(r.Next(0, 10));

                listaDiasComHorarioEnvio.Add(entrada.ToString("dd/MM/yyyy HH:mm:ss"));
                listaDiasComHorarioEnvio.Add(saida.ToString("dd/MM/yyyy HH:mm:ss"));
            }

            return listaDiasComHorarioEnvio;
        }
    }
}