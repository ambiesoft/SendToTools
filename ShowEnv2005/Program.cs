using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;

namespace ShowEnv2005
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            List<string> result = new List<string>();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                string key = (string)de.Key;
                string value = (string)de.Value;

                result.Add(key + "=" + value);
            }
            result.Sort();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in result)
            {
                sb.AppendLine(s);
            }
            MessageBox.Show(
                sb.ToString(),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}