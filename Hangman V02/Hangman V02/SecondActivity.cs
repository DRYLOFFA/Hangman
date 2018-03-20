using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Hangman_V02
{
    [Activity(Label = "SecondActivity", Theme = "@android:style/Theme.DeviceDefault.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SecondActivity : Activity
    {
        Button A;
        Button B;
        Button C;
        Button D;
        Button E;
        Button F;
        Button G;
        Button H;
        Button I;
        Button J;
        Button K;
        Button L;
        Button M;
        Button N;
        Button O;
        Button P;
        Button Q;
        Button R;
        Button S;
        Button T;
        Button U;
        Button V;
        Button W;
        Button X;
        Button Y;
        Button Z;

        Button PlayAgain;
        Button Back;
        ImageView imageHangman;
        TextView textView1;
        TextView playername;
        TextView lblScore;

        int score = 500;

        string wordToGuess = "Banana";

        string wordToGuessUppercase; 

        StringBuilder displayToPlayer; 

        List<char> correctGuesses = new List<char>();
        List<char> incorrectGuesses = new List<char>();

        int lives = 6;
        bool won = false;
        int lettersRevealed = 0;

        char guess;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here \\
            SetContentView(Resource.Layout.Game);

            A = FindViewById<Button>(Resource.Id.btnA);
            B = FindViewById<Button>(Resource.Id.btnB);
            C = FindViewById<Button>(Resource.Id.btnC);
            D = FindViewById<Button>(Resource.Id.btnD);
            E = FindViewById<Button>(Resource.Id.btnE);
            F = FindViewById<Button>(Resource.Id.btnF);
            G = FindViewById<Button>(Resource.Id.btnG);
            H = FindViewById<Button>(Resource.Id.btnH);
            I = FindViewById<Button>(Resource.Id.btnI);
            J = FindViewById<Button>(Resource.Id.btnJ);
            K = FindViewById<Button>(Resource.Id.btnK);
            L = FindViewById<Button>(Resource.Id.btnL);
            M = FindViewById<Button>(Resource.Id.btnM);
            N = FindViewById<Button>(Resource.Id.btnN);
            O = FindViewById<Button>(Resource.Id.btnO);
            P = FindViewById<Button>(Resource.Id.btnP);
            Q = FindViewById<Button>(Resource.Id.btnQ);
            R = FindViewById<Button>(Resource.Id.btnR);
            S = FindViewById<Button>(Resource.Id.btnS);
            T = FindViewById<Button>(Resource.Id.btnT);
            U = FindViewById<Button>(Resource.Id.btnU);
            V = FindViewById<Button>(Resource.Id.btnV);
            W = FindViewById<Button>(Resource.Id.btnW);
            X = FindViewById<Button>(Resource.Id.btnX);
            Y = FindViewById<Button>(Resource.Id.btnY);
            Z = FindViewById<Button>(Resource.Id.btnZ);

            PlayAgain = FindViewById<Button>(Resource.Id.btnPlayAgain);
            Back = FindViewById<Button>(Resource.Id.btnBack);
            PlayAgain.Visibility = ViewStates.Invisible;
            Back.Visibility = ViewStates.Invisible;

            imageHangman = FindViewById<ImageView>(Resource.Id.imageHangman);

            A.Click += A_Click;
            B.Click += B_Click;
            C.Click += C_Click;
            D.Click += D_Click;
            E.Click += E_Click;
            F.Click += F_Click;
            G.Click += G_Click;
            H.Click += H_Click;
            I.Click += I_Click;
            J.Click += J_Click;
            K.Click += K_Click;
            L.Click += L_Click;
            M.Click += M_Click;
            N.Click += N_Click;
            O.Click += O_Click;
            P.Click += P_Click;
            Q.Click += Q_Click;
            R.Click += R_Click;
            S.Click += S_Click;
            T.Click += T_Click;
            U.Click += U_Click;
            V.Click += V_Click;
            W.Click += W_Click;
            X.Click += X_Click;
            Y.Click += Y_Click;
            Z.Click += Z_Click;

            PlayAgain.Click += PlayAgain_Click;
            Back.Click += Back_Click;

            playername = FindViewById<TextView>(Resource.Id.lblName);
            lblScore = FindViewById<TextView>(Resource.Id.lblScore);
            string name = Intent.GetStringExtra ("name");
            //string score = Intent.GetStringExtra("score");
            playername.Text = "" + name;
            lblScore.Text = "" + score;


            // Generate a random word \\
            string path = Path.Combine(System.Environment.GetFolderPath
            (System.Environment.SpecialFolder.Personal), "dbwords.db3");
            // Get a random word \\
            var db = new SQLiteConnection(path);
            var table = db.Table<Word>().ToList();

            Random r = new Random();
            int num = r.Next(1, table.Count());

            wordToGuess = table.ElementAt(num).hangmanword;

            displayToPlayer = new StringBuilder(wordToGuess.Length);
            wordToGuessUppercase = wordToGuess.ToUpper();

            for (int i = 0; i < wordToGuess.Length; i++)
                displayToPlayer.Append('-');

            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            textView1.Text = displayToPlayer.ToString();

        }
        public void SaveScore()
        {
            string path = Path.Combine(System.Environment.GetFolderPath
            (System.Environment.SpecialFolder.Personal), "dbwords.db3");

            // setup connection \\
            var db = new SQLiteConnection(path);

            // create Scores table \\
            db.CreateTable<Scores>();

           // string playername = "test";
            Scores s = new Scores();

            s.name = playername.Text;
            s.score = score;

            db.Insert(s);
        }

        public void Scoreloose()
        {
            score = Convert.ToInt16(lblScore.Text);
            score = score - 50;
            lblScore.Text = score.ToString();
        }

        public void Scorewin()
        {
            score = Convert.ToInt16(lblScore.Text);
            score = score + 500;
            lblScore.Text = score.ToString();
        }
        private void Back_Click(object sender, EventArgs e)
        {
            // Goes back to Welcome Screen \\
            Intent nextActivity = new Intent(this, typeof(MainActivity));
            SaveScore();
            StartActivity(nextActivity);
        }

        private void PlayAgain_Click(object sender, EventArgs e)
        {
            // Keep score and load game with new word enable correct buttons \\
            won = false;
            lives = 6;
            lettersRevealed = 0;

            imageHangman.SetImageResource(Resource.Drawable.Hangman1);
            PlayAgain.Visibility = ViewStates.Invisible;
            A.Enabled = true;
            B.Enabled = true;
            C.Enabled = true;
            D.Enabled = true;
            E.Enabled = true;
            F.Enabled = true;
            G.Enabled = true;
            H.Enabled = true;
            I.Enabled = true;
            J.Enabled = true;
            K.Enabled = true;
            L.Enabled = true;
            M.Enabled = true;
            N.Enabled = true;
            O.Enabled = true;
            P.Enabled = true;
            Q.Enabled = true;
            R.Enabled = true;
            S.Enabled = true;
            T.Enabled = true;
            U.Enabled = true;
            V.Enabled = true;
            W.Enabled = true;
            X.Enabled = true;
            Y.Enabled = true;
            Z.Enabled = true;
            // Generate a random word \\
            string path = Path.Combine(System.Environment.GetFolderPath
            (System.Environment.SpecialFolder.Personal), "dbwords.db3");
            // Get a random word \\
            var db = new SQLiteConnection(path);
            var table = db.Table<Word>().ToList();

            Random r = new Random();
            int num = r.Next(1, table.Count());

            wordToGuess = table.ElementAt(num).hangmanword;

            displayToPlayer = new StringBuilder(wordToGuess.Length);
            wordToGuessUppercase = wordToGuess.ToUpper();

            for (int i = 0; i < wordToGuess.Length; i++)
                displayToPlayer.Append('-');

            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            textView1.Text = displayToPlayer.ToString();
        }

        private void Z_Click(object sender, EventArgs e)
        {
            guess = 'Z';
            Z.Enabled = false;
            Guess();
        }

        private void Y_Click(object sender, EventArgs e)
        {
            guess = 'Y';
            Y.Enabled = false;
            Guess();
        }

        private void X_Click(object sender, EventArgs e)
        {
            guess = 'X';
            X.Enabled = false;
            Guess();
        }

        private void W_Click(object sender, EventArgs e)
        {
            guess = 'W';
            W.Enabled = false;
            Guess();
        }

        private void V_Click(object sender, EventArgs e)
        {
            guess = 'V';
            V.Enabled = false;
            Guess();
        }

        private void U_Click(object sender, EventArgs e)
        {
            guess = 'U';
            U.Enabled = false;
            Guess();
        }

        private void T_Click(object sender, EventArgs e)
        {
            guess = 'T';
            T.Enabled = false;
            Guess();
        }

        private void S_Click(object sender, EventArgs e)
        {
            guess = 'S';
            S.Enabled = false;
            Guess();
        }

        private void R_Click(object sender, EventArgs e)
        {
            guess = 'R';
            R.Enabled = false;
            Guess();
        }

        private void Q_Click(object sender, EventArgs e)
        {
            guess = 'Q';
            Q.Enabled = false;
            Guess();
        }

        private void P_Click(object sender, EventArgs e)
        {
            guess = 'P';
            P.Enabled = false;
            Guess();
        }

        private void O_Click(object sender, EventArgs e)
        {
            guess = 'O';
            O.Enabled = false;
            Guess();
        }

        private void N_Click(object sender, EventArgs e)
        {
            guess = 'N';
            N.Enabled = false;
            Guess();
        }

        private void M_Click(object sender, EventArgs e)
        {
            guess = 'M';
            M.Enabled = false;
            Guess();
        }

        private void L_Click(object sender, EventArgs e)
        {
            guess = 'L';
            L.Enabled = false;
            Guess();
        }

        private void K_Click(object sender, EventArgs e)
        {
            guess = 'K';
            K.Enabled = false;
            Guess();
        }

        private void J_Click(object sender, EventArgs e)
        {
            guess = 'J';
            J.Enabled = false;
            Guess();
        }

        private void I_Click(object sender, EventArgs e)
        {
            guess = 'I';
            I.Enabled = false;
            Guess();
        }

        private void H_Click(object sender, EventArgs e)
        {
            guess = 'H';
            H.Enabled = false;
            Guess();
        }

        private void G_Click(object sender, EventArgs e)
        {
            guess = 'G';
            G.Enabled = false;
            Guess();
        }

        private void F_Click(object sender, EventArgs e)
        {
            guess = 'F';
            F.Enabled = false;
            Guess();
        }

        private void E_Click(object sender, EventArgs e)
        {
            guess = 'E';
            E.Enabled = false;
            Guess();
        }

        private void D_Click(object sender, EventArgs e)
        {
            guess = 'D';
            D.Enabled = false;
            Guess();
        }

        private void C_Click(object sender, EventArgs e)
        {
            guess = 'C';
            C.Enabled = false;
            Guess();
        }

        private void B_Click(object sender, EventArgs e)
        {
            guess = 'B';
            B.Enabled = false;
            Guess();
        }

        private void A_Click(object sender, EventArgs e)
        {
            guess = 'A';
            A.Enabled = false;
            Guess();
        }
        private void Guess()
        {
            if (!won && lives > 0)
            {
                if (wordToGuessUppercase.Contains(guess))
                {
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuessUppercase[i] == guess)
                        {
                            displayToPlayer[i] = wordToGuess[i];
                            lettersRevealed++;
                            textView1.Text = displayToPlayer.ToString();
                        }
                    }
                    if (lettersRevealed == wordToGuess.Length)
                        won = true;
                }
                else
                {
                    Toast.MakeText(this, "Nope, there's no " + guess + " in there" , ToastLength.Short).Show();
                    lives--;
                    Scoreloose();

                    if (lives == 5)
                    {
                        imageHangman.SetImageResource(Resource.Drawable.Hangman2);
                    }
                    if (lives == 4)
                    {
                        imageHangman.SetImageResource(Resource.Drawable.Hangman3);
                    }
                    if (lives == 3)
                    {
                        imageHangman.SetImageResource(Resource.Drawable.Hangman4);
                    }
                    if (lives == 2)
                    {
                        imageHangman.SetImageResource(Resource.Drawable.Hangman5);
                    }
                    if (lives == 1)
                    {
                        imageHangman.SetImageResource(Resource.Drawable.Hangman6);
                    }
                    if (lives == 0)
                    {
                        imageHangman.SetImageResource(Resource.Drawable.Hangman7);
                        A.Enabled = false;
                        B.Enabled = false;
                        C.Enabled = false;
                        D.Enabled = false;
                        E.Enabled = false;
                        F.Enabled = false;
                        G.Enabled = false;
                        H.Enabled = false;
                        I.Enabled = false;
                        J.Enabled = false;
                        K.Enabled = false;
                        L.Enabled = false;
                        M.Enabled = false;
                        N.Enabled = false;
                        O.Enabled = false;
                        P.Enabled = false;
                        Q.Enabled = false;
                        R.Enabled = false;
                        S.Enabled = false;
                        T.Enabled = false;
                        U.Enabled = false;
                        V.Enabled = false;
                        W.Enabled = false;
                        X.Enabled = false;
                        Y.Enabled = false;
                        Z.Enabled = false;
                        Toast.MakeText(this, "You lost! The word was " + wordToGuess, ToastLength.Short).Show();
                        Back.Visibility = ViewStates.Visible;
                    }
                }
            }

            if (won)
            {
                Toast.MakeText(this, "You Won!", ToastLength.Short).Show();
                Scorewin();
                A.Enabled = false;
                B.Enabled = false;
                C.Enabled = false;
                D.Enabled = false;
                E.Enabled = false;
                F.Enabled = false;
                G.Enabled = false;
                H.Enabled = false;
                I.Enabled = false;
                J.Enabled = false;
                K.Enabled = false;
                L.Enabled = false;
                M.Enabled = false;
                N.Enabled = false;
                O.Enabled = false;
                P.Enabled = false;
                Q.Enabled = false;
                R.Enabled = false;
                S.Enabled = false;
                T.Enabled = false;
                U.Enabled = false;
                V.Enabled = false;
                W.Enabled = false;
                X.Enabled = false;
                Y.Enabled = false;
                Z.Enabled = false;
                PlayAgain.Visibility = ViewStates.Visible;
            }
        }
    }
}