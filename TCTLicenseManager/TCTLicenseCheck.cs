using System;
using System.Collections.Generic;
using System.IO;

namespace TCTLicenseManager
{
    class TCTLicenseCheck
    {
        private string filename = "";
        private List<string> fileNames = new List<string> {
            "sum",
            "lev",
            "cry",
            "stam",
            "bil",
            "rev"
        };

        private List<Int64> creationTimes = new List<Int64> {
            636042690421300000,
            636043691422300000,
            636042622423300000,
            636042698421300000,
            636010693424300000,
            636042624425300000
        };
        public TCTLicenseCheck()
        {
            filename = FindLicenseFile();
        }

        public bool IsLegal
        {
            get
            {
                // Check validity values:
                // - CreationTimeUTC expressed in ticks
                // - Filesize = 0
                if (filename != string.Empty) {
                    var fileInfo = new FileInfo(filename);
                    var lines = File.ReadAllLines(filename);


                    Int64 tickSpec = 636042698421300000;
                    //tickSpec += int.Parse(lines[2]);
                    //tickSpec += 10000 * int.Parse(lines[3]);
                    //tickSpec += 1000000 * int.Parse(lines[4]);
                    var dateTime = new DateTime(tickSpec);
                    //Int64 ticks = File.GetLastWriteTimeUtc(filename).Ticks;
                    //DateTime time = File.GetLastWriteTimeUtc(filename);
                    //636042698421300000
                    //636042698424310019
                    if (fileInfo.CreationTimeUtc.Ticks == tickSpec /*636042698424300000*/) { // Change this value before compiling!
                        return true;
                    }
                }
                return false;
            }
        }

        public string FileName
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        private bool CheckValidity()
        {
            throw new System.NotImplementedException();
        }

        private string GenerateFileName()
        {
            // Construct TCT.lic in a less obvious way
            string fn = "s";
            fn += 't';
            fn += "a";
            fn += "m";

            return fn;
        }

        private string FindLicenseFile()
        {
            string fn = GenerateFileName();

            DriveInfo[] drivelist = DriveInfo.GetDrives();
            foreach(DriveInfo drive in drivelist) {
                if(drive.DriveType==DriveType.Removable) {
                    if (File.Exists(drive.Name + fn))
                        return drive.Name + fn;
                }
            }
            return string.Empty;
        }

        public void CreateLicenseFile(int Version, int Revision, int Build)
        {
            //string fn = GenerateFileName();
            for (int i = 0; i < 6; i++) {
                string[] lines = { "TCT License File", "Issued: " + new DateTime(creationTimes[i]).ToUniversalTime() };
                Int64 tickSpec = 636042698421300000;
                //tickSpec += Build;
                //tickSpec += 10000 * Revision;
                //tickSpec+=  1000000 * Version;
                DateTime timeStamp = new DateTime(creationTimes[i]);

                DriveInfo[] drivelist = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drivelist) {
                    if (drive.DriveType == DriveType.Removable) {
                        using (StreamWriter outputFile = new StreamWriter(drive + fileNames[i])) {
                            foreach (string line in lines)
                                outputFile.WriteLine(line);
                        }

                        File.SetCreationTime(drive + fileNames[i], timeStamp);
                        File.SetLastWriteTime(drive + fileNames[i], timeStamp);
                    }
                }
            }

            //return (timeStamp.Ticks);
        }
    }
}
