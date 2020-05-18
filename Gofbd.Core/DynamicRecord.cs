namespace Gofbd.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable]
    public class DynamicRecord : DynamicObject, ISerializable, IReadOnlyDictionary<string, object>
    {
        private readonly List<string> keys = new List<string>();
        private readonly List<object> values = new List<object>();

        public DynamicRecord(IDataRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            for (var i = 0; i < record.FieldCount; i++)
            {
                var column = record.GetName(i);
                var value = record[i];

                this.keys.Add(column);
                this.values.Add(value);
            }
        }

        public DynamicRecord(IEnumerable<KeyValuePair<string, object>> record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            foreach (var pair in record)
            {
                this.keys.Add(pair.Key);
                this.values.Add(pair.Value);
            }
        }

        // ReSharper disable UnusedParameter.Local
        // ReSharper restore UnusedParameter.Local
        protected DynamicRecord(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            this.keys = (List<string>)info.GetValue("keys", typeof(List<string>));
            this.values = (List<object>)info.GetValue("values", typeof(List<object>));
        }

        public object this[string key]
        {
            get
            {
                var index = this.FindIndex(key);

                if (index < 0)
                {
                    throw new KeyNotFoundException();
                }

                return this.values[index];
            }
        }

        public int Count => this.keys.Count;

        public bool ContainsKey(string key)
        {
            return this.keys.Contains(key, StringComparer.OrdinalIgnoreCase);
        }

        public bool TryGetValue(string key, out object value)
        {
            value = null;
            var index = this.FindIndex(key);

            if (index < 0)
            {
                return false;
            }

            value = this.values[index];

            return true;
        }

        public virtual IEnumerable<string> Keys => this.keys.AsReadOnly();

        public virtual IEnumerable<object> Values => this.values.AsReadOnly();

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("keys", this.keys, typeof(List<string>));
            info.AddValue("values", this.values, typeof(List<object>));
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder == null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            result = null;

            var index = this.FindIndex(binder.Name);

            if (index < -1)
            {
                return false;
            }

            result = this.values[index];

            if (result != DBNull.Value)
            {
                return result != null;
            }

            result = null;

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (indexes == null)
            {
                throw new ArgumentNullException(nameof(indexes));
            }

            result = null;

            var name = indexes[0].ToString();

            if (!int.TryParse(name, out var index))
            {
                index = this.FindIndex(name);
            }

            if (index < -1)
            {
                return false;
            }

            result = this.values[index];

            if (result != DBNull.Value)
            {
                return result != null;
            }

            result = null;

            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.keys.Select((t, i) => new KeyValuePair<string, object>(t, this.values[i])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int FindIndex(string key)
        {
            return this.keys.FindIndex(name =>
                string.Compare(name, key, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}
