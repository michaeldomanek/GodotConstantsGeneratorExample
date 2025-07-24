# Godot Constants Generator

This tool automatically extracts input actions, collision layers, and group names from `project.godot` and generates strongly-typed C# constants:

- `Actions.cs` → Input actions
- `CollisionLayers.cs` → named collision layers
- `Groups.cs` → Global group names

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
- Godot Mono 4.4 (should work on all Godot 4.x versions)
- .NET 8 SDK


## Usage

### One-time generation

Run this command from the project root:

```bash
dotnet run --project GodotConstantsGenerator
```

### Automatic regeneration with watchexec

Run this to automatically regenerate when `project.godot` changes:

```bash
watchexec -w project.godot -- dotnet run --project GodotConstantsGenerator
```

Install `watchexec`: https://github.com/watchexec/watchexec

### JetBrains Rider

This project also includes Rider run configurations for both commands:  
`.run/GodotConstantsGenerator.run.xml`\
`.run/Watchexec project.godot.run.xml`


## Include in your project

- add the `GodotConstantsGenerator` project to your solution as a submodule and reference it in your main project
- add this to your main .csproj to ignore the folder in the IDE
```xml
<ItemGroup>
    <Compile Remove="GodotConstantsGenerator\**" />
</ItemGroup>
```
- update the output namespace in `GodotConstantsGenerator/Program.cs` to match your projects namespace
- if necessary also update the other constants like generated file path
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
