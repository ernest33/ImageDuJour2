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
    
    public partial class Form1 : Form
    {
        private static bool useProxy;
        private static userProxy userproxy;
        public struct BingImageInfos
        {
            public string url;      // url
            public string desc;     // copyright
        };
        public struct userProxy
        {
            public string url;
            public string port;
            public string login;
            public string password;
        }
        public Form1()
        {
            InitializeComponent();
            userproxy = new userProxy();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BingImageInfos bii = GetPicture(DateTime.Now);
            if(bii.url == "")
            {
                Console.WriteLine("Erreur !");
            }
            else
            {
                // On charge l'image dans le controle image
                String bing_root = "https://www.bing.com";
                String full_url = bing_root + bii.url;

                var request = WebRequest.Create(full_url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pict_bing_today.Image = Bitmap.FromStream(stream);
                }
                lab_bing_desc.Text = bii.desc;
            }
        }

        static public String getCurrentLocal()
        {
            CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
            return ci.Name;
        }

        static public BingImageInfos GetPicture(DateTime dateTime)
        {
            String bing_root_url = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=";
            String bing_local_url = getCurrentLocal();
            String bing_url = bing_root_url + bing_local_url;
            string data_string = "";
            WebClient client = null;
            BingImageInfos bii = new BingImageInfos();
            if (useProxy == true)
            {
                Console.WriteLine("Using proxy");
                WebProxy p = new WebProxy(userproxy.url + ":" + userproxy.port, true)
                {
                    Credentials = new NetworkCredential(userproxy.login, userproxy.password)
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
                data_string = client.DownloadString(bing_url);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                bii.url = "";
                return bii;
            }

            Console.WriteLine(data_string);

            dynamic json = Newtonsoft.Json.Linq.JObject.Parse(data_string);
            
            bii.url = json.images[0].url;
            bii.desc = json.images[0].copyright;
            return bii;
        }

        private void chk_proxy_CheckedChanged(object sender, EventArgs e)
        {
            useProxy = this.chk_proxy.Checked;
        }

        private void bt_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
