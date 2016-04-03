using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using JavaMag.cfg;

namespace JavaMag
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader inputStream = new StreamReader(MainCfg.JavaFilesDir);
            Java8Parser parser = new Java8Parser(new CommonTokenStream(new Java8Lexer(new AntlrInputStream(inputStream.ReadToEnd()))));
            ParserRuleContext tree = parser.compilationUnit();
            Console.WriteLine(tree.GetText());
            MutatorOperator mutationOperator = new MutatorOperator(tree);
            List<string> tokensToMutate = new List<string>();
            tokensToMutate.Add("<");
            tokensToMutate.Add(">");
            mutationOperator.TooMuttate = tokensToMutate;
            mutationOperator.Mutate();
            Console.WriteLine(tree.GetText());
            Console.Read();
            Console.Read();
        }
    }



    class MutatorOperator : Java8BaseVisitor<string>
    {
        public List<string> TooMuttate { get; set; } //co chcemy mutowac {"<", ">", "<=", ">="}
        public List<string> Mutators { get; set; } // w co ma sie zmieniac, ten sam format, na razie bez nie dodane
        private ParserRuleContext tree;
        private readonly List<ParserRuleContext> _selectedTokens = new List<ParserRuleContext>();

        public MutatorOperator(ParserRuleContext treeRoot)
        {
            this.tree = treeRoot;
        }

        public void Mutate()
        {
            this.Visit(this.tree);
            MutateSelected();
        }

        private void MutateSelected()
        {
            foreach (ParserRuleContext token in this._selectedTokens)
            {
                Random randomGenerator = new Random();
                if (randomGenerator.Next(1, 3) <= 1) continue;
                var newToken = new CommonToken((CommonToken) token.GetChild(0).Payload) {Text = "<="};
                token.RemoveLastChild();
                token.AddChild(newToken);
            }
        }

        public override string VisitLesserThanOperator(Java8Parser.LesserThanOperatorContext context)
        {
            if (this.TooMuttate.Contains("<"))
            {
                this._selectedTokens.Add(context);
            }
            return base.VisitLesserThanOperator(context);
        }

        public override string VisitGreaterThanOperator(Java8Parser.GreaterThanOperatorContext context)
        {
            if (this.TooMuttate.Contains("<"))
            {
                this._selectedTokens.Add(context);
            }
            return base.VisitGreaterThanOperator(context);
        }
    }

    class JavaVisitor : Java8BaseVisitor<string>
    {
        public override string VisitLesserThanOperator(Java8Parser.LesserThanOperatorContext context)
        {
            var newToken = new CommonToken((CommonToken) context.GetChild(0).Payload) {Text = ">"};
            context.RemoveLastChild();
            context.AddChild(newToken);
            return base.VisitLesserThanOperator(context);
        }

        public override string VisitGreaterThanOperator(Java8Parser.GreaterThanOperatorContext context)
        {
            var zenon = new CommonToken((CommonToken) context.GetChild(0).Payload) {Text = "<"};
            context.RemoveLastChild();
            context.AddChild(zenon);
            return base.VisitGreaterThanOperator(context);
        }
    }
}
