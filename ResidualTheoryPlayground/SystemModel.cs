namespace ResidualTheoryPlayground;

public class SystemModel
{
    public string OriginalModel { get; set; } =
        """
        graph TB
            OR["Order Intake"] --> KQ["Kitchen Queue"]
            KQ-->PP["Payment Processing"]
            PP-->PF["Pickup Flow"]
        """;
    public string NewModel { get; set; } =
        """
        graph TB
            EM["Express menu"]-->O["Orders (limited)"]
            O-->DQ[Digital Queue]
            DQ-->DP[Dual Payment]
            CF[Cup forward]
        """;

    public List<string> PlannedStressors { get; set; } = new();

    public List<string> UnplannedStressors { get; set; } = new();

    public List<string> Components { get; set; } = new();

    public bool[,] ResidueMatrix { get; set; } = new bool[0, 0];
}
