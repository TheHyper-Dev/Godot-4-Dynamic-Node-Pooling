using Godot;
using System;

public sealed partial class World : Node // making the script inheriting from Node so that it would be compatible with any other kind of Node (Such as WorldEnvironment in this case)
{
	public static World world;
	public static SceneTree sceneTree;
	public static Window window;
	public static NodePool<Box> box_pool;

	public override void _EnterTree()
	{
		world = this;
		sceneTree = (SceneTree)Engine.GetMainLoop();
		window = sceneTree.Root;
		box_pool = new(Box.Create, world, 10);
	}
	public override void _ExitTree()
	{
		box_pool.instantiate_method -= Box.Create;
	}
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
		// else if (input.IsActionPressed("ui_focus_next")) // Tab
		// {

		// }
	}
}
