using Markdig;

namespace MyStaticBlogBuilder
{
    /// <summary>
    /// Application for taking reading markdown content from /content/
    /// and generating html to /static-output/
    /// </summary>
    internal static class Program
    {
        /// <summary>Main entry point.</summary>
        static void Main(string[] args)
        {
            // currentFolderPath is expected to be /MyStaticBlog/ 
            //   (where the sln is at /MyStaticBlog/src/MyStaticBlog.sln)
            string currentFolderPath = Environment.CurrentDirectory;
            string sourceFolderPath = Path.Combine(currentFolderPath, "content");
            string targetFolderPath = Path.Combine(currentFolderPath, "static-output");

            Console.WriteLine("current folder: {0}", currentFolderPath);
            Console.WriteLine(" source folder: {0}", sourceFolderPath);
            Console.WriteLine(" target folder: {0}", targetFolderPath);

            string sourceFilePath = Path.Combine(sourceFolderPath, "index.md");
            string targetFilePath = Path.Combine(targetFolderPath, "index.html");
            if (!File.Exists(sourceFilePath)) throw new FileNotFoundException($"Unable to find file {sourceFilePath}!", sourceFilePath);

            string sourceContent = File.ReadAllText(sourceFilePath);
            string contentHtml = Markdown.ToHtml(sourceContent);
            string targetContent = @$"<!DOCTYPE html>
<html lang=""en"">
    <head>
        <meta charset=""UTF-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        <title>Static Website App</title>
    </head>
    <body>
        <main>
{contentHtml}
        </main>
    </body>
</html>";
            Console.WriteLine("writing to file {0}...", targetFilePath);
            targetFolderPath.EnsureDirectoryExists();
            File.WriteAllText(targetFilePath, targetContent);

            Console.WriteLine("all content successfully.");
        }

        /// <summary>
        /// Checks to see if a given folder path exists. If it does not
        /// exist it creates it.
        /// </summary>
        static void EnsureDirectoryExists(this string folderPath)
        {
            if (Directory.Exists(folderPath)) return;
            Console.WriteLine("- directory {0} not found, creating...", folderPath);
            Directory.CreateDirectory(folderPath);
        }
    }
}