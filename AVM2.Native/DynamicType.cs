using System.Diagnostics;
using System.Reflection;
using System.Text;
using AVM2.Core;
using AVM2.Core.Interpreted;
using Flazzy.ABC;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace AVM2.Native;
public class DynamicType
{
    private readonly ASClass _class;
    private readonly Type _wrapType;

    public DynamicType(ASClass @class, Type wrapType)
    {
        _class = @class;
        _wrapType = wrapType;
    }

    private string Build()
    {
        var builder = new StringBuilder();
        builder.Append("using AVM2.Core; ");
        if(_wrapType.Namespace is not null && _wrapType.Namespace != string.Empty)
            builder.AppendFormat("using {0};", _wrapType.Namespace);
        builder.AppendFormat("public class {0} ", _class.QName.Name);
        builder.Append(": "+_wrapType.Name+" {");
        builder.Append("private ASObject _instance; ");

        foreach (var methodInfo in _wrapType.GetMethods(BindingFlags.Instance | BindingFlags.Public))
        {
            builder.Append("public ");
            if (methodInfo.IsVirtual && !_wrapType.IsInterface)
                builder.Append("override ");
            string returnType = methodInfo.ReturnType.Name;
            if(methodInfo.ReturnType == typeof(void))
                returnType = "void";
            builder.Append(returnType + " " + methodInfo.Name + "(");
            GenerateParamsAndBody(builder, methodInfo);

            builder.Append("} ");
        }
        builder.Append("} ");
        builder.AppendFormat("return typeof({0});",_class.QName.Name);
        return builder.ToString();
    }

    private static void GenerateParamsAndBody(StringBuilder builder, MethodInfo methodInfo)
    {
        string name = methodInfo.Name[0].ToString().ToLower()+methodInfo.Name[1..];
        var statement = new StringBuilder($"_instance.Invoke(\"{name}\"");
        var parameters = new List<string>();
        foreach (var parameterInfo in methodInfo.GetParameters())
        {
            string parameter = parameterInfo.ParameterType.Name;
            parameters.Add(parameter + " " + parameterInfo.Name);
            statement.Append(", " + parameterInfo.Name);
        }
        builder.Append(string.Join(',', parameters));
        builder.Append(")\n{ ");
        statement.Append("); ");

        if (methodInfo.ReturnType == typeof(void))
            builder.Append(statement);
        else
            builder.Append('(' + methodInfo.ReturnType.FullName + ')' + statement);
    }

    public Type Compile()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var code = Build();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        
        var options = ScriptOptions.Default.AddReferences(AppDomain.CurrentDomain.GetAssemblies());
        stopwatch.Restart();
        var type = (Type)CSharpScript.RunAsync(code, options).Result.ReturnValue;
        
        return type;
    }
}
