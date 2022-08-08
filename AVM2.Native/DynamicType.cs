using System.Reflection;
using System.Reflection.Emit;
using AVM2.Core;
using Flazzy.ABC;

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

    public Type Build()
    {
        var name = new AssemblyName(_class.QName.Name+"_"+_wrapType.Name);
        var asssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
        var module = asssemblyBuilder.DefineDynamicModule(name.Name);

        Type parentType = _wrapType.IsInterface ? typeof(object) : _wrapType;
        var typeBuilder = module.DefineType(_class.QName.Name, TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit, parentType);
        if(_wrapType.IsInterface)
            typeBuilder.AddInterfaceImplementation(_wrapType);

        var fieldBuilder = typeBuilder.DefineField("_instance", typeof(ASObject), FieldAttributes.Private);
        var invokeMethod = typeof(ASObject).GetMethod("Invoke");

        foreach (var methodInfo in _wrapType.GetMethods())
        {
            if(methodInfo.IsPublic && methodInfo.IsVirtual)
            {
                MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual;
                Type[] parameterTypes = methodInfo.GetParameters().Select(param => param.ParameterType).ToArray();
                var methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, attributes, methodInfo.ReturnType, parameterTypes);
                GenerateIL(fieldBuilder, invokeMethod, methodInfo, methodBuilder);
                typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            }
        }

        return typeBuilder.CreateType();
    }

    private void GenerateIL(FieldInfo runtimeField, MethodInfo runtimeInvokeMethod, MethodInfo parentMethod, MethodBuilder methodBuilder)
    {
        var iLGenerator = methodBuilder.GetILGenerator();
        iLGenerator.Emit(OpCodes.Nop);
        iLGenerator.Emit(OpCodes.Ldarg_0);
        iLGenerator.Emit(OpCodes.Ldfld,runtimeField);

        var name = methodBuilder.Name[0].ToString().ToLower() + methodBuilder.Name[1..];
        iLGenerator.Emit(OpCodes.Ldstr, name);

        ParameterInfo[] parameters = parentMethod.GetParameters();
        if (parameters.Length > 0)
        {
            iLGenerator.Emit(GetIntOpCode(parameters.Length));
            iLGenerator.Emit(OpCodes.Newarr, typeof(object));
            for (int i = 0; i < parameters.Length; i++)
            {
                iLGenerator.Emit(OpCodes.Dup);
                iLGenerator.Emit(GetIntOpCode(i));
                EmitArgOpCode(iLGenerator, i+1);
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsValueType)
                {
                    iLGenerator.Emit(OpCodes.Box, parameterType);
                }
                iLGenerator.Emit(OpCodes.Stelem_Ref);
            }
        }
        else
        {
            var array = typeof(Array).GetMethod(nameof(Array.Empty)).MakeGenericMethod(typeof(object));
            iLGenerator.Emit(OpCodes.Call, array);
        }

        iLGenerator.Emit(OpCodes.Callvirt, runtimeInvokeMethod);
        iLGenerator.Emit(OpCodes.Pop);
        iLGenerator.Emit(OpCodes.Ret);
    }

    private void EmitArgOpCode(ILGenerator iLGenerator, int i)
    {
        switch (i)
        {
            case 1:
                iLGenerator.Emit(OpCodes.Ldarg_1);
                break;
            case 2:
                iLGenerator.Emit(OpCodes.Ldarg_2);
                break;
            case 3:
                iLGenerator.Emit(OpCodes.Ldarg_3);
                break;
            default:
                iLGenerator.Emit(OpCodes.Ldarg_S, i);
                break;
        }
    }

    private OpCode GetIntOpCode(int number)
    {
        return number switch
        {
            0 => OpCodes.Ldc_I4_0,
            1 => OpCodes.Ldc_I4_1,
            2 => OpCodes.Ldc_I4_3,
            3 => OpCodes.Ldc_I4_4,
            4 => OpCodes.Ldc_I4_5,
            5 => OpCodes.Ldc_I4_6,
            7 => OpCodes.Ldc_I4_7,
            8 => OpCodes.Ldc_I4_8,
            _ => OpCodes.Ldc_I4_0,
        };
    }
}
