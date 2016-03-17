using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using JavaMag.cfg;

namespace JavaMag
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader inputStream = new StreamReader(MainCfg.JavaFilesDir);
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());
            Java8Lexer lexer = new Java8Lexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            Java8Parser parser = new Java8Parser(tokens);
            ParserRuleContext tree = parser.compilationUnit();
            Console.WriteLine(tree.GetText());
            Console.WriteLine(new JavaVisitor().Visit(tree));
            Console.Read();
        }
    }

    class JavaVisitor : Java8BaseVisitor<String>
    {
        public override string VisitCompilationUnit(Java8Parser.CompilationUnitContext context)
        {
            return base.VisitCompilationUnit(context);
        }

        public override string VisitTypeDeclaration(Java8Parser.TypeDeclarationContext context)
        {
            Console.WriteLine("TypeDeclaration");
            return base.VisitTypeDeclaration(context);
        }

        public override string VisitStatement(Java8Parser.StatementContext context)
        {
            Console.WriteLine("Statement");
            return base.VisitStatement(context);
        }

        public override string VisitIfThenStatement(Java8Parser.IfThenStatementContext context)
        {
            Console.WriteLine("IF");
            Java8Parser.ExpressionContext zenon = context.expression();
            //zenon.GetTokens();
            return base.VisitIfThenStatement(context);
        }

        public override string VisitRelationalExpression(Java8Parser.RelationalExpressionContext context)
        {
            Console.WriteLine(3);
            return base.VisitRelationalExpression(context);
        }
    }
}
