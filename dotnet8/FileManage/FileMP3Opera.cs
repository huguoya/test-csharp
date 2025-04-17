using System;
using System.Collections.Generic;
using System.IO;

namespace TestApp.FileManage
{
    public class MP3
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MP3File
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Artists { get; set; }
        public string Album { get; set; }
    }

    public class FileMP3Opera
    {
        public void Main()
        {
            string path = @"G:\Video\音乐\音乐\";
            List<MP3> mp3List = GetMp3List();
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();
                int count = files.Length;
                int countp = 0;

                foreach (FileInfo file in files)
                {
                    countp++;
                    //if (countp % 10 == 0)
                    //{
                    //    Console.WriteLine(countp);
                    //}
                    //ShellClass sh = new ShellClass();
                    //Folder folder = sh.NameSpace(Path.GetDirectoryName(file.FullName));
                    //FolderItem folderItem = folder.ParseName(Path.GetFileName(file.FullName));
                    //string name = folder.GetDetailsOf(folderItem, 0);
                    //string title = folder.GetDetailsOf(folderItem, 21);
                    //string artists = folder.GetDetailsOf(folderItem, 13);
                    //string album = folder.GetDetailsOf(folderItem, 14);

                    //MP3File mp3File = new MP3File() { Name = name, Title = title, Artists = artists, Album = album };
                    //if (!string.IsNullOrEmpty(mp3File.Artists) && !string.IsNullOrEmpty(mp3File.Title))
                    //{
                    //    if (mp3File.Name.ToLower().Contains(mp3File.Artists.ToLower()) && mp3File.Name.ToLower().Contains(mp3File.Title.ToLower()))
                    //    {
                    //        mp3File.Name = mp3File.Artists + " - " + mp3File.Title + file.Extension;
                    //        if (File.Exists(path + @"Full\" + mp3File.Name))
                    //        {
                    //            mp3File.Name = mp3File.Artists + " - " + mp3File.Title + countp.ToString() + file.Extension;
                    //        }
                    //        file.MoveTo(path + @"Full\" + mp3File.Name);
                    //        Console.WriteLine("move to full: " + mp3File.Name);
                    //    }
                    //}
                    TagLib.File f = TagLib.File.Create(file.FullName);
                    if (f.Tag.Track > 0)
                    {
                        f.Tag.Track = 0;
                        f.Save();
                    }
                    if (file.Name.Contains("-"))
                    {
                        string[] ele = file.Name.Substring(0, file.Name.Length - file.Extension.Length).Split('-');
                        if (ele.Length == 2)
                        {
                            f.Tag.Performers = new string[] { ele[0].Trim() };
                            f.Tag.Title = ele[1].Trim();
                            f.Save();
                            string name = ele[0].Trim() + " - " + ele[1].Trim() + file.Extension;
                            file.MoveTo(path + @"Full3\" + name);
                            Console.WriteLine("filename:{0},Title:{1},Album：{2}", file.Name, f.Tag.Title, f.Tag.Album);
                        }
                        else if (ele.Length == 3)
                        {
                            f.Tag.Performers = new string[] { ele[0].Trim() };
                            f.Tag.Title = ele[1].Trim() + " - " + ele[2].Trim();
                            f.Save();
                            string name = ele[0].Trim() + " - " + ele[1].Trim() + " - " + ele[2].Trim() + file.Extension;
                            file.MoveTo(path + @"Full3\" + name);
                            Console.WriteLine("filename:{0}", file.Name);
                        }
                    }
                }
            }
            Console.WriteLine("over");
            Console.ReadLine();
        }

        public List<MP3> GetMp3List()
        {
            List<MP3> mp3List = new List<MP3>();
            mp3List.Add(new MP3() { Id = 0, Name = "Name" });//文件名
            //mp3List.Add(new MP3() { Id = 1, Name = "Size" });//文件大小
            //mp3List.Add(new MP3() { Id = 2, Name = "Type" });
            //mp3List.Add(new MP3() { Id = 3, Name = "Date modified" });
            //mp3List.Add(new MP3() { Id = 4, Name = "Date created" });
            //mp3List.Add(new MP3() { Id = 5, Name = "Date accessed" });
            //mp3List.Add(new MP3() { Id = 6, Name = "Attributes" });
            //mp3List.Add(new MP3() { Id = 7, Name = "Offline status" });
            //mp3List.Add(new MP3() { Id = 8, Name = "Offline availability" });
            //mp3List.Add(new MP3() { Id = 9, Name = "Perceived type" });
            //mp3List.Add(new MP3() { Id = 10, Name = "Owner" });
            //mp3List.Add(new MP3() { Id = 11, Name = "Kinds" });
            //mp3List.Add(new MP3() { Id = 12, Name = "Date taken" });
            mp3List.Add(new MP3() { Id = 13, Name = "Artists" });//参与艺术家
            mp3List.Add(new MP3() { Id = 14, Name = "Album" });//唱片集
            //mp3List.Add(new MP3() { Id = 15, Name = "Year" });
            //mp3List.Add(new MP3() { Id = 16, Name = "Genre" });
            //mp3List.Add(new MP3() { Id = 17, Name = "Conductors" });
            //mp3List.Add(new MP3() { Id = 18, Name = "Tags" });
            //mp3List.Add(new MP3() { Id = 19, Name = "Rating" });
            //mp3List.Add(new MP3() { Id = 20, Name = "Authors" });
            mp3List.Add(new MP3() { Id = 21, Name = "Title" });
            //mp3List.Add(new MP3() { Id = 22, Name = "Subject" });
            //mp3List.Add(new MP3() { Id = 23, Name = "Categories" });
            //mp3List.Add(new MP3() { Id = 24, Name = "Comments" });
            //mp3List.Add(new MP3() { Id = 25, Name = "Copyright" });
            //mp3List.Add(new MP3() { Id = 26, Name = "#" });
            //mp3List.Add(new MP3() { Id = 27, Name = "Length" });//时长
            //mp3List.Add(new MP3() { Id = 28, Name = "Bit rate" });
            //mp3List.Add(new MP3() { Id = 29, Name = "Protected" });
            //mp3List.Add(new MP3() { Id = 30, Name = "Camera model" });
            //mp3List.Add(new MP3() { Id = 31, Name = "Dimensions" });
            //mp3List.Add(new MP3() { Id = 32, Name = "Camera maker" });
            //mp3List.Add(new MP3() { Id = 33, Name = "Company" });
            //mp3List.Add(new MP3() { Id = 34, Name = "File description" });
            //mp3List.Add(new MP3() { Id = 35, Name = "Program name" });
            //mp3List.Add(new MP3() { Id = 36, Name = "Duration" });
            //mp3List.Add(new MP3() { Id = 37, Name = "Is online" });
            //mp3List.Add(new MP3() { Id = 38, Name = "Is recurring" });
            //mp3List.Add(new MP3() { Id = 39, Name = "Location" });
            //mp3List.Add(new MP3() { Id = 40, Name = "Optional attendee addresses" });
            //mp3List.Add(new MP3() { Id = 41, Name = "Optional attendees" });
            //mp3List.Add(new MP3() { Id = 42, Name = "Organizer address" });
            //mp3List.Add(new MP3() { Id = 43, Name = "Organizer name" });
            //mp3List.Add(new MP3() { Id = 44, Name = "Reminder time" });
            //mp3List.Add(new MP3() { Id = 45, Name = "Required attendee addresses" });
            //mp3List.Add(new MP3() { Id = 46, Name = "Required attendees" });
            //mp3List.Add(new MP3() { Id = 47, Name = "Resources" });
            //mp3List.Add(new MP3() { Id = 48, Name = "Free/busy status" });
            //mp3List.Add(new MP3() { Id = 49, Name = "Total size" });
            //mp3List.Add(new MP3() { Id = 50, Name = "Account name" });
            //mp3List.Add(new MP3() { Id = 51, Name = "Computer" });
            //mp3List.Add(new MP3() { Id = 52, Name = "Anniversary" });
            //mp3List.Add(new MP3() { Id = 53, Name = "Assistant's name" });
            //mp3List.Add(new MP3() { Id = 54, Name = "Assistant's phone" });
            //mp3List.Add(new MP3() { Id = 55, Name = "Birthday" });
            //mp3List.Add(new MP3() { Id = 56, Name = "Business address" });
            //mp3List.Add(new MP3() { Id = 57, Name = "Business city" });
            //mp3List.Add(new MP3() { Id = 58, Name = "Business country/region" });
            //mp3List.Add(new MP3() { Id = 59, Name = "Business P.O. box" });
            //mp3List.Add(new MP3() { Id = 60, Name = "Business postal code" });
            //mp3List.Add(new MP3() { Id = 61, Name = "Business state or province" });
            //mp3List.Add(new MP3() { Id = 62, Name = "Business street" });
            //mp3List.Add(new MP3() { Id = 63, Name = "Business fax" });
            //mp3List.Add(new MP3() { Id = 64, Name = "Business home page" });
            //mp3List.Add(new MP3() { Id = 65, Name = "Business phone" });
            //mp3List.Add(new MP3() { Id = 66, Name = "Callback number" });
            //mp3List.Add(new MP3() { Id = 67, Name = "Car phone" });
            //mp3List.Add(new MP3() { Id = 68, Name = "Children" });
            //mp3List.Add(new MP3() { Id = 69, Name = "Company main phone" });
            //mp3List.Add(new MP3() { Id = 70, Name = "Department" });
            //mp3List.Add(new MP3() { Id = 71, Name = "E-mail Address" });
            //mp3List.Add(new MP3() { Id = 72, Name = "E-mail2" });
            //mp3List.Add(new MP3() { Id = 73, Name = "E-mail3" });
            //mp3List.Add(new MP3() { Id = 74, Name = "E-mail list" });
            //mp3List.Add(new MP3() { Id = 75, Name = "E-mail display name" });
            //mp3List.Add(new MP3() { Id = 76, Name = "File as" });
            //mp3List.Add(new MP3() { Id = 77, Name = "First name" });
            //mp3List.Add(new MP3() { Id = 78, Name = "Full name" });
            //mp3List.Add(new MP3() { Id = 79, Name = "Gender" });
            //mp3List.Add(new MP3() { Id = 80, Name = "Given name" });
            //mp3List.Add(new MP3() { Id = 81, Name = "Hobbies" });
            //mp3List.Add(new MP3() { Id = 82, Name = "Home address" });
            //mp3List.Add(new MP3() { Id = 83, Name = "Home city" });
            //mp3List.Add(new MP3() { Id = 84, Name = "Home country/region" });
            //mp3List.Add(new MP3() { Id = 85, Name = "Home P.O. box" });
            //mp3List.Add(new MP3() { Id = 86, Name = "Home postal code" });
            return mp3List;
        }
    }
}