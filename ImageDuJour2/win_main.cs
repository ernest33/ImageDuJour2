using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using IniParser;
using IniParser.Model;


namespace ImageDuJour2
{
    
    public partial class WinMain : Form
    {
        private static bool _useProxy;
        public static UserProxy _userproxy;
        private FileIniDataParser IniFile;
        public static IniData iniData;
        private struct BingImageInfos
        {
            public string Url;      // url
            public string Desc;     // copyright
        };

        public struct UserProxy
        {
            public string Url;
            public string Port;
            public string Login;
            public string Password;
            public bool useAuth;
        }
        public WinMain()
        {
            InitializeComponent();
            _userproxy = new UserProxy();
            IniFile = new FileIniDataParser();
            iniData = IniFile.ReadFile("settings.ini");
            _userproxy.Url = iniData["PROXY"]["host"];
            _userproxy.Port = iniData["PROXY"]["port"];
            _userproxy.Login = iniData["PROXY"]["user"];
            _userproxy.Password = iniData["PROXY"]["pwd"];
            _userproxy.useAuth = _userproxy.Login.Length > 0;
            lab_bing_desc.BackColor = Color.Transparent;
            chk_proxy.Checked = _userproxy.Url.Length > 0;
            // Get current image from Bing
            GetCurrentBingImage();
        }

        static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GetCurrentBingImage())
            {
                
            }
            else
            {
                
            }
        }

        private bool GetCurrentBingImage()
        {
            var bii = GetPicture(DateTime.Now);
            const string bingRoot = "https://www.bing.com";
            if (bii.Url == "")
            {
                Console.WriteLine("Erreur !");
                return false;
            }
            else
            {
                // On charge l'image dans le controle image
                var fullUrl = bingRoot + bii.Url;

                var request = WebRequest.Create(fullUrl);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    try
                    {
                        if (stream != null)
                        {
                            Image img = Image.FromStream(stream);
                            pict_bing_today.Image = FixedSize(img, pict_bing_today.Width, pict_bing_today.Height);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }

                lab_bing_desc.Text = bii.Desc;
                return true;
            }
        }
        
        private static string GetCurrentLocal()
        {
            var ci = System.Globalization.CultureInfo.CurrentCulture;
            return ci.Name;
        }

        private static BingImageInfos GetPicture(DateTime dateTime)
        {
            Cursor.Current = Cursors.WaitCursor;
            const string bingRootUrl = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=";
            var bingLocalUrl = GetCurrentLocal();
            var bingUrl = bingRootUrl + bingLocalUrl;
            var dataString = "";
            WebClient client = null;
            var bii = new BingImageInfos();
            if (_useProxy == true)
            {
                Console.WriteLine("Using proxy");
                var p = new WebProxy(_userproxy.Url + ":" + _userproxy.Port, true)
                {
                    Credentials = new NetworkCredential(_userproxy.Login, _userproxy.Password)
                };
                WebRequest.DefaultWebProxy = p;
                client = new WebClient
                {
                    Proxy = p
                };
            }
            else
            {
                Console.WriteLine("Without proxy");
                client = new WebClient
                {
                    Proxy = null
                };
            }
            try
            {
                // Récupére les informations de l'image du jour
                dataString = client.DownloadString(bingUrl);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                bii.Url = "";
                return bii;
            }

            Console.WriteLine(dataString);

            dynamic json = Newtonsoft.Json.Linq.JObject.Parse(dataString);
            
            bii.Url = json.images[0].url;

            byte[] bytes = Encoding.Default.GetBytes(json.images[0].copyright.ToString());
            //Console.WriteLine(bytes.ToString());
            bii.Desc = Encoding.UTF8.GetString(bytes);
            Cursor.Current = Cursors.Default;
            return bii;
        }

        private void chk_proxy_CheckedChanged(object sender, EventArgs e)
        {
            _useProxy = this.chk_proxy.Checked;
        }

        private void bt_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt_proxy_setup_Click(object sender, EventArgs e)
        {
            // Ouvre la fenêtre de configuration du proxy
            Form proxConf = new WinConfig();
            if (proxConf.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(iniData);
                IniFile.WriteFile("settings.ini", iniData);
            }
            
        }
    }
}
