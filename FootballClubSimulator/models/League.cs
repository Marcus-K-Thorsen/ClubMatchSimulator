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

    public List<Club>? UpperClubFraction
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
    
    // TODO skal lave metode der kan tage de to lister af runder og give leaguens teams deres standing ud fra rundernes udkom, listen af teams gives position, sorteres og gives tilbage
    public List<Club> CalculateTeamStandings()
    {
        // Reset alle leaguens standings
        // Gå gennem alle de første runder og tilskriv standings til leaguens teams ud fra rundernes matchoutcome
        // Opdel leaguens teams i upper og lower listerne ud fra deres points og derefter goal difference
        // Gå gennem alle den anden mængde runder og tilskriv stnadings til leaguens teams ud fra rundernes matchoutcome
        // sorter upper, lower og teams listerne ud fra deres standings og giv dem en position, som kan være delt
        // return teams listen
        return Teams;
    }
    
    // TODO skal lave en metode der kan lave to nye lister af runder ud fra leaguens teams og kalde på standard metoden CalculateTeamStandings() og returne listen af teams
    public List<Club> CalculateNewTeamStandings()
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
        // Til sidst returnes metoden CalculateTeamStandings(), der giver listen af teams der er blevet udført runder på og sorteret ud efter rundernes matchoutcome
        return CalculateTeamStandings();
    }

    // TODO få lavet metode i Club der kan resette den til på ny
    private void ResetTeamsStandings()
    {
        foreach (Club team in Teams)
        {
            // team.ResetStandings(); // Kalder metode i Club instancen der sætter dens standings til at være helt ny.
        }
    }
}