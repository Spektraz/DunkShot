using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace MVC.Model
{
    public abstract class Model<Id,InternalValue,Value>:ScriptableObject, IModel  where Value : InternalData<Id, InternalValue>
    {

        [SerializeField] protected List<Value> list;
        private readonly Dictionary<Id, InternalValue> dictionary = new Dictionary<Id, InternalValue>();

        protected Dictionary<Id, InternalValue> Dictionary
        {
            get
            {
                if (dictionary.IsNullOrEmpty())
                {
                    list.ForEach(x => dictionary.Add(x.ID, x.Value));
                }

                return dictionary;
            }
        }
        public bool HasKey(Id id) => Dictionary.ContainsKey(id);
        public Dictionary<Id, InternalValue>.KeyCollection Keys => Dictionary.Keys;
        public Dictionary<Id, InternalValue>.ValueCollection Values => Dictionary.Values;

        public List<Id> Ids => Dictionary.Keys.ToList();
    }
}