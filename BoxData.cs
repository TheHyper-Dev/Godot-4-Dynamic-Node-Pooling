using Godot;
using System;



public sealed partial class BoxData : Resource
{
    public static readonly BoxData instance = (BoxData)ResourceLoader.Load("res://BoxData.res");

    [Export] public PackedScene prefab;
}
