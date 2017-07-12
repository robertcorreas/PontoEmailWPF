using System;
using System.Collections.Generic;
using FluentAssertions;
using PontoEmail.Lib.Domain;
using Xunit;

namespace PontoEmail.Lib.Tests
{
    public class DiasDeEnvioTests
    {
        private DiasDeEnvio _sut;

        [Theory]
        [InlineData("14/07/2017", "11/07/2017")]
        [InlineData("14/07/2017", "14/07/2017")]
        [InlineData("02/07/2017", "01/07/2017")]
        [InlineData("11/08/2017", "14/07/2017")]
        [InlineData("11/07/2017", "14/07/2011")]
        public void NãoDeveCriarComDatasDeInicioEFimForaDeOrdem(string dataInicial, string dataFinal)
        {
            var action = new Action(() => { _sut = new DiasDeEnvio(dataInicial, dataFinal); });

            action.ShouldThrow<ArgumentException>().WithMessage("Datas fora de ordem!");
        }

        [Theory]
        [InlineData("asdfasdf", "fdsafsadf7")]
        [InlineData("", null)]
        [InlineData("02/07//2017", "01///07/2017")]
        [InlineData("11--f06/2017", "14/07/2017")]
        [InlineData("11/07asdfsadf/2011", "14/07/2017")]
        public void DeveValidarSeÉData(string dataInicial, string dataFinal)
        {
            var action = new Action(() => { _sut = new DiasDeEnvio(dataInicial, dataFinal); });

            action.ShouldThrow<ArgumentException>().WithMessage("Datas com formato errado!");
        }

        [Fact]
        public void DeveObterListaDeDias()
        {
            _sut = new DiasDeEnvio("11/07/2017", "14/07/2017");

            var listaDeDiasDeEnvio = _sut.GetListaDeEnvio();

            var diasEsperados = new List<string>
            {
                "11/07/2017",
                "12/07/2017",
                "13/07/2017",
                "14/07/2017"
            };

            listaDeDiasDeEnvio.Should().Equal(diasEsperados);
        }

        [Fact]
        public void NãoDeveIncluirFinalDeSemana()
        {
            _sut = new DiasDeEnvio("03/07/2017", "17/07/2017");

            var listaDeDiasDeEnvio = _sut.GetListaDeEnvio();

            var diasEsperados = new List<string>
            {
                "03/07/2017",
                "04/07/2017",
                "05/07/2017",
                "06/07/2017",
                "07/07/2017",
                "10/07/2017",
                "11/07/2017",
                "12/07/2017",
                "13/07/2017",
                "14/07/2017",
                "17/07/2017"
            };

            listaDeDiasDeEnvio.Should().Equal(diasEsperados);
        }
    }
}