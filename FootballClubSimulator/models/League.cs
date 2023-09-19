namespace FootballClubSimulator.models;

public class League
{
    
    // ___________________ STATIC PROPERTIES/FIELDS _________________________________
    public static readonly int FirstAmountOfRounds = 22;
    public static readonly int SecondAmountOfRounds = 10;
    
    
    
    // ___________________ IMPORTANT PROPERTIES/FIELDS _________________________________
    public string LeagueName { get; set; }
    
    public int ChampionsLeague { get; private set; }
    public int EuropeLeague { get; private set; }
    public int ConferenceLeague { get; private set; }
    public int Promotion { get; private set; }
    public int Relegation { get; private set; }

    private List<Club> _teams = new List<Club>();
    public List<Club> Teams
    {
        get { return _teams; }
        set
        {
            value.ForEach(team =>
            {
                bool foundNewLeagueTeam = true;
                int indexOfLeagueTeam = _teams.FindIndex(leagueTeam => leagueTeam.ClubNameAbbreviated == team.ClubNameAbbreviated);
                
                if (indexOfLeagueTeam >= 0)
                {
                    _teams.RemoveAt(indexOfLeagueTeam);
                    _teams.Insert(indexOfLeagueTeam, team);
                    foundNewLeagueTeam = false;
                }

                if (foundNewLeagueTeam)
                {
                    team.League = this;
                }
            });
        }
    }

    private List<Round[]> _firstRounds = new List<Round[]>();
    
    public List<Round[]> FirstRounds
    {
        get { return _firstRounds; }
        private set { _firstRounds = value ?? _firstRounds; }
    }
    
    private List<Round[]> _secondRounds = new List<Round[]>();
    
    public List<Round[]> SecondRounds
    { 
        get { return _secondRounds; } 
        private set { _secondRounds = value ?? _secondRounds; }
    }
    // ___________________ OTHER PROPERTIES/FIELDS _________________________________
    

    private List<Club> _upperClubFraction = new List<Club>();

    public List<Club> UpperClubFraction
    {
        get { return _upperClubFraction; }
        set { _upperClubFraction = value ?? _upperClubFraction; }
    }

    private List<Club> _lowerClubFraction = new List<Club>();

    public List<Club> LowerClubFraction
    {
        get { return _lowerClubFraction; }
        set { _lowerClubFraction = value ?? _lowerClubFraction; }
    }
    
    
    // ___________________ CONSTRUCTORS _________________________________
    public League(string leagueName)
    {
        LeagueName = leagueName;
        Teams = new List<Club>();
    }

    public League(string leagueName, List<Club> teams)
    {
        LeagueName = leagueName;
        Teams = teams;
    }

    
    
    // ___________________ CSV CONVERTER PROPERTIES/METHODS _________________________________
    
    public League(string[] leagueValues, List<Club> teams, List<Round[]> firstRounds, List<Round[]> secondRounds)
    {
        LeagueName = leagueValues[0];
        ChampionsLeague = Convert.ToInt32(leagueValues[1]);
        EuropeLeague = Convert.ToInt32(leagueValues[2]);
        ConferenceLeague = Convert.ToInt32(leagueValues[3]);
        Promotion = Convert.ToInt32(leagueValues[4]);
        Relegation = Convert.ToInt32(leagueValues[5]);
        Teams = teams;
        FirstRounds = firstRounds;
        SecondRounds = secondRounds;
    }
    
    public static string ConvertHeaderToCsvFormat = "LeagueName,ChampionsLeague,EuropeLeague,ConferenceLeague,Promotion,Relegation";
    public string ConvertToCsvFormat()
    {
        return $"{LeagueName},{ChampionsLeague},{EuropeLeague},{ConferenceLeague},{Promotion},{Relegation}";
    }
    
    // ___________________ SPECIAL METHODS _________________________________
    public List<Round[]> GetAllRoundsList()
    {
        List<Round[]> allRound = new List<Round[]>(_firstRounds);
        foreach (Round[] round in _secondRounds)
        {
            allRound.Add(round);
        }

        return allRound;
    }
    
    // en metode der går gennem de første runder og returner listen af alle teams, men nu med deres standings sat ud efter de første runder
    public List<Club> CalculateFirstRoundsAllTeams()
    {
        CalculateTeamStandingsFirstRounds();
        return Teams;
    }
    
    // en metode der går gennem de anden runder og returner listen af upper teams, men nu med deres standings sat ud efter de anden runder
    public List<Club> CalculateSecondRoundsUpperTeams()
    {
        CalculateTeamStandingsSecondRounds();
        return UpperClubFraction;
    }
    
    // en metode der går gennem de anden runder og returner listen af lower teams, men nu med deres standings sat ud efter de anden runder
    public List<Club> CalculateSecondRoundsLowerTeams()
    {
        CalculateTeamStandingsSecondRounds();
        return LowerClubFraction;
    }
    
    // en metode der kan tage listen af de første runder og give leaguens teams deres standing ud fra rundernes udkom, listen af teams sorteres og gives en position
    private void CalculateTeamStandingsFirstRounds()
    {
        // Reset alle leaguens teams' standings
        ResetTeamsStandings();
        // Gå gennem alle de første runder og tilskriv standings til leaguens teams ud fra rundernes matchoutcome
        foreach (Round[] rounds in FirstRounds)
        {
            GiveTeamsStandingsFromRounds(rounds);
        }
        
        // Sorter listen af alle Teams, og giv dem en position, som kan være delt
        SortTeamListByWinner(Teams);
        // Opdel leaguens teams i upper og lower listerne, så den første halvdel af alle teams kommer i upper og resten i lower
        SplitTeamsInUpperAndLower();
    }

    // en metode der kan tage den anden omgang af runder og give leaguens upper og lower teams deres standings ud efter rundernes udkom og derefter sorterer de lister og giver dem en position, der kan være delt
    private void CalculateTeamStandingsSecondRounds()
    {
        // Kald metoden CalculateTeamStandingsFirstRounds() for at sørge for at de første runder er blevet Calculated
        CalculateTeamStandingsFirstRounds();
        
        // Gå gennem alle den anden mængde runder og tilskriv standings til leaguens teams ud fra rundernes matchoutcome
        foreach (Round[] rounds in SecondRounds)
        {
            GiveTeamsStandingsFromRounds(rounds);
        }
        // sorter upper og lower listerne ud fra deres standings og giv dem en position, som kan være delt
        SortTeamListByWinner(UpperClubFraction);
        SortTeamListByWinner(LowerClubFraction);
    }

    private void SplitTeamsInUpperAndLower()
    {
        // Calculate the midpoint index
        int midpoint = Teams.Count / 2 + Teams.Count % 2;

        // Get the first half of the list
        UpperClubFraction = Teams.GetRange(0, midpoint);

        // Get the second half of the list
        LowerClubFraction = Teams.GetRange(midpoint, Teams.Count - midpoint);
    }
    
    // en metode der kan lave to nye lister af runder ud fra leaguens teams
    public void CalculateNewTeamStandings()
    {
        // For antalet af FirstAmountOfRounds skab en ny liste af nye runder ud fra leaguens teams
        List<Round[]> firstRounds = new List<Round[]>();
        for (int i = 1; i <= FirstAmountOfRounds; i++)
        {
            // For hvert team i Teams skal det team gå igennem Teams og skippe sig selv og skabe en runde mellem den og et andet team
            Round[] rounds = CreateRoundsFromTeams(Teams, i);
            
            // Lav listen af runder til et array og læg det array i listen af første runder
            firstRounds.Add(rounds);
        }
        FirstRounds = firstRounds;
        
        // Tilskriv Teams deres standings ud efter de lige skabte første runder
        CalculateTeamStandingsFirstRounds();

        List<Round[]> secondRounds = new List<Round[]>();
        // For antalet af SecondAmountOfRounds skab en ny liste af nye runder ud fra leaguens upper og lower teams
        for (int i = 1; i <= SecondAmountOfRounds; i++)
        {
            // den første halvdel af SecondAmountOfRounds skab runder ud fra upper og den sidste halvdel skab runder ud fra lower
            // For hvert team i upper/lower teams skal det team gå igennem upper/lower teams og skipper sig selv og skabe en runde mellem den og et andet team
            if (i <= SecondAmountOfRounds/2)
            {
                Round[] lowerRounds = CreateRoundsFromTeams(LowerClubFraction, i);
                secondRounds.Add(lowerRounds);
            }
            else
            {
                Round[] upperRounds = CreateRoundsFromTeams(UpperClubFraction, i);
                secondRounds.Add(upperRounds);
            }
        }
        SecondRounds = secondRounds;
    }

    private Round[] CreateRoundsFromTeams(List<Club> teams, int roundNumber)
    {
        // For hvert team i teams skal det team gå igennem Teams og skippe sig selv og skabe en runde mellem den og et andet team
        List<Round> rounds = new List<Round>();
        foreach (Club team1 in teams)
        {
            foreach (Club team2 in teams)
            {
                if (team1.ClubNameAbbreviated != team2.ClubNameAbbreviated)
                {
                    Round newRound = new Round(roundNumber, this, team1, team2);
                    rounds.Add(newRound);
                }
            }
        }

        return rounds.ToArray();
    }
    private void ResetTeamsStandings()
    {
        foreach (Club team in Teams)
        {
            team.ResetStandings(); // Kalder metode i Club instancen der sætter dens standings til at være helt ny.
        }
    }

    private void GiveTeamsStandingsFromRounds(Round[] rounds)
    { 
        for (int i = 0; i < rounds.Length; i++) 
        { 
            MatchOutcome roundOutcome = rounds[i].Match; 
            bool homeTeamExists = Teams.Exists(team => team.ClubNameAbbreviated == roundOutcome.HomeClubAbbreviated); 
            bool awayTeamExists = Teams.Exists(team => team.ClubNameAbbreviated == roundOutcome.AwayClubAbbreviated); 
            if (homeTeamExists && awayTeamExists) 
            { 
                Club homeTeam = Teams.Find(team => team.ClubNameAbbreviated == roundOutcome.HomeClubAbbreviated); 
                Club awayTeam = Teams.Find(team => team.ClubNameAbbreviated == roundOutcome.AwayClubAbbreviated);
                
                homeTeam.MatchesPlayed++; 
                awayTeam.MatchesPlayed++;
                
                int homeGoals = roundOutcome.HomeClubScore; 
                int awayGoals = roundOutcome.AwayClubScore;
                
                homeTeam.GamesWon += homeGoals > awayGoals ? 1 : 0; 
                homeTeam.GamesDrawn += homeGoals == awayGoals ? 1 : 0; 
                homeTeam.GamesLost += homeGoals < awayGoals ? 1 : 0;
                
                awayTeam.GamesWon += awayGoals > homeGoals ? 1 : 0; 
                awayTeam.GamesDrawn += awayGoals == homeGoals ? 1 : 0; 
                awayTeam.GamesLost += awayGoals < homeGoals ? 1 : 0;
                
                homeTeam.GoalsFor += homeGoals; 
                homeTeam.GoalsAgainst += awayGoals;
                
                awayTeam.GoalsFor += awayGoals; 
                awayTeam.GoalsAgainst += homeGoals; 
                if (homeGoals > awayGoals) 
                { 
                    homeTeam.Points += 3; 
                    awayTeam.Points += 0;
                } else if (awayGoals > homeGoals) 
                { 
                    awayTeam.Points += 3; 
                    homeTeam.Points += 0; 
                } else 
                { 
                    homeTeam.Points += 1; 
                    awayTeam.Points += 1;
                } 
            } else 
            { 
                ResetTeamsStandings(); 
                throw new Exception($"ERROR: League: '{this.LeagueName}' tried to find Home Team: '{roundOutcome.HomeClubAbbreviated}' and Away Team: '{roundOutcome.AwayClubAbbreviated}' within round number: '{rounds[i].RoundNumber}' of '{i}'.\nThe teams of the league did not contain one or both of those Teams, so it is not possible to calculate the team standings for the first rounds."); 
            } 
        }
    }

    private void SortTeamListByWinner(List<Club> teams)
    {
        // Sort alphabetically by club name
        teams.Sort((club1, club2) => club1.ClubName.CompareTo(club2.ClubName));
        
        // Sort by Goals Against (ascending)
        teams.Sort((club1, club2) => club1.GoalsAgainst.CompareTo(club2.GoalsAgainst));
        
        // Sort by Goals For (descending)
        teams.Sort((club1, club2) => club2.GoalsFor.CompareTo(club1.GoalsFor));
        
        // Sort by Goal Difference (descending)
        teams.Sort((club1, club2) => club2.GoalDifference.CompareTo(club1.GoalDifference));
        
        // Sort by Points (descending)
        teams.Sort((club1, club2) => club2.Points.CompareTo(club1.Points));
        
        
        Club pastTeam = null;
        
        for (int i = 1; i <= teams.Count; i++)
        {
            Club team = teams[i-1];
            if (pastTeam == null) { team.PositionInTable = i; }
            else
            {
                int pastTeamCollectedScore = pastTeam.Points + pastTeam.GoalDifference;
                int thisTeamCollectedScore = team.Points + team.GoalDifference;
                if (thisTeamCollectedScore == pastTeamCollectedScore) { team.PositionInTable = pastTeam.PositionInTable; }
                else { team.PositionInTable = i; }
            }
            pastTeam = team;
        }
    }
}