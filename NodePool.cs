using System;
using System.Collections.Generic;
using Godot;
public sealed partial class NodePool<T> where T : Node
{

	public HashSet<T> active_pool;
	public Queue<T> inactive_pool;
	public Func<T> instantiate_method;
	public Node parent;
	public NodePool(Func<T> instantiate_method, Node parent, in int init_capacity = 10)
	{
		this.instantiate_method += instantiate_method;
		this.parent = parent;
		active_pool = new(init_capacity);
		inactive_pool = new(init_capacity);
		int i = 0;
		do // preferring do while loop because it is faster and is already guaranteed to run anyways
		{
			inactive_pool.Enqueue(instantiate_method.Invoke());
			++i;
		} while (i < init_capacity);
	}

	public T Add()
	{
		T node = inactive_pool.Count == 0 ? instantiate_method.Invoke() : inactive_pool.Dequeue();
		active_pool.Add(node);
		parent.AddChild(node);
		return node;
	}
	public void Remove(T item)
	{
		parent.RemoveChild(item);

		active_pool.Remove(item);
		inactive_pool.Enqueue(item);
	}

}