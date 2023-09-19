// See https://aka.ms/new-console-template for more information


using FootballClubSimulator.models;
using FootballClubSimulator.repositories;
using FootballClubSimulator.util;


League league1 = new League("Super Ligaen");
League league2 = new League("Nordic Bet Ligaen");
List<Club> teams = new List<Club>()
{
    new Club("Kjøbenhavns Boldklub", "KB", league1),
    new Club("Boldklubben Frem", "BF", league1),
    new Club("Boldklubben Dana", "BD", league1),
    new Club("Akademisk Boldklub", "AB", league1),
    new Club("Østerbro Boldklub", "ØB", league1),
    new Club("Boldklubben af 1893", "B 93", league1),
    new Club("Boldklubben Urania", "BU", league1),
    new Club("Boldklubben Lydia", "BL", league1),
    new Club("Boldklubben af 1899", "B 99", league1),
    new Club("Kristelig Forening for Unge Mænds Boldklub", "KFUM", league1),
    new Club("Boldklubben Sylvia", "BS", league1),
    new Club("Boldklubben Apollo", "BA", league1)
};
List<League> leagues = new List<League>()
{
    league1,
    league2
};

StandardRepository<League> leagueRepo = new LeagueRepo();
StandardRepository<Club> clubRepo = new ClubRepo();
clubRepo.WriteAll(teams);
//league1.CalculateNewTeamStandings();
leagueRepo.WriteAll(leagues);

//League leagueOne = leagueRepo.ReadAll()[0];

//leagueOne.CalculateFirstRoundsAllTeams().ForEach(team =>
//{ print($"Pos: '{team.PositionInTable}' - Name: '{team.ClubName}' - Matches: '{team.MatchesPlayed}' - Points: '{team.Points}' Goal Difference: '{team.GoalDifference}' - Goals For '{team.GoalsFor}' - Goals Against: '{team.GoalsAgainst}'"); } );

void print(string line)
{
    Console.WriteLine(line);
}
/*
MatchSimulator match = new MatchSimulator();
League league = new League("Some Exciting Lol League");

Club clubA = new Club("Gutterne", "AF", league);
Club clubB = new Club("Trunserne", "BF", league);



Console.WriteLine($"{clubA.ClubNameAbbreviated} {clubA.ClubName} Offense: '{clubA.Offense}' Defence: '{clubA.Defense}'");
Console.WriteLine("----------------------------------VS----------------------------------");
Console.WriteLine($"{clubB.ClubNameAbbreviated} {clubB.ClubName} Offense: '{clubB.Offense}' Defence: '{clubB.Defense}'");
Console.WriteLine();
MatchOutcome outcome1 = match.SimulateOutcome(clubA, clubB);
Console.WriteLine($"Away From Home: {outcome1.HomeClubAbbreviated} Score: '{outcome1.HomeClubScore}'");
Console.WriteLine($"At Home Turf: {outcome1.AwayClubAbbreviated} Score: '{outcome1.AwayClubScore}'");
Console.WriteLine();
MatchOutcome outcome2 = match.SimulateOutcome(clubB, clubA);
Console.WriteLine($"Away From Home: {outcome2.HomeClubAbbreviated} Score: '{outcome2.HomeClubScore}'");
Console.WriteLine($"At Home Turf: {outcome2.AwayClubAbbreviated} Score: '{outcome2.AwayClubScore}'");
*/
