using System;
using System.Collections.Generic;
using System.Text;

namespace peinfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;


            foreach (var file in args)
            {
                Console.WriteLine();
                if (!System.IO.File.Exists(file))
                    Console.WriteLine("{0} does not exists");

                using (PEFile.PEFile peFile = new PEFile.PEFile(file))
                {
                    var header = peFile.Header;

                    Console.WriteLine("Information for file- {0}", file);
                    Console.WriteLine("---------------------------------------------------------");

                    Console.WriteLine("Architecture:    {0}", header.Machine.ToString());
                    Console.WriteLine("Build Date:      {0}", header.TimeDateStamp.ToString());
                    Console.WriteLine("IsManaged:       {0}", header.IsManaged.ToString());

                    string pdbFilePath = string.Empty;
                    Guid guid;
                    int age = 0;
                    peFile.GetPdbSignature(out pdbFilePath, out guid, out age);

                    Console.WriteLine("Pdb file Path:   {0}", pdbFilePath);
                    Console.WriteLine("Pdb file guid:   {0}", guid);

                    Console.WriteLine("\nHeader:");
                    Console.WriteLine("MajorOperatingSystemVersion:     {0}", header.MajorOperatingSystemVersion);
                    Console.WriteLine("MajorLinkerVersion:              {0}", header.MajorLinkerVersion);
                    Console.WriteLine("NumberOfSections:                {0}", header.NumberOfSections);
                    Console.WriteLine("NumberOfSymbols:                 {0}", header.NumberOfSymbols);
                    Console.WriteLine("SizeOfImage:                     0x{0:X}", header.SizeOfImage);
                    Console.WriteLine("SizeOfCode:                      0x{0:X}", header.SizeOfCode);
                }
            }
        }
    }
}
