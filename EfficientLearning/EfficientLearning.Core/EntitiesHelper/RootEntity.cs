using System;

namespace EfLearning.Core.EntitiesHelper
{
    [Serializable]
    public abstract class RootEntity : IRootEntity
    {
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Keys = {string.Join(", ", GetKeys().ToString())}";
        }

        public abstract object[] GetKeys();
    }

    [Serializable]
    public abstract class RootEntity<TKey> : RootEntity, IRootEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }

        protected RootEntity()
        {

        }

        protected RootEntity(TKey id)
        {
            Id = id;
        }



        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}
