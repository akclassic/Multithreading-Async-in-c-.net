public class GlobalReaderWriterLock
{
    private static readonly ReaderWriterLockSlim _lock = new(System.Threading.LockRecursionPolicy.NoRecursion);
    public Dictionary<string, string> cache { get; set;  } = new Dictionary<string, string>();

    public void AddToCache(string key, string value)
    {
        bool isLockAcquired = false;
        try
        {
            _lock.EnterWriteLock();
            isLockAcquired = true;
            cache[key] = value;
        }
        finally
        {
            if (isLockAcquired)
            {
                _lock.ExitWriteLock();
            }
        }
    }

    public string? GetFromCache(string key)
    {
        bool isLockAcquired = false;
        try
        {
            _lock.EnterReadLock();
            isLockAcquired = true;
            cache.TryGetValue(key, out var value);
            return value ?? null;
        }
        finally
        {
            if (isLockAcquired)
            {
                _lock.ExitReadLock();
            }
        }   
    }
}