# Godot Constants Generator Example

Example for the [GodotConstantsGenerator](https://github.com/michaeldomanek/GodotConstantsGenerator). \
The tool automatically extracts input actions, collision layers, and group names from `project.godot` and generates strongly-typed C# constants:

- `Actions.cs` → Input actions
- `CollisionLayers.cs` → named collision layers
- `Groups.cs` → Global group names

## Installation

```bash
dotnet tool install --global GodotConstantsGenerator
```

## Usage
Run this command from the project root:
```bash
godot-constants-generator [options]
```

### Automatic regeneration with watchexec

Run this to automatically regenerate when `project.godot` changes:

```bash
watchexec -w project.godot -- godot-constants-generator
```

Install `watchexec`: https://github.com/watchexec/watchexec

## Example usage in your C# scripts:

Example of using the input action constants:

```csharp
public static class Actions {
    public static readonly StringName Attack = "Attack";
    public static readonly StringName Jump = "Jump";
}

public override void _Input(InputEvent @event) {
    if (Input.IsActionPressed(Actions.Attack)) {
        // attack
    }
    if (Input.IsActionPressed(Actions.Jump)) {
        // jump
    }
}
```

You can view the example usage in the `scripts/ExampleUsage.cs` file and the generated code in `scripts/generated/...`.

## Requirements:
- Godot Mono 4.x (tested on Godot 4.4.1)
- .NET 8 SDK


## Defining Constants in Godot

The following editor features are used to define the constants in `project.godot` extracted by this tool:

### Input Actions

Add input actions via:

**Project > Project Settings > Input Map**


### Collision Layers

Name your physics layers under:

**Project > Project Settings > General > Layer Names > 2D Physics / 3D Physics**


### Groups

Define reusable groups under:

**Project > Project Settings > Globals > Groups**


## License

MIT License
