// See https://aka.ms/new-console-template for more information

using FootballClubSimulator.models;

MatchSimulator match = new MatchSimulator();

Club clubA = new Club("AF Gutterne");
Club clubB = new Club("BFF Trunserne");

Console.WriteLine($"{clubA.ClubName} Offense: '{clubA.Offense}' Defence: '{clubA.Defense}'");
Console.WriteLine("----------------------------------VS----------------------------------");
Console.WriteLine($"{clubB.ClubName} Offense: '{clubB.Offense}' Defence: '{clubB.Defense}'");
Console.WriteLine();
MatchOutcome outcome = match.SimulateOutcome(clubA, clubB);
Console.WriteLine($"{outcome.FirstClub.ClubName} Score: '{outcome.FirstClubScore}'");
Console.WriteLine($"{outcome.SecondClub.ClubName} Score: '{outcome.SecondClubScore}'");