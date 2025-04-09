# 📌 Sistema de Gestão de Projetos e Recursos

## 💻 Estrutura do Sistema
- **Base de Dados:** SQLServer, criar uma local (para testes/desenvolvimento), depois vê-se.
- **Língua:** A app será desenvolvida em inglês.

## 📄 Documentação
- Documentação a fazer (diagramas): ver relatórios passados .

## 📊 O que é um Indicador?
Métrica utilizada para avaliar o progresso de um projeto.  
**Exemplo:** Número de contratações (podendo variar entre projetos).

## 🔐 Autenticação LDAP
O sistema utilizará **Auth0** para autenticação.

## 📑 Bolsa vs. Contrato
- **Bolsa:** Financiada por um único projeto.
- **Contrato:** Pode ser financiado por vários projetos.  
  💡 *Conclusão: Bolsa é um tipo específico de contrato.*

## ✅ Status das Tarefas
As tafeas têm ínicio, final, nome e status.
Os status das tarefas serão baseados no cronograma do projeto:
- 🟡 **A fazer**
- 🔵 **Em execução**
- 🟢 **Feito**

## 💵 Gestão Orçamental e Financeira 

### 🪙 Orçamento do Projeto
O orçamento do projeto é igual à soma do orçamento das atividades (pode alterar ao longo do projeto).

### 🗒 Orçamento da atividade
As atividades têm o seu próprio orçamento. As tarefas não possuem um orçamento.

### 💰 O que é uma Rubrica (Budget)?
Uma rubrica é uma categoria de despesa dentro do orçamento, incluindo:
- Recursos Humanos
- Promoção e Divulgação
- Publicação de Artigos
- Aquisição de Equipamentos

## 📅 Cronogramas
- Deve apresentar as atividades e permitir navegação dentro do projeto.
- Pode incluir **plugins recomendados pelo professor**.
- Exibe a **timeline do projeto** e o **% de cumprimento dos indicadores**.

## 🎓 Gestão de Bolseiros

### 👥 Quem adiciona os bolseiros?
- **Responsável:** Investigadores.

### 🔄 Fluxo de Aprovação
- O investigador responsável adiciona os dados do bolseiro.

### ⏳ Monitorização do Tempo da Bolsa
- Alertas e notificações via email **X dias antes** do fim da bolsa.

### 📄 Modelo de Contrato
- Definido pelo setor de **Recursos Humanos (RH)**.

---

# 📌 Perguntas e Respostas

## ✅ Perguntas Respondidas
- **1 perfil irá contratar 1 pessoa ou 1 perfil pode ter várias pessoas?**
  - Par atividade/perfil (perfil não é da atividade nem vice-versa).

- **Investigadores também têm contratos ou apenas os bolseiros?**
  - Investigadores = automaticamente contrato.
  - Bolseiros = automaticamente bolsa.
  - Os bolseiros podem ter contratos que não sejam bolsas?

- **Equipa aprovada:**
  - Par atividade/perfil.
  - Fase de execução: 1 perfil pode ser consumido por várias pessoas.

- **É para tratar de candidaturas**
  - Numa parte final do projeto, no caso de existir tempo.    

## 📌 Perguntas Futuras

- relatório: pode ter coisas iguais (ex.: estado da arte)?
- qual é a tradução de "rubrica"? - fica CostType pra ja
- posso editar a rubrica do orçamento? sim.
- posso editar o gasto planeado do orçamento? sim, mas tem q se meter a afetação

Diogo:
- temos de fazer mais algum diagrama antes da implementação? - fisica e lógica
- se o stor vai faltar na semana de 11 e se dá para eventualemnete em caso de termos duvidas, podermos falar com ele online (email ou ate uma pqeuena reunião) - sim

- investigador pode ser contratado ou docente (esclarecer mais)


- adicionar tempo previsto (consumo por mes) ao consumo do perfil
- consumo perfil:
  - inicio/fim (por atividade)
  - dividir os PMs pelos meses
  (avisar qtos pms sobram no menu dps na UI)

- adicionar pedido de pagamento (afetação prevista > afetação lacrada (definitiva))

- adicionar salário: **por contrato**, mas pode ser alterado

- investigador já existe,
  - agendar mudanças de salario q mude as afetações previstas

BD (afetação):
- projeto (dá jeito)
- atividade
- pessoa
- (reproduzir tabela)

TALVEZ as afetações têm uma data tudo antes é executado e tudo depois é previsto
(testar isto)