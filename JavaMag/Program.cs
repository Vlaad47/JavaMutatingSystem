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
            mutationOperator.TooMutate = tokensToMutate;
            List<string> expecetedNewTokens = new List<string>();
            expecetedNewTokens.Add("<=");
            expecetedNewTokens.Add(">=");
            mutationOperator.Mutators = expecetedNewTokens;
            mutationOperator.Mutate();
            Console.WriteLine(tree.GetText());
            Console.Read();
            Console.Read();
        }
    }



    class MutatorOperator : Java8BaseVisitor<string>
    {
        public List<string> TooMutate { get; set; } //co chcemy mutowac {"<", ">", "<=", ">="}
        public List<string> Mutators { get; set; } // w co ma sie zmieniac, ten sam format, zakladam, ze jest rozlaczne z TooMutate
        private readonly ParserRuleContext _tree;
        private readonly List<ParserRuleContext> _selectedTokens = new List<ParserRuleContext>();

        public MutatorOperator(ParserRuleContext treeRoot)
        {
            this._tree = treeRoot;
        }

        public void Mutate()
        {
            this.Visit(this._tree);
            MutateSelected();
        }

        private void MutateSelected()
        {
            foreach (ParserRuleContext token in this._selectedTokens)
            {
                Random randomGenerator = new Random();
                if (randomGenerator.Next(1, 3) <= 1) continue;
                var newToken = new CommonToken((CommonToken) token.GetChild(0).Payload) {Text = this.Mutators[randomGenerator.Next(0, this.Mutators.Count)]};
                token.RemoveLastChild();
                token.AddChild(newToken);
            }
        }

        public override string VisitLesserThanOperator(Java8Parser.LesserThanOperatorContext context)
        {
            if (this.TooMutate.Contains("<"))
            {
                this._selectedTokens.Add(context);
            }
            return base.VisitLesserThanOperator(context);
        }

        public override string VisitGreaterThanOperator(Java8Parser.GreaterThanOperatorContext context)
        {
            if (this.TooMutate.Contains("<"))
            {
                this._selectedTokens.Add(context);
            }
            return base.VisitGreaterThanOperator(context);
        }
    }
}
