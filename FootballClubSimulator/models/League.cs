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
    
    public List<Round[]>? FirstRounds
    {
        get { return _firstRounds; }
        private set { _firstRounds = value ?? _firstRounds; }
    }
    
    private List<Round[]> _secondRounds = new List<Round[]>();
    
    public List<Round[]>? SecondRounds
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
    
    // TODO skal lave en metode der går gennem de første runder og returner listen af alle teams, men nu med deres standings sat ud efter de første runder
    public List<Club> CalculateFirstRoundsAllTeams()
    {
        // Kalder metoden CalculateTeamStandingsFirstRounds()
        // og returner listen af alle teams
        return Teams;
    }
    
    // TODO skal lave en metode der går gennem de anden runder og returner listen af upper teams, men nu med deres standings sat ud efter de anden runder
    public List<Club> CalculateSecondRoundsUpperTeams()
    {
        // Kalder metoden CalculateTeamStandingsFirstRounds()
        // Kalder metoden CalculateTeamStandingsSecondRounds()
        // og returner listen af alle upper teams
        return UpperClubFraction;
    }
    
    // TODO skal lave en metode der går gennem de anden runder og returner listen af lower teams, men nu med deres standings sat ud efter de anden runder
    public List<Club> CalculateSecondRoundsLowerTeams()
    {
        // Kalder metoden CalculateTeamStandingsFirstRounds()
        // Kalder metoden CalculateTeamStandingsSecondRounds()
        // og returner listen af alle lower teams
        return LowerClubFraction;
    }
    
    // TODO skal lave metode der kan tage listen af de første runder og give leaguens teams deres standing ud fra rundernes udkom, listen af teams sorteres og gives en position
    private void CalculateTeamStandingsFirstRounds()
    {
        // Reset alle leaguens standings
        // Gå gennem alle de første runder og tilskriv standings til leaguens teams ud fra rundernes matchoutcome
        // Sorter listen af alle Teams, og giv dem en position, som kan være delt
        
        
    }

    // TODO skal lave en metode der kan tage den anden omgang af runder og give leaguens upper og lower teams deres standings ud efter rundernes udkom og derefter sorterer de lister og giver dem en position, der kan være delt
    private void CalculateTeamStandingsSecondRounds()
    {
        // Kald metoden CalculateTeamStandingsFirstRounds()
        // Opdel leaguens teams i upper og lower listerne, så den første halvdel af alle teams kommer i upper og resten i lower
        // Gå gennem alle den anden mængde runder og tilskriv stadings til leaguens teams ud fra rundernes matchoutcome
        // sorter upper og lower listerne ud fra deres standings og giv dem en position, som kan være delt
    }
    
    // TODO skal lave en metode der kan lave to nye lister af runder ud fra leaguens teams
    public void CalculateNewTeamStandings()
    {
        // Reset leaguens runde lister, første og anden
        // For antalet af FirstAmountOfRounds skab en ny liste af nye runder ud fra leaguens teams
        //      For hvert team i Teams skal det team gå igennem Teams og skippe sig selv og skabe en runde mellem den og et andet team
        //          Skab en runde der lægges i en liste og tilskriv points og goal difference til de forskellige teams ud efter deres matchoutcome
        //      Lav listen af runder til et array og læg det array i listen af første runder
        // Split Teams i to, så dem med flest point ellers højst goal difference kommer i upper tier og resten i lower tier listen
        // For antalet af SecondAmountOfRounds skab en ny liste af nye runder ud fra leaguens upper og lower teams
        //      den første halvdel af SecondAmountOfRounds skab runder ud fra upper og den sidste halvdel skab runder ud fra lower
        //      For hvert team i upper/lower teams skal det team gå igennem upper/lower teams og skipper sig selv og skabe en runde mellem den og et andet team
        //          Skab en runde der lægges i en liste
        //      Lav listen af runder til et array or læg det array i listen af anden runder
    }

    
    private void ResetTeamsStandings()
    {
        foreach (Club team in Teams)
        {
            team.ResetStandings(); // Kalder metode i Club instancen der sætter dens standings til at være helt ny.
        }
    }
}