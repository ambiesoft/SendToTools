using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CopyFileContent
{
    static class Program
    {
        enum ConvertType
        {
            Unknown,
            Text,
            Image,
            Audio,
        }
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //if (args.Length < 1)
                //{
                //    MessageBox.Show(Properties.Resources.NO_ARG,
                //        Application.ProductName,
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Asterisk);
                //    return;
                //}

                ConvertType ct = ConvertType.Unknown;
                var p = new OptionSet() {
                    { 
                        "t", 
                        "treat as text.",
                        v => { ct = ConvertType.Text;}
                    },
                    { 
                        "i", 
                        "treat as image.",
                        v => { ct = ConvertType.Image;}
                    },
                    { 
                        "o", 
                        "treat as audio.",
                        v => { ct = ConvertType.Audio;}
                    }

                };


                List<string> extra = p.Parse(args);
                if (extra.Count != 1)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Properties.Resources.NO_FILE);
                    sb.AppendLine();
                    sb.AppendLine();
                    StringWriter sw = new StringWriter(sb);
                    p.WriteOptionDescriptions(sw);

                    throw new Exception(sb.ToString());
                }

                string file = extra[0];

                //if (args.Length == 1)
                //{
                //    file = args[0];
                //}
                //else if (args.Length == 2)
                //{
                //    if (args[0] == "-t")
                //        ct = ConvertType.Text;
                //    else if (args[0] == "-i")
                //        ct = ConvertType.Image;
                //    else
                //        throw new Exception(string.Format(Properties.Resources.UNKNOWN_OPTION, args[1]));

                //    file = args[1];
                //}
                //else
                //{
                //    throw new Exception(Properties.Resources.TOO_MANY_OPTIONS);
                //}

                if(ct==ConvertType.Unknown)
                    ct = getCorrectTypeFromFile(file);

                switch(ct)
                {
                    case ConvertType.Image:
                        Image image = Image.FromFile(file);
                        Clipboard.SetImage(image);
                        break;

                    case ConvertType.Text:
                        string all = System.IO.File.ReadAllText(file);
                        Clipboard.SetText(all);
                        break;

                    case ConvertType.Audio:
                        throw new Exception("Not implemented");

                    case ConvertType.Unknown:
                        throw new Exception("Unknown file type, use -t or -i.");
                }

                NotifyIcon ni = new NotifyIcon();
                ni.BalloonTipTitle = Application.ProductName;
                ni.BalloonTipText = Properties.Resources.CLIPBOARDSET;
                ni.Icon = Properties.Resources.icon;

                ni.Text = Application.ProductName;
                ni.Visible = true;
                ni.ShowBalloonTip(5 * 1000);

                System.Threading.Thread.Sleep(5 * 1000);
                ni.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static ConvertType getCorrectTypeFromFile(string file)
        {
            try
            {
                FileInfo fi = new FileInfo(file);
                string ext = fi.Extension.ToLower().TrimStart('.');

                string mime=Ambiesoft.CppUtils.getMimeTypeFromExtention(ext);
                if(!string.IsNullOrEmpty(mime))
                {
                    string mainmime = mime.Split('/')[0];
                    if (mainmime.ToLower() == "image")
                        return ConvertType.Image;
                    if (mainmime.ToLower() == "text")
                        return ConvertType.Text;
                    if (mainmime.ToLower() == "audio")
                        return ConvertType.Audio;
                }
                if(ext=="bmp" || ext=="jpg" || ext=="jpeg"||ext=="png")
                {
                    return ConvertType.Image;
                }
                if(ext=="txt"||ext=="ini"||ext=="cpp"||ext=="c"||ext=="h")
                {
                    return ConvertType.Text;
                }
            }
            catch(Exception)
            {

            }
            return ConvertType.Unknown;
        }
    }
}