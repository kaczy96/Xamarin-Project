using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Service.Notification.NotificationListenerService;

namespace Project.DbHelper
{
    public class DbHelper : SQLiteOpenHelper
    {
        private static string DB_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static string DB_NAME = "MyDB.db";
        private static int VERSION = 1;
        private Context context;

        public DbHelper(Context context):base(context,DB_NAME, null,VERSION)
        {
            this.context = context;
        }

        private string GetSQLitePath()
        {
            return Path.Combine(DB_PATH, DB_NAME);
        }

        public override SQLiteDatabase WritableDatabase
        {
            get
            {
                return CreateSQLiteDB();
            }
        }

        private SQLiteDatabase CreateSQLiteDB()
        {
            SQLiteDatabase sqliteDB = null;
            string path = GetSQLitePath();
            Stream streamSQLite = null;
            FileStream streamWriter = null;
            Boolean isSQLiteInit = false;
            try
            {
                if (File.Exists(path))
                    isSQLiteInit = true;
                else
                {
                    streamSQLite = context.Resources.OpenRawResource(Resource.Raw.MyDB);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    if(streamSQLite != null && streamWriter != null)
                    {
                        if (CopySQLiteDB(streamSQLite, streamWriter))
                            isSQLiteInit = true;
                    }
                }
                if (isSQLiteInit)
                    sqliteDB = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadwrite);
            }

            catch
            {

            }
            return sqliteDB;
        }

        private bool CopySQLiteDB(Stream streamSQLite, FileStream streamWriter)
        {
            bool isSuccess = false;
            int length = 1024;
            Byte[] buffer = new byte[length];
            try
            {
                int bytesRead = streamSQLite.Read(buffer, 0, length);
                while(bytesRead > 0)
                {
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSQLite.Read(buffer, 0, length);
                }
                isSuccess = true;
            }
            catch
            {
            }
            finally
            {
                streamWriter.Close();
                streamSQLite.Close();
            }
            return isSuccess;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            
        }

        public void InsertScore(int score)
        {
            String query = $"INSERT INTO Ranking(Score) VALUES ({score})";
            SQLiteDatabase db = this.WritableDatabase;
            db.ExecSQL(query);

        }

        public List<Ranking> GetRanking()
        {
            List<Ranking> lstRanking = new List<Ranking>();
            SQLiteDatabase db = this.WritableDatabase;
            ICursor c;
            try
            {
                c = db.RawQuery("SELECT * FROM Ranking ORDER BY Score", null);
                if (c == null) return null;
                c.MoveToNext();
                do
                {
                    int Id = c.GetInt(c.GetColumnIndex("Id"));
                    int Score = c.GetInt(c.GetColumnIndex("Score"));
                    Ranking ranking = new Model.Ranking(Id, Score);
                    lstRanking.Add(ranking);

                }
                while (c.MoveToNext());
                c.Close();
            }
            catch { }
            db.Close();
            return lstRanking;
        }

        public List<Question> GetQuestionsMode(string mode)
        {
            List<Question> lstQuestion = new List<Question>();
        }
    }
}