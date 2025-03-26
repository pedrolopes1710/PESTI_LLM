# 📌 Sistema de Gestão de Projetos e Recursos

## 📊 O que é um Indicador?
Métrica utilizada para avaliar o progresso de um projeto. Exemplo: **número de contratações** (podendo variar entre projetos).

## 🔐 Autenticação LDAP
O sistema utilizará **Auth0** para autenticação.

## 📑 Bolsa vs. Contrato
- **Bolsa:** Financiada por um único projeto.
- **Contrato:** Pode ser financiado por vários projetos.
- 💡 *Conclusão: bolsa é um tipo específico de contrato.*

## ✅ Status das Tarefas
Os status das tarefas serão baseados no cronograma do projeto:
- 🟡 **A fazer**
- 🔵 **Em execução**
- 🟢 **Feito**

## 💰 O que é uma Rubrica (Budget)?
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

###  Quem adiciona os bolseiros?
- **Responsável:** Investigadores

###  Fluxo de Aprovação
- O investigador responsável adiciona os dados do bolseiro.

###  Monitorização do Tempo da Bolsa
- Alertas e notificações via email **X dias antes** do fim da bolsa.

###  Modelo de Contrato
- Definido pelo setor de **Recursos Humanos (RH)**.

---
# 📌 Perguntas novas


## Respondidas
- Base de dados, fazemos nós uma nossa ou vai ser disponibilizada alguma. - (SQLServer, criar uma local (para testes/desenvolvimento), dps vê-se)
- 1 perfil irá contratar 1 pessoa ou 1 perfil pode ter varias pessoas? - par atividade/perfil (perfil nao é da atividade (nem vice versa))
- Investigadores também têm contratos ou apenas os bolseiros? (inv = automaticamente contrato, bolseiro = automaticamente bolsa)
    - Se sim, os bolseiros podem ter contratos que nao sejam bolsas? 
- cadatarefa tem o seu proprio orçamento ou só as atividades? (só atividades) (tarefas têm inicio e fim e nome e deve tar bom)
- orçamento do projeto é igual à soma das atividades ou pode haver extras em considerações de atividades que possam vir a existir? (apenas soma das atividades (pode alterar ao longo do projeto))

equipa aprovada:
- par atividade/perfil
- faze de execução: 1 perfil pode ser consumido por varias pessoas
📌