namespace ResidualTheoryPlayground;

public class MermaidParser
{
    public string[] GetBlocks(string diagramDefinition)
    {
        var parts = diagramDefinition.Split(['\n','\r'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length == 0)
            throw new System.FormatException();
        if (parts[0].Trim() != "graph TB")
            throw new System.FormatException();
        HashSet<string> result = new();
        Dictionary<string, string> names = new();
        foreach (var part in parts.Skip(1))
        {
            var items = part.Split("-->", StringSplitOptions.TrimEntries);
            foreach (var component in items)
            {
                if (component.Contains("["))
                {
                    var bracketIndex = component.IndexOf("[");
                    var closingBracketIndex = component.LastIndexOf("]");
                    var name = component[..bracketIndex].Trim();
                    var displayName = component.Substring(bracketIndex + 1, closingBracketIndex - bracketIndex - 1).Trim('"');
                    names[name] = displayName;
                    result.Add(displayName);
                }
                else
                {
                    if (names.TryGetValue(component, out var displayName))
                    {
                        result.Add(displayName);
                    }
                    else
                    {
                        result.Add(component);
                    }
                }
            }
        }
        return result.ToArray();
    }
}
