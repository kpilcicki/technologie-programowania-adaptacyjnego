using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataContract.Model.Enums;

namespace BusinessLogic.Model
{
    internal class MethodMetadata
    {
        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods
                where currentMethod.GetVisible()
                select new MethodMetadata(currentMethod);
        }

        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            return methodInfo == null ? null : TypeMetadata.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = AccessLevel.IsPrivate;
            if (method.IsPublic)
                access = AccessLevel.IsPublic;
            else if (method.IsFamily)
                access = AccessLevel.IsProtected;
            else if (method.IsFamilyAndAssembly)
                access = AccessLevel.IsProtectedInternal;
            AbstractEnum isAbstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                isAbstract = AbstractEnum.Abstract;
            StaticEnum isStatic = StaticEnum.NotStatic;
            if (method.IsStatic)
                isStatic = StaticEnum.Static;
            VirtualEnum isVirtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                isVirtual = VirtualEnum.Virtual;
            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, isAbstract, isStatic, isVirtual);
        }

        public string Name { get; }

        public IEnumerable<TypeMetadata> GenericArguments { get; }

        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; }

        public TypeMetadata ReturnType { get; }

        public bool Extension { get; }

        public IEnumerable<ParameterMetadata> Parameters { get; }

        // constructor
        private MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition
                ? null
                : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters());
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }
    }
}