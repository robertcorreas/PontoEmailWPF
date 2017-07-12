using System;
using System.Linq;
using FluentAssertions;
using PontoEmail.Lib.Domain;
using Xunit;

namespace PontoEmail.Lib.Tests
{
    public class DiasComHorasDeEnvioTests
    {
        private DiasComHorasDeEnvio _sut;

        private void AssertDataHoraEnvioEntrada(DateTime primeiroDiaHora)
        {
            if (primeiroDiaHora.Hour == 8 && primeiroDiaHora.Minute >= 0 && primeiroDiaHora.Minute <= 5)
            {
                Assert.True(true);
                return;
            }
            if (primeiroDiaHora.Hour == 7 && primeiroDiaHora.Minute >= 55 && primeiroDiaHora.Minute <= 59)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        private void AssertDataHoraEnvioSaida(DateTime primeiroDiaHora)
        {
            if (primeiroDiaHora.Hour == 17 && primeiroDiaHora.Minute >= 25 && primeiroDiaHora.Minute <= 35)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void DeveObterListaComHorariosDeEnvio()
        {
            var diasDeEnvio = new DiasDeEnvio("11/07/2017", "14/07/2017");

            _sut = new DiasComHorasDeEnvio(diasDeEnvio);

            var diasComHorasDeEnvio = _sut.GetDiasComHorasDeEnvio().ToList();

            for (var i = 0; i < diasComHorasDeEnvio.Count; i += 2)
            {
                var datahoraEntrada = DateTime.Parse(diasComHorasDeEnvio[i]);
                var datahoraSaida = DateTime.Parse(diasComHorasDeEnvio[i + 1]);

                datahoraEntrada.Day.Should().Be(datahoraSaida.Day);
                datahoraEntrada.Month.Should().Be(datahoraSaida.Month);
                datahoraEntrada.Year.Should().Be(datahoraSaida.Year);

                AssertDataHoraEnvioEntrada(datahoraEntrada);
                AssertDataHoraEnvioSaida(datahoraSaida);
            }
        }
    }
}