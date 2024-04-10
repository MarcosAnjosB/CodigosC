using Questao5.Domain.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Questao5.Business.Contract;
using Questao5.Domain.Helpers;
using System.Net;
using Questao5.Domain.Enumerators;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace Questao5.Business.Implementation
{
    public class MovimentoContaCorrenteManagement: IMovimentoContaCorrenteManagement
    {
        private readonly string _connectionString = "data source=DESKTOP-JJ4LACR\\SQLEXPRESS;initial catalog=DesafioAutomacao_Dev;Integrated Security=True;";
        public List<MovimentoContaCorrente> ConsultarSaldo(int ContaCorrente)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                    SELECT 
                        cc.numero,
                        cc.nome,
                        m.valor,
	                    m.tipomovimento
                    FROM 
                        contacorrente cc
                    INNER JOIN 
                        movimento m on cc.idcontacorrente = m.idcontacorrente
                    WHERE 
                        cc.numero = @ContaCorrente";

                    return connection.Query<MovimentoContaCorrente>(query, new { ContaCorrente = ContaCorrente }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fazer uma solicitação HTTP.", ex);
            }
        }

        public MovimentoResult ProcessarMovimento(string idRequisicao, string idcontacorrente, decimal vlmovimentacao, string tipomovimento)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Verificar se a conta corrente está cadastrada e ativa
                    var contaCorrente = connection.QueryFirstOrDefault<ContaCorrente>(
                        "SELECT * FROM contacorrente WHERE Id = @IdContaCorrente AND ativo = 1",
                        new { IdContaCorrente = idcontacorrente });

                    if (contaCorrente == null)
                    {
                        return new MovimentoResult { Sucesso = false, TipoFalha = TipoFalha.INVALID_ACCOUNT, Mensagem = "Conta corrente inválida." };
                    }

                    // Validar o valor
                    if (vlmovimentacao <= 0)
                    {
                        return new MovimentoResult { Sucesso = false, TipoFalha = TipoFalha.INVALID_VALUE, Mensagem = "Valor inválido." };
                    }

                    // Validar o tipo de movimento
                    if (tipomovimento != "C" && tipomovimento != "D")
                    {
                        return new MovimentoResult { Sucesso = false, TipoFalha = TipoFalha.INVALID_TYPE, Mensagem = "Tipo de movimento inválido." };
                    }

                    // Persistir o movimento na tabela MOVIMENTO
                    var result = connection.Execute(
                        "INSERT INTO movimento (idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@idcontacorrente, @datamovimento, @tipomovimento, @valor)",
                        new { idcontacorrente = idcontacorrente, datamovimento = Convert.ToString(DateTime.Now), valor = vlmovimentacao});

                    // Verificar se a operação foi bem-sucedida
                    if (result > 0)
                    {
                        // Obter o ID do movimento gerado
                        var idMovimento = connection.QueryFirstOrDefault<int>("SELECT @@IDENTITY");

                        return new MovimentoResult { Sucesso = true, IdMovimento = idMovimento };
                    }
                    else
                    {
                        return new MovimentoResult { Sucesso = false, Mensagem = "Falha ao processar o movimento." };
                    }
                }
            }
            catch (Exception ex)
            {
                // Logar a exceção
                return new MovimentoResult { Sucesso = false, Mensagem = "Falha ao processar o movimento." };
            }
        }
    }
}
