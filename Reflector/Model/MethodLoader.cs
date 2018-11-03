using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataContract.Model;
using Reflector.ExtensionMethods;
using DataContract.Model.Enums;

namespace Reflector.Model
{
    internal class MethodLoader
    {
        internal static MethodMetadataDto LoadMethodMetadataDto(MethodBase method, AssemblyMetadataStorage metaStore)
        {
            MethodMetadataDto methodMetadataDto = new MethodMetadataDto()
            {
                Name = method.Name,
                Modifiers = EmitModifiers(method),
                Extension = EmitExtension(method)
            };
            methodMetadataDto.GenericArguments = !method.IsGenericMethodDefinition ? null : TypeLoader.EmitGenericArguments(method.GetGenericArguments());
            methodMetadataDto.ReturnType = EmitReturnType(method);
            methodMetadataDto.Parameters = EmitParameters(method.GetParameters());

            return methodMetadataDto;
        }

        internal static IEnumerable<MethodMetadataDto> EmitMethods(IEnumerable<MethodBase> methods, AssemblyMetadataStorage metaStore)
        {
            return from MethodBase currentMethod in methods
                where currentMethod.IsVisible()
                select LoadMethodMetadataDto(currentMethod, metaStore);
        }

        private static IEnumerable<ParameterMetadataDto> EmitParameters(IEnumerable<ParameterInfo> parameters)
        {
            return from parameter in parameters
                   select new ParameterMetadataDto(parameter.Name, TypeLoader.EmitReference(parameter.ParameterType));
        }

        private static TypeMetadataDto EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            return methodInfo == null ? null : TypeLoader.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = AccessLevel.IsPrivate;
            if (method.IsPublic)
            {
                access = AccessLevel.IsPublic;
            }
            else if (method.IsFamily)
            {
                access = AccessLevel.IsProtected;
            }
            else if (method.IsFamilyAndAssembly)
            {
                access = AccessLevel.IsProtectedInternal;
            }

            AbstractEnum isAbstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
            {
                isAbstract = AbstractEnum.Abstract;
            }

            StaticEnum isStatic = StaticEnum.NotStatic;
            if (method.IsStatic)
            {
                isStatic = StaticEnum.Static;
            }

            VirtualEnum isVirtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
            {
                isVirtual = VirtualEnum.Virtual;
            }

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, isAbstract, isStatic, isVirtual);
        }
    }
}