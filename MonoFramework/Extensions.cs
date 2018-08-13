﻿using MonoFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace MonoFramework
{
    public static class Extensions
    {
        public static string XMLSerialize(this object obj)
        {
            YAXSerializer serializer = new YAXSerializer(obj.GetType());
            return serializer.Serialize(obj);
        }
        public static object XMLDeserialize(this string content, Type type)
        {
            if (content == string.Empty)
                return Activator.CreateInstance(type);

            YAXSerializer serializer = new YAXSerializer(type);
            return Convert.ChangeType(serializer.Deserialize(content), type);
        }
        /// <summary>
        /// Less Faster then XMLDeserialize(this string content,Type type)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static object XMLDeserialize(this string content, Assembly assembly)
        {
            string typeAsString = new string(content.Split('>')[0].Skip(1).ToArray());
            var type = assembly.GetTypes().FirstOrDefault(x => x.Name == typeAsString);
            return XMLDeserialize(content, type);
        }
        public static T XMLDeserialize<T>(this string content)
        {
            return (T)XMLDeserialize(content, typeof(T));
        }
        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            int count = enumerable.Count();
            if (count <= 0)
                return default(T);

            return enumerable.ElementAt(new AsyncRandom().Next(count));
        }
        public static T[] Random<T>(this IEnumerable<T> enumerable, int count)
        {
            T[] array = new T[count];

            int lenght = enumerable.Count();

            if (lenght <= 0)
                return new T[0];

            var random = new AsyncRandom();

            for (int i = 0; i < count; i++)
            {
                array[i] = enumerable.ElementAt(random.Next(lenght));
            }

            return array;
        }
        public static T CreateDelegate<T>(this ConstructorInfo ctor)
        {
            List<ParameterExpression> list =
                Enumerable.ToList<ParameterExpression>(Enumerable.Select<ParameterInfo, ParameterExpression>((IEnumerable<ParameterInfo>)ctor.GetParameters(),
                (Func<ParameterInfo, ParameterExpression>)(param => Expression.Parameter(param.ParameterType, string.Empty))));

            var list2 = list.ConvertAll<Expression>(x => (Expression)x);
            return Expression.Lambda<T>((Expression)Expression.New(ctor, list2), list).Compile();
        }
        public static Delegate CreateDelegate(this MethodInfo method, params Type[] delegParams)
        {
            Type[] array = (
                from p in method.GetParameters()
                select p.ParameterType).ToArray<Type>();
            if (delegParams.Length != array.Length)
            {
                throw new Exception("Method parameters count != delegParams.Length");
            }
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, null, new Type[]
            {
                typeof(object)
            }.Concat(delegParams).ToArray<Type>(), true);
            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            if (!method.IsStatic)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(method.DeclaringType.IsClass ? OpCodes.Castclass : OpCodes.Unbox, method.DeclaringType);
            }
            for (int i = 0; i < delegParams.Length; i++)
            {
                iLGenerator.Emit(OpCodes.Ldarg, i + 1);
                if (delegParams[i] != array[i])
                {
                    if (!array[i].IsSubclassOf(delegParams[i]) && !HasInterface(array[i], delegParams[i]))
                    {
                        throw new Exception(string.Format("Cannot cast {0} to {1}", array[i].Name, delegParams[i].Name + " check your parameters order."));
                    }
                    iLGenerator.Emit(array[i].IsClass ? OpCodes.Castclass : OpCodes.Unbox, array[i]);
                }
            }
            iLGenerator.Emit(OpCodes.Call, method);
            iLGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(Expression.GetActionType(new Type[]
            {
                typeof(object)
            }.Concat(delegParams).ToArray<Type>()));
        }
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            return type.FindInterfaces(new TypeFilter(FilterByName), interfaceType).Length > 0;
        }
        private static bool FilterByName(Type typeObj, object criteriaObj)
        {
            return typeObj.ToString() == criteriaObj.ToString();
        }
        public static object GetCustomAttribute(this MethodInfo methodInfo, Type attributeType)
        {
            return methodInfo.GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == attributeType);
        }
    }

    
}
