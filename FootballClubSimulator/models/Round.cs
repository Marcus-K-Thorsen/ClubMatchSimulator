namespace FootballClubSimulator.models;

public class Round
{

    // ___________________ STATIC PROPERTIES/FIELDS _________________________________
    private static readonly MatchSimulator MatchSimulator = new MatchSimulator();
    
    
    // ___________________ IMPORTANT PROPERTIES/FIELDS _________________________________
    private int _roundNumber;
    public int RoundNumber
    {
        get => _roundNumber;
        private set => _roundNumber = value;
    }
    

    private string _leagueName;
    public string LeagueName
    {
        get => _leagueName;
        private set => _leagueName = value;
    }

    private MatchOutcome _match;
    public MatchOutcome Match
    {
        get { return _match; }
        private set { _match = value ?? throw new ArgumentNullException(nameof(value)); }
    }
    
    

    // ___________________ CONSTRUCTORS _________________________________
    
    public Round(int roundNumber, League league, Club homeTeam, Club awayTeam)
    {
        RoundNumber = roundNumber;
        LeagueName = league.LeagueName;
        Match = MatchSimulator.SimulateOutcome(homeTeam, awayTeam);
    }
    
    
    
    // ___________________ CSV CONVERTER PROPERTIES/METHODS _________________________________
    public Round(string[] roundValues)
    {
        RoundNumber = Convert.ToInt32(roundValues[0]);
        LeagueName = roundValues[1];
        string homeTeamAbbreviated = roundValues[2];
        int homeTeamScore = Convert.ToInt32(roundValues[3]); 
        string awayTeamAbbreviated = roundValues[4];
        int awayTeamScore = Convert.ToInt32(roundValues[5]);
    
        Match = new MatchOutcome(homeTeamAbbreviated, homeTeamScore, awayTeamAbbreviated, awayTeamScore);
    }
    
    public static readonly string ConvertHeaderToCsvFormat = "RoundNumber,LeagueName,HomeTeamAbbreviated,HomeTeamGoals,AwayTeamAbbreviated,AwayTeamGoals";
    

    public string ConvertToCsvFormat()
    {
        return
            $"{RoundNumber},{LeagueName},{Match.HomeClubAbbreviated},{Match.HomeClubScore},{Match.AwayClubAbbreviated},{Match.AwayClubScore}";
    }
    
    
}