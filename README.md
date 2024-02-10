This is a demontration of dynamic Node pooling where it recycles the same type of Nodes between active and inactive ones.

I also have included some nice tips, such as:
* How you should setup your essential variables by caching them in a master class (I called it World)
* How you can load Custom Resources without any use from a Node (Useful for storing PackedScenes)

USAGE:

First have a global variable of a NodePool with the desired Node Type with Generics
```cs
	public static NodePool<Box> box_pool;
```
Then, initialize it somewhere with your own Node creation method that returns the created Node
```cs
public override void _EnterTree()
	{
		box_pool = new(Box.Create, world, 10);
	}
```
After that you may simply use the Add() Method to create the Node and add it to the pool
```cs
public override void _UnhandledInput(InputEvent input)
	{
		if (input.IsActionPressed("ui_accept")) // Space
		{
			GD.Print("Test");
			Box box = box_pool.Add();
			// do or set stuff with the box, whatever you wanna do
			Vector3 new_position = new(GD.RandRange(-50, 50), 1f, GD.RandRange(-50, 50)); // placing it elsewhere, cuz why not

			box.GlobalPosition = new_position;
		}
	}
``` 
Finally, you should use the Remove(pooled_node) method to remove the object from the SceneTree and the active pool, it will be sent to the inactive pool for future reuse
```cs
public partial class Box : StaticBody3D
{
	public async override void _EnterTree()
	{
		await ToSignal(World.sceneTree.CreateTimer(5f), SceneTreeTimer.SignalName.Timeout); // wait for 5 seconds
		World.box_pool.Remove(this); // Remove it from the SceneTree and from the active pool but keep it in the inactive queue
	}
	public static Box Create() // customizable instantiating, whether it would be from a new c# instance or from a PackedScene, up to you
	{
		Box box = (Box)BoxData.instance.prefab.Instantiate();
		return box;
	}
}
```

This is my first attempt on object pooling and I think it went pretty well, enjoy!
Ask any questions if you have them on discord : the_hyper_dev
