using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace JavaMag
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader inputStream = new StreamReader("C:\\MAG\\BeerSong.java");
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());
            Java8Lexer lexer = new Java8Lexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            Java8Parser parser = new Java8Parser(tokens);
            ParserRuleContext tree = parser.compilationUnit();
            Console.WriteLine(tree.GetText());
            foreach (var dupa in tree.children)
            {
                Console.WriteLine(dupa);
            }
            var zenon = new Java8Parser.TypeDeclarationContext(tree, 1);
            tree.children[1] = zenon;
            Console.WriteLine(tree.GetText());
            Console.Read();
        }
    }
}
