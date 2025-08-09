#if TOOLS
using Godot;

namespace GodotConstantsGeneratorExample.addons.godot_meetup_example;

[Tool]
public partial class GodotMeetupExample : EditorPlugin {
	public Button button = new Button {
		Text = "Click Me",
		TooltipText = "This is a button added by the Godot Meetup Example plugin.",
	};

	public CustomControlContainer container = CustomControlContainer.Toolbar;

	public override void _EnterTree() {
		button.Pressed += () => {;
			GD.Print("Button pressed!");
		};
		AddControlToContainer(container, button);
	}

	public override void _ExitTree() {
		RemoveControlFromContainer(container, button);
	}
}
#endif
