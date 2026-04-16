namespace ResidualTheoryPlayground;

public class ModelTestResult
{
    public string Model { get; set; }
    public bool[,] ResidueMatrix { get; set; } = new bool[0, 0];
    public string Name { get; internal set; }
}
