namespace FootballClubSimulator.util;

public class FileHandler
{
    public string FilePath { private set; get; }
    public string FileHeader { private set; get; }

        // denne constructor skal du bare skrive navnet på den fil du vil læse fx "teams.csv"
        public FileHandler(string fileName, string header)
        {
            FilePath = Path.Combine($"{Environment.CurrentDirectory}\\..\\..\\..\\files", fileName);
            FileHeader = header;
        }


        // Denne metode læser en CSV fil, og giver os en arrayliste der indeholder et array af strings for hver række i CSV filen (dvs hver række splittes fra en string til et array, og puttes så i et nyt array der indeholder alle rows som hver deres arrays.
        public List<string[]> ReadCsvFile()
        {
            List<string[]> rows = new List<string[]>();

            bool skipFirstLine = true;

            try
            {
                // Step 1: Open the CSV file for reading using a StreamReader.
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    // Step 2: Read the file line by line until the end.
                    while (!reader.EndOfStream)
                    {

                        // Hver row giver en lang string fx "Jeg, elsker, is". Vi splitter den string vha commaerne.
                        string line = reader.ReadLine();

                        if (skipFirstLine)
                        {
                            skipFirstLine = false;
                            continue;
                        }

                        // Step 3: Split each line into an array of values using a comma as the delimiter.
                        string[] values = line.Split(',');

                        // Step 4: Add the array of values to the list of rows.
                        rows.Add(values);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // Handle the case where the file is not found.
                Console.WriteLine("File not found: " + FilePath);
            }
            catch (IOException ex)
            {
                // Handle other IO-related exceptions.
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

            // Step 5: Return the list of rows containing the CSV data.
            return rows;
        }

        public void WriteCsvFile(List<string> rows)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(FilePath))
                {
                    streamWriter.WriteLine(FileHeader);
                    foreach (string row in rows)
                    {
                        streamWriter.WriteLine(row);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to the file: " + ex.Message);
                Console.WriteLine($"Tried to write to the file at: '{FilePath}'");
            }
        }
        
    
}