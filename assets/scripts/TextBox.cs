using System;
using Godot;

public class TextBox : Panel
{
    public static PackedScene Prefab = ResourceLoader.Load<PackedScene>("assets/prefabs/TextBox.tscn");
    Action OnExit;
    public Label Label;

    public override void _EnterTree()
    {
        Label = GetChild(0) as Label;
    }

    public override void _PhysicsProcess(float delta)
    {
        if(Input.IsActionJustPressed("interact"))
        {
            Close();
        }
    }

    public void Close()
    {
        QueueFree();
        OnExit?.Invoke();
    }

    public static void Create(Node caller, string text, Action onExit)
    {
        TextBox box = Prefab.Instance() as TextBox;
        caller.GetTree().Root.AddChild(box);

        box.OnExit = onExit;
        box.Label.Text = text;
    }
}