namespace ResidualTheoryPlayground;

public class SystemModel
{
    public ModelInfo[] Models =
        [
            new() 
            {
                Name = "Original system",
                Model = 
                """
                graph TB
                    OR["Order Intake"] --> KQ["Kitchen Queue"]
                    KQ-->PP["Payment Processing"]
                    PP-->PF["Pickup Flow"]
                """
            },
            new()
            {
                Name = "New system",
                Model =
                """
                graph TB
                    EM["Express menu"]-->O["Orders (limited)"]
                    O-->DQ[Digital Queue]
                    DQ-->DP[Dual Payment]
                    CF[Cup forward]
                """
            },
        ];

    private Dictionary<string, ModelTestResult> _testResults = new();

    public ModelTestResult GetModelTestResult(ModelInfo model)
    {
        if (_testResults.TryGetValue(model.Name, out var result))
        {
            return result;
        }
        else
        {
            var newResult = new ModelTestResult();
            var components = GetModelComponents(model);
            newResult.ResidueMatrix = new bool[PlannedStressors.Count + UnplannedStressors.Count, components.Length];
            _testResults[model.Name] = newResult;
            return newResult;
        }
    }

    public void ToggleResidue(ModelInfo model, int stressorIndex, int componentIndex)
    {
        var result = GetModelTestResult(model);
        result.ResidueMatrix[stressorIndex, componentIndex] = !result.ResidueMatrix[stressorIndex, componentIndex];
    }

    public List<string> PlannedStressors { get; set; } = 
        ["Payment down", "Internet fails", "Tablet breaks", 
        "Monday rush", "No-show", "Competition"];

    public List<string> UnplannedStressors { get; set; } = [
        "Health inspector",
        "TikTok viral"];

    public string[] GetModelComponents(ModelInfo model)
    {
        var parser = new MermaidParser();
        return parser.GetBlocks(model.Model);
    }

}
