string path = args[0];
string filePattern = args[1];
string searchText = args[2];

#region checking arguments
string errorMessage = "";
bool error = false;

if (args.Length != 3)
{
    errorMessage = "Wrong Number of Arguments.";
    error = true;
}
if (!char.IsUpper(path[0]) || path[1] != ':' || (path[2] != '\\' && path[2] != '/'))
{
    errorMessage = $"Incorrect path \"{path}\".";
    error = true;
}
if (filePattern[0] != '*' || filePattern[1] != '.')
{
    errorMessage = "Incorrect file name pattern.";
    error = true;
}

if (error)
{
    Console.WriteLine($"{errorMessage} The program will end now.");
    Environment.Exit(0);
}

#endregion

var fileMatches = Directory.GetFiles(path, filePattern);
var searchTextMatches = new string[fileMatches.Length];

int summaryLines = 0;
int summaryOccurences = 0;

for (int i = 0; i < fileMatches.Length; i++)
{
    var fileLines = File.ReadAllLines(fileMatches[i]);
    int count = 1;
    foreach (string line in fileLines)
    {
        if (line.Contains(searchText))
        {
            if (!searchTextMatches.Contains(fileMatches[i]))
            {
                searchTextMatches[i] = fileMatches[i];
                Console.WriteLine(searchTextMatches[i]);
            }

            if (line.Contains(searchText))
            {
                Console.WriteLine($"    {count}: {line}");
                summaryLines++;
                for (int j = 0; j < line.Length - 5; j++)
                {
                    if (line.Substring(j, 5) == searchText)
                    {
                        summaryOccurences++;
                    }
                }
            }
        }
        count++;
    }
}
CalculateAndPrintSummary(summaryLines, summaryOccurences, searchTextMatches);

void CalculateAndPrintSummary(int lines, int occurences, string[] searchText)
{
    int files = searchText.Length;
    Console.WriteLine($"SUMMARY:\n    Number of found files: {files}\n    Number of found lines: {lines}\n    Number of occurences: {occurences}");
}

