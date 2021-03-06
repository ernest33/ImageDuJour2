﻿using System;
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using IniParser;
using IniParser.Model;
using static System.Console;


//using Windows.System.UserProfile;

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
			public string Url; // url
			public string Desc; // copyright
			public string hsh; // ?
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
			bt_refresh.Enabled = false;
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

			nPercentW = ((float) Width / (float) sourceWidth);
			nPercentH = ((float) Height / (float) sourceHeight);
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

			int destWidth = (int) (sourceWidth * nPercent);
			int destHeight = (int) (sourceHeight * nPercent);

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

		[DllImport("User32", CharSet = CharSet.Auto)]
		public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, uint fWinIni);

		private void button1_Click(object sender, EventArgs e)
		{
			if (GetCurrentBingImage())
			{
				// Desktop wallpaper
				try
				{
					SystemParametersInfo(0x0014, 0, "c:\\temp\\bingimage.png", 0x0001);
				}
				catch (Exception ex)
				{
					lab_bing_desc.Text = ex.Message;
					return;
				}

				// Lockscreen wallpaper
				//System.UserProfile.LockScreen.SetImageFileAsync(file);
				setLockScreenWallpaper("c:\\temp\\bingimage.png");
			}
			else
			{
				
			}
		}

		private async void setLockScreenWallpaper(string image)
		{
			//await LockScreen.SetImageFileAsync(image);
		}

		private bool GetCurrentBingImage()
		{
			var bii = GetPicture(DateTime.Now);
			const string bingRoot = "https://www.bing.com";
			if (bii.Url == "")
			{
				WriteLine("Erreur !");
				lab_bing_desc.Text = "Erreur pendant la récupération de l'url. Reéssayer svp.";
				return false;
			}
			else
			{
				// On charge l'image dans le controle image
				var fullUrl = bingRoot + "/hpwp/" + bii.hsh + "?cc=fr";
				var request = WebRequest.Create(fullUrl);
				if (!_useProxy)
				{
					request.Proxy = null;
				}

				try
				{
					using (var response = request.GetResponse())
					using (var stream = response.GetResponseStream())
					{
						try
						{
							if (stream != null)
							{
								Image img = Image.FromStream(stream);
								img.Save("c:\\temp\\bingimage.png", ImageFormat.Png);
								pict_bing_today.Image = FixedSize(img, pict_bing_today.Width, pict_bing_today.Height);
							}
						}
						catch (Exception exception)
						{
							WriteLine(exception);
							throw;
						}
					}
				}
				catch (Exception ex)
				{
					lab_bing_desc.Text = ex.Message;
					return false;
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
			//const string bingRootUrl = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt={0}";
			const string bingRootUrl = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt={0}";
			var bingLocalUrl = GetCurrentLocal();
			var bingUrl = bingRootUrl + bingLocalUrl;
			var url = string.Format(bingUrl, bingLocalUrl);
			var dataString = "";
			WebClient client = null;
			var bii = new BingImageInfos();
			if (_useProxy == true)
			{
				//WriteLine("Using proxy");
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
				//WriteLine("Without proxy");
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
			catch (Exception e)
			{
				WriteLine(e.Message);
				bii.Url = "";
				return bii;
			}

			//WriteLine(dataString);

			dynamic json = Newtonsoft.Json.Linq.JObject.Parse(dataString);

			bii.Url = json.images[0].url;
			bii.hsh = json.images[0].hsh;

			byte[] bytes = Encoding.Default.GetBytes(json.images[0].copyright.ToString());
			//Console.WriteLine(bytes.ToString());
			bii.Desc = Encoding.UTF8.GetString(bytes);
			Cursor.Current = Cursors.Default;
			return bii;
		}

		private void chk_proxy_CheckedChanged(object sender, EventArgs e)
		{
			_useProxy = this.chk_proxy.Checked;
			bt_refresh.Enabled = true;
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
				//WriteLine(iniData);
				IniFile.WriteFile("settings.ini", iniData);
			}
		}

		private void bt_refresh_Click(object sender, EventArgs e)
		{
			// Get current image from Bing
			GetCurrentBingImage();
			bt_refresh.Enabled = false;
		}
	}
}