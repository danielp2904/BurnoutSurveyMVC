using BurnoutSurveyMVC.Models;

namespace BurnoutSurveyMVC.Data
{
    public static class SeedData
    {
        public static void Initialize(PesquisaContext db)
        {
            if (db.SurveyQuestions.Any()) return;

            var questions = new List<SurveyQuestion>();

            string expEx = "1. Exaustão emocional: avalia desgaste e esgotamento no trabalho (0=Nunca ... 6=Todos os dias).";
            questions.Add(new SurveyQuestion { Codigo = "1.1", Texto = "Sinto-me emocionalmente esgotado(a) com meu trabalho.", Grupo = QuestionGroup.ExaustaoEmocional, Ordem = 1, ExplicacaoTopo = expEx });
            questions.Add(new SurveyQuestion { Codigo = "1.2", Texto = "Ao final do dia de trabalho, sinto-me exausto(a).", Grupo = QuestionGroup.ExaustaoEmocional, Ordem = 2 });
            questions.Add(new SurveyQuestion { Codigo = "1.3", Texto = "Sinto-me fatigado(a) ao levantar pela manhã e encarar outro dia de trabalho.", Grupo = QuestionGroup.ExaustaoEmocional, Ordem = 3 });
            questions.Add(new SurveyQuestion { Codigo = "1.4", Texto = "Trabalhar diretamente com código ou projetos exige demais da minha energia.", Grupo = QuestionGroup.ExaustaoEmocional, Ordem = 4 });
            questions.Add(new SurveyQuestion { Codigo = "1.5", Texto = "Sinto que estou no limite da minha capacidade emocional.", Grupo = QuestionGroup.ExaustaoEmocional, Ordem = 5 });

            string expDe = "2. Despersonalização: atitudes de cinismo/indiferença em relação ao trabalho (0=Nunca ... 6=Todos os dias).";
            questions.Add(new SurveyQuestion { Codigo = "2.1", Texto = "Tenho me tornado mais insensível em relação ao meu trabalho como desenvolvedor(a).", Grupo = QuestionGroup.Despersonalizacao, Ordem = 1, ExplicacaoTopo = expDe });
            questions.Add(new SurveyQuestion { Codigo = "2.2", Texto = "Trato meu trabalho de forma mais “fria” e impessoal do que deveria.", Grupo = QuestionGroup.Despersonalizacao, Ordem = 2 });
            questions.Add(new SurveyQuestion { Codigo = "2.3", Texto = "Sinto que estou me distanciando emocionalmente das tarefas que executo.", Grupo = QuestionGroup.Despersonalizacao, Ordem = 3 });
            questions.Add(new SurveyQuestion { Codigo = "2.4", Texto = "Não me importo tanto quanto antes com a qualidade do meu trabalho.", Grupo = QuestionGroup.Despersonalizacao, Ordem = 4 });

            string expRp = "3. Realização pessoal (invertida no cálculo): percepção de competência e crescimento (0=Nunca ... 6=Todos os dias).";
            questions.Add(new SurveyQuestion { Codigo = "3.1", Texto = "Sinto que posso resolver de forma eficaz os problemas que surgem no desenvolvimento.", Grupo = QuestionGroup.RealizacaoPessoal, Ordem = 1, ExplicacaoTopo = expRp });
            questions.Add(new SurveyQuestion { Codigo = "3.2", Texto = "Acredito que meu trabalho contribui positivamente para os projetos em que atuo.", Grupo = QuestionGroup.RealizacaoPessoal, Ordem = 2 });
            questions.Add(new SurveyQuestion { Codigo = "3.3", Texto = "Tenho conseguido atingir meus objetivos profissionais.", Grupo = QuestionGroup.RealizacaoPessoal, Ordem = 3 });
            questions.Add(new SurveyQuestion { Codigo = "3.4", Texto = "Sinto que estou crescendo profissionalmente.", Grupo = QuestionGroup.RealizacaoPessoal, Ordem = 4 });
            questions.Add(new SurveyQuestion { Codigo = "3.5", Texto = "Considero que sou bom(boa) no que faço.", Grupo = QuestionGroup.RealizacaoPessoal, Ordem = 5 });

            db.SurveyQuestions.AddRange(questions);
            db.SaveChanges();
        }
    }
}
