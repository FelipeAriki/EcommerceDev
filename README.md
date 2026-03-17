🛒 EcommerceDev
EcommerceDev é uma API de e-commerce desenvolvida com ASP.NET Core 10, focada em boas práticas, escalabilidade e arquitetura moderna.
O projeto aplica conceitos avançados de engenharia de software, como Clean Architecture e CQRS, promovendo separação de responsabilidades, alta manutenibilidade e facilidade de evolução.
A aplicação também integra mensageria, cache distribuído, comunicação em tempo real e serviços externos, simulando um cenário próximo ao de aplicações reais de mercado.

🚀 Tecnologias e Conceitos Utilizados
Backend
  ASP.NET Core 10;
  Entity Framework Core;
  PostgreSQL;
  Arquitetura e Padrões;
  Clean Architecture;
  CQRS (Command Query Responsibility Segregation);
  Mediator Pattern;
  Repository Pattern;
  Domain Services;
  Infraestrutura e Integrações;
  RabbitMQ – mensageria orientada a eventos;
  Redis – cache distribuído;
  Azure Blob Storage – armazenamento de arquivos;
  Google Distance API – cálculo de frete;
  SignalR – comunicação em tempo real;
  Stripe (base preparada) – processamento de pagamentos;
  Processamento Assíncrono;
  Hangfire – execução de tarefas em background;

🏗 Arquitetura do Projeto
O projeto segue Clean Architecture, separando responsabilidades em camadas independentes.

EcommerceDev
│
├── EcommerceDev.API
│   └── Controllers / Configurações da aplicação
│
├── EcommerceDev.Application
│   └── Commands / Queries / Handlers
│
├── EcommerceDev.Core
│   └── Entidades de domínio / Interfaces / Regras de negócio
│
├── EcommerceDev.Infrastructure
│   └── Banco de dados / Mensageria / Cache / Serviços externos
│
└── EcommerceDev.UnitTests
    └── Testes unitários

Fluxo da aplicação:

Controller
     ↓
Command / Query
     ↓
Mediator
     ↓
Handler
     ↓
Domain / Repository
     ↓
Infrastructure
⚙️ Principais Funcionalidades

A API implementa os principais recursos de um sistema de e-commerce:

👤 Clientes

Cadastro e autenticação

Gerenciamento de endereços

🛍 Produtos

Cadastro de produtos

Categorias

Upload de imagens

Controle de estoque

🛒 Carrinho de Compras

Adicionar/remover produtos

Atualização automática do carrinho

📦 Pedidos

Criação de pedidos

Histórico de atualizações

Cálculo de frete

💳 Pagamentos

Integração preparada para Stripe

Geração de link de pagamento

⚡ Comunicação em Tempo Real

Uso de SignalR para enviar o link de pagamento em tempo real ao cliente

⭐ Avaliações

Clientes podem avaliar produtos comprados

🔄 Fluxo de Compra da Aplicação

Fluxo simplificado da experiência de compra:

Cliente cria conta
      ↓
Adiciona produtos ao carrinho
      ↓
Solicita cálculo de frete
      ↓
Cria pedido
      ↓
Sistema gera pagamento
      ↓
SignalR envia link de pagamento em tempo real
      ↓
Cliente realiza pagamento
      ↓
Eventos são publicados via RabbitMQ
      ↓
Jobs em background processam ações adicionais

📌 Objetivo do Projeto
Este repositório serve como base para o desenvolvimento contínuo de uma API de e-commerce completa.
O objetivo é explorar arquiteturas modernas e boas práticas de engenharia de software, adicionando novas funcionalidades progressivamente, sempre priorizando:
  -Escalabilidade;
  -Organização do código;
  -Performance;
  -Boas práticas de arquitetura;
  -O projeto também funciona como laboratório de aprendizado e referência para aplicações .NET modernas.

🧪 Testes
O projeto possui uma estrutura dedicada para testes unitários:
EcommerceDev.UnitTests
Os testes garantem a confiabilidade das regras de negócio e ajudam a manter a qualidade da aplicação durante sua evolução.

📚 Evolução do Projeto
Novas funcionalidades e melhorias serão adicionadas ao longo do tempo, incluindo:
  -melhorias no fluxo de pagamento;
  -novos recursos de pedidos;
  -otimizações de performance;
  -novas integrações externas.
