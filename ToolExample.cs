using Godot;

namespace GodotConstantsGeneratorExample;

[Tool]
public partial class ToolExample : Label3D {
	[ExportGroup("Buttons")]
	[ExportToolButton("Hello")] private Callable helloButton => Callable.From(Hello);
	[ExportToolButton("Random label")] private Callable node3dExample => Callable.From(RandomLabel);

	private string[] messages = {
		"Hello World!",
		"ToolExample is working!",
		"Godot is awesome!",
		"Godot Meetup is awesome!"
	};

	private void Hello() {
		GD.Print("Hello from ToolExample!");
	}

	private void RandomLabel() {
		Text = messages[GD.RandRange(0, messages.Length - 1)];
	}
}
