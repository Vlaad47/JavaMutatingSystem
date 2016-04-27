namespace JavaMag
{
    public class SwitchBuilder
    {
        private string _result;
        private int _currentCaseNumber;
        private bool _isEnded;
        public SwitchBuilder(string varName)
        {
            _isEnded = false;
            _currentCaseNumber = 1;
            _result = "switch (System.getProperty(\"" + varName + "\", \"\")) {\n";
        }

        public SwitchBuilder AddEnd()
        {
            _result += "}";
            _isEnded = true;
            return this;
        }

        public SwitchBuilder AddDefaultCase(string originalValue)
        {
            _result += "default: {\n";
            _result += originalValue + "\n";
            _result += "break;\n";
            _result += "}\n";
            return this;
        }

        public SwitchBuilder AddCase(string value)
        {
            _result += "case \"" + _currentCaseNumber + "\": {\n";
            _result += value + "\n";
            _result += "break;\n";
            _result += "}\n";
            _currentCaseNumber++;
            return this;
        }

        public override string ToString()
        {
            if (_isEnded)
            {
                return _result;
            }
            else
            {
                throw new IncorrectlyEndedSwitch();
            }
        }
    }
}