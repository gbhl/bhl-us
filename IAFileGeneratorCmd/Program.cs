using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Test.CommandLineParsing;

namespace MOBOT.IAFileGenerator.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandLineArguments a = new CommandLineArguments();
                CommandLineParser.ParseArguments(a, args);

                // Set up the generator
                IAFileGenerator.Generator generator = new Generator(
                    a.Identifier,
                    a.Collections,
                    a.ImageFile,
                    a.MetadataFile,
                    a.MarcFile,
                    a.OutputFolder,
                    a.AccessKey,
                    a.SecretKey);

                // Generate the metadata (use a separate thread so the app doesn't "freeze")
                Processor processor = new Processor();
                Thread thread = new Thread(processor.Generate);
                thread.IsBackground = true;
                thread.Start(generator);
                thread.Join();
            }
            catch (ArgumentException argException)
            {
                // Warn users of missing arguments
                Console.WriteLine(argException.Message);
            }

        }

    }

    public class Processor
    {
        private int _imagesProcessed = 0;

        /// <summary>
        /// Call the file generator to produce the files.  Report on the success or failure of the process.
        /// </summary>
        /// <param name="data">An instance of the file generator</param>
        public void Generate(object data)
        {
            Generator generator = (Generator)data;

            // Wire up the events so that we can track the progress of the file generation
            generator.DCMetadataStart += new DCMetadataStartEventHandler(this.DCMetadataStart);
            generator.DCMetadataEnd += new DCMetadataEndEventHandler(this.DCMetadataEnd);
            generator.UploadStart += new UploadStartEventHandler(this.UploadStart);
            generator.UploadEnd += new UploadEndEventHandler(this.UploadEnd);
            generator.ScandataStart += new ScandataStartEventHandler(this.ScandataStart);
            generator.ImageProcessed += new ImageProcessedEventHandler(this.ImageProcessed);
            generator.ScandataEnd += new ScandataEndEventHandler(this.ScandataEnd);

            // Generate the files
            if (generator.Generate())
            {
                Console.WriteLine("Success.  Metadata files created for '" + generator.Identifier + "' in '" + generator.OutputPath + "'");
            }
            else
            {
                Console.WriteLine("File generation FAILED.  View the log for details.");
            }
        }

        #region File generation event handlers

        public void DCMetadataStart(object sender, DCMetadataStartEventArgs e)
        {
            Console.WriteLine("Dublin Core file generation started.");
        }

        public void DCMetadataEnd(object sencer, DCMetadataEndEventArgs e)
        {
            Console.WriteLine("Dublin Core file generation complete.");
        }

        public void UploadStart(object sender, UploadStartEventArgs e)
        {
            Console.WriteLine("Metadata file generation started.");
        }

        public void UploadEnd(object sender, UploadEndEventArgs e)
        {
            Console.WriteLine("Metadata file generation complete.");
        }

        public void ScandataStart(object sender, ScandataStartEventArgs e)
        {
            Console.WriteLine("Scandata file generation started.");
        }

        public void ImageProcessed(object sender, ImageProcessedEventArgs e)
        {
            this._imagesProcessed++;
            Console.WriteLine(this._imagesProcessed.ToString() + " of " + e.MaxImages.ToString() + " images processed.");
        }

        public void ScandataEnd(object sender, ScandataEndEventArgs e)
        {
            Console.WriteLine("Scandata file generation complete.");
        }

        #endregion File generation event handlers

    }

    public class CommandLineArguments
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public List<string> Collections { get; set; }
        [Required]
        public string ImageFile { get; set; }
        [Required]
        public string MetadataFile { get; set; }
        [Required]
        public string MarcFile { get; set; }
        [Required]
        public string OutputFolder { get; set; }
        [Required]
        public string AccessKey { get; set; }
        [Required]
        public string SecretKey { get; set; }
    }
}
