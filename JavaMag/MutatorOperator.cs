using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace JavaMag
{
    class MutatorOperator : Java8BaseVisitor<string>
    {
        public List<string> TooMutate { get; set; } //co chcemy mutowac {"<", ">", "<=", ">="}
        public List<string> Mutators { get; set; } // w co ma sie zmieniac, ten sam format, zakladam, ze jest rozlaczne z TooMutate
        private readonly ParserRuleContext _tree;
        private readonly List<ParserRuleContext> _selectedTokens = new List<ParserRuleContext>();
        public int MaxValue { get; private set; }
        public int BorderValue { get; private set; }

        public void SetMutationRatio(int maxRandomValue, int borderRandomValue)
        {
            MaxValue = maxRandomValue;
            BorderValue = borderRandomValue;
        }

        public MutatorOperator(ParserRuleContext treeRoot)
        {
            _tree = treeRoot;
            MaxValue = 3;
            BorderValue = 1;
        }

        public MutatorOperator(ParserRuleContext treeRoot, int maxRandomValue, int borderRandomValue)
        {
            _tree = treeRoot;
            MaxValue = maxRandomValue;
            BorderValue = borderRandomValue;
        }

        public void Mutate()
        {
            Visit(_tree);
            MutateSelected();
        }

        private void MutateSelected()
        {
            foreach (ParserRuleContext token in _selectedTokens)
            {
                Random randomGenerator = new Random();
                if (randomGenerator.Next(1, MaxValue) <= BorderValue) continue;
                var newToken = new CommonToken((CommonToken) token.GetChild(0).Payload) {Text = Mutators[randomGenerator.Next(0, Mutators.Count)]};
                token.RemoveLastChild();
                token.AddChild(newToken);
            }
        }

        public override string VisitLesserThanOperator(Java8Parser.LesserThanOperatorContext context)
        {
            if (TooMutate.Contains("<"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitLesserThanOperator(context);
        }

        public override string VisitGreaterThanOperator(Java8Parser.GreaterThanOperatorContext context)
        {
            if (TooMutate.Contains(">"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitGreaterThanOperator(context);
        }

        public override string VisitPreDecrementOperator(Java8Parser.PreDecrementOperatorContext context)
        {
            //TODO znalezc sposob na rozroznienie pre i post decrement
            return base.VisitPreDecrementOperator(context);
        }

        public override string VisitPreIncrementOperator(Java8Parser.PreIncrementOperatorContext context)
        {
            //TODO znalezc sposob na rozroznienie pre i post increment
            return base.VisitPreIncrementOperator(context);
        }

        public override string VisitUnaryPlusOperator(Java8Parser.UnaryPlusOperatorContext context)
        {
            if (TooMutate.Contains("+"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitUnaryPlusOperator(context);
        }

        public override string VisitUnaryMinusOperator(Java8Parser.UnaryMinusOperatorContext context)
        {
            if (TooMutate.Contains("-"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitUnaryMinusOperator(context);
        }

        public override string VisitPreLogicalNegationOperator(Java8Parser.PreLogicalNegationOperatorContext context)
        {
            if (TooMutate.Contains("!"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitPreLogicalNegationOperator(context);
        }

        public override string VisitPreBitwiseNegationOperator(Java8Parser.PreBitwiseNegationOperatorContext context)
        {
            if (TooMutate.Contains("~"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitPreBitwiseNegationOperator(context);
        }

        public override string VisitMuliplyOperator(Java8Parser.MuliplyOperatorContext context)
        {
            if (TooMutate.Contains("*"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitMuliplyOperator(context);
        }

        public override string VisitDivisionOperator(Java8Parser.DivisionOperatorContext context)
        {
            if (TooMutate.Contains("/"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitDivisionOperator(context);
        }

        public override string VisitReminderOperator(Java8Parser.ReminderOperatorContext context)
        {
            if (TooMutate.Contains("%"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitReminderOperator(context);
        }

        public override string VisitAdditionOperator(Java8Parser.AdditionOperatorContext context)
        {
            //TODO odroznic od unarnego plusa
            return base.VisitAdditionOperator(context);
        }

        public override string VisitSubstractionOperator(Java8Parser.SubstractionOperatorContext context)
        {
            //TODO odroznic od unarnego minusa
            return base.VisitSubstractionOperator(context);
        }

        public override string VisitShiftLeftOperator(Java8Parser.ShiftLeftOperatorContext context)
        {
            if (TooMutate.Contains("<<"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitShiftLeftOperator(context);
        }

        public override string VisitShiftRightOperator(Java8Parser.ShiftRightOperatorContext context)
        {
            if (TooMutate.Contains(">>"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitShiftRightOperator(context);
        }

        public override string VisitShiftRightWithZeroOperator(Java8Parser.ShiftRightWithZeroOperatorContext context)
        {
            if (TooMutate.Contains(">>>"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitShiftRightWithZeroOperator(context);
        }

        public override string VisitGreaterOrEqualToOperator(Java8Parser.GreaterOrEqualToOperatorContext context)
        {
            if (TooMutate.Contains(">="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitGreaterOrEqualToOperator(context);
        }

        public override string VisitLesserOrEqualToOperator(Java8Parser.LesserOrEqualToOperatorContext context)
        {
            if (TooMutate.Contains("<="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitLesserOrEqualToOperator(context);
        }

        public override string VisitEqualOperator(Java8Parser.EqualOperatorContext context)
        {
            if (TooMutate.Contains("=="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitEqualOperator(context);
        }

        public override string VisitNotEqualOperator(Java8Parser.NotEqualOperatorContext context)
        {
            if (TooMutate.Contains("!="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitNotEqualOperator(context);
        }

        public override string VisitBitwiseAndOperator(Java8Parser.BitwiseAndOperatorContext context)
        {
            if (TooMutate.Contains("&"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitwiseAndOperator(context);
        }

        public override string VisitBitwiseExclusiveOrOperator(Java8Parser.BitwiseExclusiveOrOperatorContext context)
        {
            if (TooMutate.Contains("^"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitwiseExclusiveOrOperator(context);
        }

        public override string VisitBitwiseInclusiveOrOperator(Java8Parser.BitwiseInclusiveOrOperatorContext context)
        {
            if (TooMutate.Contains("|"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitwiseInclusiveOrOperator(context);
        }

        public override string VisitLogicalOrOperator(Java8Parser.LogicalOrOperatorContext context)
        {
            if (TooMutate.Contains("||"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitLogicalOrOperator(context);
        }

        public override string VisitLogicalAndOperator(Java8Parser.LogicalAndOperatorContext context)
        {
            if (TooMutate.Contains("&&"))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitLogicalAndOperator(context);
        }

        public override string VisitSimpleAssignmentOperator(Java8Parser.SimpleAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitSimpleAssignmentOperator(context);
        }

        public override string VisitMultipleAssignmentOperator(Java8Parser.MultipleAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("*="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitMultipleAssignmentOperator(context);
        }

        public override string VisitDivideAssignmentOperator(Java8Parser.DivideAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("/="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitDivideAssignmentOperator(context);
        }

        public override string VisitReminderAssignmentOperator(Java8Parser.ReminderAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("%="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitReminderAssignmentOperator(context);
        }

        public override string VisitAddAssignmentOperator(Java8Parser.AddAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("+="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitAddAssignmentOperator(context);
        }

        public override string VisitSubstractAssignmentOperator(Java8Parser.SubstractAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("-="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitSubstractAssignmentOperator(context);
        }

        public override string VisitBitShiftLeftAssignmentOperator(Java8Parser.BitShiftLeftAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("<<="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitShiftLeftAssignmentOperator(context);
        }

        public override string VisitBitShiftRightAssignmentOperator(Java8Parser.BitShiftRightAssignmentOperatorContext context)
        {
            if (TooMutate.Contains(">>="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitShiftRightAssignmentOperator(context);
        }

        public override string VisitBitShiftRightWithZeroAssignmentOperator(Java8Parser.BitShiftRightWithZeroAssignmentOperatorContext context)
        {
            if (TooMutate.Contains(">>>="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitShiftRightWithZeroAssignmentOperator(context);
        }

        public override string VisitBitAndAssignmentOperator(Java8Parser.BitAndAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("&="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitAndAssignmentOperator(context);
        }

        public override string VisitBitXOrAssignmentOperator(Java8Parser.BitXOrAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("^="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitXOrAssignmentOperator(context);
        }

        public override string VisitBitOrAssignmentOperator(Java8Parser.BitOrAssignmentOperatorContext context)
        {
            if (TooMutate.Contains("|="))
            {
                _selectedTokens.Add(context);
            }
            return base.VisitBitOrAssignmentOperator(context);
        }

        public override string VisitPostfixIncrementOperator(Java8Parser.PostfixIncrementOperatorContext context)
        {
            //TODO odroznic od pre
            return base.VisitPostfixIncrementOperator(context);
        }

        public override string VisitPostfixDecrementOperator(Java8Parser.PostfixDecrementOperatorContext context)
        {
            //TODO odroznic od pre
            return base.VisitPostfixDecrementOperator(context);
        }
    }
}