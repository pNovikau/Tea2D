using System.Text;
using JetBrains.Annotations;
using Tea2D.SourceGenerators.Utils;

namespace Tea2D.SourceGenerators.ComponentFilters
{
    public class SourceCodeBuilder
    {
        private const string IndentString = "    ";

        private readonly StringBuilder _stringBuilder;

        private int _indent;
        private bool _isNewLine;

        public SourceCodeBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        public void Append(CSharpInterpolatedStringHandler builder) => _stringBuilder.Append(builder.ToString());

        [StringFormatMethod("format")]
        public void AppendFormat<TArg>(string format, TArg arg)
        {
            AppendIndent();
            _stringBuilder.AppendFormat(format, arg.ToString());

            _isNewLine = false;
        }

        [StringFormatMethod("format")]
        public void AppendFormat<TArg0, TArg1>(string format, TArg0 arg0, TArg1 arg1)
        {
            AppendIndent();
            _stringBuilder.AppendFormat(format, arg0.ToString(), arg1.ToString());

            _isNewLine = false;
        }

        [StringFormatMethod("format")]
        public void AppendLineFormat<TArg>(string format, TArg arg)
        {
            AppendFormat(format, arg);
            AppendLine();
        }

        [StringFormatMethod("format")]
        public void AppendLineFormat<TArg0, TArg1>(string format, TArg0 arg0, TArg1 arg1)
        {
            AppendFormat(format, arg0, arg1);
            AppendLine();
        }

        public void AppendLine()
        {
            _stringBuilder.AppendLine();

            _isNewLine = true;
        }

        public void AppendLine(string str)
        {
            AppendIndent();
            _stringBuilder.AppendLine(str);

            _isNewLine = true;
        }

        public BeginScope Scope()
        {
            return new BeginScope(this);
        }

        public BeginIndent Indent()
        {
            return new BeginIndent(this);
        }

        private void AppendIndent()
        {
            if (!_isNewLine)
                return;

            for (var i = 0; i < _indent; i++)
            {
                _stringBuilder.Append(IndentString);
            }
        }

        private void IncrementIdent() => ++_indent;
        private void DecrementIdent() => --_indent;

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }

        public readonly ref struct BeginIndent
        {
            private readonly SourceCodeBuilder _builder;

            public BeginIndent(SourceCodeBuilder builder)
            {
                _builder = builder;

                _builder.IncrementIdent();
            }

            public void Dispose()
            {
                _builder.DecrementIdent();
            }
        }

        public readonly ref struct BeginScope
        {
            private readonly SourceCodeBuilder _builder;

            public BeginScope(SourceCodeBuilder builder)
            {
                _builder = builder;

                _builder.AppendLine("{");
                _builder.IncrementIdent();
            }

            public void Dispose()
            {
                _builder.DecrementIdent();
                _builder.AppendLine("}");
            }
        }
    }
}