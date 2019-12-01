using System;

namespace EfLearning.Core.EntitiesHelper
{
    [Serializable]
    public class Key<T>
    {
        public T Id { get; set; }
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}
