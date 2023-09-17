using FootballClubSimulator.models;

namespace FootballClubSimulator.repositories;

public class RoundMasterRepo
{
    private RoundRepo GetRoundRepoNumber(int roundNumber)
    {
        return new RoundRepo(roundNumber);
    }

    public List<Round[]> ReadAllFirstRoundsForLeague(string leagueName)
    {
        List<Round[]> allFirstRounds = new List<Round[]>();
        int amountOfFirstRounds = League.FirstAmountOfRounds;
        for (int i = 1; i <= amountOfFirstRounds; i++)
        {
            RoundRepo roundRepo = GetRoundRepoNumber(i);
            List<Round> allRounds = roundRepo.ReadAll();
            List<Round> foundLeagueRounds = new List<Round>();
            
            allRounds.ForEach(round =>
            { 
                if (round.LeagueName == leagueName) 
                { 
                    foundLeagueRounds.Add(round);
                }
            });
            
            allFirstRounds.Add(foundLeagueRounds.ToArray());
        }

        return allFirstRounds;
    }

    public List<Round[]> ReadAllSecondRoundsForLeague(string leagueName)
    {
        List<Round[]> allSecondRounds = new List<Round[]>();
        int amountOfLastRounds = League.FirstAmountOfRounds + League.SecondAmountOfRounds;
        int startingAmountFromFirstRounds = League.FirstAmountOfRounds + 1;
        for (int i = startingAmountFromFirstRounds; i <= amountOfLastRounds; i++)
        {
            RoundRepo roundRepo = GetRoundRepoNumber(i);
            List<Round> allRounds = roundRepo.ReadAll();
            List<Round> foundLeagueRounds = new List<Round>();
            allRounds.ForEach(round =>
            {
                if (round.LeagueName == leagueName)
                {
                    foundLeagueRounds.Add(round);
                }
            });
            allSecondRounds.Add(foundLeagueRounds.ToArray());
        }
        return allSecondRounds;
    }

    private List<Round[]> ReadAllRounds()
    {
        List<Round[]> allRounds = new List<Round[]>();
        int amountOfRounds = League.FirstAmountOfRounds + League.SecondAmountOfRounds;
        for (int i = 1; i <= amountOfRounds; i++)
        {
            RoundRepo roundRepo = GetRoundRepoNumber(i);
            Round[] rounds = roundRepo.ReadAll().ToArray();
            allRounds.Add(rounds);
        }

        return allRounds;
    }

    public void WriteAllRounds(List<Round[]> allRounds)
    {
        for (var i = 1; i <= allRounds.Count; i++)
        {
            RoundRepo roundRepo = GetRoundRepoNumber(i);
            Round[] allRoundsAtI = allRounds[i - 1];
            roundRepo.WriteAll(new List<Round>(allRoundsAtI));
        }
    }

    public void WriteAllRoundsForLeague(League league)
    {
        List<Round[]> allRounds = ReadAllRounds();
        List<Round[]> leagueRounds = league.GetAllRoundsList();
        List<Round[]> newRoundsList = new List<Round[]>();
        for (int i = 1; i <= allRounds.Count; i++)
        {
            List<Round> allRoundsAtI = new List<Round>(allRounds[i - 1]);
            foreach (Round round in allRoundsAtI)
            {
                if (round.LeagueName == league.LeagueName)
                {
                    allRoundsAtI.Remove(round);
                }
            }

            if (league.GetAllRoundsList().Count > 0)
            { 
                List<Round> allLeagueRoundsAtI = new List<Round>(leagueRounds[i - 1]); 
                foreach (Round leagueRound in allLeagueRoundsAtI) 
                { 
                    allRoundsAtI.Add(leagueRound);
                }
            }
            
            newRoundsList.Add(allRoundsAtI.ToArray());
        }
        WriteAllRounds(newRoundsList);
    }
}