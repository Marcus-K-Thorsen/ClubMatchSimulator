using FootballClubSimulator.models;
using FootballClubSimulator.util;

namespace FootballClubSimulator.repositories;

public class ClubRepo : StandardRepository<Club>
{

    public ClubRepo():base("teams.csv", Club.ConvertHeaderToCsvFormat)
    {
    }

    public override List<Club> ReadAll()
    {
        List<Club> allClubs = new List<Club>();
        FileHandler.ReadCsvFile().ForEach(stringArrayOfClub => allClubs.Add(new Club(stringArrayOfClub)));
        return allClubs;
    }

    public override void WriteAll(List<Club> clubsToWrite)
    {
        List<string> clubRows = new List<string>();
        clubsToWrite.ForEach(club => clubRows.Add(club.ConvertToCsvFormat()));
        FileHandler.WriteCsvFile(clubRows);
    }
}