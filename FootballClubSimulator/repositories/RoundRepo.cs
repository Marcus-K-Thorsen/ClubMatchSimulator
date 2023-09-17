using FootballClubSimulator.models;
using FootballClubSimulator.util;

namespace FootballClubSimulator.repositories;

public class RoundRepo : StandardRepository<Round>
{
    public RoundRepo(int roundNumber) : base($"round-{roundNumber}.csv", Round.ConvertHeaderToCsvFormat)
    {
    }

    public override List<Round> ReadAll()
    {
        List<Round> allRounds = new List<Round>();
        List<string[]> roundStringRows = FileHandler.ReadCsvFile();
        foreach (string[] roundStringRow in roundStringRows)
        {
            allRounds.Add(new Round(roundStringRow));
        }

        return allRounds;
    }

    public override void WriteAll(List<Round> rounds)
    {
        List<string> roundStringRows = new List<string>();
        rounds.ForEach(round => roundStringRows.Add(round.ConvertToCsvFormat()));
        FileHandler.WriteCsvFile(roundStringRows);
    }
}