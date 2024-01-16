using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]

public class BenchmarkPerformance
{
    [Params(100, 200)]
    public int N;

    string countries = null;
    int index, numberOfCharactersToExtract;

    [GlobalSetup]
    public void GlobalSetup()
    {
        countries = "India, USA, UK, Australia, Netherlands, Belgium";
        index = countries.LastIndexOf(",", StringComparison.Ordinal);
        numberOfCharactersToExtract = countries.Length - index;
    }

    [Benchmark]
    public void Substring()
    {
        for (int i = 0; i < N; i++)
        {
            var data = countries.Substring(index + 1, numberOfCharactersToExtract - 1);
        }
    }

    [Benchmark(Baseline = true)]
    public void Span()
    {
        for (int i = 0; i < N; i++)
        {
            var data = countries.AsSpan().Slice(index + 1, numberOfCharactersToExtract - 1);
        }
    }
}
