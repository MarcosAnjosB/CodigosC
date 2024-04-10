using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Questao1
{
    class ContaBancaria
    {
        List<Conta> Conta = new List<Conta>();
        public void CadastroConta(int numero, string titular, double depositoInicial, string senha)
        {
            try
            {
                bool contaEncontrada = false;

                foreach (Conta conta in Conta)
                {
                    contaEncontrada = true;
                }

                if (!contaEncontrada)
                {
                    Conta conta = new Conta();
                    conta.numConta = numero;
                    conta.nomeTitular = titular;
                    conta.depValor = depositoInicial;
                    conta.senha = senha;
                    Conta.Add(conta);
                    Console.WriteLine("Dados da conta:");
                    Console.WriteLine("Conta " + conta.numConta + " ,Titular: " + conta.nomeTitular + " ,Saldo:" + conta.depValor);
                }
                else
                {
                    Console.WriteLine("Conta " + numero + " já existente!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível inserir a Conta " + numero);
            }
        }
        public void AlteraDadosConta(int numero, string titular)
        {
            try
            {
                bool contaEncontrada = false;

                foreach (Conta conta in Conta)
                {
                    if (conta.numConta == numero && conta.nomeTitular != titular)
                    {
                        Conta con = new Conta();
                        con.numConta = numero;
                        con.nomeTitular = titular;
                        int indice = Conta.IndexOf(conta);
                        Conta[indice] = con;

                        Console.WriteLine("Dados da conta:");
                        Console.WriteLine("Conta " + conta.numConta + " ,Titular: " + conta.nomeTitular + " ,Saldo:" + conta.depValor);

                        break;
                    }
                    else if (conta.numConta != numero && conta.nomeTitular == titular)
                    {
                        Console.WriteLine("Não é possível modificar o número da conta!");

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível atualizar a Conta " + numero);
            }
        }

        public void Deposito(int numero, double deposito, string senha)
        {
            try
            {
                foreach (Conta conta in Conta)
                {
                    if (conta.numConta == numero)
                    {
                        if (conta.senha == senha)
                        {
                            Conta con = new Conta();
                            con.numConta = numero;
                            con.nomeTitular = conta.nomeTitular;
                            con.depValor = deposito + conta.depValor;
                            con.senha = senha;
                            int indice = Conta.IndexOf(conta);
                            Conta[indice] = con;

                            Console.WriteLine("Dados da conta:");
                            Console.WriteLine("Conta " + con.numConta + " ,Titular: " + con.nomeTitular + " ,Saldo:" + con.depValor);

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Senha incorreta!");

                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Conta não encontrada!");

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível inserir valores em sua conta");
            }
        }

        public void Saque(int numero, double retirada, string senha)
        {
            try
            {
                foreach (Conta conta in Conta)
                {
                    if (conta.numConta == numero)
                    {
                        if (conta.senha == senha)
                        {
                            Conta con = new Conta();
                            con.numConta = numero;
                            con.nomeTitular = conta.nomeTitular;
                            con.depValor = 0;
                            con.depValor = conta.depValor - retirada - 3.50;
                            con.senha = senha;
                            int indice = Conta.IndexOf(conta);
                            Conta[indice] = con;

                            Console.WriteLine("Dados da conta:");
                            Console.WriteLine("Conta " + con.numConta + " ,Titular: " + con.nomeTitular + " ,Saldo:" + con.depValor);

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Senha incorreta!");

                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Conta não encontrada!");

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível retirar os valores em sua conta");
            }
        }
    }

    public class Conta
    {
        public int numConta { get; set; }
        public string nomeTitular { get; set; }
        public double? depValor { get; set; }
        public string senha { get; set; }
    }
}
