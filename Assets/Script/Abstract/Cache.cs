namespace Abstract
{
    public abstract class Cache<K, V>
    {
        // Interfaces
        public abstract bool TryGet(K key, out V value);
        public abstract void Add(K key, V value);
    }
}