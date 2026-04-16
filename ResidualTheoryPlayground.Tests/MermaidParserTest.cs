namespace ResidualTheoryPlayground.Tests;

[TestClass]
public sealed class MermaidParserTest
{
    [TestMethod]
    public void InvalidDiagramThrowError()
    {
        var parser = new MermaidParser();
        var invalidDiagram = "graph TB\nA-->B\nInvalidLine\nC-->D";
        Assert.Throws<FormatException>(() => parser.GetBlocks(invalidDiagram));
    }
    [TestMethod]
    public void MissingHeaderDiagramThrowError()
    {
        var parser = new MermaidParser();
        var invalidDiagram = "A-->B\nC-->D";
        Assert.Throws<FormatException>(() => parser.GetBlocks(invalidDiagram));
    }
    [TestMethod]
    public void EmptyDiagramThrowError()
    {
        var parser = new MermaidParser();
        var invalidDiagram = "";
        Assert.Throws<FormatException>(() => parser.GetBlocks(invalidDiagram));
    }

    [TestMethod]
    public void ParseSimpleUnconnectedDiagram()
    {
        var parser = new MermaidParser();
        var invalidDiagram = "graph TB\nA-->B\nC-->D";

        var components = parser.GetBlocks(invalidDiagram);

        CollectionAssert.AreEquivalent(new string[] { "A", "B", "C", "D" }, components);
    }

    [TestMethod]
    public void ParseSimpleConnectedDiagram()
    {
        var parser = new MermaidParser();
        var invalidDiagram = "graph TB\nA-->B\nC-->C";

        var components = parser.GetBlocks(invalidDiagram);

        CollectionAssert.AreEquivalent(new string[] { "A", "B", "C" }, components);
    }

    [TestMethod]
    public void ParseNamedNodesConnectedDiagram()
    {
        var parser = new MermaidParser();
        var invalidDiagram =
            """
            graph TB
            A[Name]-->B
            B-->C["Name with quotes"]
            """;

        var components = parser.GetBlocks(invalidDiagram);

        CollectionAssert.AreEquivalent(new string[] { "Name", "B", "Name with quotes" }, components);
    }
}
