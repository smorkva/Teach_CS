using System;
using System.IO;
using System.Text;
using System.Globalization;

namespace mJSON.Utils
{
    public class JsonWriter
    {
        private readonly StringBuilder _strBuilder;
        private readonly StringWriter _strWriter;

        private readonly string _indendValue = "  ";
        private readonly string _newLine = "\r\n";
        private readonly string _separatorIntended = ": ";
        private readonly string _separator = ":";

        private int _indentStep = 0;
        private bool _showIndent;
        
        public JsonWriter(bool showIndent = false)
        {
            _showIndent = showIndent;
            _strBuilder = new StringBuilder(256);
            _strWriter = new StringWriter(_strBuilder, CultureInfo.InvariantCulture);
            _strWriter.NewLine = "";
           
            if (null == _strWriter)
            {
                throw new ArgumentNullException(nameof(_strWriter));
            }
        }

        public void OpenBracket(char bracket)
        {
            _strWriter.Write(bracket);

            if(_showIndent)
            {
                _strWriter.Write(_newLine);
                _indentStep++;
            }
        }
        public void CloseBracket(char bracket)
        {
            if (_showIndent)
            {
                _strWriter.Write(_newLine);
                _indentStep--;
                AddIndent();
            }
            
            _strWriter.Write(bracket);
        }
        public void AddIndent()
        {
            for (var i = 0; i < _indentStep; i++)
            {
                _strWriter.Write(_indendValue);
            }
        }
        public void AddTypeName(string value)
        {
            if (_showIndent)
            {
                AddIndent();
            }
            AppendQuoted(value);
            AddSeparator();
        }
        public void AddComa(bool firstElement)
        {
            if (!firstElement)
            {
                _strWriter.Write(',');
            }

            if (_showIndent)
            {
                if (!firstElement)
                {
                    _strWriter.Write(_newLine);
                }
                AddIndent();
            }
        }
        public void AddSeparator()
        {
            if (_showIndent)
            {
                _strWriter.Write(_separatorIntended);
            }
            else
            {
                _strWriter.Write(_separator);
            }
        }
        public void Append(string value)
        {
            foreach (var c in value)
            {
                switch (c)
                {
                    case '"':
                        _strWriter.Write(@"\""");
                        break;

                    case '\\':
                        _strWriter.Write(@"\\");
                        break;

                    case '\b':
                        _strWriter.Write(@"\b");
                        break;

                    case '\f':
                        _strWriter.Write(@"\f");
                        break;

                    case '\n':
                        _strWriter.Write(@"\n");
                        break;

                    case '\r':
                        _strWriter.Write(@"\r");
                        break;

                    case '\t':
                        _strWriter.Write(@"\t");
                        break;

                    default:
                        int codepoint = Convert.ToInt32(c);
                        if ((codepoint >= 32) && (codepoint <= 126))
                        {
                            _strWriter.Write(c);
                        }
                        else
                        {
                            _strWriter.Write("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                        }
                        break;
                }
            }

            //_strWriter.Write(value);
        }
        public void AppendQuoted(string value)
        {
            _strWriter.Write($"\"");
            Append(value);
            _strWriter.Write($"\"");
        }
        public override string ToString()
        {
            return _strWriter.ToString();
        }
    }
}
