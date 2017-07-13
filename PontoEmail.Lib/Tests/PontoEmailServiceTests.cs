using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using PontoEmail.Lib.Services;
using Xunit;

namespace PontoEmail.Lib.Tests
{
    public class PontoEmailServiceTests
    {
        private PontoEmailService _sut;

        [Theory]
        [InlineData("11/07/2017", "12/07/2017")]
        [InlineData("11-07-2017", "12-07-2017")]
        public void DeveRetornarDatasHorasDeEnvio(string dataInicio, string dataFim)
        {
            _sut = new PontoEmailService();

            var result = _sut.GerarDatasEnvio(dataInicio, dataFim);

            result.IsOk.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Theory]
        [InlineData("12/07/2017", "11/07/2017")]
        [InlineData("11/07/2017", "12/07/2015")]
        [InlineData("11/08/2017", "12/07/2017")]
        public void DeveRetornarErroParaDatasInválidas(string dataInicio, string dataFim)
        {
            _sut = new PontoEmailService();

            var result = _sut.GerarDatasEnvio(dataInicio,dataFim);

            result.IsOk.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Message.Should().Be("Datas fora de ordem!");
        }

        [Theory]
        [InlineData("11/07/2017", "12/07/201asdf7")]
        [InlineData("11/07/20asdf17", "12/07/2017")]
        public void DeveRetornarErroParaDatasComFormatoErrado(string dataInicio, string dataFim)
        {
            _sut = new PontoEmailService();

            var result = _sut.GerarDatasEnvio(dataInicio, dataFim);

            result.IsOk.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Message.Should().Be("Datas com formato errado. Formato correto: dd/mm/aaaa ou dd-mm-aaaa!");
        }
    }
}
