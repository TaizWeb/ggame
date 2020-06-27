using Godot;
using System;

public class NPC : KinematicBody2D, IInteractable
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public virtual void Interact(testscript player) 
    {
        player.Controllable = false;
        TextBox.Create(this, "Interaction is live boys", () => player.Controllable = true);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
