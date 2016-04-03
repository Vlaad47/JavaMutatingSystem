﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using JavaMag;
using System.IO;
using Antlr4.Runtime;
using JavaMagTests.cfg;

namespace JavaMagTests
{
    [TestClass()]
    public class GrammaryTests
    {
        [TestMethod()]
        public void TestGrammaryClases()
        {
            StreamReader inputStream = new StreamReader(TestCfg.JavaTestFile);
            Java8Parser parser = new Java8Parser(new CommonTokenStream(new Java8Lexer(new AntlrInputStream(inputStream.ReadToEnd()))));
            ParserRuleContext tree = parser.compilationUnit();
        }
    }
}