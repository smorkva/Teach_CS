using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using mJSON.Utils;

namespace mJSON
{
    public sealed class JsonConverter
    {
        private JsonWriter _writer;
        private string _typeIdentifier = "$type";
        private bool _showTypeIdentifier;
        private bool _breakRecure;
        private Stack<object> _stackOfParents;

        private JsonConverter(Formatting format = Formatting.None)
        {
            _writer = new JsonWriter((format & Formatting.Indented) == Formatting.Indented);
            _showTypeIdentifier = (format & Formatting.IncludeTypeName) == Formatting.IncludeTypeName;
            _breakRecure = (format & Formatting.BreakReqursion) == Formatting.BreakReqursion;
            _stackOfParents = new Stack<object>(20);
        }
        
        private void _serialize()
        {
            _writer.Append("null");
        }
        private void _serialize(string value)
        {
            _writer.AppendQuoted(value);
        }
        private void _serialize(bool value)
        {
            _writer.Append((value) ? "true" : "false");
        }
        private void _serialize(IList value)
        {
            if (0 == value.Count)
            {
                _writer.Append("[]");
            }
            else
            {
                _writer.OpenBracket('[');

                var firstItem = true;
                foreach (object obj in value)
                {
                    _writer.AddComa(firstItem);
                    _serialize(obj);
                    firstItem = false;
                }

                _writer.CloseBracket(']');
            }
        }
        private void _serialize(IDictionary value)
        {
            if (0 == value.Count)
            {
                _writer.Append("{}");
            }
            else
            {
                _writer.OpenBracket('{');

                var firstItem = true;
                foreach (object e in value.Keys)
                {
                    _writer.AddComa(firstItem);
                    _serialize(e.ToString());
                    _writer.AddSeparator();
                    _serializeItem(value[e]);

                    firstItem = false;
                }

                _writer.CloseBracket('}');
            }
        }
        private void _serialize(object value)
        {
            if (value is int ||
                value is long ||
                value is byte ||
                value is sbyte ||
                value is short ||
                value is uint ||
                value is ulong ||
                value is ushort)
            {
                _writer.Append(value.ToString());
            }
            else
            {
                if (value is float || value is double)
                {
                    if (null == value)
                    {
                        _serialize();
                    }
                    else
                    {
                        var formattedValue = (value as IFormattable).ToString("G", CultureInfo.InvariantCulture);
                        _writer.Append(formattedValue);
                    }
                }
                else
                {
                    _serializeObject(value);
                }
            }
        }
        private string _getTypeName(Type value)
        {
            if ("Object" != value?.BaseType?.Name)
            {
                return $"{value.FullName}, {value.Namespace}";
            }
            else
            {
                return default(string);
            }
        }
        private void _serializeObject(object value)
        {
            //kill requre
            if (_stackOfParents.Contains(value))
            {
                if (_breakRecure)
                {
                    _serialize();
                    return;
                }
                else
                {
                    throw new Exception("Recursion detected");
                }
            }

            _stackOfParents.Push(value);
            _writer.OpenBracket('{');

            var type = value.GetType();
            var firstItem = true;
            
            if (_showTypeIdentifier)
            {
                var typeName = _getTypeName(type);
                if (null != typeName)
                {
                    _writer.AddTypeName(_typeIdentifier);
                    _writer.AppendQuoted(typeName);

                    firstItem = false;
                }
            }
            
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                _writer.AddComa(firstItem);
                _writer.AppendQuoted(field.Name);
                _writer.AddSeparator();
                _serializeItem(field.GetValue(value));

                firstItem = false;
            }
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    _writer.AddComa(firstItem);
                    _writer.AppendQuoted(property.Name);
                    _writer.AddSeparator();
                    _serializeItem(property.GetValue(value, null));

                    firstItem = false;
                }
            }

            _writer.CloseBracket('}');
            _stackOfParents.Pop();
        }

        private void _serializeItem(object value)
        {
            if (null == value)
            {
                _serialize();
            }
            else
            {
                var asString = value as string;
                if (null != asString)
                {
                    _serialize(asString);
                }
                else
                {
                    if (value is bool)
                    {
                        _serialize((bool)value);
                    }
                    else
                    {
                        if (value is Enum)
                        {
                            var asEnum = value as Enum;
                            var enumValue = Convert.ChangeType(asEnum, asEnum.GetTypeCode());
                            
                            _serialize(enumValue);
                        }
                        else
                        {
                            var asList = value as IList;
                            if (null != asList)
                            {
                                _serialize(asList);
                            }
                            else
                            {
                                var asDictonary = value as IDictionary;
                                if (null != asDictonary)
                                {
                                    _serialize(asDictonary);
                                }
                                else
                                {
                                    if (value is char)
                                    {
                                        _serialize(value.ToString());
                                    }
                                    else
                                    {
                                        _serialize(value);
                                    }
                                }
                            } 
                        }
                    }
                }
            }
        }

        public static string SerializeObject(object value, Formatting format = Formatting.None)
        {
            var instance = new JsonConverter(format);
            instance._serializeItem(value);
            return instance._writer.ToString();
        }
    }
}
