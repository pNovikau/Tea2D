using System.Collections.Generic;
using System.Diagnostics;
using CppAst;

namespace Tea2D.Vulkan.Generator.CSharp
{
    [DebuggerDisplay("{" + nameof(FullName) + "}")]
    public class CSharpDelegate : CSharpType
    {
        private readonly CppFunctionType _cppFunctionType;

        private CSharpType? _returnType;
        private CSharpType[]? _parameters;

        public CSharpDelegate(CppFunctionType cppFunctionType) : base(cppFunctionType)
        {
            _cppFunctionType = cppFunctionType;
        }

        public IEnumerable<CSharpType> Parameters
        {
            get
            {
                if (_parameters != null)
                    return _parameters;

                _parameters = new CSharpType[_cppFunctionType.Parameters.Count];

                for (var i = 0; i < _cppFunctionType.Parameters.Count; i++)
                {
                    var cppParameter = _cppFunctionType.Parameters[i];

                    _parameters[i] = new CSharpType(cppParameter.Type);
                }

                return _parameters;
            }
        }

        public CSharpType ReturnType => _returnType ??= new CSharpType(_cppFunctionType.ReturnType);

        public override void Write(ISourceWriter writer)
        {
            writer.WriteLine(ToString());
        }

        public override string ToString()
        {
            var parameters = string.Join(", ", Parameters);

            return $"unsafe delegate* unmanaged<{parameters}, {ReturnType}>";
        }
    }
}