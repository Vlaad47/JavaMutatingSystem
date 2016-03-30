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
            new JavaVisitor().Visit(tree);
            Console.WriteLine(tree.GetText());
            Console.Read();
        }

    }



    class MutatorOperator
    {
        public string TooMuttate;// co chce mutowac np: <,>,!=,.....
        public string Mutators; // <,> *
    }

    class JavaVisitor : Java8BaseVisitor<String>
    {
        public override string VisitLesserThanOperator(Java8Parser.LesserThanOperatorContext context)
        {
            Console.WriteLine(context.GetChild(0).Payload.GetType());
            CommonToken zenon = new CommonToken((CommonToken)context.GetChild(0).Payload);
            zenon.Text = ">";
            context.RemoveLastChild();
            context.AddChild(zenon);
            return base.VisitLesserThanOperator(context);
        }

        public override string VisitGreaterThanOperator(Java8Parser.GreaterThanOperatorContext context)
        {
            Console.WriteLine(context.GetChild(0).Payload.GetType());
            CommonToken zenon = new CommonToken((CommonToken)context.GetChild(0).Payload);
            zenon.Text = "<";
            context.RemoveLastChild();
            context.AddChild(zenon);
            return base.VisitGreaterThanOperator(context);
        }

        public override string VisitCompareOperator(Java8Parser.CompareOperatorContext context)
        {
            Console.WriteLine(context.GetChild(0).Payload.GetType());
            CommonToken zenon = new CommonToken((CommonToken)context.GetChild(0).Payload);
            zenon.Text = ">"; //random z *
            context.RemoveLastChild();
            context.AddChild(zenon);
            return base.VisitCompareOperator(context);
        }
    }

    //TODO fix this, this class should behave like Character class in Java
    public class Character
    {
        public static bool isJavaIdentifierStart(object la)
        {
            return true;
        }

        public static int toCodePoint(char la, char c)
        {
            throw new System.NotImplementedException();
        }

        public static bool isJavaIdentifierPart(int la)
        {
            return true;
        }
    }
}
