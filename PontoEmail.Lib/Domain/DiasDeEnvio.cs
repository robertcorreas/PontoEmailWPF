using System;
using System.Collections.Generic;

namespace PontoEmail.Lib.Domain
{
    internal class DiasDeEnvio
    {
        public DiasDeEnvio(string dataInicio, string dataFim)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;

            if (!DatasFormatoCorreto()) throw new ArgumentException("Datas com formato errado. Formato correto: dd/mm/aaaa ou dd-mm-aaaa!");

            if (!DatasVálidas()) throw new ArgumentException("Datas fora de ordem!");
        }

        public string DataInicio { get; }
        public string DataFim { get; }

        private bool DatasFormatoCorreto()
        {
            DateTime value;

            return DateTime.TryParse(DataInicio, out value) &&
                   DateTime.TryParse(DataFim, out value);
        }

        private bool DatasVálidas()
        {
            var datainicial = DateTime.Parse(DataInicio);
            var datafim = DateTime.Parse(DataFim);

            if (datainicial.Year <= datafim.Year)
            {
                if (datainicial.Month <= datafim.Month)
                {
                    if (datainicial.Day < datafim.Day)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public IReadOnlyCollection<string> GetListaDeEnvio()
        {
            var datainicial = DateTime.Parse(DataInicio);
            var datafim = DateTime.Parse(DataFim);

            var dataMeio = datafim - datainicial;

            var listaEnvio = new List<string> {DataInicio};

            for (var i = 1; i < dataMeio.Days; i++)
            {
                var dataIncremental = datainicial.AddDays(i);

                if (IsFinalDeSemana(dataIncremental)) continue;

                listaEnvio.Add(dataIncremental.ToString("dd/MM/yyyy"));
            }

            listaEnvio.Add(DataFim);

            return listaEnvio;
        }


        private bool IsFinalDeSemana(DateTime data)
        {
            var diaDaSemana = data.DayOfWeek;

            if (diaDaSemana == DayOfWeek.Sunday || diaDaSemana == DayOfWeek.Saturday)
                return true;
            return false;
        }
    }
}