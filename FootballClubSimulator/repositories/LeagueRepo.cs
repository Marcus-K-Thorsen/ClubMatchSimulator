using FootballClubSimulator.models;
using FootballClubSimulator.util;

namespace FootballClubSimulator.repositories;

public class LeagueRepo : StandardRepository<League>
{
    private readonly StandardRepository<Club> _clubRepo;
    private readonly RoundMasterRepo _roundRepo;
    
    public LeagueRepo() : base("league.csv", League.ConvertHeaderToCsvFormat)
    {
        _clubRepo = new ClubRepo();
        _roundRepo = new RoundMasterRepo();
    }

    public override List<League> ReadAll()
    {
        List<League> allLeagues = new List<League>();
        List<string[]> leagueStringRows = FileHandler.ReadCsvFile();
        foreach (string[] leagueStringRow in leagueStringRows)
        {
            string leagueName = leagueStringRow[0];
            List<Club> leagueTeams = new List<Club>();
            List<string[]> teamStringRows = _clubRepo.FileHandler.ReadCsvFile();
            int indexOfPositionOfLeagueNameInClubHeader = _clubRepo.GetHeaderIndexOfColumnName("LeagueName");
            foreach (string[] teamStringRow in teamStringRows) 
            {
                        string teamLeagueName = teamStringRow[indexOfPositionOfLeagueNameInClubHeader];
                        if (teamLeagueName == leagueName)
                        {
                            leagueTeams.Add(new Club(teamStringRow));
                        } 
            }

            List<Round[]> allTheFirstRounds = _roundRepo.ReadAllFirstRoundsForLeague(leagueName);
            
            List<Round[]> allTheSecondRounds = _roundRepo.ReadAllSecondRoundsForLeague(leagueName);
            
            League league = new League(leagueStringRow, leagueTeams, allTheFirstRounds, allTheSecondRounds);
            allLeagues.Add(league);
        }

        return allLeagues;
    }

    public override void WriteAll(List<League> leagues)
    {
        List<string> leagueStringRows = new List<string>();
        leagues.ForEach(league => leagueStringRows.Add(league.ConvertToCsvFormat()));
        FileHandler.WriteCsvFile(leagueStringRows);
        List<Club> allClubs = _clubRepo.ReadAll();
        foreach (League league in leagues)
        {
            List<Club> leagueTeams = league.Teams;
            foreach (Club team in leagueTeams)
            {
                string teamName = team.ClubName;
                bool isTeamNewClub = true;
                foreach (Club club in allClubs)
                {
                    string clubName = club.ClubName;
                    if (teamName == clubName)
                    {
                        int indexOfClub = allClubs.IndexOf(club);
                        allClubs.Insert(indexOfClub, team);
                        allClubs.Remove(club);
                        isTeamNewClub = false;
                        break;
                    }
                }
                if (isTeamNewClub) { allClubs.Add(team); }
            }
            _roundRepo.WriteAllRoundsForLeague(league);
        }
        _clubRepo.WriteAll(allClubs);
    }
}