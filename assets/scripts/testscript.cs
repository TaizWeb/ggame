using Godot;
using System;

public class testscript : KinematicBody2D
{
	[Export] public int speed = 200;

	public Vector2 velocity = new Vector2();


	public bool Controllable;
	Area2D interactionArea;
	Control interactionIcon;
	float interactionOffset;
	IInteractable interactable;
	public IInteractable Interactable
	{
		get { return interactable; }
		set
		{
			interactable = value;
			interactionIcon.Visible = interactable != null;
		}
	}

	public override void _Ready()
	{
		Controllable = true;
		interactionArea = GetChild(2) as Area2D;
		interactionIcon = GetParent().GetChild(2) as Control;
		interactionIcon.Visible = false;
		interactionOffset = interactionArea.Position.Length();
		interactionArea.Connect("body_entered", this, nameof(CheckInteractableEnter));
		interactionArea.Connect("body_exited", this, nameof(CheckInteractableExit));
	}

	public void CheckInteractableEnter(Node node)
	{
		if (node is IInteractable interactable)
		{
			Interactable = interactable;
		}
	}

	public void CheckInteractableExit(Node node)
	{
		if (node is IInteractable interactable && this.interactable == interactable)
		{
			Interactable = null;
		}
	}

	public void GetInput()
	{
		if (Input.IsActionJustPressed("interact") && interactable != null)
		{
			interactable.Interact(this);
			return;
		}

		velocity = new Vector2();

		if (Input.IsActionPressed("right"))
			velocity.x += 1;

		if (Input.IsActionPressed("left"))
			velocity.x -= 1;

		if (Input.IsActionPressed("down"))
			velocity.y += 1;

		if (Input.IsActionPressed("up"))
			velocity.y -= 1;

		velocity = velocity.Normalized() * speed;
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Controllable)
		{
			GetInput();
		}
		velocity = MoveAndSlide(velocity);
		interactionArea.Position = interactionOffset * velocity.Normalized();
	}
}

