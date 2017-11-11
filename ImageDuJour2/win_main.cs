using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
//using System.Web.Script.Serialization;
//using System.Web.UI.WebControls;

namespace ImageDuJour2
{
    
    public partial class win_main : Form
    {
        private static bool _useProxy;
        private static UserProxy _userproxy;

        private struct BingImageInfos
        {
            public string Url;      // url
            public string Desc;     // copyright
        };

        private struct UserProxy
        {
            public string Url;
            public string Port;
            public string Login;
            public string Password;
        }
        public win_main()
        {
            InitializeComponent();
            _userproxy = new UserProxy();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bii = GetPicture(DateTime.Now);
            const string bingRoot = "https://www.bing.com";
            if(bii.Url == "")
            {
                Console.WriteLine("Erreur !");
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
                        pict_bing_today.Image = Bitmap.FromStream(stream);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }                
                }
                lab_bing_desc.Text = bii.Desc;
            }
        }

        private static string GetCurrentLocal()
        {
            var ci = System.Globalization.CultureInfo.CurrentCulture;
            return ci.Name;
        }

        private static BingImageInfos GetPicture(DateTime dateTime)
        {
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
            bii.Desc = json.images[0].copyright;
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
            Form prox_conf = new win_config();
            prox_conf.Show();
        }
    }
}
