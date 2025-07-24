using System.Text;

const string godotProjectPath = "project.godot";
const string outputNamespace = "GodotConstantsGeneratorExample.scripts.generated";
const string actionPath = "scripts/generated/Actions.cs";
const string layersPath = "scripts/generated/CollisionLayers.cs";
const string groupsPath = "scripts/generated/Groups.cs";

if (!File.Exists(godotProjectPath)) {
	Console.WriteLine("project.godot not found.");
	return;
}

string[] lines = File.ReadAllLines(godotProjectPath);

bool inInput = false;
bool inLayers = false;
bool inGroups = false;

var inputActions = new List<string>();
var collisionLayers = new Dictionary<string, int>();
var groups = new List<string>();

foreach (string line in lines) {
	string trimmed = line.Trim();

	switch (trimmed) {
		case "[input]":
			inInput = true;
			inLayers = false;
			inGroups = false;
			continue;
		case "[layer_names]":
			inInput = false;
			inLayers = true;
			inGroups = false;
			continue;
		case "[global_group]":
			inInput = false;
			inLayers = false;
			inGroups = true;
			continue;
	}

	if (trimmed.StartsWith('[')) {
		inInput = false;
		inLayers = false;
		inGroups = false;
		continue;
	}

	if (inInput && line.Contains('=')) {
		string name = line.Split('=')[0].Trim();
		if (!String.IsNullOrWhiteSpace(name))
			inputActions.Add(name);
	}

	if (inLayers && line.Contains("d_physics/layer_")) {
		string[] parts = line.Split('=');
		string layerStr = parts[0].Split('_')[2].Trim();
		string name = parts[1].Trim().Trim('"');

		if (Int32.TryParse(layerStr, out int layerIndex))
			collisionLayers[name] = layerIndex - 1;
	}

	if (inGroups && line.Contains('=')) {
		string group = line.Split('=')[0].Trim();
		if (!String.IsNullOrWhiteSpace(group))
			groups.Add(group);
	}
}

// Generate Actions.cs
var actionBuilder = new StringBuilder();
actionBuilder.AppendLine("using Godot;\n");
actionBuilder.AppendLine($"namespace {outputNamespace};\n");
actionBuilder.AppendLine("public static class Actions {");
foreach (string name in inputActions) {
	string sanitized = SanitizeName(name);
	actionBuilder.AppendLine($"\tpublic static readonly StringName {sanitized} = \"{name}\";");
}
actionBuilder.AppendLine("}");
File.WriteAllText(actionPath, actionBuilder.ToString());
Console.WriteLine($"Generated: {actionPath} with {inputActions.Count} Actions");

// Generate CollisionLayers.cs
var layersBuilder = new StringBuilder();
layersBuilder.AppendLine($"namespace {outputNamespace};\n");
layersBuilder.AppendLine("public static class CollisionLayers {");
foreach ((string name, int index) in collisionLayers) {
	string sanitized = SanitizeName(name);
	layersBuilder.AppendLine($"\tpublic const uint {sanitized} = 1 << {index};");
}
layersBuilder.AppendLine("}");
File.WriteAllText(layersPath, layersBuilder.ToString());
Console.WriteLine($"Generated: {layersPath} with {collisionLayers.Count} Layers");

// Generate Groups.cs
var groupsBuilder = new StringBuilder();
groupsBuilder.AppendLine("using Godot;\n");
groupsBuilder.AppendLine($"namespace {outputNamespace};\n");
groupsBuilder.AppendLine("public static class Groups {");
foreach (string name in groups) {
	string sanitized = SanitizeName(name);
	groupsBuilder.AppendLine($"\tpublic static readonly StringName {sanitized} = \"{name}\";");
}
groupsBuilder.AppendLine("}");
File.WriteAllText(groupsPath, groupsBuilder.ToString());
Console.WriteLine($"Generated: {groupsPath } with {groups.Count} Groups");
return;

// Helper: sanitize names for C# identifiers
static string SanitizeName(string raw) {
	if (String.IsNullOrEmpty(raw)) return "Unnamed";

	var sb = new StringBuilder();
	bool capitalizeNext = true;
	foreach (char c in raw) {
		if (Char.IsLetterOrDigit(c)) {
			sb.Append(capitalizeNext ? Char.ToUpper(c) : c);
			capitalizeNext = false;
		} else {
			capitalizeNext = true; // skip symbols/spaces/dashes
		}
	}

	string result = sb.ToString();
	if (Char.IsDigit(result[0])) result = "_" + result;
	return result;
}
