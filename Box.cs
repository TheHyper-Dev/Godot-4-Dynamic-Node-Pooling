using Godot;
using System;

public partial class Box : StaticBody3D
{
	public async override void _EnterTree()
	{
		await ToSignal(World.sceneTree.CreateTimer(20f), SceneTreeTimer.SignalName.Timeout); // wait for 2 seconds
		World.box_pool.Remove(this); // Remove it from the Scene but keep it in the inactive queue
	}
	public static Box Create() // customizable instantiating, whether it would be from a new c# instance or from a PackedScene, up to you
	{
		Box box = (Box)BoxData.instance.prefab.Instantiate();
		return box;
	}
}