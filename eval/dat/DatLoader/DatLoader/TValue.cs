using System;
using System.Collections.Generic;

namespace DatLoader
{
    public class TValue : Dictionary<string, string>
    {
        public static string entryId = "id";

        public TValue() { }
        public TValue(int id) { SetId(id); }
        public TValue(TValue value)
        {
            foreach (var key in value.Keys) SetValue(key, value.GetValue(key));
        }

        public int GetId()
        {
            return Convert.ToInt32(GetValue(entryId));
        }

        public void SetId(int id)
        {
            SetValue(entryId, id.ToString());
        }

        public bool HasValue(string key) { return ContainsKey(key); }
        public void SetValue(string key, string value)
        {
            //if (ContainsKey(key))
                this[key] = value;
            //else
            //    Add(key, value);
        }
        public string GetValue(string key, string def = "")
        {
            return TryGetValue(key, out var value) ? value : def;
        }
    }
}