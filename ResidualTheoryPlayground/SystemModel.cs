namespace ResidualTheoryPlayground;

public class SystemModel
{
    public ModelTestResult[] Models =
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

    public List<string> PlannedStressors { get; set; } = 
        ["Payment down", "Internet fails", "Tablet breaks", 
        "Monday rush", "No-show", "Competition"];

    public List<string> UnplannedStressors { get; set; } = [
        "Health inspector",
        "TikTok viral"];

    public string[] GetModelComponents(ModelTestResult model)
    {
        var parser = new MermaidParser();
        return parser.GetBlocks(model.Model);
    }

}
