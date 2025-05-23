﻿using System;
using dddnetcore.Domain.Projetos;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Indicadores
{
    public class Indicador : Entity<IndicadorId>, IAggregateRoot
    {
        public NomeIndicador Nome { get; private set; }
        public ValorAtual ValorAtual { get; private set; }
        public ValorMaximo ValorMaximo { get; private set; }
        public ProjetoId ProjetoId { get; private set; }

        private Indicador() { }

        public Indicador(string nome, double valorAtual, double valorMaximo, ProjetoId projetoId)
        {
            Id = new IndicadorId(Guid.NewGuid());
            Nome = new NomeIndicador(nome);
            ValorAtual = new ValorAtual(valorAtual);
            ValorMaximo = new ValorMaximo(valorMaximo);
            ProjetoId = projetoId;

            if (valorAtual > valorMaximo)
                throw new BusinessRuleValidationException("O valor atual não pode ser maior que o valor máximo.");
        }

        public void AtualizarValorAtual(double novoValor)
        {
            if (novoValor > ValorMaximo.Valor)
                throw new BusinessRuleValidationException("O valor atual não pode ser maior que o valor máximo.");

            ValorAtual = new ValorAtual(novoValor);
        }
    }
}