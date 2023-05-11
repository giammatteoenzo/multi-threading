using System.Diagnostics;

string fileNameBase = "file_";
string fileExtension = ".txt";
int numFiles = 10;
int numChars = 100000;

void deleteAllFiles()
{
    for (int i = 0; i < numFiles; i++)
    {
        string filePath = "./files/" +  i.ToString() + fileExtension;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

deleteAllFiles();

Random rand = new Random();
Stopwatch stopwatch = new Stopwatch();

Console.WriteLine("Création de " + numFiles + " fichiers de " + numChars + " caractères sans paralélisation ...");

stopwatch.Start();

for (int i = 0; i < numFiles; i++)
{
    string filePath = "./files/" +  i.ToString() + fileExtension;
    
    string randomText = "";   
    for (int j = 0; j < numChars; j++)
    {
        randomText += (char)rand.Next(32, 127);
    }

    using (StreamWriter sw = File.CreateText(filePath))
    {
        sw.WriteLine(randomText);
    }
}

stopwatch.Stop();
Console.WriteLine("Création des fichiers sans paralélisation  terminée en " + stopwatch.ElapsedMilliseconds + " ms.");

deleteAllFiles();

Console.WriteLine("Création de " + numFiles + " fichiers de " + numChars + " caractères avec paralélisation ...");
stopwatch.Restart();
Parallel.For(0, numFiles, i =>
{
    string filePath = "./files/" + i.ToString() + fileExtension;
    string randomText = "";
    
    for (int j = 0; j < numChars; j++)
    {
        randomText += (char) rand.Next(32, 127);
    }

    using (StreamWriter sw = File.CreateText(filePath))
    {
        sw.WriteLine(randomText);
    }
});
stopwatch.Stop();
Console.WriteLine("Création des fichiers avec paralélisation  terminée en " + stopwatch.ElapsedMilliseconds + " ms.");

List<string> fileContents = new List<string>();

for (int i = 0; i < numFiles; i++)
{
    string filePath = "./files/" + i.ToString() + fileExtension;

    using (StreamReader reader = File.OpenText(filePath))
    {
        char[] buffer = new char[10000];
        reader.ReadBlock(buffer, 0, 10000);
        fileContents.Add(new string(buffer));
    }
}

// Sorting the lists using a heavy sorting algorithm 
void BubbleSort(char[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - 1 - i; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                char temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

stopwatch.Restart();

// Non-parallelized sorting
foreach (string content in fileContents)
{
    char[] charArray = content.ToCharArray();
    BubbleSort(charArray);
}

stopwatch.Stop();
Console.WriteLine("Tri sans paralélisation terminée en  " + stopwatch.ElapsedMilliseconds + " ms.");

stopwatch.Restart();

// Parallelized sorting
Parallel.ForEach(fileContents, (content) =>
{
    char[] charArray = content.ToCharArray();
    BubbleSort(charArray);
});

stopwatch.Stop();
Console.WriteLine("Tri avec paralélisation terminé en " + stopwatch.ElapsedMilliseconds + " ms.");

Console.ReadLine();