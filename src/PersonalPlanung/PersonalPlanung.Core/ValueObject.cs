using System;
using System.Collections.Generic;
using System.Reflection;

namespace PersonalPlanung.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreValueAttribute : System.Attribute {}

    public abstract class ValueObject<TValue>: IEquatable<TValue> where TValue : ValueObject<TValue>
    {
        public bool Equals(TValue other)
        {
            if (other == null)
                return false;
            if (GetType() != other.GetType())
                return false;

            var fields = GetFieldsFromTypeHierarchy();
            foreach (var field in fields)
            {
                var value1 = field.GetValue(this);
                var value2 = field.GetValue(other);
                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return Equals(obj as TValue);
        }

        public override int GetHashCode()
        {
            var fields = GetFieldsFromTypeHierarchy();
            const int startValue = 17;
            const int multiplier = 59;

            var hashcode = startValue;
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                hashcode = (hashcode * multiplier) + (value != null ? value.GetHashCode() : startValue);
            }
            return hashcode;
        }

        IEnumerable<PropertyInfo> GetFieldsFromTypeHierarchy()
        {
            var t = GetType();
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute<IgnoreValueAttribute>() == null)
                    yield return prop;
            }
#if NOT
            MemberInfo[] members = t.GetMembers(BindingFlags.Public| BindingFlags.Instance| BindingFlags.DeclaredOnly);
            var fields = new List<FieldInfo>();
            while (t != typeof(object))
            {
                var typeFields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var typeField in typeFields)
                {
                    var pi = t.GetProperty(typeField.Name);
                    if( pi==null || pi.GetCustomAttribute<IgnoreValueAttribute>()!=null )
                    //if (!Attribute.IsDefined(typeField, typeof(IgnoreValueAttribute)))
                        fields.Add(typeField);
                }
//                    .Where(x => x.GetCustomAttribute<>()==null); // .GetType().IsGenericType); // !x.GetType().GetInterfaces().Contains(typeof(IList<>)));
//                fields.AddRange(typeFields);
                t = t.BaseType;
            }
            return fields;
#endif
        }

        public static bool operator ==(ValueObject<TValue> left, ValueObject<TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<TValue> left, ValueObject<TValue> right)
        {
            return !Equals(left, right);
        }

        /* Implementation of "Implement value objects" from
           https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects

            Drawbacks:
            - has to be subclassed
            - It is possible to forget Fields in AtomicValues
         *
        protected abstract IEnumerable<object> GetAtomicValues();

        protected static bool EqualOperator(TValue left, TValue right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(TValue left, TValue right)
        {
            return !(EqualOperator(left, right));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var other = (TValue)obj;
            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                    return false;
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                    return false;
            }

            var result = !thisValues.MoveNext() && !otherValues.MoveNext();
            thisValues.Dispose();
            otherValues.Dispose();
            return result;
        }

        public override int GetHashCode()
        {
            return GetAtomicValues().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
        }
        */
    }
}