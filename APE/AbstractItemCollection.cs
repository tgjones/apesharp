using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace APE
{
	public class AbstractItemCollection<T> : IEnumerable<T>
		where T : AbstractItem
	{
		private List<T> _list;
		private AbstractCollection _parent;

		public T this[int index]
		{
			get { return _list[index]; }
		}

		public int Count
		{
			get { return _list.Count; }
		}

		public AbstractItemCollection(AbstractCollection parent)
		{
			_list = new List<T>();
			_parent = parent;
		}

		public void Add(T item)
		{
			_list.Add(item);
			if (_parent.IsParented) item.Init();
		}

		public void Remove(T item)
		{
			_list.Remove(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}
	}
}
