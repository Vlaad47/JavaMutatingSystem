using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using JavaMag.cfg;
using Microsoft.SqlServer.Server;

namespace JavaMag
{
    class Program
    {
        public static void Main(string[] args)
        {
            StreamReader inputStream = new StreamReader(MainCfg.JavaFilesDir);
            Java8Parser parser = new Java8Parser(new CommonTokenStream(new Java8Lexer(new AntlrInputStream(inputStream.ReadToEnd()))));
            ParserRuleContext tree = parser.compilationUnit();
            Console.WriteLine(tree.GetText());
//            MutatorOperator mutationOperator = new MutatorOperator(tree);
//            List<string> tokensToMutate = new List<string> {"<", ">"};
//            mutationOperator.TooMutate = tokensToMutate;
//            List<string> expecetedNewTokens = new List<string> {"<=", ">="};
//            mutationOperator.Mutators = expecetedNewTokens;
//            mutationOperator.Mutate();
//            Console.WriteLine(tree.GetText());
            AssignmentVisitor assignmentsVisitor = new AssignmentVisitor(tree);
            assignmentsVisitor.Visit(tree);
            Console.Read();
        }
    }


    public class AssignmentVisitor : Java8BaseVisitor<string>
    {
        private ParserRuleContext _root;
        private readonly List<ParserRuleContext> _selectedTokens = new List<ParserRuleContext>();
        public AssignmentVisitor(ParserRuleContext root)
        {
            _root = root;
        }
        
        public override string VisitAssignment(Java8Parser.AssignmentContext context)
        {
            //nie dziala
//            _selectedTokens.Add(context);
//            ParserRuleContext token = (ParserRuleContext) context.Parent;
//            IParseTree a = token.Payload;
//            new Java8Parser.StatementExpressionContext(null, 0);
//            var newToken = new Java8Parser.StatementExpressionContext((ParserRuleContext) token.Parent, 0) { Text = new SwitchBuilder("zenon").AddDefaultCase("i=0").AddEnd().ToString()};
//            token.RemoveLastChild();
//            token.AddChild(newToken);
            return base.VisitAssignment(context);
        }
    }

    internal class IncorrectlyEndedSwitch : Exception
    {
    }
}
