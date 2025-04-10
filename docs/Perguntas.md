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

### Dia 09/04

- **relatório: pode ter coisas iguais (ex.: estado da arte)?**
  - Professor ainda vai analisar e disse que depois dava resposta.

- **qual é a tradução de "rubrica"?**
  - Fica CostType para já.

- **Posso editar a rubrica do orçamento?**
  - Sim.

- **Posso editar o gasto planeado do orçamento?**
  - Sim, mas tem que se meter a afetação.

- **Temos de fazer mais algum diagrama antes da implementação?** 
  - Não respondido a 100% mas apenas vista fisica e lógica.

- **O professor vai faltar na semana de 11 e se dá para eventualmente em caso de termos duvidas, podermos falar com ele online (email ou ate uma pequena reunião)**   
  - Sim

- **Tipos de Contrato** 
  - investigador pode ser contratado ou docente (esclarecer mais)

- **Consumo Perfil**
  - Adicionar tempo previsto (consumo por mes) ao consumo do perfil
  - Consumo perfil:
    - inicio/fim (por atividade)
    - dividir os PMs pelos meses
    (avisar qtos pms sobram no menu dps na UI)

- **Pedidos de Pagamentos**
  - Adicionar pedido de pagamento (afetação prevista > afetação aprovada)

- **Contratos**
  - Adicionar salário mensal: **por contrato**, mas pode ser alterado

- **Salários (EXTRA)**
  - Investigador já existe,
    - agendar mudanças de salario que mude as afetações previstas

- **Base de Dados (afetação)**
  - Projeto (dá jeito)
  - Atividade
  - Pessoa
  - (reproduzir tabela)

- **Afetações**
  - TALVEZ as afetações têm uma data tudo antes é executado e tudo depois é previsto
(testar isto)

## 📌 Perguntas Futuras

- **relatório: pode ter coisas iguais (ex.: estado da arte)?**
  - Professor ainda vai analisar e disse que depois dava resposta.