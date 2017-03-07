using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

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
           
            if (_strWriter == null)
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
            for (int i = 0; i < _indentStep; i++)
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
            _strWriter.Write(value);
        }
        public void AppendQuoted(string value)
        {
            _strWriter.Write($"\"{value}\"");
        }
        public override string ToString()
        {
            return _strWriter.ToString();
        }
    }
}
