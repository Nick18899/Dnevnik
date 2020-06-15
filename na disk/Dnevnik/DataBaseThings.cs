using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;

namespace Dnevnik
{

    [Table(Name = "Lessons")]
    public class Lesson
    {
        [Column(IsPrimaryKey = true)]
        public int LessonID; //1-48
        [Column]
        public string Name;
        [Column]
        public string Notes;
    }
    [Table(Name = "HomeworkPoints")]
    public class HomeworkPoint
    {
        [Column(IsPrimaryKey = true)]
        public int HomeworkPointID; 
        [Column]
        public int LessonID;
        [Column]
        public string Description;
        [Column]
        public bool isDone;

    }
    class ConnectionWithDataBase
    {
        public DataContext db;
        public Table<Lesson> Lessons;
        public Table<HomeworkPoint> HomeworkPoints;
        static public int ChoosenLessonID;
        public ConnectionWithDataBase()
        {
            db = new DataContext("C:\\lessons\\lessonsdatabaseee.mdf");
            Lessons = db.GetTable<Lesson>();
            HomeworkPoints = db.GetTable<HomeworkPoint>();
        }
        public string[] GetAllLessons()
        {
            string[] result = new string[48];
            int index = 0;
            foreach(Lesson les in Lessons)
            {
                result[index] = les.Name;
                ++index;
            }
            return result;
        }

        public struct HomeworkPointInfo
        {
            public string Name;
            public bool IsDone;
        }
        public HomeworkPointInfo[] GetLessonHomeworkPoints()
        {
            HomeworkPointInfo[] result = new HomeworkPointInfo[5];
            int index = 0;
            foreach(HomeworkPoint hw in HomeworkPoints)
            {
                if(hw.LessonID == ChoosenLessonID)
                {
                    HomeworkPointInfo hwpi = new HomeworkPointInfo();
                    hwpi.Name = hw.Description;
                    hwpi.IsDone = hw.isDone;
                    result[index] = hwpi;
                    ++index;
                }
            }
            return result;
        }
        public string[] GetLessonHomeworkPointsNames()
        {
            string[] result = new string[5];
            int index = 0;
            foreach (HomeworkPoint hw in HomeworkPoints)
            {
                if (hw.LessonID == ChoosenLessonID)
                {
                    result[index] = hw.Description;
                    ++index;
                }
            }
            return result;
        }
        public string GetChoosenSubjectName()
        {
            foreach(Lesson les in Lessons)
            {
                if (les.LessonID == ChoosenLessonID)
                {
                    return les.Name;
                }
            }
            return "";
        }
        public void ChangeNote(string note)
        {
            foreach(Lesson les in Lessons)
            {
                if (les.LessonID == ChoosenLessonID)
                {
                    les.Notes = note;
                }
            }
            db.SubmitChanges();
        }
        public string GetNote()
        {
            foreach(Lesson les in Lessons)
            {
                if (les.LessonID == ChoosenLessonID)
                {
                    return les.Notes;
                }
            }
            return "";
        }
        private void DeleteHW(HomeworkPoint hw)
        {
            hw.Description = "";
            hw.isDone = false;
        }
        public void SubmitLessonsChanges(string[] changes)
        {
            int index = 0;
            foreach(Lesson les in Lessons)
            {
                if (les.Name != changes[index])
                {
                    foreach(HomeworkPoint hw in HomeworkPoints)
                    {
                        if (hw.LessonID == (index+1))
                        {
                            DeleteHW(hw);
                        }
                    }
                    les.Notes = "";
                }
                les.Name = changes[index];
                ++index;
            }
            db.SubmitChanges();
        }
        private string Crypt(string ToCrypt)
        {
            char[] tocrypt = ToCrypt.ToCharArray();
            for (int i = 0; i < tocrypt.Length; i++)
            {
                tocrypt[i] = (char)((int)tocrypt[i] + 5);
            }
            return tocrypt.ToString();
        }
        private string DeCrypt(string ToDeCrypt)
        {
            char[] todecrypt = ToDeCrypt.ToCharArray();
            for (int i = 0; i < todecrypt.Length; i++)
            {
                todecrypt[i] = (char)((int)todecrypt[i] - 5);
            }
            return todecrypt.ToString();
        }
        public void SubmitHomeworkPointChanges(HomeworkPointInfo[] hwpi) 
        {
            int hwpindex = (ChoosenLessonID - 1) * 5 + 1;
            int arrayindex = 0;
            foreach (HomeworkPoint hwp in HomeworkPoints)
            {
                if (hwp.HomeworkPointID == hwpindex)
                {
                    hwp.Description = hwpi[arrayindex].Name;
                    hwp.isDone = hwpi[arrayindex].IsDone;
                    ++arrayindex;
                }
                ++hwpindex;
            }

            db.SubmitChanges();
        }
    }

}
