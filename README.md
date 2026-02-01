# PESTI_LLM

# PESTI_LLM

## 📌 Descrição do Projeto

O **PESTI_LLM** é uma aplicação web desenvolvida em **.NET** com base em princípios de **Domain-Driven Design (DDD)**, cujo objetivo é suportar a **gestão de projetos de Investigação e Desenvolvimento (I&D)**, com foco particular na gestão de **atividades, tarefas, cargas mensais e afetações de recursos humanos**.

O sistema foi concebido para responder às necessidades de planeamento, acompanhamento e controlo de projetos, permitindo uma visão estruturada e organizada do trabalho desenvolvido por equipas técnicas ao longo do tempo.

---

## 🎯 Objetivos Principais

* Apoiar a **gestão estruturada de projetos de I&D**;
* Permitir a criação e gestão de **atividades e respetivas categorias**;
* Gerir **cargas mensais** e **afetações de colaboradores** a atividades;
* Garantir uma arquitetura robusta, escalável e de fácil manutenção;
* Aplicar boas práticas de engenharia de software, nomeadamente **DDD**, separação de responsabilidades e testes automatizados.

---

## 🧱 Arquitetura

A aplicação segue uma arquitetura em camadas, inspirada nos princípios de **Domain-Driven Design**:

* **Camada de Domínio**
  Contém as entidades, value objects, agregados e regras de negócio centrais do sistema.

* **Camada de Aplicação**
  Responsável pela coordenação dos casos de uso, serviços de aplicação e lógica de orquestração.

* **Camada de Infraestrutura**
  Inclui o acesso a dados, persistência, repositórios e integração com tecnologias externas.

* **Camada de Apresentação (Web API)**
  Disponibiliza uma API REST para interação com o sistema, através de controllers bem definidos.

---

## 🌐 Funcionalidades Principais

* Gestão de **Atividades** e **Categorias**;
* Registo e controlo de **Cargas Mensais**;
* Gestão de **Afetações Mensais** de colaboradores a atividades;
* Exposição de endpoints REST através de controllers dedicados;
* Validação de regras de negócio ao nível do domínio.

Exemplos de controllers disponíveis:

* `AtividadesController`
* `CategoriesController`
* `CargasMensaisController`
* `AfetacaoMensaisController`

---

## 🛠️ Tecnologias Utilizadas

* **C# / .NET**
* **ASP.NET Core Web API**
* **Domain-Driven Design (DDD)**
* **Entity Framework Core** (persistência de dados)
* **REST APIs**

---

## ✅ Boas Práticas Aplicadas

* Separação clara de responsabilidades por camadas;
* Uso de **entidades ricas** e **value objects**;
* Encapsulamento da lógica de negócio no domínio;
* Estrutura preparada para testes automatizados;
* Código modular e extensível.

---

## 🚀 Execução do Projeto

1. Abrir a solução `PESTI_LLM.sln` no Visual Studio;
2. Restaurar as dependências;
3. Configurar a ligação à base de dados (se aplicável);
4. Executar o projeto `dddnetcore`;
5. Aceder à API através do browser ou ferramentas como Postman/Swagger.

---

## 📄 Notas Finais

Este projeto foi desenvolvido com fins académicos e/ou experimentais, servindo como base para a exploração de boas práticas de arquitetura de software aplicadas a sistemas de gestão de projetos de I&D.

O código encontra-se preparado para evolução futura, nomeadamente integração com interfaces frontend, autenticação/autorização e utilização de modelos de linguagem (LLMs) para apoio à gestão e análise de informação.
