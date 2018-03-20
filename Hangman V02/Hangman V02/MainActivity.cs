using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using System.IO;
using SQLite;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hangman_V02
{
    [Activity(Label = "HangMan", Theme = "@android:style/Theme.DeviceDefault.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        EditText txtPlayerName;
        Button Play;
        ListView lvHighScroes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource \\
            SetContentView(Resource.Layout.Welcome);

            txtPlayerName = FindViewById<EditText>(Resource.Id.txtPlayerName);
            Play = FindViewById<Button>(Resource.Id.btnPlay);
            lvHighScroes = FindViewById<ListView>(Resource.Id.lvHighScroes);

            Play.Click += Play_Click;

            string word;

            //Path to database \\
            string path = Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), "dbwords.db3");

            // Setup Connection \\
            var db = new SQLiteConnection(path);

            // Create Words Table \\
            db.CreateTable<Word>();

            var table = db.Table<Word>();

            // Check if word already exists \\
            if (table.Count() > 0)
            {
                // Do nothing \\
                return;
            }

            AssetManager assets = this.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("Words.txt")))
            {
                while ((word = sr.ReadLine()) != null)
                {
                    // Add word to the database \\
                    Word w = new Word();
                    w.hangmanword = word;
                    db.Insert(w);
                }
            }
            db.CreateTable<Scores>();
            // Scores to List View \\
            var scores = db.Table<Scores>().ToList();

            List<string> lscores = new List<string>();

            foreach (var score in scores)
            {
                string line;
                line = score.name + "-" + score.score;
                lscores.Add(line);

            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lscores);
            lvHighScroes.Adapter = adapter;

        }



        protected override void OnResume()
        {
            base.OnResume();

            //Path to database \\
            string path = Path.Combine(System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal), "dbwords.db3");

            // Setup Connection \\
            var db = new SQLiteConnection(path);


            db.CreateTable<Scores>();
            // Scores to List View \\
            var scores = db.Table<Scores>().ToList();

            List<string> lscores = new List<string>();

            foreach (var score in scores)
            {
                string line;
                line = score.name + "-" + score.score;
                lscores.Add(line);

            }

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lscores);
            lvHighScroes.Adapter = adapter;
        }


        private void Play_Click(object sender, System.EventArgs e)
        {
            // Player name must be used \\
            if (txtPlayerName.Text == "")
            {
                Toast.MakeText(this, "Please enter Player Name", ToastLength.Short).Show();
                return;
            }
            else Play.Enabled = true;

            // Open the next activity \\
            Intent nextActivity = new Intent(this, typeof(SecondActivity));
            nextActivity.PutExtra("name", txtPlayerName.Text);

            StartActivity(nextActivity);
        }
    }
}

