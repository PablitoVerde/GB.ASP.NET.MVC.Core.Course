﻿namespace Core
{
    public class ConcurrentList<T>
    {
        private List<T> _list;
        private object _lock = new object();

        public ConcurrentList()
        {
            _list = new List<T>();
        }

        public void AddItem(T item)
        {
            lock (_lock)
            {
                _list.Add(item);
            }
        }

        public void DeleteItem(T item)
        {
            lock (_lock)
            {
                _list = _list.FindAll(x => !x.Equals(item)).ToList();
            }
        }

        public int CountItems()
        {
            lock (_lock)
            {
                return _list.Count;
            }
        }

        public T FindItem(int id)
        {
            lock (_lock)
            {
                if (_list.Count < id)
                {
                    return _list[id + 1];
                }
                else
                {
                    return _list[0];
                }
            }
        }

        public List<T> GetProducts()
        {
            lock (_lock)
            {
                return _list;
            }
        }

        public void ClearList()
        {
            lock (_lock)
            {
                _list.Clear();
            }
        }
    }
}