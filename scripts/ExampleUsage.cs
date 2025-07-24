using Godot;
using Godot.Collections;
using GodotConstantsGeneratorExample.scripts.generated;

namespace GodotConstantsGeneratorExample.scripts;

public partial class ExampleUsage : Node3D
{
	public override void _Process(double delta) {
		PhysicsRayQueryParameters3D query = new PhysicsRayQueryParameters3D {
			From = Vector3.Zero,
			To = Vector3.One,
			CollisionMask = CollisionLayers.Layer4,
		};

		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		Dictionary result = spaceState.IntersectRay(query);
	}

	public override void _Input(InputEvent @event) {
		if (@event.IsActionPressed(Actions.ZoomIn)) {
			// zoom in
		} else if (@event.IsActionPressed(Actions.ZoomOut)) {
			// zoom out
		}

		if (Input.IsActionPressed(Actions.Attack)) {
			// attack
		}

		if (Input.IsActionPressed(Actions.Jump)) {
			// jump
		}
	}

	private void OnBodyEntered(Node3D body) {
		if (body.IsInGroup(Groups.Player)) {
			// player
		}
		if (body.IsInGroup(Groups.Enemy)) {
			// enemy
		}
	}
}