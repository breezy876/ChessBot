using CefSharp;
using CefSharp.DevTools.CSS;
using CefSharp.WinForms;
using Common.Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    internal class ChessSiteJsInvoker
    {

        ChromiumWebBrowser browser;

        string currentHost;

        public string[] hosts;

        public string CurrentHost { 
            get => currentHost; 
            set { 
                if (hosts.Contains(value))
                    currentHost = value;
            }
        }

        public bool IsChessHost => !string.IsNullOrEmpty(currentHost) && hosts.Contains(currentHost);

        public ChessSiteJsInvoker(ChromiumWebBrowser browser)
        {
            this.browser = browser;
            string sitesPath = Path.Combine(Globals.AppPath, "sites");

            if (!Directory.Exists(sitesPath))
                Directory.CreateDirectory(sitesPath);

            hosts = Directory.GetDirectories(sitesPath).Select(d => d.PathOnly()).ToArray();
        }

        public enum ChessSiteScriptType {  GameStarted, GameEnded, GameInProgress, Clocks, Users, Result, Moves, EnterMove, Player  }

        public async Task<object> InvokeAsync(ChessSiteScriptType type)
        {
            if (string.IsNullOrEmpty(currentHost))
                return null;

            string fileName = string.Join('-', Regex.Split(type.ToString(), @"(?<!^)(?=[A-Z])")).ToLower();
            string filePath = Path.Combine(Globals.AppPath, "sites", CurrentHost, $"{fileName}.js");
            string jsCode = await File.ReadAllTextAsync(filePath);
            return (await browser.EvaluateScriptAsync(jsCode)).Result;
        }
    }
}
