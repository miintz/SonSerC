using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

using Tesseract;
using System.Text.RegularExpressions;

namespace SonSerC
{
    class Program
    {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        static string TrackRegex = "spotify:track:([a-zA-Z0-9]{22})";
        static string AlbumRegex = "spotify:album:([a-zA-Z0-9]{22})";

        static void Main(string[] args)
        {                   
            //negeer alle args vanaf 2
            try
            {
                string arg = args[0];                

                switch (arg)
                { 
                    case "play":
                    case "pause":
                        FocusSpotify(false);
                        PausePlaySpotify();                    
                        break;
                    case "start":
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Spotify\\Spotify.exe";
                        Process.Start(path); //start spotify hiero, werkt alleen voor standaard pad...
                        System.Threading.Thread.Sleep(1000);

                        FocusSpotify();
                        break;
                    default:
                        //mogelijk een trackid/albumid ofzo

                        Match Album = Regex.Match(arg, AlbumRegex);
                        Match Track = Regex.Match(arg, TrackRegex);

                        if (Album.Length != 0)
                        {
                            FocusSpotify(false, true);

                            SelectSearchbarSpotify();
                            TypeSpotify(arg);

                            SendKeys.SendWait("{ENTER}");

                            ClickClearSearchSpotify();

                            //beweeg cursor naar groene dingens
                            ClickDoubleDelaySpotify(575, 400);    
                        }
                        else if (Track.Length != 0)
                        {
                            FocusSpotify(false, true);

                            SelectSearchbarSpotify();
                            TypeSpotify(arg);

                            SendKeys.SendWait("{ENTER}");

                            ClickClearSearchSpotify();
                        }
                        else
                        { 
                            //niks
                            Application.Exit();
                        }

                    break;
                }


            }
            catch (ArgumentOutOfRangeException E)
            {
                //Nope nope nope nope. Geen args
                Application.Exit();
            }


        }

        static private Process FocusSpotify(bool r = false, bool pause = false)
        {
            //soms is de windowtitle helemaal leeg ?
            Process SPOTIFY = Process.GetProcessesByName("Spotify").FirstOrDefault(p => p.MainWindowTitle != "");

            SetForegroundWindow(SPOTIFY.MainWindowHandle);
            ShowWindow(SPOTIFY.MainWindowHandle, ShowWindowCommands.ShowMaximized);

            string Name = SPOTIFY.MainWindowTitle;
            if (Name != "Spotify")
            { 
                //pauzeer eerst, anders doet de trackid + enter niks (vreemd) doe niet als arg play of pause is.
                if(pause)
                    PausePlaySpotify();
            }

            if (r)
                return SPOTIFY;
            else
                return null;

        }

        private static void ClickSpotify(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }
        private static void ClickDoubleSpotify(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        private static void ClickDoubleDelaySpotify(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            System.Threading.Thread.Sleep(500);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        private static void ClickDelaySpotify(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            System.Threading.Thread.Sleep(500);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public static void PausePlaySpotify()
        {            
            ClickDelaySpotify(75, 1000);
        }

        private static void ClickClearSearchSpotify()
        {
            ClickDelaySpotify(315, 70);
        }

        private static void SelectSearchbarSpotify()
        {
            ClickDelaySpotify(125, 75);
        }

        private static void SelectAllResults()
        {
            System.Threading.Thread.Sleep(500);
            ClickSpotify(125, 125);
        }

        private static void PlayFromResultsScreen()
        {
            System.Threading.Thread.Sleep(1000);
            ClickDelaySpotify(305, 200);
        }

        private static void PlayFromResultsLowerScreen()
        {
            System.Threading.Thread.Sleep(1000);
            ClickDelaySpotify(300, 450);
        }

        private static void TypeSpotify(string p)
        {
            char[] keys = p.ToArray<char>();

            foreach (char key in keys)
            {                
                SendKeys.SendWait(key.ToString());
            }
        }

        public String SeeifAlbumsInSearchResults()
        {
            int topsize = 30;
            int widthsize = 125;
            int left = 265;
            int top = 180;

            Bitmap bmp = new Bitmap(widthsize, topsize, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                var rect = new User32.Rect();
                User32.GetWindowRect(FocusSpotify(true).MainWindowHandle, ref rect);

                Graphics graphics = Graphics.FromImage(bmp);
                g.CopyFromScreen(left, top, 0, 0, new Size(widthsize, topsize), CopyPixelOperation.SourceCopy);
            }

            //make it a big bigger first
            Size si = new Size(bmp.Width * 2, bmp.Height * 2); //bump up the size to increase BlueSimilarity's change of not fudging the text
            bmp = ResizeImage(bmp, si);

            bmp.Save("lastsearch.png");

            String MatchedText;

            using (var engine = new TesseractEngine("tesseract-ocr", "eng", EngineMode.Default))
            {
                using (var pix = PixConverter.ToPix(bmp))
                {
                    using (var page = engine.Process(pix))
                    {
                        //3. Get matched text from tesseract                        
                        MatchedText = page.GetText();
                    }
                }
            }

            String[] Lines = MatchedText.Split(new char[] { '\n' });
            String Word = Lines[0];

            return Word;
        }

        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            Bitmap b = new Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
            }

            return b;
        }
    }
}
