# BurnoutSurveyMVC

Aplicação web para pesquisa de **Burnout em desenvolvedores de software**, construída com **ASP.NET Core MVC**, **Entity Framework Core** e **SQL Server**.

O sistema aplica dois instrumentos validados:
- **MBI — Maslach Burnout Inventory** (adaptado para TI): avalia três dimensões — Exaustão Emocional, Despersonalização e Realização Pessoal
- **SUS — System Usability Scale**: avalia a usabilidade percebida da própria aplicação

Ao final do fluxo, o participante recebe seu resultado calculado automaticamente, com classificação de risco de burnout.

---

## Tecnologias

| Camada | Tecnologia |
|--------|-----------|
| Framework | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core 8 |
| Banco de dados | SQL Server / LocalDB |
| Containerização | Docker |
| Front-end | Razor Views + CSS customizado |

---

## Arquitetura

O projeto segue a separação de responsabilidades em camadas:

```
BurnoutSurveyMVC/
├── Controllers/        # Recebem requisições HTTP e delegam para Services/DbContext
│   ├── ConsentController.cs    # TCLE e cadastro do participante
│   ├── MBIController.cs        # Questionário MBI (22 itens em 3 grupos)
│   ├── SUSController.cs        # Questionário SUS (10 itens)
│   └── ResultadoController.cs  # Exibe resultado calculado
├── Services/
│   └── ScoringService.cs       # Cálculo dos escores MBI e SUS
├── Models/             # Entidades persistidas no banco
│   ├── Participante.cs         # Dados demográficos + GUID como PK
│   ├── RespostaMBI.cs          # Resposta individual do MBI (0–6)
│   ├── RespostaSUS.cs          # Resposta individual do SUS (1–5)
│   ├── SurveyQuestion.cs       # Perguntas do MBI seedadas no banco
│   └── Enums.cs                # QuestionGroup e QuestionScale
├── ViewModels/         # Modelos de transferência para as Views
├── Data/
│   ├── PesquisaContext.cs      # DbContext com 4 DbSets
│   └── SeedData.cs             # Seed inicial das perguntas MBI
├── Migrations/         # Histórico de Migrations do EF Core
└── Views/              # Razor Views por Controller
```

---

## Fluxo da Aplicação

```
/ (Home)
  └─► /Consent    — TCLE + dados demográficos → cria Participante no banco
        └─► /MBI  — 14 questões em 3 grupos → persiste RespostasMBI
              └─► /SUS  — 10 questões de usabilidade → persiste RespostasSUS
                    └─► /Resultado/Resumo  — exibe escores calculados
```

Cada etapa recebe o `Guid` do participante via rota, mantendo o fluxo sem sessão server-side.

---

## Cálculo dos Escores

### MBI
Três dimensões calculadas pelo `ScoringService`:

| Dimensão | Cálculo |
|----------|---------|
| Exaustão Emocional | Média das respostas (escala 0–6) |
| Despersonalização | Média das respostas (escala 0–6) |
| Realização Pessoal | Média **invertida** `(6 - valor)` — score alto indica baixa realização |

**Classificação:** Exaustão ≥ 5 → Alto risco · ≥ 3 → Moderado · < 3 → Baixo risco

### SUS
Fórmula padrão (Brooke, 1996):
- Questões **ímpares**: `(valor - 1)`
- Questões **pares**: `(5 - valor)`
- Score final: `soma × 2,5` → resultado de 0 a 100

---

## Como Rodar

### Pré-requisitos
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- SQL Server local ou LocalDB (incluso no Visual Studio)

### Passo a passo

```bash
# 1. Clonar o repositório
git clone https://github.com/danielp2904/BurnoutSurveyMVC.git
cd BurnoutSurveyMVC

# 2. Restaurar dependências
dotnet restore

# 3. Instalar ferramenta EF Core (se necessário)
dotnet tool install --global dotnet-ef --version 8.0.7

# 4. Criar o banco e rodar as Migrations
dotnet ef database update

# 5. Rodar a aplicação
dotnet run
```

Acesse `https://localhost:5001` no navegador.

> **Nota:** O seed das perguntas MBI é executado automaticamente na primeira inicialização. Se `Migrate()` falhar, o app usa `EnsureCreated()` como fallback.

### Com Docker

```bash
docker build -t burnout-survey .
docker run -p 8080:80 burnout-survey
```

---

## Modelo de Dados

```
Participante (Guid PK)
├── Id              Guid        PK gerado automaticamente
├── DataRegistro    DateTime    UTC no momento do cadastro
├── Ip              string?     IP do participante (anonimizado)
├── Idade           int?
├── Genero          string?
├── TempoAtuacao    string?
├── RegimeTrabalho  string?
├── CargaHoraria    string?
└── AceitouTCLE     bool

RespostaMBI (int PK)
├── ParticipanteId  Guid        FK → Participante
├── QuestionId      int         FK → SurveyQuestion
└── Valor           int         0–6 (validado via [Range])

RespostaSUS (int PK)
├── ParticipanteId  Guid        FK → Participante
├── NumeroQuestao   int         1–10
└── Valor           int         1–5 (validado via [Range])

SurveyQuestion (int PK)
├── Codigo          string      Ex: "1.1", "2.3"
├── Texto           string      Enunciado da questão
├── Grupo           enum        ExaustaoEmocional | Despersonalizacao | RealizacaoPessoal | SUS
├── Ordem           int         Ordem de exibição dentro do grupo
└── ExplicacaoTopo  string?     Texto explicativo exibido no topo de cada grupo
```

---

## Decisões Técnicas

**`Guid` como chave primária do `Participante`** — garante anonimato e evita exposição de IDs sequenciais na URL, relevante para uma pesquisa com dados sensíveis.

**Seed via `SeedData.cs`** — as perguntas do MBI são inseridas automaticamente na primeira execução, eliminando dependência de scripts SQL externos.

**`AsNoTracking()` nas consultas de leitura** — utilizado no `MBIController` para melhorar performance em queries que não fazem update.

**`ValidateAntiForgeryToken`** em todos os POSTs — proteção contra CSRF em todos os formulários.

**`ScoringService` com DI** — a lógica de cálculo é isolada do Controller e injetada via `AddScoped`, facilitando futura substituição ou testes unitários.

---

## Pacotes NuGet

| Pacote | Versão |
|--------|--------|
| Microsoft.EntityFrameworkCore.SqlServer | 8.0.7 |
| Microsoft.EntityFrameworkCore.Tools | 8.0.7 |
| Microsoft.EntityFrameworkCore.Design | 8.0.7 |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 8.0.7 |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.22.1 |

---

## Próximos Passos (Roadmap)

- [ ] Testes unitários para o `ScoringService`
- [ ] Painel administrativo para visualização agregada dos resultados
- [ ] Exportação dos dados para CSV/Excel
- [ ] Autenticação para acesso ao painel de resultados
- [ ] Suporte a múltiplos idiomas (i18n)

---

## Autor

Daniel — [github.com/danielp2904](https://github.com/danielp2904)
